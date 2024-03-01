using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API_Financeiro_Next.Models;

public class Categorias
{
    [Key]
    [Required]
 
    public int Id { get; set; }

    [Required]
    public string TituloCategoria { get; set; }

    // Adicionando propriedades para armazenar informações sobre quem criou a categoria
    [Required]
    public string CreatedByUserId { get; set; }

    [ForeignKey("CreatedByUserId")]
    public virtual Usuario CreatedByUser { get; set; }

    public virtual ICollection<ContasPagar> ContasAPagar { get; set; }



  
}
