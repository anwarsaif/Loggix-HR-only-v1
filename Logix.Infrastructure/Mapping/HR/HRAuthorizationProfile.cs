using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAuthorizationProfile : Profile
    {
        public HrAuthorizationProfile()
        {
            CreateMap<HrAuthorizationDto, HrAuthorization>().ReverseMap();
            CreateMap<HrAuthorizationEditDto, HrAuthorization>().ReverseMap();
        }
    } 
   
}
