using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRequestConfig : IEntityTypeConfiguration<HrRequest>
    {
        public void Configure(EntityTypeBuilder<HrRequest> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

        }
    }



}


