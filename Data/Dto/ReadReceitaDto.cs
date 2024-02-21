using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto;

public class ReadReceitaDto
{
 
    public int Id { get; set; }

    public string TituloProduto { get; set; }

    public string Segmento { get; set; }

    public ICollection<ReadDespesaFixaDto> ReadDespesaFixaDtos { get; set; }
    
    public ICollection<ReadDespesaVariavelDto> ReadDespesaVariavelDtos { get; set; }
}
