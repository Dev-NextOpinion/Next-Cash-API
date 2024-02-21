using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto
{
    public class ReadDespesaFixaDto
    {
        public int Id { get; set; }

        public string TituloDespesaFixa { get; set; }

        public int ValorDespesaFixa { get; set; }
    }
}
