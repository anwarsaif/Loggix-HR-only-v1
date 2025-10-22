using Logix.Domain.HR;
using Logix.Domain.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmployeeConfig : IEntityTypeConfiguration<HrEmployee>
    {
        public void Configure(EntityTypeBuilder<HrEmployee> entity)
        {
            entity.ToView("HR_Employee");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }

}
