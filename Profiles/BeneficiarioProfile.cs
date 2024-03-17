using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;

namespace API_Financeiro_Next.Profiles;

public class BeneficiarioProfile : Profile
{
    public BeneficiarioProfile()
    {
        CreateMap<CreateBeneficiariosDto, Beneficiarios>();

        CreateMap<Beneficiarios, ReadBeneficiariosDto>()
            .ForMember(beneficiarioDto => beneficiarioDto.ReadContasPagarDto,
            opt => opt.MapFrom(beneficiario => beneficiario.ContasAPagar));

        CreateMap<UpdateBeneficiarioDto, Beneficiarios>();
    }
}
