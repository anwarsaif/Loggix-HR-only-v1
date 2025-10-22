using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVacationBalanceVwConfig : IEntityTypeConfiguration<HrVacationBalanceVw>
    {
        public void Configure(EntityTypeBuilder<HrVacationBalanceVw> entity)
        {

            entity.ToView("HR_VacationBalance_VW");
        }
    }

}
