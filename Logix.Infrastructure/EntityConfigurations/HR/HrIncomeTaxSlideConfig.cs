using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrIncomeTaxSlideConfig : IEntityTypeConfiguration<HrIncomeTaxSlide>
    {
        public void Configure(EntityTypeBuilder<HrIncomeTaxSlide> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_HR_Income_Taxes_Slides");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        }
    }



}


