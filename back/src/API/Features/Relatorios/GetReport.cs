using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using FastReport;
using FastReport.Export.PdfSimple;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Relatorios;

public class GetReport
{
    #region Response

    public record Response(List<RelatorioView> Assunto);

    #endregion

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("relatorios", Handler).WithTags("Relatorios");
        }

        public static async Task<IResult> Handler(AppDbContext context)
        {
            List<RelatorioView> data = await context.ViewLivroAutor.ToListAsync();

            using var report = new Report();
            report.Load("Reports/LivrosAutores.frx");

            report.RegisterData(data, "LivroAutorView");
            report.GetDataSource("LivroAutorView").Enabled = true;

            report.Prepare();

            using MemoryStream ms = new MemoryStream();
            PDFSimpleExport pdfExport = new PDFSimpleExport();
            report.Export(pdfExport, ms);
            ms.Position = 0;

            return TypedResults.File(ms.ToArray(), "application/pdf", "RelatorioLivros.pdf");
        }
    }
}
