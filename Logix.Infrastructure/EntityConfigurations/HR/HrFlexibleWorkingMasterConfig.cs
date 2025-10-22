using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrFlexibleWorkingMasterConfig : IEntityTypeConfiguration<HrFlexibleWorkingMaster>
    {
        public void Configure(EntityTypeBuilder<HrFlexibleWorkingMaster> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

        }
    }



}


