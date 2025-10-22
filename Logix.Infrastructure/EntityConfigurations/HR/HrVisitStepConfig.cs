using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVisitStepConfig : IEntityTypeConfiguration<HrVisitStep>
    {
        public void Configure(EntityTypeBuilder<HrVisitStep> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }



}


