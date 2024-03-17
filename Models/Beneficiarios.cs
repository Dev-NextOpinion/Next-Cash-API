using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Models;

public class Beneficiarios
{
    [Key]
    [Required]

    public int Id { get; set; }

    [Required] 
    public string NomeBeneficiario { get; set; }

    [Required]
    public string Cpf_Cnpj { get; set; }

    [Required]
    public string Referencia { get; set; }

    [Required]
    public string Tipo { get; set; }

    [Required]
    public string Descricao { get; set; }

    public virtual ICollection<ContasPagar> ContasAPagar { get; set; }
}
