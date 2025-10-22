using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCheckInOutProfile : Profile
    {
        public HrCheckInOutProfile()
        {
            CreateMap<HrCheckInOutDto, HrCheckInOut>().ReverseMap();
        }
    } 
}
