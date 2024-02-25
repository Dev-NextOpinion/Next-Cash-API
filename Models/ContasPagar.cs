using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Models;

public class ContasPagar
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo 'Fornecedor' é obrigatório")]
    public string Fornecedor { get; set; }

    [Required]
    public string DescricaoDespesa {  get; set; }

    [Required]
    public DateTime DataVencimento { get; set; }

    [Required]
    public int Valor { get; set; }

    public int? CategoriasId { get; set; }
    public virtual Categorias Categorias { get; set; }

}
