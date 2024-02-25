using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Models;

public class Categorias
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string TituloCategoria { get; set; }

    public virtual ICollection<ContasPagar> ContasAPagar { get; set; }
}
