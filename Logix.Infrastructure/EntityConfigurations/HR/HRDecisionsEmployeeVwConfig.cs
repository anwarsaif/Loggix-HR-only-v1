using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDecisionsEmployeeVwConfig : IEntityTypeConfiguration<HrDecisionsEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrDecisionsEmployeeVw> entity)
        {
            entity.ToView("HR_Decisions_Employee_VW");

        }
    } 
}


