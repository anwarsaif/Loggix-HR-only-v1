using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrKpiTypeConfig : IEntityTypeConfiguration<HrKpiType>
    {
        public void Configure(EntityTypeBuilder<HrKpiType> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Isdeleted).HasDefaultValueSql("((0))");

        }
    }
}
