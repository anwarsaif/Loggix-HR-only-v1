using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrSalaryGroupConfig : IEntityTypeConfiguration<HrSalaryGroup>
    {
        public void Configure(EntityTypeBuilder<HrSalaryGroup> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        }
    }  
    
}
