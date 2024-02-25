namespace API_Financeiro_Next.Data.Dto;

public class ReadContasPagarDto
{
    public int Id { get; set; }

    public string Fornecedor { get; set; }

    public string DescricaoDespesa { get; set; }

    public DateTime DataVencimento { get; set; }

    public int Valor { get; set; }

  
}
