using API_Financeiro_Next.Data;
using API_Financeiro_Next.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.ComponentModel;

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

        var document = Document.Create(container =>
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
                       .Text(text =>
                       {
                           text.DefaultTextStyle(x => x.FontSize(25).FontColor(Colors.Blue.Medium));
                           text.AlignCenter();
                           text.Span("Contas à Pagar");
                       });
                     
                    page.Content()
                       .Padding(15)
                       .MinimalBox()
                       .DefaultTextStyle(x => x.FontSize(16))
                       .Column(x =>
                       {                          
                           x.Spacing(10);
                           x.Item().Background(Colors.Blue.Medium).Height(50).Text($"Fornecedor: {conta.Fornecedor}").FontColor(Colors.White); ;

                           
                           x.Item().PaddingVertical(1).LineHorizontal(1).LineColor(Colors.Black);
                           x.Item().Background(Colors.Blue.Medium).Height(50).Text($"Descrição: {conta.DescricaoDespesa}").FontColor(Colors.White); ;

                           
                           x.Item().PaddingVertical(1).LineHorizontal(1).LineColor(Colors.Black);
                           x.Item().Background(Colors.Blue.Medium).Height(50).Text($"Data de Vencimento: {conta.DataVencimento} horas").FontColor(Colors.White); ;

                           
                           x.Item().PaddingVertical(1).LineHorizontal(1).LineColor(Colors.Black);
                           x.Item().Background(Colors.Blue.Medium).Height(50).Text($"Valor da conta: {conta.Valor} R$").FontColor(Colors.White);
                       });
                    
                    page.Footer()
                       .AlignCenter()
                       .Text(x =>
                       {
                           x.Span("Página ");
                           x.CurrentPageNumber();
                       });
                });

            }
        }).GeneratePdf();

        //// Configurando cabeçalhos da resposta HTTP
        Response.Headers.Add("Content-Disposition", "attachment; filename=Contas-Pagar.pdf");
        Response.Headers.Add("Content-Type", "application/pdf");

        // Retorna o arquivo PDF diretamente
        return File(document, "application/pdf", "ContasPagar.pdf");



        //// use the following invocation
        //document.ShowInPreviewer();

        //// optionally, you can specify an HTTP port to communicate with the previewer host (default is 12500)
        //document.ShowInPreviewer(12345);

        ////// Configurando cabeçalhos da resposta HTTP
        ////Response.Headers.Add("Content-Disposition", "attachment; filename=Contas-Pagar.pdf");
        ////Response.Headers.Add("Content-Type", "application/pdf");

        ////// Retorna o arquivo PDF diretamente
        //return File(document);
    }

    //private IActionResult File(Document document)
    //{
    //    throw new NotImplementedException();
    //}
}
