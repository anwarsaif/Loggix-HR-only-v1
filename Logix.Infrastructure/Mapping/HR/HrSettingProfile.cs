using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrSettingProfile : Profile
    {
        public HrSettingProfile()
        {
            CreateMap<HrSettingDto, HrSetting>().ReverseMap();
        }
    }
}
