using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPsDeductionVwConfig : IEntityTypeConfiguration<HrPsDeductionVw>
    {
        public void Configure(EntityTypeBuilder<HrPsDeductionVw> entity)
        {
            entity.ToView("HR_PS_Deduction_VW");

        }
    }
}
