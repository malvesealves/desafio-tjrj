using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API.Exception;

public class ExceptionHandling(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
        {
            context.Response.StatusCode = pgEx.SqlState switch
            {
                PostgresErrorCodes.UniqueViolation => StatusCodes.Status409Conflict,
                PostgresErrorCodes.ForeignKeyViolation => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            ProblemDetails problem = new()
            {
                Title = "Erro ao acessar o banco de dados",
                Status = context.Response.StatusCode,
                Detail = pgEx.MessageText,
                Instance = context.Request.Path
            };

            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (System.Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = new ProblemDetails
            {
                Title = "Erro interno no servidor",
                Status = 500,
                Detail = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                Instance = context.Request.Path
            };

            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
