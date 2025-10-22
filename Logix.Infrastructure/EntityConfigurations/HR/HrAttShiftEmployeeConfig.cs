using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttShiftEmployeeConfig : IEntityTypeConfiguration<HrAttShiftEmployee>
    {
        public void Configure(EntityTypeBuilder<HrAttShiftEmployee> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }



}


