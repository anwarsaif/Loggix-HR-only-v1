using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLicensesVwConfig : IEntityTypeConfiguration<HrLicensesVw>
    {
        public void Configure(EntityTypeBuilder<HrLicensesVw> entity)
        {
            entity.ToView("HR_Licenses_VW");
        }
    }

}
