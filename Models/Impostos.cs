using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Models;

public class Impostos
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Titulo imposto é obrigatório!")]
    public string TituloImposto { get; set; }

    [Required(ErrorMessage = "Tipo obrigatório")] 
    public string TipoImposto { get; set; }

    [Required(ErrorMessage = "O valor do imposto é obrigatório")]
    public int ValorImposto { get; set; }
}
