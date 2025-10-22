using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrMandateLocationMasterConfig : IEntityTypeConfiguration<HrMandateLocationMaster>
    {
        public void Configure(EntityTypeBuilder<HrMandateLocationMaster> entity)
        {

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }



}


