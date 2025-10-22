using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollTypeConfig : IEntityTypeConfiguration<HrPayrollType>
    {
        public void Configure(EntityTypeBuilder<HrPayrollType> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }
}


