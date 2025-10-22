using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollConfig : IEntityTypeConfiguration<HrPayroll>
    {
        public void Configure(EntityTypeBuilder<HrPayroll> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MsMonth).IsFixedLength();
            entity.Property(e => e.PayrollTypeId).HasDefaultValue(1);
            entity.Property(e => e.State).HasDefaultValue(0);
        }
    } 
    
}
