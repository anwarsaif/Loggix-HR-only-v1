using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCustodyItemsVwConfig : IEntityTypeConfiguration<HrCustodyItemsVw>
    {
        public void Configure(EntityTypeBuilder<HrCustodyItemsVw> entity)
        {
            entity.ToView("HR_Custody_Items_VW");

        }
    }

    }



