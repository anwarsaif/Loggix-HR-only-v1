using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrKpiTemplatesVwConfig : IEntityTypeConfiguration<HrKpiTemplatesVw>
    {
        public void Configure(EntityTypeBuilder<HrKpiTemplatesVw> entity)
        {
            entity.ToView("HR_KPI_Templates_VW");
        }
    }  
    
}
