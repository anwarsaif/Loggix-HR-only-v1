using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrKpiRepository : GenericRepository<HrKpi, HrKpiVw>, IHrKpiRepository
    {
        private readonly ApplicationDbContext context;

        public HrKpiRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<object>> GetKpiEmployeeDetailsAsync(HRKpiQueryFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                // Step 1: Get the KPI IDs
                var kpiIds = context.HrKpis
                    .Where(k => k.IsDeleted == false && k.PerformanceId == filter.PerformanceId)
                    .GroupBy(k => k.EmpId)
                    .Select(g => new
                    {
                        EmpId = g.Key,
                        KpiId = g.Select(k => k.Id).FirstOrDefault() // Adjust as needed
                    })
                    .ToList(); 

                // Step 2: Compute DegreeTotal
                var degreeTotals = kpiIds
                    .Select(kpi => new
                    {
                        KpiId = kpi.KpiId,
                        DegreeTotal = context.HrKpiDetailesVws
                            .Where(detail => detail.KpiId == kpi.KpiId)
                            .GroupBy(detail => 1)
                            .Select(group => (decimal?)group.Sum(detail => detail.Degree) / group.Sum(detail => detail.Score) * 100)
                            .FirstOrDefault()
                    })
                    .ToDictionary(x => x.KpiId, x => x.DegreeTotal); // Convert to dictionary for quick lookup

                // Step 3: Fetch employee data first
                var employees = await context.HrEmployeeVws
                    .Where(employee => employee.IsDeleted == false && employee.StatusId != 2
                        && (filter.DeptId == 0 || employee.DeptId == filter.DeptId)
                        && (filter.Location == 0 || employee.Location == filter.Location)
                        && (filter.BranchId == 0 || employee.BranchId == filter.BranchId))
                    .ToListAsync(cancellationToken);

                // Step 4: Filter employees in memory for probationary employees
                var filteredEmployees = employees
                    .Where(employee => filter.ExcludingProbationaryEmployees == 0 ||
                        (filter.ExcludingProbationaryEmployees == 1 && DateDiffDayFromToday(employee.Doappointment) > 90))
                    .ToList();

                // Step 5: Construct the final query with filtered employees and additional conditions
                var query = from employee in filteredEmployees
                            let degreeTotal = degreeTotals.ContainsKey(context.HrKpis
                                .Where(k => k.IsDeleted == false && k.EmpId == employee.Id && k.PerformanceId == filter.PerformanceId)
                                .Select(k => k.Id)
                                .FirstOrDefault()) ? degreeTotals[context.HrKpis
                                .Where(k => k.IsDeleted == false && k.EmpId == employee.Id && k.PerformanceId == filter.PerformanceId)
                                .Select(k => k.Id)
                                .FirstOrDefault()] : 0
                            let status = context.HrKpis.Any(k => k.IsDeleted == false && k.EmpId == employee.Id && k.PerformanceId == filter.PerformanceId) ? "مقيم" : "غير مقيم"
                            let hrKpiId = context.HrKpis
                                       .Where(k => k.IsDeleted == false && k.EmpId == employee.Id && k.PerformanceId == filter.PerformanceId)
                                       .Select(k => k.Id)
                                       .FirstOrDefault()
                            let evaluator = context.SysUsers
                                       .Where(u => u.UserPkId == (context.HrKpis
                                                                .Where(k => k.IsDeleted == false && k.EmpId == employee.Id && k.PerformanceId == filter.PerformanceId)
                                                                .Select(k => k.CreatedBy)
                                                                .FirstOrDefault()))
                                       .Select(u => u.UserFullname)
                                       .FirstOrDefault()
                            let appId = context.WfApplications
                                       .Where(a => a.ApplicantsId == (context.HrKpis
                                                                         .Where(k => k.IsDeleted == false && k.EmpId == employee.Id && k.PerformanceId == filter.PerformanceId)
                                                                         .Select(k => k.AppId)
                                                                         .FirstOrDefault()))
                                       .Select(a => a.ApplicationCode)
                                       .FirstOrDefault()
                            let evaluationDate = context.HrKpis
                                        .Where(k => k.IsDeleted == false && k.EmpId == employee.Id && k.PerformanceId == filter.PerformanceId)
                                        .Select(k => k.EvaDate)
                                        .FirstOrDefault()
                            let statusName = context.WfApplicationsVws
                                       .Where(a => a.Id == (context.HrKpis
                                                           .Where(k => k.IsDeleted == false && k.EmpId == employee.Id && k.PerformanceId == filter.PerformanceId && k.AppId != 0)
                                                           .Select(k => k.AppId)
                                                           .FirstOrDefault()))
                                       .Select(a => a.StatusName)
                                       .FirstOrDefault()
                            let stepName = context.WfApplicationsVws
                                      .Where(a => a.Id == (context.HrKpis
                                                          .Where(k => k.IsDeleted == false && k.EmpId == employee.Id && k.PerformanceId == filter.PerformanceId && k.AppId != 0)
                                                          .Select(k => k.AppId)
                                                          .FirstOrDefault()))
                                      .Select(a => a.StepName)
                                      .FirstOrDefault()
                            let templateName = context.HrKpiVws
                                        .Where(k => k.KpiTemId == (context.HrKpis
                                                                   .Where(k => k.IsDeleted == false && k.EmpId == employee.Id && k.PerformanceId == filter.PerformanceId)
                                                                   .Select(k => k.KpiTemId)
                                                                   .FirstOrDefault()))
                                        .Select(k => k.TemName)
                                        .FirstOrDefault()
                            where filter.Status == 0 || (filter.Status == 1 && context.HrKpis.Any(k => k.IsDeleted == false && k.EmpId == employee.Id
                                                        && (filter.PerformanceId == 0 || k.PerformanceId == filter.PerformanceId)
                                                        && (filter.EvaluationStatus == 0 || k.StatusId == filter.EvaluationStatus)
                                                        && (filter.Month == 0 || k.EvaDate.Substring(5, 2) == filter.Month.ToString().PadLeft(2, '0'))
                                                        && (string.IsNullOrEmpty(filter.FinancialYear) || k.EvaDate.Substring(0, 4) == filter.FinancialYear)))
                                  || (filter.Status == 2 && !context.HrKpis.Any(k => k.IsDeleted == false && k.EmpId == employee.Id
                                                        && (filter.PerformanceId == 0 || k.PerformanceId == filter.PerformanceId)
                                                        && (filter.EvaluationStatus == 0 || k.StatusId == filter.EvaluationStatus)
                                                        && (filter.Month == 0 || k.EvaDate.Substring(5, 2) == filter.Month.ToString().PadLeft(2, '0'))
                                                        && (string.IsNullOrEmpty(filter.FinancialYear) || k.EvaDate.Substring(0, 4) == filter.FinancialYear)))
                            select new
                            {
                                DegreeTotal = degreeTotal,
                                Status = status,
                                HrKpiId = hrKpiId,
                                Evaluator = evaluator,
                                AppId = appId,
                                EvaluationDate = evaluationDate,
                                StatusName = statusName,
                                StepName = stepName,
                                TemplateName = templateName,
                                EmployeeCode = employee.EmpId,
                                EmployeeName = employee.EmpName ?? "",
                                EmployeeName2 = employee.EmpName2 ?? "",
                                AppointmentDate = employee.Doappointment ?? "",
                                JobTitle = employee.CatName ?? "",
                                BranchName = employee.BraName ?? "",
                                BranchName2 = employee.BraName2 ?? ""
                            };

                var resultList = query.ToList();
                return resultList;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message.ToString());
            }
        }


        private int DateDiffDayFromToday(string dateString)
        {
            string[] formats = { "yyyy/MM/dd", "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy" }; // Add any other formats you expect
            if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return (DateTime.Now - date).Days;
            }
            else
            {
                throw new ArgumentException("Invalid date string");
            }
        }



    }
}
