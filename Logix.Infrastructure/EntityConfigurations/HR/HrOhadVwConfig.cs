using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrOhadVwConfig : IEntityTypeConfiguration<HrOhadVw>
    {
        public void Configure(EntityTypeBuilder<HrOhadVw> entity)
        {

            entity.ToView("HR_Ohad_VW");

        }


    }
}


