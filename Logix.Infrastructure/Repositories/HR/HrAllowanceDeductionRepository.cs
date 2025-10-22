using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAllowanceDeductionRepository : GenericRepository<HrAllowanceDeduction, HrAllowanceDeductionVw>, IHrAllowanceDeductionRepository
    {
        private readonly ApplicationDbContext context;

        public HrAllowanceDeductionRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<HrAllowanceDeductionVw>> GetAllFromView(Expression<Func<HrAllowanceDeductionVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrAllowanceDeductionVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrAllowanceDeductionVw>();
            }
        }

        public async Task<decimal> GetTotalAllowances(long EmpId)
        {
            decimal TotalAllowance = 0;
            var getTotalAllowance = await context.HrAllowanceDeductions
                .Where(e => e.EmpId == EmpId && e.IsDeleted == false && e.FixedOrTemporary == 1 && e.TypeId == 1)
                .ToListAsync();

            if (getTotalAllowance != null && getTotalAllowance.Any())
            {
                TotalAllowance = getTotalAllowance
                    .Sum(item => item.Amount.HasValue ? item.Amount.Value : 0);
            }
            return TotalAllowance;
        }

        public async Task<decimal> GetTotalDeduction(long EmpId)
        {
            decimal TotalDeduction = 0;
            var getTotalDeduction = await context.HrAllowanceDeductions
                .Where(e => e.EmpId == EmpId && e.IsDeleted == false && e.FixedOrTemporary == 1 && e.TypeId == 2)
                .ToListAsync();

            if (getTotalDeduction != null && getTotalDeduction.Any())
            {
                TotalDeduction = getTotalDeduction
                    .Sum(item => item.Amount.HasValue ? item.Amount.Value : 0);
            }
            return TotalDeduction;
        }
        public async Task<int> Chk_Allowance_Deduction_Exists_2(long empId, int typeId, int adId, int fixedOrTemporary, string dueDate)
        {
            var count = await context.HrAllowanceDeductions
                .Where(e => e.IsDeleted == false
                            && e.EmpId == empId
                            && e.TypeId == typeId
                            && e.AdId == adId
                            && e.FixedOrTemporary == fixedOrTemporary
                            && e.DueDate == dueDate)
                .CountAsync();

            return count;
        }




    }


}
