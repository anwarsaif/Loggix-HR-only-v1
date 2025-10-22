using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrClearanceAllowanceDeductionConfig : IEntityTypeConfiguration<HrClearanceAllowanceDeduction>
    {
        public void Configure(EntityTypeBuilder<HrClearanceAllowanceDeduction> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FixedOrTemporary).HasDefaultValueSql("((1))");
        }
    }
}
