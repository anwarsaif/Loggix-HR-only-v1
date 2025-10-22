using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLanguagesVwConfig : IEntityTypeConfiguration<HrLanguagesVw>
    {
        public void Configure(EntityTypeBuilder<HrLanguagesVw> entity)
        {
            entity.ToView("HR_Languages_VW", "dbo");

        }
    }



}


