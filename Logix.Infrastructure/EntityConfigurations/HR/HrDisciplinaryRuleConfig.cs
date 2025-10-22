using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDisciplinaryRuleConfig : IEntityTypeConfiguration<HrDisciplinaryRule>
    {
        public void Configure(EntityTypeBuilder<HrDisciplinaryRule> entity)
        {
            entity.Property(e => e.DeductedLate).HasDefaultValueSql("((0))");
        }
    } 
    
}
