using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrGosiVwConfig : IEntityTypeConfiguration<HrGosiVw>
    {
        public void Configure(EntityTypeBuilder<HrGosiVw> entity)
        {
            entity.ToView("HR_GOSI_VW", "dbo");


        }
    }



}


