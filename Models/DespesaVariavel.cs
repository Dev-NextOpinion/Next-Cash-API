using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Models;

public class DespesaVariavel
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo de título é obrigatório!")]
    public string TituloDespesaVariavel {  get; set; }

    [Required(ErrorMessage = "O campo de valor é obrigatório!")]
    public int ValorDespesaVariavel  { get; set; }

    public int? ReceitaId { get; set; }

    public virtual Receita Receita { get; set; }



}
