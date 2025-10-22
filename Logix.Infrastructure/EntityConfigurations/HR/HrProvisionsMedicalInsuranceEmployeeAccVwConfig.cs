using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
	public class HrProvisionsMedicalInsuranceEmployeeAccVwConfig : IEntityTypeConfiguration<HrProvisionsMedicalInsuranceEmployeeAccVw>
    {
        public void Configure(EntityTypeBuilder<HrProvisionsMedicalInsuranceEmployeeAccVw> entity)
        {
			entity.ToView("HR_Provisions_MedicalInsurance_Employee_Acc_VW");
		}
    } 
}


