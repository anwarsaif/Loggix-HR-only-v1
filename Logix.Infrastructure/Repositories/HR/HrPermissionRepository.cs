using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPermissionRepository : GenericRepository<HrPermission, HrPermissionsVw>, IHrPermissionRepository
    {
        private readonly ApplicationDbContext context;

        public HrPermissionRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrPermissionsVw>> GetAllFromView(Expression<Func<HrPermissionsVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrPermissionsVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrPermissionsVw>();
            }
        }
    }



}
