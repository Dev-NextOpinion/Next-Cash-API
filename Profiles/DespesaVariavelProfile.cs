using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;

namespace API_Financeiro_Next.Profiles;

public class DespesaVariavelProfile : Profile
{
    public DespesaVariavelProfile()
    {
        CreateMap<CreateDespesaVariavelDto, DespesaVariavel>();

        CreateMap<DespesaVariavel, ReadDespesaVariavelDto>();

        CreateMap<UpdateDespesaVariavelDto, DespesaVariavel>();
    }
}
