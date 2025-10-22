using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{

    public class HrAllowanceDeductionMConfig : IEntityTypeConfiguration<HrAllowanceDeductionM>

    {
        public void Configure(EntityTypeBuilder<HrAllowanceDeductionM> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.PreparationSalariesId).HasDefaultValueSql("((0))");

            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        }
    }
}


