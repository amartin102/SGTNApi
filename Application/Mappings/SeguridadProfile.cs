using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class SeguridadProfile : Profile
    {
        public SeguridadProfile()
        {
            // Rol mappings
            CreateMap<Rol, RolDto>();
            CreateMap<CreateRolDto, Rol>()
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UpdateRolDto, Rol>()
                .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => DateTime.Now));

            // Usuario mappings
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.NombreRol, opt => opt.MapFrom(src => src.Rol != null ? src.Rol.Nombre : null));
            CreateMap<CreateUsuarioDto, Usuario>()
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UpdateUsuarioDto, Usuario>()
                .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Contrasena, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Contrasena)));

            // Permiso mappings
            CreateMap<Permiso, PermisoDto>();
            CreateMap<CreatePermisoDto, Permiso>();
            CreateMap<UpdatePermisoDto, Permiso>();

            // Complex mappings
            CreateMap<Rol, RolConPermisosDto>()
                .ForMember(dest => dest.Permisos, opt => opt.MapFrom(src => src.RolPermisos.Select(rp => rp.Permiso)));

            CreateMap<Usuario, UsuarioConRolDto>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol));
        }
    }
}
