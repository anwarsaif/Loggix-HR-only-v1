using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttShiftCloseProfile : Profile
    {
        public HrAttShiftCloseProfile()
        {
            CreateMap<HrAttShiftCloseDto, HrAttShiftClose>().ReverseMap();
            CreateMap<HrAttShiftCloseEditDto, HrAttShiftClose>().ReverseMap();
        }
    }
}
