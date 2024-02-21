﻿using System.ComponentModel.DataAnnotations;

namespace API_Financeiro_Next.Data.Dto;

public class CreateUsuarioDto
{

    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Cpf { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string PasswordConfirmation { get; set; }

}
