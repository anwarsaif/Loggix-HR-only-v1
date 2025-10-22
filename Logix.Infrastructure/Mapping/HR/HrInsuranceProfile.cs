using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrInsuranceProfile : Profile
    {
        public HrInsuranceProfile()
        {
            CreateMap<HrInsuranceDto, HrInsurance>().ReverseMap();
            CreateMap<HrInsuranceEditDto, HrInsurance>().ReverseMap();
        }
    }
}
