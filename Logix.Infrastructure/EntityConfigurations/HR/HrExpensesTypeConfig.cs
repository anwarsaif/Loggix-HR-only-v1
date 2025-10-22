using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrExpensesTypeConfig : IEntityTypeConfiguration<HrExpensesType>
    {
        public void Configure(EntityTypeBuilder<HrExpensesType> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.FacilityId).HasDefaultValueSql("((1))");
        }
    }



}


