using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRecruitmentApplicationVwConfig : IEntityTypeConfiguration<HrRecruitmentApplicationVw>
    {
        public void Configure(EntityTypeBuilder<HrRecruitmentApplicationVw> entity)
        {
            entity.ToView("HR_Recruitment_Application_VW");

        }
    }

}
