using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVisaVwConfig : IEntityTypeConfiguration<HrVisaVw>
    {
        public void Configure(EntityTypeBuilder<HrVisaVw> entity)
        {
            entity.ToView("HR_Visa_VW");

        }
    }



}


