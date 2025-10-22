using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
	public class HrProvisionsMedicalInsuranceProfile : Profile
    {
        public HrProvisionsMedicalInsuranceProfile()
        {
            CreateMap<HrProvisionsMedicalInsuranceDto, HrProvisionsMedicalInsurance>().ReverseMap();
            CreateMap<HrProvisionsMedicalInsuranceEditDto, HrProvisionsMedicalInsurance>().ReverseMap();
            CreateMap<HrProvisionsMedicalInsuranceEditDto, HrProvisionsMedicalInsuranceVw>().ReverseMap();
        }
    }
}
