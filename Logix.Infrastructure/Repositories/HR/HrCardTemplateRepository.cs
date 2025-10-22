using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrCardTemplateRepository : GenericRepository<HrCardTemplate>, IHrCardTemplateRepository
    {
        private readonly ApplicationDbContext _context;

        public HrCardTemplateRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
