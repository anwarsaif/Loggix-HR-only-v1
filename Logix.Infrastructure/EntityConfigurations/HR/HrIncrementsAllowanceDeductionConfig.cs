using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrIncrementsAllowanceDeductionConfig : IEntityTypeConfiguration<HrIncrementsAllowanceDeduction>
    {
        public void Configure(EntityTypeBuilder<HrIncrementsAllowanceDeduction> entity)
        {
            entity.Property(e => e.AllDedId).HasDefaultValue(0L);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(true);
        }
    }
}


