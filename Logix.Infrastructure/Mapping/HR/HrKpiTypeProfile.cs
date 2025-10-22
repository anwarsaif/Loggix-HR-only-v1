using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrKpiTypeProfile : Profile
    {
        public HrKpiTypeProfile()
        {
            CreateMap<HrKpiTypeDto, HrKpiType>().ReverseMap();
            CreateMap<HrKpiTypeEditDto, HrKpiType>().ReverseMap();
        }
    }
}
