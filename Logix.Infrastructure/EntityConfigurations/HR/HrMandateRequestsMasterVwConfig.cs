using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrMandateRequestsMasterVwConfig : IEntityTypeConfiguration<HrMandateRequestsMasterVw>
    {
        public void Configure(EntityTypeBuilder<HrMandateRequestsMasterVw> entity)
        {
            entity.ToView("HR_Mandate_Requests_Master_VW", "dbo");

        }
    }




}


