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
    public class MasterParameterProfile : Profile
    {
        public MasterParameterProfile()
        {
            // MasterParameter mappings
            CreateMap<MasterParameter, MasterParameterDto>()
                .ForMember(dest => dest.DataTypeDescription, opt => opt.MapFrom(src => src.DataType.Description))
                .ForMember(dest => dest.InconsistencyLevelDescription, opt => opt.MapFrom(src => src.InconsistencyLevel.Description))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DataOrigin, opt => opt.MapFrom(src => src.DataOrigin));

            CreateMap<CreateMasterParameterDto, MasterParameter>();

            CreateMap<UpdateMasterParameterDto, MasterParameter>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
