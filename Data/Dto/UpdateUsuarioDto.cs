using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto;

public class UpdateUsuarioDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    public string? Cpf { get; set; }

    public string? Cnpj { get; set; }

    //[Required]
    //[DataType(DataType.Password)]
    //public string Password { get; set; }

    //[Required]
    //[Compare("Password")]
    //public string PasswordConfirmation { get; set; }
}
