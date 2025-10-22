using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrJobOfferAdvantageConfig : IEntityTypeConfiguration<HrJobOfferAdvantage>
    {
        public void Configure(EntityTypeBuilder<HrJobOfferAdvantage> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }



}


