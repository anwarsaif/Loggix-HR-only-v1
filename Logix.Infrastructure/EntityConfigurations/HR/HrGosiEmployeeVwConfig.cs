using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrGosiEmployeeVwConfig : IEntityTypeConfiguration<HrGosiEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrGosiEmployeeVw> entity)
        {
            entity.ToView("HR_GOSI_Employee_VW", "dbo");

        }
    }



}


