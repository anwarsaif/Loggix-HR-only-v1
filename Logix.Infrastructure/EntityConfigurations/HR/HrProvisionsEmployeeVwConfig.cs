using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrProvisionsEmployeeVwConfig : IEntityTypeConfiguration<HrProvisionsEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrProvisionsEmployeeVw> entity)
        {
            entity.ToView("HR_Provisions_Employee_VW", "dbo");

        }
    }



}


