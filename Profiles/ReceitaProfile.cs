using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;

namespace API_Financeiro_Next.Profiles;

public class ReceitaProfile : Profile
{
    public ReceitaProfile()
    {
        CreateMap<CreateReceitaDto, Receita>();

        CreateMap<Receita, ReadReceitaDto>()
            .ForMember(receitaDto => receitaDto.ReadDespesaFixaDtos,
            opt => opt.MapFrom(despesaFixa => despesaFixa.DespesaFixa))

            .ForMember(receitaDto => receitaDto.ReadDespesaVariavelDtos,
            opt => opt.MapFrom(despesaVariavel => despesaVariavel.DespesaVariavel));

        CreateMap<UpdateReceitaDto, Receita>();


    }
}
