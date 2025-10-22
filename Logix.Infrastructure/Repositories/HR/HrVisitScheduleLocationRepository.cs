using System.Linq;
using System.Linq.Expressions;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrVisitScheduleLocationRepository : GenericRepository<HrVisitScheduleLocation>, IHrVisitScheduleLocationRepository
    {
        private readonly ApplicationDbContext _context;

        public HrVisitScheduleLocationRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<int> GetNewVisitCount(long groupId, string branchesId)
        {
            try
            {
                var branchesIdList = branchesId.Split(',');
                var steps = await _context.HrVisitSteps.AsNoTracking().ToListAsync();
                // get step IDs that contains the user group
                var stepIdList = steps
                    .Where(x => !string.IsNullOrEmpty(x.GroupsId) && x.GroupsId.Split(',').Contains(groupId.ToString()))
                    .Select(x => x.Id).ToList();

                int visitCount = await _context.HrVisitScheduleLocationVws
                    .Where(x => x.IsDeleted == false && x.IsDeletedM == false && x.CheckOut != null && stepIdList.Contains(x.ToStepId ?? 0)
                            && branchesIdList.Contains(x.BranchId.ToString()))
                    .CountAsync();

                return visitCount;
            }
            catch
            {
                return 0;
            }
        }
    }
}
