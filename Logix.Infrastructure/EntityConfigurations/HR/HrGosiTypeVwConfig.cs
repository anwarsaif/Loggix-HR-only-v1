using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrGosiTypeVwConfig : IEntityTypeConfiguration<HrGosiTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrGosiTypeVw> entity)
        {
            entity.ToView("HR_Gosi_Type_VW", "dbo");

        }
    }



}


