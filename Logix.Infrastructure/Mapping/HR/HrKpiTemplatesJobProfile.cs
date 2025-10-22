using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrKpiTemplatesJobProfile : Profile
    {
        public HrKpiTemplatesJobProfile()
        {
            CreateMap<HrKpiTemplatesJobDto, HrKpiTemplatesJob>().ReverseMap();
            CreateMap<HrKpiTemplatesJobEditDto, HrKpiTemplatesJob>().ReverseMap();
        }
    }
}
