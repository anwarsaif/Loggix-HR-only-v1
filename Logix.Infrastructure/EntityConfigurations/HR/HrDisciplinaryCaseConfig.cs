using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDisciplinaryCaseConfig : IEntityTypeConfiguration<HrDisciplinaryCase>
    {
        public void Configure(EntityTypeBuilder<HrDisciplinaryCase> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    } 
    
}
