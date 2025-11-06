using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class TurnoProgramadoProfile : Profile
    {
        public TurnoProgramadoProfile()
        {
            CreateMap<TurnoProgramado, TurnoProgramadoDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : null));

            CreateMap<CreateTurnoProgramadoDto, TurnoProgramado>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreationDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());

            CreateMap<UpdateTurnoProgramadoDto, TurnoProgramado>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
