using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmployeeCostConfig : IEntityTypeConfiguration<HrEmployeeCost>
    {
        public void Configure(EntityTypeBuilder<HrEmployeeCost> entity)
        {

               entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
               entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }



}


