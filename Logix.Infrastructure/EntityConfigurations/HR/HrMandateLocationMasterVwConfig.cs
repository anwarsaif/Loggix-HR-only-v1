using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrMandateLocationMasterVwConfig : IEntityTypeConfiguration<HrMandateLocationMasterVw>
    {
        public void Configure(EntityTypeBuilder<HrMandateLocationMasterVw> entity)
        {
            entity.ToView("HR_Mandate_Location_Master_VW", "dbo");

        }
    }



}


