using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDisciplinaryCaseActionConfig : IEntityTypeConfiguration<HrDisciplinaryCaseAction>
    {
        public void Configure(EntityTypeBuilder<HrDisciplinaryCaseAction> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    } 
    
}
