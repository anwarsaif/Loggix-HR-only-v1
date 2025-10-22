using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrKpiTemplatesCompetenceRepository : GenericRepository<HrKpiTemplatesCompetence>, IHrKpiTemplatesCompetenceRepository
    {
        private readonly ApplicationDbContext _context;

        public HrKpiTemplatesCompetenceRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
