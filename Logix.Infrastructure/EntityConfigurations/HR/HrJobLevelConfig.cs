using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrJobLevelConfig : IEntityTypeConfiguration<HrJobLevel>
    {
        public void Configure(EntityTypeBuilder<HrJobLevel> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        }
    }

}
