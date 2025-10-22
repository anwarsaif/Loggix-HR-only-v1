using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrJobOfferAdvantageRepository : GenericRepository<HrJobOfferAdvantage>, IHrJobOfferAdvantageRepository
    {
        private readonly ApplicationDbContext context;

        public HrJobOfferAdvantageRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
