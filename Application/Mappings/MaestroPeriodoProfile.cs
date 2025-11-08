using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MaestroPeriodoProfile : Profile
    {
        public MaestroPeriodoProfile()
        {
            CreateMap<MaestroPeriodo, MaestroPeriodoDto>()
                .ForMember(dest => dest.Periodicidad, opt => opt.MapFrom(src => src.ValorParametroPeriodicidad != null ? src.ValorParametroPeriodicidad.TextValue : null));

            CreateMap<CreateMaestroPeriodoDto, MaestroPeriodo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
                .ForMember(dest => dest.FechaModificacion, opt => opt.Ignore());
            CreateMap<UpdateMaestroPeriodoDto, MaestroPeriodo>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
