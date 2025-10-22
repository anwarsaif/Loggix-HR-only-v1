using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrTrainingBagRepository : GenericRepository<HrTrainingBag>, IHrTrainingBagRepository
    {
        private readonly ApplicationDbContext _context;

        public HrTrainingBagRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
