using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrNotificationsTypeProfile : Profile
    {
        public HrNotificationsTypeProfile()
        {
            CreateMap<HrNotificationsTypeDto, HrNotificationsType>().ReverseMap();
            CreateMap<HrNotificationsTypeEditDto, HrNotificationsType>().ReverseMap();
        }
    }
}
