using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrNotificationsSettingRepository : GenericRepository<HrNotificationsSetting>, IHrNotificationsSettingRepository
    {
        private readonly ApplicationDbContext _context;

        public HrNotificationsSettingRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
