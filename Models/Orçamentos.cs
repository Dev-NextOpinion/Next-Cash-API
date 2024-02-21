using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Models;

public class Orçamentos
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string TituloOrçamento { get; set; }


}
