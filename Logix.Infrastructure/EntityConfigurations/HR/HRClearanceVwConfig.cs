using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrClearanceVwConfig : IEntityTypeConfiguration<HrClearanceVw>
    {
        public void Configure(EntityTypeBuilder<HrClearanceVw> entity)
        {
            entity.ToView("HR_Clearance_VW");

            entity.Property(e => e.BankId).IsFixedLength();
        }
    }  
}


