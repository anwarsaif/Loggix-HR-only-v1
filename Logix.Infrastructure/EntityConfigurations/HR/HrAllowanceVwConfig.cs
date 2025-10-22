using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAllowanceVwConfig : IEntityTypeConfiguration<HrAllowanceVw>
    {
        public void Configure(EntityTypeBuilder<HrAllowanceVw> entity)
        {
            entity.ToView("HR_Allowance_VW");
        }
    }



}


