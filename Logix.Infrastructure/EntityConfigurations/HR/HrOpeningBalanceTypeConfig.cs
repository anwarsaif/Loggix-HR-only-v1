using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrOpeningBalanceTypeConfig : IEntityTypeConfiguration<HrOpeningBalanceType>
    {
        public void Configure(EntityTypeBuilder<HrOpeningBalanceType> entity)
        {


            entity.Property(e => e.TypeId).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }
}


