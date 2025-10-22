using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDirectJobConfig : IEntityTypeConfiguration<HrDirectJob>
    {
        public void Configure(EntityTypeBuilder<HrDirectJob> entity)
        {

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }

}
