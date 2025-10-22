using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    class HrJobLevelsVwConfig : IEntityTypeConfiguration<HrJobLevelsVw>
    {
        public void Configure(EntityTypeBuilder<HrJobLevelsVw> entity)
        {
            entity.ToView("HR_Job_Levels_VW");
        }
    }
}

