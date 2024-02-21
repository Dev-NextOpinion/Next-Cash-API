using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto
{
    public class ReadDespesaVariavelDto
    {
        public int Id { get; set; }

        public string TituloDespesaVariavel { get; set; }

        public int ValorDespesaVariavel { get; set; }
    }
}
