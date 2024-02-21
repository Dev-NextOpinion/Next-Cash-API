using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto;

public class UpdateDespesaFixaDto
{
    [Required(ErrorMessage = "O campo titulo é obrigatório!")]
    public string TituloDespesaFixa { get; set; }

    [Required(ErrorMessage = "Adicione o valor da despesa, é obrigatório!")]
    public int ValorDespesaFixa { get; set; }

    public int? ReceitaId { get; set; }
}
