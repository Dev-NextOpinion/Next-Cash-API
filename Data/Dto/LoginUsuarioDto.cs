using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto;

public class LoginUsuarioDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
