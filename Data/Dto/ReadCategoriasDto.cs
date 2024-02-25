using API_Financeiro_Next.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto;

public class ReadCategoriasDto
{
    public int Id { get; set; }

    public string TituloCategoria { get; set; }

    public ICollection<ReadContasPagarDto> ReadContasPagarDto { get; set; }
}
