using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
	public class HrProvisionsMedicalInsuranceEmployeeVwConfig : IEntityTypeConfiguration<HrProvisionsMedicalInsuranceEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrProvisionsMedicalInsuranceEmployeeVw> entity)
        {
			entity.ToView("HR_Provisions_MedicalInsurance_Employee_VW");
		}
    } 
}


