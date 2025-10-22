using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVacancyStatusVwConfig : IEntityTypeConfiguration<HrVacancyStatusVw>
    {
        public void Configure(EntityTypeBuilder<HrVacancyStatusVw> entity)
        {
            entity.ToView("HR_Vacancy_Status_VW");

        }
    }

}
