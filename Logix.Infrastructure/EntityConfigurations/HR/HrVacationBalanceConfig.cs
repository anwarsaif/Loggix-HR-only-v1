using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVacationBalanceConfig : IEntityTypeConfiguration<HrVacationBalance>
    {
        public void Configure(EntityTypeBuilder<HrVacationBalance> entity)
        {

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.Isacitve).HasDefaultValueSql("((1))");

            entity.Property(e => e.VacationTypeId).HasDefaultValueSql("((1))");
        }
    }

}
