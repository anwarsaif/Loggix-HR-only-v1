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
    public class HrStructureVwConfig : IEntityTypeConfiguration<HrStructureVw>
    {
        public void Configure(EntityTypeBuilder<HrStructureVw> entity)
        {
            entity.ToView("HR_Structure_VW");
        }
    }
}
