using Microsoft.AspNetCore.Identity;

namespace API_Financeiro_Next.Models;

public class Usuario : IdentityUser
{
    public string Cpf {  get; set; }

    public string Cnpj { get; set; }

   // public virtual Categorias Categorias { get; set; }

    public Usuario() : base() { }
}
