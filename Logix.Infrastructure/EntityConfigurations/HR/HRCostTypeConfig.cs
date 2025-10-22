using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCostTypeConfig : IEntityTypeConfiguration<HrCostType>
    {
        public void Configure(EntityTypeBuilder<HrCostType> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    } 
}


