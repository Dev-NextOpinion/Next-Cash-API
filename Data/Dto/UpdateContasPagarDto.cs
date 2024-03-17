using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto;

public class UpdateContasPagarDto
{
    [Required(ErrorMessage = "Campo 'Fornecedor' é obrigatório")]
    public string Fornecedor { get; set; }

    [Required]
    public string DescricaoDespesa { get; set; }

    [Required]
    public DateTime DataVencimento { get; set; }

    [Required]
    public int Valor { get; set; }

    public int? CategoriasId { get; set; }

    public int? BeneficiariosId { get; set; }
}
