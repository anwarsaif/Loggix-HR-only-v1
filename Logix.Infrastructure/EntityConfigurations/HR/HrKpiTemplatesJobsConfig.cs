using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrKpiTemplatesJobsConfig : IEntityTypeConfiguration<HrKpiTemplatesJob>
    {
        public void Configure(EntityTypeBuilder<HrKpiTemplatesJob> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }



}


