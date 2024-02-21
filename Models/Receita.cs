using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Models;

public class Receita
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo de título obrigatório!")]
    public string TituloProduto { get; set; }

    [Required(ErrorMessage = "O campo de segmento é obrigatório")]
    public string Segmento { get; set; }

    public virtual ICollection<DespesaFixa> DespesaFixa { get; set; }

    public virtual ICollection<DespesaVariavel> DespesaVariavel { get; set; }




    /*
     public virtual ICollection<Vendas> Vendas { get; set; }

     public virtual Vendas Vendas { get; set; }

     public virtual Liquido Liquido { get; set; }
     */




}
