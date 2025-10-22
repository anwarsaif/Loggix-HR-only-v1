using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrCompetenceRepository : GenericRepository<HrCompetence>, IHrCompetenceRepository
    {
        private readonly ApplicationDbContext _context;

        public HrCompetenceRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }

}
