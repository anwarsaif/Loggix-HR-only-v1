using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrIncomeTaxPeriodConfig : IEntityTypeConfiguration<HrIncomeTaxPeriod>
    {
        public void Configure(EntityTypeBuilder<HrIncomeTaxPeriod> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.PersonalExemption).HasDefaultValueSql("((0))");

        }
    }



}


