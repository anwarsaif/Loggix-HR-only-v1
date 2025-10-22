using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmpStatusHistoryVwConfig : IEntityTypeConfiguration<HrEmpStatusHistoryVw>
    {
        public void Configure(EntityTypeBuilder<HrEmpStatusHistoryVw> entity)
        {
            entity.ToView("HR_Emp_Status_History_VW", "dbo");

        }
    }
}


