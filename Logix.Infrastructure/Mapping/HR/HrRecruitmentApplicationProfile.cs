using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrRecruitmentApplicationProfile : Profile
    {
        public HrRecruitmentApplicationProfile()
        {
            CreateMap<HrRecruitmentApplicationDto, HrRecruitmentApplication>().ReverseMap();
            CreateMap<HrRecruitmentApplicationEditDto, HrRecruitmentApplication>().ReverseMap();
        }
    }
}
