using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrMandateProfile : Profile
    {
        public HrMandateProfile()
        {
            CreateMap<HrMandateDto, HrMandate>().ReverseMap();
            CreateMap<HrMandateEditDto, HrMandate>().ReverseMap();
        }
    }
}
