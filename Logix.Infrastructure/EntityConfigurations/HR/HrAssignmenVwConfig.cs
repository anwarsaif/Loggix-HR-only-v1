using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAssignmenVwConfig : IEntityTypeConfiguration<HrAssignmenVw>
    {
        public void Configure(EntityTypeBuilder<HrAssignmenVw> entity)
        {
            entity.ToView("HR_Assignmen_VW");
        }
    }



}


