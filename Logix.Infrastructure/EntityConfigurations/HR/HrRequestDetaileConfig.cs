using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRequestDetaileConfig : IEntityTypeConfiguration<HrRequestDetaile>
    {
        public void Configure(EntityTypeBuilder<HrRequestDetaile> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }



}


