using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto;

public class UpdateDespesaVariavelDto
{

    [Required(ErrorMessage = "O campo de título é obrigatório!")]
    public string TituloDespesaVariavel { get; set; }

    [Required(ErrorMessage = "O campo de valor é obrigatório!")]
    public int ValorDespesaVariavel { get; set; }

    public int? ReceitaId { get; set; }
}
