using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPsAllowanceDeductionConfig : IEntityTypeConfiguration<HrPsAllowanceDeduction>
    {
        public void Configure(EntityTypeBuilder<HrPsAllowanceDeduction> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    } 
    
}
