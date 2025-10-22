using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVacationsDayTypeConfig : IEntityTypeConfiguration<HrVacationsDayType>
    {
        public void Configure(EntityTypeBuilder<HrVacationsDayType> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

        }
    } 
}


