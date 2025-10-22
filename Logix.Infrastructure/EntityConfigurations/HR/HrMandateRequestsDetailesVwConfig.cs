using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrMandateRequestsDetailesVwConfig : IEntityTypeConfiguration<HrMandateRequestsDetailesVw>
    {
        public void Configure(EntityTypeBuilder<HrMandateRequestsDetailesVw> entity)
        {
            entity.ToView("HR_Mandate_Requests_Detailes_VW", "dbo");

        }
    }




}


