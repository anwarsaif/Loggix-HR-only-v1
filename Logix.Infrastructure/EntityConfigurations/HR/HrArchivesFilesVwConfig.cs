using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrArchivesFilesVwConfig : IEntityTypeConfiguration<HrArchivesFilesVw>
    {
        public void Configure(EntityTypeBuilder<HrArchivesFilesVw> entity)
        {
            entity.ToView("HR_Archives_Files_VW");

        }
    }

}
