using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrNotificationsTypeRepository : GenericRepository<HrNotificationsType>, IHrNotificationsTypeRepository
    {
        private readonly ApplicationDbContext context;

        public HrNotificationsTypeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<HrNotificationsTypeVw>> GetAllVW(Expression<Func<HrNotificationsTypeVw, bool>> expression)
        {
            return await context.Set<HrNotificationsTypeVw>().Where(expression).AsNoTracking().ToListAsync();
        }
    }



}
