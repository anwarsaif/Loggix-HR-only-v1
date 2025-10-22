using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrClearanceMonthProfile : Profile
    {
        public HrClearanceMonthProfile()
        {
            CreateMap<HrClearanceMonthDto, HrClearanceMonth>().ReverseMap();
            CreateMap<HrClearanceMonthEditDto, HrClearanceMonth>().ReverseMap();
        }
    }
   
}
