using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCheckInOutConfig : IEntityTypeConfiguration<HrCheckInOut>
    {
        public void Configure(EntityTypeBuilder<HrCheckInOut> entity)
        {
            entity.Property(e => e.IsSend).HasDefaultValueSql("((0))");
        }
    }  
}


