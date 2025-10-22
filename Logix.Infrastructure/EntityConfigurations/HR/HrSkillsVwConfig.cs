using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrSkillsVwConfig : IEntityTypeConfiguration<HrSkillsVw>
    {
        public void Configure(EntityTypeBuilder<HrSkillsVw> entity)
        {
            entity.ToView("HR_Skills_VW", "dbo");

        }
    }



}


