using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrPerformanceProfile : Profile
    {
        public HrPerformanceProfile()
        {
            CreateMap<HrPerformanceDto, HrPerformance>().ReverseMap();
            CreateMap<HrPerformanceEditDto, HrPerformance>().ReverseMap();
        }
    }
}
