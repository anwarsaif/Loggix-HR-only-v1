using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttLocationEmployeeConfig : IEntityTypeConfiguration<HrAttLocationEmployee>
    {
        public void Configure(EntityTypeBuilder<HrAttLocationEmployee> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }
}



