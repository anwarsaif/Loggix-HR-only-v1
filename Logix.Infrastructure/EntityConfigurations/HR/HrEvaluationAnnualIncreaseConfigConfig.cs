using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEvaluationAnnualIncreaseConfigConfig : IEntityTypeConfiguration<HrEvaluationAnnualIncreaseConfig>
    {
        public void Configure(EntityTypeBuilder<HrEvaluationAnnualIncreaseConfig> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }
    
}
