using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDecisionConfig : IEntityTypeConfiguration<HrDecision>
    {
        public void Configure(EntityTypeBuilder<HrDecision> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.DecisionSigning).HasDefaultValueSql("((0))");
        }
    } 
}


