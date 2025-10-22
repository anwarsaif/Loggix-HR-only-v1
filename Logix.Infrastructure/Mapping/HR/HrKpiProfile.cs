using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrKpiProfile : Profile
    {
        public HrKpiProfile()
        {
            CreateMap<HrKpiDto, HrKpi>().ReverseMap();
            CreateMap<HrKpiEditDto, HrKpi>().ReverseMap();
        }
    }
}
