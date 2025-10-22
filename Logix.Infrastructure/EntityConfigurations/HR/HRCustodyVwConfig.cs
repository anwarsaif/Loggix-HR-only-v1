using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCustodyVwConfig : IEntityTypeConfiguration<HrCustodyVw>
    {

        public void Configure(EntityTypeBuilder<HrCustodyVw> entity)
        {
            entity.ToView("HR_Custody_VW");

        }
    }  
}


