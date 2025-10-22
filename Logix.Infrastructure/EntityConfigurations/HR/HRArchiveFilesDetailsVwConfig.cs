using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrArchiveFilesDetailsVwConfig : IEntityTypeConfiguration<HrArchiveFilesDetailsVw>
    {
        public void Configure(EntityTypeBuilder<HrArchiveFilesDetailsVw> entity)
        {
            entity.ToView("HR_Archive_FilesDetails_VW");
        }
    }
}


