using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrLanguageRepository : GenericRepository<HrLanguage>, IHrLanguageRepository
    {
        private readonly ApplicationDbContext _context;

        public HrLanguageRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
