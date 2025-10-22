using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrGosiEmployeeConfig : IEntityTypeConfiguration<HrGosiEmployee>
    {
        public void Configure(EntityTypeBuilder<HrGosiEmployee> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }



}


