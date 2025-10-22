using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
	public class HrProvisionsMedicalInsuranceConfig : IEntityTypeConfiguration<HrProvisionsMedicalInsurance>
    {
        public void Configure(EntityTypeBuilder<HrProvisionsMedicalInsurance> entity)
        {
			entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
			entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
		}
    } 
}


