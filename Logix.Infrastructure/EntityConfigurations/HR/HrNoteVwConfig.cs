using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrNoteVwConfig : IEntityTypeConfiguration<HrNoteVw>
    {
        public void Configure(EntityTypeBuilder<HrNoteVw> entity)
        {
            entity.ToView("HR_Note_VW");

        }
    }



}


