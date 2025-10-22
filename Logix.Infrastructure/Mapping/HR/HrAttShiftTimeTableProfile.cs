using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttShiftTimeTableProfile : Profile
    {
        public HrAttShiftTimeTableProfile()
        {
            CreateMap<HrAttShiftTimeTableDto, HrAttShiftTimeTable>().ReverseMap();
            CreateMap<HrAttShiftTimeTableEditDto, HrAttShiftTimeTable>().ReverseMap();
        }
    }
}
