using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrIncomeTaxConfig : IEntityTypeConfiguration<HrIncomeTax>
    {
        public void Configure(EntityTypeBuilder<HrIncomeTax> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

        }
    }



}


