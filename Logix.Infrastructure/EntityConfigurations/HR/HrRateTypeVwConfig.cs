using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRateTypeVwConfig : IEntityTypeConfiguration<HrRateTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrRateTypeVw> entity)
        {
            entity.ToView("HR_Rate_Type_VW");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }



}


