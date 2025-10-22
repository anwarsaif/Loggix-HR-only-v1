using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrInsurancePolicyConfig : IEntityTypeConfiguration<HrInsurancePolicy>
    {
        public void Configure(EntityTypeBuilder<HrInsurancePolicy> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }   
}


