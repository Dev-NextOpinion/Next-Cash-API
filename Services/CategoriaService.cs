using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Data;
using API_Financeiro_Next.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API_Financeiro_Next.Services;

public class CategoriaService
{
    private readonly EntidadesContext _entidadesContext;

    public CategoriaService(EntidadesContext entidadesContext)
    {
        _entidadesContext = entidadesContext;
    }

    public async Task CriarCategoria(CreateCategoriaDto categoriaDto, string userId)
    {
        // Verifique se o Id do usuário é válido
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentException("Não foi possível obter o Id do usuário autenticado");
        }

        // Agora você pode usar o userId para associar à categoria
        Categorias novaCategoria = new Categorias
        {
            TituloCategoria = categoriaDto.TituloCategoria,
            CreatedByUserId = userId
            // Adicione outras propriedades conforme necessário
        };

        // Adicione a nova categoria ao contexto do banco de dados
        _entidadesContext.Categorias.Add(novaCategoria);

        // Salve as alterações no banco de dados
        await _entidadesContext.SaveChangesAsync();
    }
}
