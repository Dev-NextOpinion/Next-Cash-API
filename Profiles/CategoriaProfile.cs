using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;

namespace API_Financeiro_Next.Profiles;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<CreateCategoriaDto, Categorias>();

        CreateMap<Categorias, ReadCategoriasDto>()
            .ForMember(categoriaDto => categoriaDto.ReadContasPagarDto,
                opt => opt.MapFrom(categoria => categoria.ContasAPagar));

        CreateMap<UpdateCategoriasDto, Categorias>();

    }
}
