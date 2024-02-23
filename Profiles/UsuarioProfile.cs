using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;

namespace API_Financeiro_Next.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario>();

        CreateMap<Usuario, ReadUsuariosDto>();

        CreateMap<UpdateUsuarioDto, Usuario>();
    }
}
