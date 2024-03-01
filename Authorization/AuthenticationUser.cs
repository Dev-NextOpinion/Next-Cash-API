using Microsoft.AspNetCore.Authorization;

namespace API_Financeiro_Next.Authorization;

public class AuthenticationUser : IAuthorizationRequirement
{
    public AuthenticationUser()
    {
        
    }
}
