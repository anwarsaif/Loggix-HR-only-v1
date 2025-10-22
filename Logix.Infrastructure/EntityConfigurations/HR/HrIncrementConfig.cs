using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrIncrementConfig : IEntityTypeConfiguration<HrIncrement>
    {
        public void Configure(EntityTypeBuilder<HrIncrement> entity)
        {

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.StatusId).HasDefaultValueSql("((0))");
        }
    }

}
