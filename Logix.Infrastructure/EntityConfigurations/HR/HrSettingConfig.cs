using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrSettingConfig : IEntityTypeConfiguration<HrSetting>
    {
        public void Configure(EntityTypeBuilder<HrSetting> entity)
        {
            entity.Property(e => e.MonthEndDay).IsFixedLength();

            entity.Property(e => e.MonthStartDay).IsFixedLength();
        }
    } 
    
}
