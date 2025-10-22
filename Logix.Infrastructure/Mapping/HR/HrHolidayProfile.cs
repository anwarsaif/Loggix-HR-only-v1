using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrHolidayProfile : Profile
    {
        public HrHolidayProfile()
        {
            CreateMap<HrHolidayDto, HrHoliday>().ReverseMap();
            CreateMap<HrHolidayEditDto, HrHoliday>().ReverseMap();
        }
    }
}
