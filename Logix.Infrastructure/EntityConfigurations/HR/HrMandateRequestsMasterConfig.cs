using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrMandateRequestsMasterConfig : IEntityTypeConfiguration<HrMandateRequestsMaster>
    {
        public void Configure(EntityTypeBuilder<HrMandateRequestsMaster> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.ModifiedOn).HasDefaultValueSql("(getdate())");
        }
    }




}


