using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrIncomeTaxVwConfig : IEntityTypeConfiguration<HrIncomeTaxVw>
    {
        public void Configure(EntityTypeBuilder<HrIncomeTaxVw> entity)
        {
            entity.ToView("HR_Income_Tax_VW", "dbo");

        }
    }



}


