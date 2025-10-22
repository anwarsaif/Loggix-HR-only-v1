using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrArchivesFileConfig : IEntityTypeConfiguration<HrArchivesFile>
    {
        public void Configure(EntityTypeBuilder<HrArchivesFile> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");


        }
    }

}
