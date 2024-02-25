using API_Financeiro_Next.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto;

public class CreateCategoriaDto
{
    [Required]
    public string TituloCategoria { get; set; }

}
