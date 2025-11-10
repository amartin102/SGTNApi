using Application.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class ParameterValueProfile : Profile
    {
        public ParameterValueProfile()
        {
            // Mapeo de ParameterValue a ParameterValueDto (GET)
            CreateMap<ParameterValue, ParameterValueDto>()
                .ForMember(dest => dest.ParameterCode, opt => opt.MapFrom(src => src.MasterParameter.Code))
                .ForMember(dest => dest.ParameterDescription, opt => opt.MapFrom(src => $"Parámetro: {src.MasterParameter.Code}"))
                // Mapeos añadidos para el tipo de dato asociado al parámetro
                .ForMember(dest => dest.DataTypeId, opt => opt.MapFrom(src => src.MasterParameter != null ? src.MasterParameter.DataTypeId : 0))
                .ForMember(dest => dest.DataTypeDescription, opt => opt.MapFrom(src => src.MasterParameter != null && src.MasterParameter.DataType != null ? src.MasterParameter.DataType.Description : null))
                .ForMember(dest => dest.OriginValue, opt => opt.MapFrom(src => src.MasterParameter != null ? src.MasterParameter.DataOrigin : null))
                // Mapeo de Nivel de Inconsistencia
                .ForMember(dest => dest.IntIdNivelInconsistencia, opt => opt.MapFrom(src => src.MasterParameter != null ? src.MasterParameter.InconsistencyLevelId : 0))
                .ForMember(dest => dest.StrNivelInconsistencia, opt => opt.MapFrom(src => 
                    src.MasterParameter != null 
                        ? (src.MasterParameter.InconsistencyLevelId == 1 ? "General" 
                           : src.MasterParameter.InconsistencyLevelId == 2 ? "Cliente" 
                           : src.MasterParameter.InconsistencyLevelId == 3 ? "Empleado" 
                           : null)
                        : null))
                .ForMember(dest => dest.HourValue, opt => opt.MapFrom(src => src.HourValue))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src =>
                    src.Employee != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : null));

            // Mapeo de CreateParameterValueDto a ParameterValue (POST) - ¡ESTE FALTABA!
            CreateMap<CreateParameterValueDto, ParameterValue>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Se genera automáticamente
                .ForMember(dest => dest.CreationDate, opt => opt.Ignore()) // Se genera automáticamente
                .ForMember(dest => dest.ModificationDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.MasterParameter, opt => opt.Ignore())
                .ForMember(dest => dest.HourValue, opt => opt.MapFrom(src => src.HourValue))
                .ForMember(dest => dest.Client, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());

            // Mapeo de UpdateParameterValueDto a ParameterValue (PUT)
            CreateMap<UpdateParameterValueDto, ParameterValue>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
