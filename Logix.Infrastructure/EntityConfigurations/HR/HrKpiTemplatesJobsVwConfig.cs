using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrKpiTemplatesJobsVwConfig : IEntityTypeConfiguration<HrKpiTemplatesJobsVw>
    {
        public void Configure(EntityTypeBuilder<HrKpiTemplatesJobsVw> entity)
        {
            entity.ToView("HR_KPI_Templates_Jobs_VW", "dbo");

        }
    }



}


