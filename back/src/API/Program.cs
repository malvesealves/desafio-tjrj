using API.DatabaseContext;
using API.Endpoints;
using API.Exception;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpoints();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IConfiguration config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");

    var scriptDirectory = Path.Combine(AppContext.BaseDirectory, "DatabaseContext/Scripts");

    var sqlFiles = Directory.GetFiles(scriptDirectory, "*.sql").OrderBy(f => f);

    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    foreach (string? file in sqlFiles)
    {
        string sql = File.ReadAllText(file);
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
        Console.WriteLine($"Executado: {Path.GetFileName(file)}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseMiddleware<ExceptionHandling>();

app.UseCors("AngularApp");

app.MapEndpoints();

app.UseHttpsRedirection();

app.Run();

public partial class Program { }