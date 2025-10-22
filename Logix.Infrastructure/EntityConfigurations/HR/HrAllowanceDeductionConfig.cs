using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAllowanceDeductionConfig : IEntityTypeConfiguration<HrAllowanceDeduction>
    {
        public void Configure(EntityTypeBuilder<HrAllowanceDeduction> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.PreparationSalariesId).HasDefaultValueSql("((0))");

            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        }
    }  
}


