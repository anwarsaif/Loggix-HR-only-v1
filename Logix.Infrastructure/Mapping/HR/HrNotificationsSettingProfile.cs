using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrNotificationsSettingProfile : Profile
    {
        public HrNotificationsSettingProfile()
        {
            CreateMap<HrNotificationsSettingDto, HrNotificationsSetting>().ReverseMap();
            CreateMap<HrNotificationsSettingEditDto, HrNotificationsSetting>().ReverseMap();
        }
    }
}
