using API_Financeiro_Next.Data;
using API_Financeiro_Next.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class DownloadReceitaController : ControllerBase
{
    private readonly EntidadesContext _context;
    private readonly IMapper _mapper;

    public DownloadReceitaController(EntidadesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult DownloadReceita()
    {
        List<Receita> receitas = _context.Receitas.ToList();

        Document.Create(container =>
        {
            foreach (var receita in receitas)
            {
                 container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("Receita - Instituto Gestar")
                        .SemiBold().FontSize(30).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Padding(15)
                        .MinimalBox()
                        .DefaultTextStyle(x => x.FontSize(16))
                        .Column(x =>
                        {
                            x.Spacing(20);

                            x.Item().Text($"ID da Receita: {receita.Id}");
                            x.Item().PaddingVertical(3).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                            x.Item().Text($"Título do Produto: {receita.TituloProduto}");
                            x.Item().PaddingVertical(3).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                            x.Item().Text($"Segmento: {receita.Segmento}");

                            // Adicione mais informações conforme necessário

                            // Exemplo com despesas fixas
                            x.Item().Text("Despesas Fixas:");
                            foreach (var despesaFixa in receita.DespesaFixa)
                            {
                                x.Item().Text($" - {despesaFixa.TituloDespesaFixa}: {despesaFixa.ValorDespesaFixa}");
                            }

                            // Exemplo com despesas variáveis
                            x.Item().Text("Despesas Variáveis:");
                            foreach (var despesaVariavel in receita.DespesaVariavel)
                            {
                                x.Item().Text($" - {despesaVariavel.TituloDespesaVariavel}: {despesaVariavel.ValorDespesaVariavel}");
                            }
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
        })

        .GeneratePdf("detalhes_receitas.pdf");
       


        return Ok("PDF gerado com sucesso!");
    }
}
