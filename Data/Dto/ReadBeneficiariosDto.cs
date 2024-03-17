using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto
{
    public class ReadBeneficiariosDto
    {
        public int Id { get; set; }

        public string NomeBeneficiario { get; set; }

        public string Cpf_Cnpj { get; set; }

        public string Referencia { get; set; }

        public string Tipo { get; set; }

        public string Descricao { get; set; }

        public ICollection<ReadContasPagarDto> ReadContasPagarDto { get; set; }
    }
}
