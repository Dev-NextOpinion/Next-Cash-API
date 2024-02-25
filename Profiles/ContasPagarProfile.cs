using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;

namespace API_Financeiro_Next.Profiles;

public class ContasPagarProfile : Profile
{
    public ContasPagarProfile()
    {
        CreateMap<CreateContasPagarDto, ContasPagar>();

        CreateMap<ContasPagar, ReadContasPagarDto>();

        CreateMap<UpdateContasPagarDto, ContasPagar>();
    }
}
