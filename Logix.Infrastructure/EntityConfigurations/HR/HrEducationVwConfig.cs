using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEducationVwConfig : IEntityTypeConfiguration<HrEducationVw>
    {
        public void Configure(EntityTypeBuilder<HrEducationVw> entity)
        {
            entity.ToView("HR_Education_VW", "dbo");

        }
    }



}


