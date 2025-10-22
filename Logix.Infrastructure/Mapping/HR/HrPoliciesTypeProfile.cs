using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrPoliciesTypeProfile : Profile
    {
        public HrPoliciesTypeProfile()
        {
            CreateMap<HrPoliciesTypeDto, HrPoliciesType>().ReverseMap();
            CreateMap<HrPoliciesTypeEditDto, HrPoliciesType>().ReverseMap();
        }
    }
}
