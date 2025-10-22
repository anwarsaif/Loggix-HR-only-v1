using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollAllowanceDeductionConfig : IEntityTypeConfiguration<HrPayrollAllowanceDeduction>
    {
        public void Configure(EntityTypeBuilder<HrPayrollAllowanceDeduction> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }

}
