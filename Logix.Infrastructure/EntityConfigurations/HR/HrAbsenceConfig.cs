using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAbsenceConfig : IEntityTypeConfiguration<HrAbsence>
    {
        public void Configure(EntityTypeBuilder<HrAbsence> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Type).IsFixedLength();
        }
    }
}


