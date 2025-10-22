using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRequestTypeConfig : IEntityTypeConfiguration<HrRequestType>
    {
        public void Configure(EntityTypeBuilder<HrRequestType> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }



}


