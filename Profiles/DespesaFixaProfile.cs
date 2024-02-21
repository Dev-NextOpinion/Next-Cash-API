using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;

namespace API_Financeiro_Next.Profiles;

public class DespesaFixaProfile : Profile
{
    public DespesaFixaProfile()
    {
        CreateMap<CreateDespesaFixaDto, DespesaFixa>();

        CreateMap<DespesaFixa, ReadDespesaFixaDto>();

        CreateMap<UpdateDespesaFixaDto, DespesaFixa>();
    }
}
