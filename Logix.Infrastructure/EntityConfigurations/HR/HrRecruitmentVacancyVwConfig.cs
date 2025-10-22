using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRecruitmentVacancyVwConfig : IEntityTypeConfiguration<HrRecruitmentVacancyVw>
    {
        public void Configure(EntityTypeBuilder<HrRecruitmentVacancyVw> entity)
        {
            entity.ToView("HR_Recruitment_Vacancy_VW");


        }
    }

}
