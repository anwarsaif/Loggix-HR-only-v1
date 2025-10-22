using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrSettingRepository : GenericRepository<HrSetting>, IHrSettingRepository
    {
        private readonly ApplicationDbContext _context;

        public HrSettingRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
