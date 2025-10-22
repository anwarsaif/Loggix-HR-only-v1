using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrInsurancePolicyProfile : Profile
    {
        public HrInsurancePolicyProfile()
        {
            CreateMap<HrInsurancePolicyDto, HrInsurancePolicy>().ReverseMap();
            CreateMap<HrInsurancePolicyEditDto, HrInsurancePolicy>().ReverseMap();
        }
    }
}
