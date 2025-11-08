using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class NovedadProfiles : Profile
    {
        public NovedadProfiles()
        {
            CreateMap<RegistroNovedad, RegistroNovedadDto>()
                .ForMember(dest => dest.EmpleadoNombre, opt => opt.MapFrom(src => src.Empleado != null ? $"{src.Empleado.FirstName} {src.Empleado.LastName}" : null))
                .ForMember(dest => dest.EmpleadoIdentificacion, opt => opt.MapFrom(src => src.Empleado != null ? src.Empleado.Identification : null))
                .ForMember(dest => dest.ConceptoNombre, opt => opt.MapFrom(src => src.Concepto != null ? src.Concepto.NombreConcepto : null))
                .ForMember(dest => dest.TipoConceptoNombre, opt => opt.MapFrom(src => src.Concepto != null && src.Concepto.TipoConcepto != null ? src.Concepto.TipoConcepto.NombreTipo : null))
                .ForMember(dest => dest.PeriodoIdentificador, opt => opt.MapFrom(src => src.Periodo != null ? src.Periodo.IdentificadorPeriodo : null));
        }
    }
}
