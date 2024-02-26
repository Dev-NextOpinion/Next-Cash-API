using API_Financeiro_Next.Data;
using API_Financeiro_Next.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace API_Financeiro_Next.Controllers;

[ApiController]
[Route("[controller]")]
public class DownloadContasPagarController : ControllerBase
{
    private readonly EntidadesContext _context;
    private readonly IMapper _mapper;

    public DownloadContasPagarController(EntidadesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult DownloadContasPagar()
    {
        List<ContasPagar> contasPagar = _context.Contas.ToList();

        var pdfBytes = Document.Create(container =>
        {
            foreach (var conta in contasPagar)
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(17));

                    page.Header()
                       .Text("Contas a Pagar")
                       .SemiBold().FontSize(25).FontColor(Colors.Blue.Medium);

                    page.Content()
                       .Padding(15)
                       .MinimalBox()
                       .DefaultTextStyle(x => x.FontSize(16))
                       .Column(x =>
                       {
                           x.Spacing(20);

                           x.Item().Text($"Fornecedor: {conta.Fornecedor}");
                           x.Item().PaddingVertical(3).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                           x.Item().Text($"Descrição: {conta.DescricaoDespesa}");
                           x.Item().PaddingVertical(3).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                           x.Item().Text($"Data de Vencimento: {conta.DataVencimento}");
                           x.Item().PaddingVertical(3).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                           x.Item().Text($"Valor da conta: {conta.Valor}");

                           //// Adicione mais informações conforme necessário

                           //// Exemplo com despesas fixas
                           //x.Item().Text("Despesas Fixas:");
                           //foreach (var despesaFixa in receita.DespesaFixa)
                           //{
                           //    x.Item().Text($" - {despesaFixa.TituloDespesaFixa}: {despesaFixa.ValorDespesaFixa}");
                           //}

                       });
                    page.Footer()
                       .AlignCenter()
                       .Text(x =>
                       {
                           x.Span("Page ");
                           x.CurrentPageNumber();
                       });
                });

            }
        }).GeneratePdf();

        // Configurando cabeçalhos da resposta HTTP
        Response.Headers.Add("Content-Disposition", "attachment; filename=Contas-Pagar.pdf");
        Response.Headers.Add("Content-Type", "application/pdf");

        // Retorna o arquivo PDF diretamente
        return File(pdfBytes, "application/pdf", "ContasPagar.pdf");

    }
}
