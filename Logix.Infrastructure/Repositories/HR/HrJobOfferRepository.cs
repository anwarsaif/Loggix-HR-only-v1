using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrJobOfferRepository : GenericRepository<HrJobOffer>, IHrJobOfferRepository
    {
        private readonly ApplicationDbContext context;

        public HrJobOfferRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
