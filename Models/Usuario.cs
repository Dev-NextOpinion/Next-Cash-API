using Microsoft.AspNetCore.Identity;

namespace API_Financeiro_Next.Models;

public class Usuario : IdentityUser
{
    public string Cpf {  get; set; }

    public Usuario() : base() { }
}
