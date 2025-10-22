using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPayrollDRepository : GenericRepository<HrPayrollD, HrPayrollDVw>, IHrPayrollDRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentData session;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;
        private IQueryRepository<HrPayrollDVw> queryRepository;

        public HrPayrollDRepository(ApplicationDbContext context, IQueryRepository<HrPayrollDVw> queryRepository, ICurrentData session, ISysConfigurationAppHelper sysConfigurationAppHelper) : base(context)
        {
            this.context = context;
            this.queryRepository = queryRepository;
            this.session = session;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
        }


        public async Task<List<PayrollAccountingEntryDto>> GetHrPayrollDTrans(long msId, long FacilityId)
        {
            try
            {
                var getFromPayrollDVW = context.HrPayrollDVw.Where(x => x.MsId == msId).Take(1).FirstOrDefault();

                if (FacilityId <= 0) FacilityId = session.FacilityId;
                var getType = await sysConfigurationAppHelper.GetValue(14, FacilityId);
                if (getType == null || string.IsNullOrEmpty(getType))
                {
                    getType = "1";

                }
                //  القيد الإفتراضي
                if (getType == "1")
                {

                    var description = $"قيد الرواتب التلقائي لشهر {getFromPayrollDVW.MsMonth} للعام {getFromPayrollDVW.FinancelYear}";

                    // Salary Expense
                    var salaryExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                             join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                             where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                             group payrollDVW by salaryGroup.AccountSalaryId into grouped
                                             where grouped.Sum(x => x.Salary) > 0
                                             select new PayrollAccountingEntryDto
                                             {
                                                 AccountID = grouped.Key,
                                                 Debit = grouped.Sum(x => x.Salary),
                                                 Credit = 0,
                                                 ReferenceNo = 0,
                                                 ReferenceTypeID = 1,
                                                 Description = description,
                                                 CCId = 0
                                             };
                    // Allowance Expense
                    var allowanceExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                                join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                                where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                                group payrollDVW by salaryGroup.AccountAllowancesId into grouped
                                                where grouped.Sum(x => x.Allowance) > 0
                                                select new PayrollAccountingEntryDto
                                                {
                                                    AccountID = grouped.Key,
                                                    Debit = grouped.Sum(x => x.Allowance),
                                                    Credit = 0,
                                                    ReferenceNo = 0,
                                                    ReferenceTypeID = 1,
                                                    Description = description,
                                                    CCId = 0
                                                };
                    // Overtime Expense
                    var overtimeExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                               join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                               where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                               group payrollDVW by salaryGroup.AccountOverTimeId into grouped
                                               where grouped.Sum(x => x.OverTime) > 0
                                               select new PayrollAccountingEntryDto
                                               {
                                                   AccountID = grouped.Key,
                                                   Debit = grouped.Sum(x => x.OverTime),
                                                   Credit = 0,
                                                   ReferenceNo = 0,
                                                   ReferenceTypeID = 1,
                                                   Description = description,
                                                   CCId = 0
                                               };
                    // Salary Payable
                    var salaryPayableQuery = from p in context.HrPayrollDVw
                                             join g in context.HrSalaryGroups on p.SalaryGroupId equals g.Id
                                             where p.MsId == msId && p.IsDeleted == false
                                             group p by new { g.AccountDueSalaryId, p.EmpId } into grp
                                             where grp.Sum(x => x.Salary + x.Allowance + x.OverTime - x.Loan - x.Absence - x.Delay - x.Deduction) > 0
                                             select new PayrollAccountingEntryDto
                                             {
                                                 AccountID = grp.Key.AccountDueSalaryId,
                                                 Debit = 0,
                                                 Credit = grp.Sum(x => x.Salary + x.Allowance + x.OverTime - x.Loan - x.Absence - x.Delay - x.Deduction),
                                                 ReferenceNo = grp.Key.EmpId,
                                                 ReferenceTypeID = 9,
                                                 Description = description,
                                                 CCId = 0
                                             };
                    var loanDeductionQuery = from p in context.HrPayrollDVw
                                             join g in context.HrSalaryGroups on p.SalaryGroupId equals g.Id
                                             where p.MsId == msId && p.IsDeleted == false
                                             group p by new { g.AccountLoanId, p.EmpId } into grp
                                             where grp.Sum(x => x.Loan) > 0
                                             select new PayrollAccountingEntryDto
                                             {
                                                 AccountID = grp.Key.AccountLoanId,
                                                 Debit = 0,
                                                 Credit = grp.Sum(x => x.Loan),
                                                 ReferenceNo = grp.Key.EmpId,
                                                 ReferenceTypeID = 10,
                                                 Description = description,
                                                 CCId = 0
                                             };

                    var absenceDelayDeductionQuery = from payrollDVW in context.HrPayrollDVw
                                                     join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                                     where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                                     group payrollDVW by new { payrollDVW.EmpId, salaryGroup.AccountDeductionId } into grouped // Group by both EmpId and AccountDeductionId
                                                     where grouped.Sum(x => x.Absence + x.Delay + x.Deduction) > 0
                                                     select new PayrollAccountingEntryDto
                                                     {
                                                         AccountID = grouped.Key.AccountDeductionId, // Assuming AccountDeductionId is relevant for absence/delay/deduction
                                                         Debit = 0,
                                                         Credit = grouped.Sum(x => x.Absence + x.Delay + x.Deduction),
                                                         ReferenceNo = grouped.Key.EmpId,
                                                         ReferenceTypeID = 1, // You might need to adjust ReferenceTypeID depending on your definition
                                                         Description = description,
                                                         CCId = 0
                                                     };


                    var combinedResults = salaryExpenseQuery.ToList().Union(allowanceExpenseQuery.ToList())
                                                           .Union(overtimeExpenseQuery.ToList())
                                                           .Union(salaryPayableQuery.ToList())
                                                           .Union(loanDeductionQuery.ToList())
                                                           .Union(absenceDelayDeductionQuery.ToList());

                    return combinedResults.ToList();
                }
                // قيد اتحاد الإخوة تفصيل البدلات
                if (getType == "3")
                {

                    var description = $"شهر {getFromPayrollDVW.MsMonth} للعام {getFromPayrollDVW.FinancelYear}";

                    // Salary Expense
                    var salaryExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                             join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                             where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                             group new { payrollDVW, salaryGroup } by new { salaryGroup.AccountSalaryId, payrollDVW.EmpId, payrollDVW.EmpName, payrollDVW.CcId } into grouped
                                             where grouped.Sum(x => x.payrollDVW.Salary + x.payrollDVW.OverTime - x.payrollDVW.Absence - x.payrollDVW.Delay) > 0
                                             select new PayrollAccountingEntryDto
                                             {
                                                 AccountID = grouped.Key.AccountSalaryId,
                                                 Debit = grouped.Sum(x => x.payrollDVW.Salary + x.payrollDVW.OverTime - x.payrollDVW.Absence - x.payrollDVW.Delay),
                                                 Credit = 0,
                                                 ReferenceNo = 0,
                                                 ReferenceTypeID = 1,
                                                 Description = grouped.Key.EmpName + description,
                                                 CCId = grouped.Key.CcId,
                                                 EmpName = grouped.Key.EmpName
                                             };

                    // Allowances Expense
                    var allowancesExpenseQuery = from allowanceDVW in context.HrPayrollDeductionAccountsVws
                                                 where allowanceDVW.MsId == msId && allowanceDVW.IsDeleted == false
                                                 group allowanceDVW by new { allowanceDVW.AccountExpId, allowanceDVW.EmpId, allowanceDVW.EmpName, allowanceDVW.CcId } into grouped
                                                 where grouped.Sum(x => x.Amount) > 0
                                                 select new PayrollAccountingEntryDto
                                                 {
                                                     AccountID = grouped.Key.AccountExpId,
                                                     Debit = grouped.Sum(x => x.Amount),
                                                     Credit = 0,
                                                     ReferenceNo = 0,
                                                     ReferenceTypeID = 1,
                                                     Description = grouped.Key.EmpName + description,
                                                     CCId = grouped.Key.CcId,
                                                     EmpName = grouped.Key.EmpName
                                                 };

                    // Deductions Expense
                    var deductionsExpenseQuery = from deductionDVW in context.HrPayrollDeductionAccountsVws
                                                 where deductionDVW.MsId == msId && deductionDVW.IsDeleted == false
                                                 group deductionDVW by new { deductionDVW.AccountExpId, deductionDVW.EmpId, deductionDVW.EmpName, deductionDVW.CcId } into grouped
                                                 where grouped.Sum(x => x.Amount) > 0
                                                 select new PayrollAccountingEntryDto
                                                 {
                                                     AccountID = grouped.Key.AccountExpId,
                                                     Debit = grouped.Sum(x => x.Amount),
                                                     Credit = 0,
                                                     ReferenceNo = 0,
                                                     ReferenceTypeID = 1,
                                                     Description = grouped.Key.EmpName + description,
                                                     CCId = grouped.Key.CcId,
                                                     EmpName = grouped.Key.EmpName
                                                 };

                    // Other queries...

                    var combinedResults = salaryExpenseQuery.ToList()
                                           .Union(allowancesExpenseQuery.ToList())
                                           .Union(deductionsExpenseQuery.ToList());

                    return combinedResults.ToList();
                }

                //    قيد مصنع طيبة السلف مرتبطة تفصيل البدلات
                if (getType == "4")
                {
                    var description = $"شهر {getFromPayrollDVW.MsMonth} للعام {getFromPayrollDVW.FinancelYear}";


                    var salaryExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                             join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                             where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                             group payrollDVW by new { salaryGroup.AccountSalaryId, payrollDVW.EmpId, payrollDVW.EmpName, payrollDVW.CcId } into grouped
                                             where grouped.Sum(x => x.Salary + x.OverTime - x.Absence - x.Delay) > 0
                                             select new
                                             {
                                                 Account_ID = grouped.Key.AccountSalaryId,
                                                 Debit = grouped.Sum(x => x.Salary + x.OverTime - x.Absence - x.Delay),
                                                 Credit = 0,
                                                 Reference_No = 0,
                                                 Reference_Type_ID = 1,
                                                 Description = grouped.Key.EmpName + description,
                                                 CC_ID = grouped.Key.CcId,
                                                 Emp_ID = grouped.Key.EmpId,
                                                 Emp_Name = grouped.Key.EmpName
                                             };

                    var allowancesExpenseQuery = from allowance in context.HrPayrollDeductionAccountsVws
                                                 where allowance.MsId == msId && allowance.IsDeleted == false
                                                 group allowance by new { allowance.AccountExpId, allowance.EmpId, allowance.EmpName, allowance.CcId } into grouped
                                                 where grouped.Sum(x => x.Amount) > 0
                                                 select new
                                                 {
                                                     Account_ID = grouped.Key.AccountExpId,
                                                     Debit = grouped.Sum(x => x.Amount),
                                                     Credit = 0,
                                                     Reference_No = 0,
                                                     Reference_Type_ID = 1,
                                                     Description = grouped.Key.EmpName + description,
                                                     CC_ID = grouped.Key.CcId,
                                                     Emp_ID = grouped.Key.EmpId,
                                                     Emp_Name = grouped.Key.EmpName
                                                 };

                    // Similarly, create queries for other cases like deductions, due salaries, loan payments, etc.

                    var combinedResults = salaryExpenseQuery
                        .Union(allowancesExpenseQuery)
                        // Add other union queries here
                        .OrderBy(x => x.Emp_ID)
                        .ThenByDescending(x => x.Debit)
                        .ToList();

                    // Convert the combined results to the desired output type
                    var result = combinedResults.Select(x => new PayrollAccountingEntryDto
                    {
                        AccountID = x.Account_ID,
                        Debit = x.Debit,
                        Credit = x.Credit,
                        ReferenceNo = x.Reference_No,
                        ReferenceTypeID = x.Reference_Type_ID,
                        Description = x.Description,
                        CCId = x.CC_ID,
                        EmpId = x.Emp_ID,
                        EmpName = x.Emp_Name
                    }).ToList();

                    return result;

                }

                // تقييد البدلات والحسميات مفصلة
                if (getType == "5")
                {
                    var description = $"قيد الرواتب التلقائي لشهر {getFromPayrollDVW.MsMonth} للعام {getFromPayrollDVW.FinancelYear}";


                    // Query to calculate salary expense
                    var salaryExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                             join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                             where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                             group payrollDVW by salaryGroup.AccountSalaryId into grouped
                                             where grouped.Sum(x => x.Salary) > 0
                                             select new PayrollAccountingEntryDto
                                             {
                                                 AccountID = grouped.Key,
                                                 Debit = grouped.Sum(x => x.Salary),
                                                 Credit = 0,
                                                 ReferenceNo = 0,
                                                 ReferenceTypeID = 1,
                                                 Description = description,
                                                 CCId = 0
                                             };

                    // Query to calculate allowances expenses
                    var allowancesExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                                 join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                                 where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                                 group payrollDVW by salaryGroup.AccountAllowancesId into grouped
                                                 where grouped.Sum(x => x.Allowance) > 0
                                                 select new PayrollAccountingEntryDto
                                                 {
                                                     AccountID = grouped.Key,
                                                     Debit = grouped.Sum(x => x.Allowance),
                                                     Credit = 0,
                                                     ReferenceNo = 0,
                                                     ReferenceTypeID = 1,
                                                     Description = description,
                                                     CCId = 0
                                                 };

                    // Query to calculate overtime expenses
                    var overtimeExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                               join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                               where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                               group payrollDVW by salaryGroup.AccountOverTimeId into grouped
                                               where grouped.Sum(x => x.OverTime) > 0
                                               select new PayrollAccountingEntryDto
                                               {
                                                   AccountID = grouped.Key,
                                                   Debit = grouped.Sum(x => x.OverTime),
                                                   Credit = 0,
                                                   ReferenceNo = 0,
                                                   ReferenceTypeID = 1,
                                                   Description = description,
                                                   CCId = 0
                                               };

                    // Query to calculate due salary expenses
                    var dueSalaryExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                                join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                                where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                                group payrollDVW by new { salaryGroup.AccountDueSalaryId, payrollDVW.EmpId } into grouped
                                                where grouped.Sum(x => x.Salary + x.Allowance + x.OverTime - x.Loan - x.Absence - x.Delay - x.Deduction) > 0
                                                select new PayrollAccountingEntryDto
                                                {
                                                    AccountID = grouped.Key.AccountDueSalaryId,
                                                    Debit = 0,
                                                    Credit = grouped.Sum(x => x.Salary + x.Allowance + x.OverTime - x.Loan - x.Absence - x.Delay - x.Deduction),
                                                    ReferenceNo = 0,
                                                    ReferenceTypeID = 9,
                                                    Description = description,
                                                    CCId = 0
                                                };

                    // Query to calculate loan expenses
                    var loanExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                           join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                           where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                           group payrollDVW by new { salaryGroup.AccountLoanId, payrollDVW.EmpId } into grouped
                                           where grouped.Sum(x => x.Loan) > 0
                                           select new PayrollAccountingEntryDto
                                           {
                                               AccountID = grouped.Key.AccountLoanId,
                                               Debit = 0,
                                               Credit = grouped.Sum(x => x.Loan),
                                               ReferenceNo = 0,
                                               ReferenceTypeID = 10,
                                               Description = description,
                                               CCId = 0
                                           };


                    // Query to calculate Absence salary expenses
                    var AbsenceExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                              join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                              where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                              group payrollDVW by new { salaryGroup.AccountDeductionId, payrollDVW.EmpId } into grouped
                                              where grouped.Sum(x => x.Absence + x.Delay) > 0
                                              select new PayrollAccountingEntryDto
                                              {
                                                  AccountID = grouped.Key.AccountDeductionId,
                                                  Debit = 0,
                                                  Credit = grouped.Sum(x => x.Absence + x.Delay),
                                                  ReferenceNo = 0,
                                                  ReferenceTypeID = 1,
                                                  Description = description,
                                                  CCId = 0
                                              };


                    // Query for deduction details
                    var deductionDetailsQuery = from deductionAccountsVW in context.HrPayrollDeductionAccountsVws
                                                where deductionAccountsVW.MsId == msId && deductionAccountsVW.IsDeleted == false
                                                group deductionAccountsVW by new { deductionAccountsVW.AccountDueId, deductionAccountsVW.CcId, deductionAccountsVW.EmpId, deductionAccountsVW.EmpName } into grouped
                                                where grouped.Sum(x => x.Amount) > 0
                                                select new PayrollAccountingEntryDto
                                                {
                                                    AccountID = grouped.Key.AccountDueId,
                                                    Debit = 0,
                                                    Credit = grouped.Sum(x => x.Amount),
                                                    ReferenceNo = 0,
                                                    ReferenceTypeID = 1,
                                                    Description = grouped.Key.EmpName + description,
                                                    CCId = grouped.Key.CcId
                                                };

                    // Query for allowance details
                    var allowanceDetailsQuery = from allowanceAccountsVW in context.HrPayrollAllowanceAccountsVws
                                                where allowanceAccountsVW.MsId == msId && allowanceAccountsVW.IsDeleted == false
                                                group allowanceAccountsVW by new { allowanceAccountsVW.AccountExpId, allowanceAccountsVW.CcId, allowanceAccountsVW.EmpId, allowanceAccountsVW.EmpName } into grouped
                                                where grouped.Sum(x => x.Amount) > 0
                                                select new PayrollAccountingEntryDto
                                                {
                                                    AccountID = grouped.Key.AccountExpId,
                                                    Debit = grouped.Sum(x => x.Amount),
                                                    Credit = 0,
                                                    ReferenceNo = 0,
                                                    ReferenceTypeID = 1,
                                                    Description = grouped.Key.EmpName + description,
                                                    CCId = grouped.Key.CcId
                                                };




                    // Combine the queries using Union
                    var combinedResults = salaryExpenseQuery.ToList()
                        .Union(allowancesExpenseQuery.ToList())
                        .Union(overtimeExpenseQuery.ToList())
                        .Union(dueSalaryExpenseQuery.ToList())
                        .Union(loanExpenseQuery.ToList())
                        .Union(AbsenceExpenseQuery.ToList())
                        .Union(deductionDetailsQuery.ToList())
                        .Union(allowanceDetailsQuery.ToList());



                    return combinedResults.ToList();

                }

                //  تقييد البدلات والحسميات وربطها بتنقلات الموظفين على مراكز التكلفة
                if (getType == "6")
                {
                    var description = $"قيد الرواتب التلقائي لشهر {getFromPayrollDVW.MsMonth} للعام {getFromPayrollDVW.FinancelYear}";

                    // Salary Expense query
                    var salaryExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                             join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                             join payrollCostCenter in context.HrPayrollCostcenters on payrollDVW.MsdId equals payrollCostCenter.MsdId
                                             where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                             group new { payrollDVW, payrollCostCenter } by salaryGroup.AccountSalaryId into grouped
                                             where grouped.Sum(x => x.payrollDVW.Salary * (x.payrollCostCenter.Rate / 100)) > 0
                                             select new
                                             {
                                                 Account_ID = grouped.Key,
                                                 Debit = grouped.Sum(x => x.payrollDVW.Salary * (x.payrollCostCenter.Rate / 100)),
                                                 Credit = 0,
                                                 Reference_No = 0,
                                                 Reference_Type_ID = 1,
                                                 Description = description,
                                                 CC_ID = grouped.FirstOrDefault().payrollCostCenter.CcId
                                             };

                    // Allowances Expense query
                    var allowancesExpenseQuery = from payrollDVW in context.HrPayrollDVw
                                                 join salaryGroup in context.HrSalaryGroups on payrollDVW.SalaryGroupId equals salaryGroup.Id
                                                 join payrollCostCenter in context.HrPayrollCostcenters on payrollDVW.MsdId equals payrollCostCenter.MsdId
                                                 where payrollDVW.MsId == msId && payrollDVW.IsDeleted == false
                                                 group new { payrollDVW, payrollCostCenter } by salaryGroup.AccountAllowancesId into grouped
                                                 where grouped.Sum(x => x.payrollDVW.Allowance * (x.payrollCostCenter.Rate / 100)) > 0
                                                 select new
                                                 {
                                                     Account_ID = grouped.Key,
                                                     Debit = grouped.Sum(x => x.payrollDVW.Allowance * (x.payrollCostCenter.Rate / 100)),
                                                     Credit = 0,
                                                     Reference_No = 0,
                                                     Reference_Type_ID = 1,
                                                     Description = description,
                                                     CC_ID = grouped.FirstOrDefault().payrollCostCenter.CcId
                                                 };

                    // Combine queries
                    var combinedResults = salaryExpenseQuery
                        .Union(allowancesExpenseQuery)
                        // Add other union queries here
                        .ToList();

                    // Convert results to PayrollAccountingEntryDto
                    var result = combinedResults.Select(x => new PayrollAccountingEntryDto
                    {
                        AccountID = x.Account_ID,
                        Debit = x.Debit,
                        Credit = x.Credit,
                        ReferenceNo = x.Reference_No,
                        ReferenceTypeID = x.Reference_Type_ID,
                        Description = x.Description,
                        CCId = x.CC_ID
                    }).ToList();

                    return result;
                }



                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<PayrollAccountingEntryResultDto>> GetPayrollReports(HrPayrollFilterDto filter, int type)
        {
            string[] branches = null;
            if (filter.BranchId != 0)
            {
                filter.BranchsId = null;
            }
            else
            {
                branches = session.Branches.Split(",");

            }
            if (type == 1)
            {


                var query = from d in context.HrPayrollDVw
                            where d.IsDeleted == false
                                && (string.IsNullOrEmpty(filter.MsMonth) || d.MsMonth == filter.MsMonth)
                                && (filter.FinancelYear == 0 || d.FinancelYear == filter.FinancelYear)
                                && (filter.PayrollTypeId == 0 || d.PayrollTypeId == filter.PayrollTypeId)
                                && (filter.FacilityId == 0 || d.FacilityId == filter.FacilityId)
                                && (filter.BranchId == 0 || d.BranchId == filter.BranchId)
                                && (string.IsNullOrEmpty(filter.BranchsId) || branches.Contains(d.BranchId.ToString()))
                            group d by new { d.LocationName, d.Location } into g
                            select new PayrollAccountingEntryResultDto
                            {
                                LocationName = g.Key.LocationName,
                                CntEmp = g.Count(),
                                TotalBasicSalary = g.Sum(x => x.Salary),
                                TotalFixedAllowance = (
                                    from a in context.HrPayrollAllowanceVws
                                    where a.IsDeleted == false && a.FixedOrTemporary == 1
                                        && a.Location == g.Key.Location
                                        && (string.IsNullOrEmpty(filter.MsMonth) || a.MsMonth == filter.MsMonth)
                                        && (filter.FinancelYear == 0 || a.FinancelYear == filter.FinancelYear)
                                        && (filter.PayrollTypeId == 0 || a.PayrollTypeId == filter.PayrollTypeId)
                                        && (filter.FacilityId == 0 || a.FacilityId == filter.FacilityId)
                                        && (filter.BranchId == 0 || a.BranchId == filter.BranchId)
                                        && (string.IsNullOrEmpty(filter.BranchsId) || branches.Contains(a.BranchId.ToString()))

                                    select a.Amount).DefaultIfEmpty().Sum(),
                                TotalOtherAllowance = (
                                    from a in context.HrPayrollAllowanceVws
                                    where a.IsDeleted == false && a.FixedOrTemporary == 2
                                        && a.Location == g.Key.Location
                                        && (string.IsNullOrEmpty(filter.MsMonth) || a.MsMonth == filter.MsMonth)
                                        && (filter.FinancelYear == 0 || a.FinancelYear == filter.FinancelYear)
                                        && (filter.PayrollTypeId == 0 || a.PayrollTypeId == filter.PayrollTypeId)
                                        && (filter.FacilityId == 0 || a.FacilityId == filter.FacilityId)
                                        && (filter.BranchId == 0 || a.BranchId == filter.BranchId)
                                    select a.Amount).DefaultIfEmpty().Sum(),
                                TotalOverTime = g.Sum(x => x.OverTime),
                                TotalSalary = g.Sum(x => x.Salary + x.Allowance + x.OverTime),
                                TotalLoan = g.Sum(x => x.Loan),
                                TotalOtherDeduction = g.Sum(x => x.Deduction + x.Absence + x.Delay + x.Penalties),
                                TotalNet = g.Sum(x => x.Net)
                            };


                return query.ToList();
            }
            if (type == 2)
            {
                var query = from d in context.HrPayrollDVw
                            where !d.IsDeleted &&
                                  (string.IsNullOrEmpty(filter.MsMonth) || d.MsMonth == filter.MsMonth) &&
                                  (filter.FinancelYear == 0 || d.FinancelYear == filter.FinancelYear) &&
                                  (filter.PayrollTypeId == 0 || d.PayrollTypeId == filter.PayrollTypeId) &&
                                  (filter.FacilityId == 0 || d.FacilityId == filter.FacilityId) &&
                                  (filter.BranchId == 0 || d.BranchId == filter.BranchId) &&
                                  (string.IsNullOrEmpty(filter.BranchsId) || branches.Contains(d.BranchId.ToString()))
                            group d by new { d.DepName, d.DeptId } into g
                            select new PayrollAccountingEntryResultDto
                            {
                                DeptName = g.Key.DepName,
                                DeptId = g.Key.DeptId,
                                CntEmp = g.Count(),
                                TotalBasicSalary = g.Sum(x => x.Salary),
                                TotalFixedAllowance = (
                                    from a in context.HrPayrollAllowanceVws
                                    where !a.IsDeleted && a.FixedOrTemporary == 1 &&
                                          a.DeptId == g.Key.DeptId &&
                                          (string.IsNullOrEmpty(filter.MsMonth) || a.MsMonth == filter.MsMonth) &&
                                          (filter.FinancelYear == 0 || a.FinancelYear == filter.FinancelYear) &&
                                          (filter.PayrollTypeId == 0 || a.PayrollTypeId == filter.PayrollTypeId) &&
                                          (filter.FacilityId == 0 || a.FacilityId == filter.FacilityId) &&
                                          (filter.BranchId == 0 || a.BranchId == filter.BranchId) &&
                                          (string.IsNullOrEmpty(filter.BranchsId) || branches.Contains(a.BranchId.ToString()))
                                    select (decimal?)a.Amount ?? 0
                                ).DefaultIfEmpty().Sum(),
                                TotalOtherAllowance = (
                                    from a in context.HrPayrollAllowanceVws
                                    where !a.IsDeleted && a.FixedOrTemporary == 2 &&
                                          a.DeptId == g.Key.DeptId &&
                                          (string.IsNullOrEmpty(filter.MsMonth) || a.MsMonth == filter.MsMonth) &&
                                          (filter.FinancelYear == 0 || a.FinancelYear == filter.FinancelYear) &&
                                          (filter.PayrollTypeId == 0 || a.PayrollTypeId == filter.PayrollTypeId) &&
                                          (filter.FacilityId == 0 || a.FacilityId == filter.FacilityId) &&
                                          (filter.BranchId == 0 || a.BranchId == filter.BranchId) &&
                                          (string.IsNullOrEmpty(filter.BranchsId) || branches.Contains(a.BranchId.ToString()))
                                    select (decimal?)a.Amount ?? 0
                                ).DefaultIfEmpty().Sum(),
                                TotalOverTime = g.Sum(x => x.OverTime),
                                TotalSalary = g.Sum(x => x.Salary + x.Allowance + x.OverTime),
                                TotalLoan = g.Sum(x => x.Loan),
                                TotalOtherDeduction = g.Sum(x => x.Deduction + x.Absence + x.Delay + x.Penalties),
                                TotalNet = g.Sum(x => x.Net)
                            };
                return query.ToList();

            }



            if (type == 4)
            {
                var query = from d in context.HrPayrollDVw
                            where !d.IsDeleted &&
                                  (string.IsNullOrEmpty(filter.MsMonth) || d.MsMonth == filter.MsMonth) &&
                                  (filter.FinancelYear == 0 || d.FinancelYear == filter.FinancelYear) &&
                                  (filter.PayrollTypeId == 0 || d.PayrollTypeId == filter.PayrollTypeId) &&
                                  (filter.FacilityId == 0 || d.FacilityId == filter.FacilityId) &&
                                  (filter.BranchId == 0 || d.BranchId == filter.BranchId) &&
                                  (string.IsNullOrEmpty(filter.BranchsId) || branches.Contains(d.BranchId.ToString()))
                            group d by new { d.BraName, d.BranchId } into g
                            select new PayrollAccountingEntryResultDto
                            {
                                BranchName = g.Key.BraName,
                                BranchId = g.Key.BranchId,
                                CntEmp = g.Count(),
                                TotalBasicSalary = g.Sum(x => x.Salary),
                                TotalFixedAllowance = (
                                    from a in context.HrPayrollAllowanceVws
                                    where !a.IsDeleted && a.FixedOrTemporary == 1 &&
                                          a.BranchId == g.Key.BranchId &&
                                          (string.IsNullOrEmpty(filter.MsMonth) || a.MsMonth == filter.MsMonth) &&
                                          (filter.FinancelYear == 0 || a.FinancelYear == filter.FinancelYear) &&
                                          (filter.PayrollTypeId == 0 || a.PayrollTypeId == filter.PayrollTypeId) &&
                                          (filter.FacilityId == 0 || a.FacilityId == filter.FacilityId) &&
                                          (filter.BranchId == 0 || a.BranchId == filter.BranchId) &&
                                          (string.IsNullOrEmpty(filter.BranchsId) || branches.Contains(a.BranchId.ToString()))
                                    select (decimal?)a.Amount ?? 0
                                ).DefaultIfEmpty().Sum(),
                                TotalOtherAllowance = (
                                    from a in context.HrPayrollAllowanceVws
                                    where !a.IsDeleted && a.FixedOrTemporary == 2 &&
                                          a.BranchId == g.Key.BranchId &&
                                          (string.IsNullOrEmpty(filter.MsMonth) || a.MsMonth == filter.MsMonth) &&
                                          (filter.FinancelYear == 0 || a.FinancelYear == filter.FinancelYear) &&
                                          (filter.PayrollTypeId == 0 || a.PayrollTypeId == filter.PayrollTypeId) &&
                                          (filter.FacilityId == 0 || a.FacilityId == filter.FacilityId) &&
                                          (filter.BranchId == 0 || a.BranchId == filter.BranchId) &&
                                          (string.IsNullOrEmpty(filter.BranchsId) || branches.Contains(a.BranchId.ToString()))
                                    select (decimal?)a.Amount ?? 0
                                ).DefaultIfEmpty().Sum(),
                                TotalOverTime = g.Sum(x => x.OverTime),
                                TotalSalary = g.Sum(x => x.Salary + x.Allowance + x.OverTime),
                                TotalLoan = g.Sum(x => x.Loan),
                                TotalOtherDeduction = g.Sum(x => x.Deduction + x.Absence + x.Delay + x.Penalties),
                                TotalNet = g.Sum(x => x.Net)
                            };
                return query.ToList();


            }
            return null;
        }


        public async Task<List<HrPayrollCompareResult>> PayrollCompare(HrPayrollCompareFilterDto filter, int CmdType)
        {
            var branches = session.Branches.Split(",");

            filter.DeptId ??= 0;
            filter.Location ??= 0;
            filter.PayrollType ??= 0;
            filter.BranchId ??= 0;
            if (CmdType == 1)
            {

                var pervMonth = filter.PreviousMonth.PadLeft(2, '0');
                var curMonth = filter.CurrentMonth.PadLeft(2, '0');

                var baseQuery = context.HrPayrollDVw
                    .Where(x => x.IsDeleted == false
                                && x.FacilityId == session.FacilityId
                                && (filter.BranchId == 0 || x.BranchId == filter.BranchId)
                                && (branches == null || branches.Contains(x.BranchId.ToString()))
                                && x.FinancelYear == filter.FinancialYear
                                && (filter.DeptId == 0 || x.DeptId == filter.DeptId)
                                && (filter.Location == 0 || x.Location == filter.Location)
                                && (filter.PayrollType == 0 || x.PaymentTypeId == filter.PayrollType)
                                && (x.MsMonth == pervMonth || x.MsMonth == curMonth))
                    .AsEnumerable();

                var grouped = baseQuery
                    .GroupBy(x => new { x.DeptId, x.Location, x.DepName, x.LocationName, x.FinancelYear })
                    .Select(g =>
                    {
                        var deptId = g.Key.DeptId;
                        var locId = g.Key.Location;

                        var pervCount = context.HrPayrollDVw.Count(x =>
                            x.MsMonth == pervMonth && !x.IsDeleted
                            && x.FinancelYear == filter.FinancialYear
                            && (filter.DeptId == 0 || x.DeptId == filter.DeptId)
                            && (filter.Location == 0 || x.Location == filter.Location)
                            && (filter.PayrollType == 0 || x.PaymentTypeId == filter.PayrollType)
                            && x.FacilityId == session.FacilityId
                            && (filter.BranchId == 0 || x.BranchId == filter.BranchId)
                            && (branches == null || branches.Contains(x.BranchId.ToString()))
                            && x.DeptId == deptId && x.Location == locId);

                        var curCount = context.HrPayrollDVw.Count(x =>
                            x.MsMonth == curMonth && !x.IsDeleted
                            && x.FinancelYear == filter.FinancialYear
                            && (filter.DeptId == 0 || x.DeptId == filter.DeptId)
                            && (filter.Location == 0 || x.Location == filter.Location)
                            && (filter.PayrollType == 0 || x.PaymentTypeId == filter.PayrollType)
                            && x.FacilityId == session.FacilityId
                            && (filter.BranchId == 0 || x.BranchId == filter.BranchId)
                            && (branches == null || branches.Contains(x.BranchId.ToString()))
                            && x.DeptId == deptId && x.Location == locId);

                        return new HrPayrollCompareResult
                        {
                            DepName = g.Key.DepName,
                            LocationName = g.Key.LocationName,
                            FinancelYear = (int)g.Key.FinancelYear,
                            PervMonth = pervCount,
                            CurMonth = curCount,
                            Difference = pervCount - curCount
                        };
                    })
                    .ToList();
                return grouped;
            }

            if (CmdType == 2)
            {
                //الموظفين الموجودين في الشهر السابق وغير موجودين في الشهر الحالي

                var empId = context.HrPayrollDVw
                    .Where(x => x.MsMonth == filter.CurrentMonth && x.IsDeleted == false)
                    .Select(x => x.EmpId);
                var prevMonthEmployees = context.HrPayrollDVw
                    .Where(x =>
                        x.MsMonth == filter.PreviousMonth
                        && x.IsDeleted == false
                        && !empId.Contains(x.EmpId)
                        && x.FinancelYear == filter.FinancialYear
                        && x.FacilityId == session.FacilityId
                        && (filter.DeptId == 0 || x.DeptId == filter.DeptId)
                        && (filter.Location == 0 || x.Location == filter.Location)
                        && (filter.BranchId == 0 || x.BranchId == filter.BranchId)
                        && (filter.PayrollType == 0 || x.PaymentTypeId == filter.PayrollType)
                        && (branches == null || branches.Contains(x.BranchId.ToString()))
                    )
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
                return prevMonthEmployees;
            }
            if (CmdType == 3)
            {
                //--الموظفين الموجودين في الشهر الحالي وغير موجودين في الشهر السابق

                var empId = context.HrPayrollDVw
                    .Where(x => x.MsMonth == filter.PreviousMonth && x.IsDeleted == false)
                    .Select(x => x.EmpId).ToHashSet();
                var curMonthEmployees = context.HrPayrollDVw
                    .Where(x =>
                        x.MsMonth == filter.CurrentMonth
                        && x.IsDeleted == false
                        && !empId.Contains(x.EmpId)
                        && x.FinancelYear == filter.FinancialYear
                        && x.FacilityId == session.FacilityId
                        && (filter.DeptId == 0 || x.DeptId == filter.DeptId)
                        && (filter.Location == 0 || x.Location == filter.Location)
                        && (filter.BranchId == 0 || x.BranchId == filter.BranchId)
                        && (filter.PayrollType == 0 || x.PaymentTypeId == filter.PayrollType)
                        && (branches == null || branches.Contains(x.BranchId.ToString()))
                    )
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
                return curMonthEmployees;
            }
            if (CmdType == 4)
            {

                var result = (from presentMonth in context.HrPayrollDVw
                              let previousMonthNet = (from previousMonth in context.HrPayrollDVw
                                                      where previousMonth.MsMonth == filter.PreviousMonth &&
                                                            previousMonth.FinancelYear == filter.FinancialYear &&
                                                            previousMonth.IsDeleted == false &&
                                                            previousMonth.FacilityId == session.FacilityId &&
                                                            (filter.BranchId == 0 || previousMonth.BranchId == filter.BranchId) &&
                                                            (filter.DeptId == 0 || previousMonth.DeptId == filter.DeptId) &&
                                                            (filter.PayrollType == 0 || previousMonth.PayrollTypeId == filter.PayrollType) &&
                                                            (branches.Length == 0 || branches.Contains(previousMonth.BranchId.ToString())) &&
                                                            previousMonth.EmpId == presentMonth.EmpId
                                                      select (decimal?)previousMonth.Net).FirstOrDefault() ?? 0
                              where presentMonth.MsMonth == filter.CurrentMonth &&
                                    presentMonth.IsDeleted == false &&
                                    presentMonth.FacilityId == session.FacilityId &&
                                    (filter.BranchId == 0 || presentMonth.BranchId == filter.BranchId) &&
                                    (filter.DeptId == 0 || presentMonth.DeptId == filter.DeptId) &&
                                    (filter.Location == 0 || presentMonth.Location == filter.Location) &&
                                    (filter.PayrollType == 0 || presentMonth.PayrollTypeId == filter.PayrollType) &&
                                    presentMonth.FinancelYear == filter.FinancialYear &&
                                    (branches.Length == 0 || branches.Contains(presentMonth.BranchId.ToString()))
                              select new
                              {
                                  presentMonth.EmpId,
                                  presentMonth.DepName,
                                  presentMonth.LocationName,
                                  presentMonth.EmpCode,
                                  presentMonth.EmpName,
                                  presentMonth.FinancelYear,
                                  presentMonth.MsMonth,
                                  PresentMonthNet = presentMonth.Net,
                                  PreviousMonthNet = previousMonthNet
                              }).Where(x => x.PresentMonthNet != x.PreviousMonthNet)
                              .Select(x => new HrPayrollCompareResult
                              {
                                  EmpId = x.EmpId,
                                  DepName = x.DepName ?? "",
                                  LocationName = x.LocationName ?? "",
                                  empCode = x.EmpCode,
                                  empName = x.EmpName,
                                  FinancelYear = (int)x.FinancelYear,
                                  MsMonth = x.MsMonth,
                                  PresentMonthNet = x.PresentMonthNet,
                                  PreviousMonthNet = x.PreviousMonthNet
                              }).ToList();

                return result;

            }
            if (CmdType == 5)
            {
                var curMonthQuery = from main in context.HrPayrollDVw
                                    where main.IsDeleted == false &&
                                          (filter.CurrentMonth == "00" || filter.CurrentMonth == "0" || main.MsMonth == filter.CurrentMonth) &&
                                          (filter.FinancialYear == 0 || main.FinancelYear == filter.FinancialYear) &&
                                          main.BranchId == filter.BranchId &&
                                          main.FacilityId == session.FacilityId
                                    group main by new { main.BraName, main.MonthName, main.BranchId } into g
                                    select new HrPayrollCompareResult
                                    {
                                        BranchName = g.Key.BraName,
                                        MonthName = g.Key.MonthName,
                                        CntEmp = g.Count(),
                                        TotalBasicSalary = g.Sum(x => x.Salary),
                                        TotalFixedAllowance = context.HrPayrollAllowanceVws
                                            .Where(a => !a.IsDeleted && a.FixedOrTemporary == 1 &&
                                                        a.BranchId == g.Key.BranchId &&
                                                        (filter.CurrentMonth == "00" || filter.CurrentMonth == "0" || a.MsMonth == filter.CurrentMonth) &&
                                                        (filter.FinancialYear == 0 || a.FinancelYear == filter.FinancialYear))
                                            .Sum(a => (decimal?)a.Amount) ?? 0,
                                        TotalOtherAllowance = context.HrPayrollAllowanceVws
                                            .Where(a => !a.IsDeleted && a.FixedOrTemporary == 2 &&
                                                        a.BranchId == g.Key.BranchId &&
                                                        (filter.CurrentMonth == "00" || filter.CurrentMonth == "0" || a.MsMonth == filter.CurrentMonth) &&
                                                        (filter.FinancialYear == 0 || a.FinancelYear == filter.FinancialYear))
                                            .Sum(a => (decimal?)a.Amount) ?? 0,
                                        TotalOverTime = g.Sum(x => x.OverTime),
                                        TotalSalary = g.Sum(x => x.Salary + x.Allowance + x.OverTime),
                                        TotalLoan = g.Sum(x => x.Loan),
                                        TotalOtherDeduction = g.Sum(x => x.Deduction + x.Absence + x.Delay + x.Penalties),
                                        TotalNet = g.Sum(x => x.Net)
                                    };

                var prevMonthQuery = from main in context.HrPayrollDVw
                                     where main.IsDeleted == false &&
                                           (filter.PreviousMonth == "00" || filter.PreviousMonth == "0" || main.MsMonth == filter.PreviousMonth) &&
                                           (filter.FinancialYear == 0 || main.FinancelYear == filter.FinancialYear) &&
                                           main.BranchId == filter.BranchId &&
                                           main.FacilityId == session.FacilityId
                                     group main by new { main.BraName, main.MonthName, main.BranchId } into g
                                     select new HrPayrollCompareResult
                                     {
                                         BranchName = g.Key.BraName,
                                         MonthName = g.Key.MonthName,
                                         CntEmp = g.Count(),
                                         TotalBasicSalary = g.Sum(x => x.Salary),
                                         TotalFixedAllowance = context.HrPayrollAllowanceVws
                                            .Where(a => !a.IsDeleted && a.FixedOrTemporary == 1 &&
                                                        a.BranchId == g.Key.BranchId &&
                                                        (filter.PreviousMonth == "00" || filter.PreviousMonth == "0" || a.MsMonth == filter.PreviousMonth) &&
                                                        (filter.FinancialYear == 0 || a.FinancelYear == filter.FinancialYear))
                                            .Sum(a => (decimal?)a.Amount) ?? 0,
                                         TotalOtherAllowance = context.HrPayrollAllowanceVws
                                            .Where(a => !a.IsDeleted && a.FixedOrTemporary == 2 &&
                                                        a.BranchId == g.Key.BranchId &&
                                                        (filter.PreviousMonth == "00" || filter.PreviousMonth == "0" || a.MsMonth == filter.PreviousMonth) &&
                                                        (filter.FinancialYear == 0 || a.FinancelYear == filter.FinancialYear))
                                            .Sum(a => (decimal?)a.Amount) ?? 0,
                                         TotalOverTime = g.Sum(x => x.OverTime),
                                         TotalSalary = g.Sum(x => x.Salary + x.Allowance + x.OverTime),
                                         TotalLoan = g.Sum(x => x.Loan),
                                         TotalOtherDeduction = g.Sum(x => x.Deduction + x.Absence + x.Delay + x.Penalties),
                                         TotalNet = g.Sum(x => x.Net)
                                     };

                var unionQuery = curMonthQuery.Union(prevMonthQuery);

                var orderedQuery = unionQuery.OrderBy(x => x.MonthName);

                return orderedQuery.ToList();
                //            var baseQuery = context.HrPayrollDVws.Where(main =>
                //main.IsDeleted == false &&
                //main.FacilityId == session.FacilityId &&
                //main.BranchId == filter.BranchId &&
                //filter.FinancialYear == 0 || main.FinancelYear == filter.FinancialYear);

                //            var curMonthQuery = baseQuery
                //                .Where(main => filter.CurrentMonth == "00" || filter.CurrentMonth == "0" || main.MsMonth == filter.CurrentMonth)
                //                .GroupBy(main => new { main.BraName, main.MonthName, main.BranchId })
                //                .Select(g => new HrPayrollCompareResult
                //                {
                //                    BranchName = g.Key.BraName,
                //                    MonthName = g.Key.MonthName,
                //                    CntEmp = g.Count(),
                //                    TotalBasicSalary = g.Sum(x => x.Salary),
                //                    TotalFixedAllowance = context.HrPayrollAllowanceVws
                //                        .Where(a => a.IsDeleted == false &&
                //                                    a.FixedOrTemporary == 1 &&
                //                                    a.BranchId == g.Key.BranchId &&
                //                                    (filter.CurrentMonth == "00" || filter.CurrentMonth == "0" || a.MsMonth == filter.CurrentMonth) &&
                //                                    (filter.FinancialYear == 0 || a.FinancelYear == filter.FinancialYear))
                //                        .Sum(a => (decimal?)a.Amount) ?? 0,
                //                    TotalOtherAllowance = context.HrPayrollAllowanceVws
                //                        .Where(a => a.IsDeleted == false &&
                //                                    a.FixedOrTemporary == 2 &&
                //                                    a.BranchId == g.Key.BranchId &&
                //                                    (filter.CurrentMonth == "00" || filter.CurrentMonth == "0" || a.MsMonth == filter.CurrentMonth) &&
                //                                    (filter.FinancialYear == 0 || a.FinancelYear == filter.FinancialYear))
                //                        .Sum(a => (decimal?)a.Amount) ?? 0,
                //                    TotalOverTime = g.Sum(x => x.OverTime),
                //                    TotalSalary = g.Sum(x => x.Salary + x.Allowance + x.OverTime),
                //                    TotalLoan = g.Sum(x => x.Loan),
                //                    TotalOtherDeduction = g.Sum(x => x.Deduction + x.Absence + x.Delay + x.Penalties),
                //                    TotalNet = g.Sum(x => x.Net)
                //                });

                //            var prevMonthQuery = baseQuery
                //                .Where(main => filter.PreviousMonth == "00" || filter.PreviousMonth == "0" || main.MsMonth == filter.PreviousMonth)
                //                .GroupBy(main => new { main.BraName, main.MonthName, main.BranchId })
                //                .Select(g => new HrPayrollCompareResult
                //                {
                //                    BranchName = g.Key.BraName,
                //                    MonthName = g.Key.MonthName,
                //                    CntEmp = g.Count(),
                //                    TotalBasicSalary = g.Sum(x => x.Salary),
                //                    TotalFixedAllowance = context.HrPayrollAllowanceVws
                //                        .Where(a => a.IsDeleted == false &&
                //                                    a.FixedOrTemporary == 1 &&
                //                                    a.BranchId == g.Key.BranchId &&
                //                                    (filter.PreviousMonth == "00" || filter.PreviousMonth == "0" || a.MsMonth == filter.PreviousMonth) &&
                //                                    (filter.FinancialYear == 0 || a.FinancelYear == filter.FinancialYear))
                //                        .Sum(a => (decimal?)a.Amount) ?? 0,
                //                    TotalOtherAllowance = context.HrPayrollAllowanceVws
                //                        .Where(a => a.IsDeleted == false &&
                //                                    a.FixedOrTemporary == 2 &&
                //                                    a.BranchId == g.Key.BranchId &&
                //                                    (filter.PreviousMonth == "00" || filter.PreviousMonth == "0" || a.MsMonth == filter.PreviousMonth) &&
                //                                    (filter.FinancialYear == 0 || a.FinancelYear == filter.FinancialYear))
                //                        .Sum(a => (decimal?)a.Amount) ?? 0,
                //                    TotalOverTime = g.Sum(x => x.OverTime),
                //                    TotalSalary = g.Sum(x => x.Salary + x.Allowance + x.OverTime),
                //                    TotalLoan = g.Sum(x => x.Loan),
                //                    TotalOtherDeduction = g.Sum(x => x.Deduction + x.Absence + x.Delay + x.Penalties),
                //                    TotalNet = g.Sum(x => x.Net)
                //                });

                //            var unionQuery = curMonthQuery.Union(prevMonthQuery);
                //            var orderedQuery = unionQuery.OrderBy(x => x.MonthName);
                //            return orderedQuery.ToList();


            }

            return null;
        }


        public async Task<int> Check_Emp_Exists_In_Payroll(string msDate, long payrollTypeId, long empId)
        {
            // صيغة التاريخ المتوقعة
            string format = "yyyy/MM/dd";
            var culture = CultureInfo.InvariantCulture;

            // نحاول نحول تاريخ الإدخال
            if (!DateTime.TryParseExact(msDate, format, culture, DateTimeStyles.None, out var msDateParsed))
                return 0; // لو التاريخ غير صالح نرجع صفر

            var data = await context.HrPayrollDVw
                .Where(e => e.IsDeleted == false
                            && e.PayrollTypeId == payrollTypeId
                            && e.EmpId == empId)
                .ToListAsync(); // ننفذ الاستعلام ونجيب البيانات

            // بعدين نحول النصوص إلى تاريخ بنفس الصيغة ونقارن
            var count = data.Count(e =>
            {
                if (DateTime.TryParseExact(e.StartDate, format, culture, DateTimeStyles.None, out var startDate) &&
                    DateTime.TryParseExact(e.EndDate, format, culture, DateTimeStyles.None, out var endDate))
                {
                    return msDateParsed >= startDate && msDateParsed <= endDate;
                }
                return false;
            });

            return count;
        }


    }

}
