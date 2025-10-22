using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrJobDescriptionConfig : IEntityTypeConfiguration<HrJobDescription>
    {
        public void Configure(EntityTypeBuilder<HrJobDescription> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }

}
