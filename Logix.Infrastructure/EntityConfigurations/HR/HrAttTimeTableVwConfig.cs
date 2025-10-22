using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttTimeTableVwConfig : IEntityTypeConfiguration<HrAttTimeTableVw>
    {
        public void Configure(EntityTypeBuilder<HrAttTimeTableVw> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

        }
    }



}


