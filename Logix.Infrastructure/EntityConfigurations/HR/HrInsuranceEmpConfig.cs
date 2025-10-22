using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrInsuranceEmpConfig : IEntityTypeConfiguration<HrInsuranceEmp>
    {
        public void Configure(EntityTypeBuilder<HrInsuranceEmp> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    } 
}


