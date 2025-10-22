using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
	public class HrProvisionsMedicalInsuranceEmployeeProfile : Profile
    {
        public HrProvisionsMedicalInsuranceEmployeeProfile()
        {
            CreateMap<HrProvisionsMedicalInsuranceEmployeeDto, HrProvisionsMedicalInsuranceEmployee>().ReverseMap();
            CreateMap<HrProvisionsMedicalInsuranceEmployeeEditDto, HrProvisionsMedicalInsuranceEmployee>().ReverseMap();
            CreateMap<HrProvisionsMedicalInsuranceEmployeeResultDto, HrProvisionsMedicalInsuranceEmployeeVw>().ReverseMap();

        }
    }
}
