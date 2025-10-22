using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrOpeningBalanceVwConfig : IEntityTypeConfiguration<HrOpeningBalanceVw>
    {
        public void Configure(EntityTypeBuilder<HrOpeningBalanceVw> entity)
        {
            entity.ToView("HR_Opening_Balance_VW");
        }
    }
}


