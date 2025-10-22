using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrFlexibleWorkingConfig : IEntityTypeConfiguration<HrFlexibleWorking>
    {
        public void Configure(EntityTypeBuilder<HrFlexibleWorking> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

        }
    }



}


