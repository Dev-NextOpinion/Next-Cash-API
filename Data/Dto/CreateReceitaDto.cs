using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto
{
    public class CreateReceitaDto
    {

        [Required(ErrorMessage = "O campo de título obrigatório!")]
        public string TituloProduto { get; set; }

        [Required(ErrorMessage = "O campo de segmento é obrigatório")]
        public string Segmento { get; set; }
    }
}
