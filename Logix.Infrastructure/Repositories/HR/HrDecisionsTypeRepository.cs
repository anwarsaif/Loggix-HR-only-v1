using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrDecisionsTypeRepository : GenericRepository<HrDecisionsType>, IHrDecisionsTypeRepository
    {
        private readonly ApplicationDbContext context;

        public HrDecisionsTypeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
