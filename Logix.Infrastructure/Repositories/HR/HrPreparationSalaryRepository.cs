using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPreparationSalaryRepository : GenericRepository<HrPreparationSalary>, IHrPreparationSalaryRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentData session;

        public HrPreparationSalaryRepository(ApplicationDbContext context, ICurrentData session) : base(context)
        {
            this.context = context;
            this.session = session;
        }
        public async Task<IEnumerable<HrPreparationSalariesVw>> GetAllFromView(Expression<Func<HrPreparationSalariesVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrPreparationSalariesVw>().Where(expression).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {
                return new List<HrPreparationSalariesVw>();
            }
        }

        public async Task<List<HrPayrollCompareResult>> PreparationSalariesPayrollCompare(HrPayrollCompareFilterDto filter, int CmdType)
        {
            string[] branches = new string[0];

            if (filter.BranchId == 0)
            {
                branches = session.Branches.Split(",");
            }

            if (CmdType == 1)
            {
                var query = from main in context.HrPreparationSalariesVws
                            where new[] { filter.PreviousMonth, filter.CurrentMonth }.Contains(main.MsMonth)
                                  && main.IsDeleted == false
                                  && main.FinancelYear == filter.FinancialYear
                            group main by new
                            {
                                main.DeptId,
                                main.Location,
                                main.DepName,
                                main.LocationName,
                                main.FinancelYear
                            } into g
                            select new HrPayrollCompareResult
                            {
                                DepName = g.Key.DepName,
                                LocationName = g.Key.LocationName,
                                FinancelYear = g.Key.FinancelYear ?? 0,
                                PervMonth = g.Count(x => x.MsMonth == filter.PreviousMonth),
                                CurMonth = g.Count(x => x.MsMonth == filter.CurrentMonth),
                                Difference = g.Count(x => x.MsMonth == filter.PreviousMonth) -
                                             g.Count(x => x.MsMonth == filter.CurrentMonth)
                            };

                return query.ToList();
            }

            if (CmdType == 2)
            {

                var empId = context.HrPreparationSalaries.Where(x => x.MsMonth == filter.CurrentMonth && x.IsDeleted == false).Select(x => x.EmpId);
                var prevMonthEmployees = context.HrPreparationSalariesVws.Where(x => x.MsMonth == filter.PreviousMonth && x.IsDeleted == false && !empId.Contains(x.EmpId))
                    .Select(main => new HrPayrollCompareResult
                    {

                        EmpId = main.EmpId,
                        DepName = main.DepName,
                        LocationName = main.LocationName,
                        empCode = main.EmpCode,
                        empName = main.EmpName,
                        FinancelYear = (int)main.FinancelYear,
                        MsMonth = main.MsMonth
                    }).ToList();
                //var result 
                return prevMonthEmployees;
            }

            if (CmdType == 3)
            {

                var empId = context.HrPreparationSalaries.Where(x => x.MsMonth == filter.PreviousMonth && x.IsDeleted == false).Select(x => x.EmpId);
                var currentMonthEmployees = context.HrPreparationSalariesVws.Where(x => x.MsMonth == filter.CurrentMonth && x.IsDeleted == false && !empId.Contains(x.EmpId))
                    .Select(main => new HrPayrollCompareResult
                    {

                        EmpId = main.EmpId,
                        DepName = main.DepName,
                        LocationName = main.LocationName,
                        empCode = main.EmpCode,
                        empName = main.EmpName,
                        FinancelYear = (int)main.FinancelYear,
                        MsMonth = main.MsMonth
                    }).ToList();
                //var result 
                return currentMonthEmployees;
            }

            return null;
        }

    }

}
