using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Domain.Main;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrOhadDetailsVwRepository : GenericRepository<HrOhadDetailsVw>, IHrOhadDetailsVwRepository
    {
        private readonly ApplicationDbContext _context;

        public HrOhadDetailsVwRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<HrOhadDetailsVw>> GetByInvestEmployeesAsync(long empId = 0, int transTypeId = 1)
        {
            if (empId <= 0)
            {
                return Enumerable.Empty<HrOhadDetailsVw>();
            }
            var query = from ohad in _context.HrOhadDetailsVws
                        where ohad.IsDeleted == false
                              && ohad.TransTypeId == transTypeId
                              && ohad.EmpId == empId
                              && (
                                    (from sub in _context.HrOhadDetailsVws
                                     where sub.IsDeleted == false
                                           && sub.EmpId == ohad.EmpId
                                           && sub.ItemId == ohad.ItemId
                                           && sub.ItemStateId == ohad.ItemStateId
                                           && sub.OrgnalId == ohad.OrgnalId
                                     select (decimal?)sub.QtyIn
                                    ).Sum()
                                    -
                                    (from sub in _context.HrOhadDetailsVws
                                     where sub.IsDeleted == false
                                           && sub.EmpId == ohad.EmpId
                                           && sub.ItemId == ohad.ItemId
                                           && sub.ItemStateId == ohad.ItemStateId
                                           && sub.OrgnalId == ohad.OrgnalId
                                     select (decimal?)sub.QtyOut
                                    ).Sum()
                                 ) > 0
                        select ohad;
            return await query.ToListAsync();
        }
    }
}
