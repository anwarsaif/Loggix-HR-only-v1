using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrPolicyProfile : Profile
    {
        public HrPolicyProfile()
        {
            CreateMap<HrPolicyDto, HrPolicy>().ReverseMap();
            CreateMap<HrPolicyEditDto, HrPolicy>().ReverseMap();
        }
    }
}
