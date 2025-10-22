using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class hrOverTimeDVwConfig : IEntityTypeConfiguration<HrOverTimeDVw>
{
    public void Configure(EntityTypeBuilder<HrOverTimeDVw> entity)
    {
        entity.ToView("HR_OverTime_D_VW");

    }
}


