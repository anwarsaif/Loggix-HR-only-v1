using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVacationsTypeConfig : IEntityTypeConfiguration<HrVacationsType>
    {
        public void Configure(EntityTypeBuilder<HrVacationsType> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    } 
    
}
