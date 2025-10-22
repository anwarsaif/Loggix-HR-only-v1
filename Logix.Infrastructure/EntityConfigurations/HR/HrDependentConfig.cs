using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDependentConfig : IEntityTypeConfiguration<HrDependent>
    {
        public void Configure(EntityTypeBuilder<HrDependent> entity)
        {

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }

}
