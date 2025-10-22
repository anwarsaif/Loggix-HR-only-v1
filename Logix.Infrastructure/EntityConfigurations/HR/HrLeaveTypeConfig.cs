using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLeaveTypeConfig : IEntityTypeConfiguration<HrLeaveType>
    {
        public void Configure(EntityTypeBuilder<HrLeaveType> entity)
        {
            entity.Property(e => e.TypeId).ValueGeneratedNever();

        }
    }

}
