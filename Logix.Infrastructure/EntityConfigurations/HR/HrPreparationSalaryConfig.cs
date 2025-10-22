using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPreparationSalaryConfig : IEntityTypeConfiguration<HrPreparationSalary>
    {
      public void Configure(EntityTypeBuilder<HrPreparationSalary> entity)
          {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.MsMonth).IsFixedLength();

            entity.Property(e => e.PayrollTypeId).HasDefaultValueSql("((1))");
        }
    }
    
}
