using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttTimeTableProfile : Profile
    {
        public HrAttTimeTableProfile()
        {
            CreateMap<HrAttTimeTableDto, HrAttTimeTable>().ReverseMap();
            CreateMap<HrAttTimeTableEditDto, HrAttTimeTable>().ReverseMap();
        }
    }
}
