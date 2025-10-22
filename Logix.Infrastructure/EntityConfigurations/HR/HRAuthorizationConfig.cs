using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAuthorizationConfig : IEntityTypeConfiguration<HrAuthorization>
    {
        public void Configure(EntityTypeBuilder<HrAuthorization> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    } 
}


