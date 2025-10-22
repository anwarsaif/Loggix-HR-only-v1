using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
	public class HrProvisionsMedicalInsuranceVwConfig : IEntityTypeConfiguration<HrProvisionsMedicalInsuranceVw>
    {
        public void Configure(EntityTypeBuilder<HrProvisionsMedicalInsuranceVw> entity)
        {
			entity.ToView("HR_Provisions_MedicalInsurance_VW");
		}
    } 
}


