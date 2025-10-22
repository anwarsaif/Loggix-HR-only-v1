using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCompensatoryVacationProfile : Profile
    {
        public HrCompensatoryVacationProfile()
        {
            CreateMap<HrCompensatoryVacationDto, HrCompensatoryVacation>().ReverseMap();
            CreateMap<HrCompensatoryVacationEditDto, HrCompensatoryVacation>().ReverseMap();
        }
    } 
   
}
