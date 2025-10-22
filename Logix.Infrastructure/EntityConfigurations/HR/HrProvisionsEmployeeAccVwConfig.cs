using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrProvisionsEmployeeAccVwConfig : IEntityTypeConfiguration<HrProvisionsEmployeeAccVw>
    {
        public void Configure(EntityTypeBuilder<HrProvisionsEmployeeAccVw> entity)
        {
            entity.ToView("HR_Provisions_Employee_Acc_VW", "dbo");

        }
    } 
}


