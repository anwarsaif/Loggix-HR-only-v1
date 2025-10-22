using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrGosiEmployeeAccVwConfig : IEntityTypeConfiguration<HrGosiEmployeeAccVw>
    {
        public void Configure(EntityTypeBuilder<HrGosiEmployeeAccVw> entity)
        {
            entity.ToView("HR_GOSI_Employee_Acc_VW", "dbo");
        }
    }



}


