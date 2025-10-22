using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttShiftProfile : Profile
    {
        public HrAttShiftProfile()
        {
            CreateMap<HrAttShiftDto, HrAttShift>().ReverseMap();
            CreateMap<HrAttShiftEditDto, HrAttShift>().ReverseMap();
        }
    }
}
