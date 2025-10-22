using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrKpiDetaileConfig : IEntityTypeConfiguration<HrKpiDetaile>
    {
        public void Configure(EntityTypeBuilder<HrKpiDetaile> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.KpiTemComId).IsFixedLength();
        }
    }

}
