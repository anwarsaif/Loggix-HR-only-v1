using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDecisionsTypeEmployeeVwConfig : IEntityTypeConfiguration<HrDecisionsTypeEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrDecisionsTypeEmployeeVw> entity)
        {
            entity.ToView("HR_Decisions_Type_Employee_VW");

        }
    }

}
