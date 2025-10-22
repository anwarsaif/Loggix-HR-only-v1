using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrMandateLocationDetailesVwConfig : IEntityTypeConfiguration<HrMandateLocationDetailesVw>
    {
        public void Configure(EntityTypeBuilder<HrMandateLocationDetailesVw> entity)
        {
            entity.ToView("HR_Mandate_Location_Detailes_VW", "dbo");

        }
    }



}


