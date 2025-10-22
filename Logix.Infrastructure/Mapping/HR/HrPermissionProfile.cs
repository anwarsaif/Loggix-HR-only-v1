using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrPermissionProfile : Profile
    {
        public HrPermissionProfile()
        {
            CreateMap<HrPermissionDto, HrPermission>().ReverseMap();
            CreateMap<HrPermissionEditDto, HrPermission>().ReverseMap();
        }
    }
}
