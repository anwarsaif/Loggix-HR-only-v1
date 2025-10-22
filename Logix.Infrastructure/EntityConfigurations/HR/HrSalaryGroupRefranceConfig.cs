using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrSalaryGroupRefranceConfig : IEntityTypeConfiguration<HrSalaryGroupRefrance>
    {
        public void Configure(EntityTypeBuilder<HrSalaryGroupRefrance> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    } 
    
}
