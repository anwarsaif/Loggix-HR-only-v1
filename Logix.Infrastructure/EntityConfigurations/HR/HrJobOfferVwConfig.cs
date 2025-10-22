using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrJobOfferVwConfig : IEntityTypeConfiguration<HrJobOfferVw>
    {
        public void Configure(EntityTypeBuilder<HrJobOfferVw> entity)
        {
            entity.ToView("HR_Job_Offer_VW", "dbo");

        }
    }



}


