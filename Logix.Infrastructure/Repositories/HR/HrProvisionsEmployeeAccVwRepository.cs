using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrProvisionsEmployeeAccVwRepository : GenericRepository<HrProvisionsEmployeeAccVw>, IHrProvisionsEmployeeAccVwRepository
    {
        private readonly ApplicationDbContext context;

        public HrProvisionsEmployeeAccVwRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <param name="type">
        /// //if type=3 =====>>>>>  مخصص الاجازة
        /// if type=2 =====>>>>>  مخصص التذاكر      
        /// if type=1 =====>>>>>  مخصص نهاية الخدمة      
        /// </param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ProvisionsAccDto>> GetAccDetailsAsync(long id, int type)
        {
            try
            {

                var description = "";
                // Fetch provision from HR_Provisions
                var provision = await context.HrProvisions
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
                if (provision == null)
                {
                    throw new KeyNotFoundException("Provision not found");
                }
                // Get employee provision records from HR_Provisions_Employee_Acc_VW view
                var employeeAccRecords = await context.HrProvisionsEmployeeAccVws
                    .Where(e => e.Id == id && e.SalaryGroupId != 0)
                    .ToListAsync();

                if (!employeeAccRecords.Any())
                {
                    throw new KeyNotFoundException("No employee provision records found");
                }

                var resultList = new List<ProvisionsAccDto>();

                foreach (var employeeAcc in employeeAccRecords)
                {
                    long? DebitAccountID = 0;
                    long? CreditAccountID = 0;
                    int? CreditReferenceTypeID = 0;
                    int? DebitReferenceTypeID = 0;
                    // Fetch related salary group details
                    var salaryGroup = await context.HrSalaryGroups
                        .FirstOrDefaultAsync(sg => sg.Id == employeeAcc.SalaryGroupId && sg.IsDeleted == false);

                    if (salaryGroup == null)
                    {
                        throw new KeyNotFoundException("Salary group not found");
                    }
                    switch (type)
                    {
                        case 3:
                            description = $"قيد استحقاق مخصص الاجازة لشهر للعام {provision.FinYear}";
                            if (salaryGroup.AccountVacationSalaryId == null || salaryGroup.AccountVacationSalaryId <= 0)
                                throw new KeyNotFoundException($"يجب مراجعة حساب مجموعة الرواتب رقم  {employeeAcc.SalaryGroupId}");
                            if (salaryGroup.AccountDueVacationId == null || salaryGroup.AccountDueVacationId <= 0)
                                throw new KeyNotFoundException($"يجب مراجعة حساب مخصص اجازات لمجموعة الرواتب رقم  {employeeAcc.SalaryGroupId}");
                            CreditAccountID = salaryGroup.AccountDueVacationId;
                            DebitAccountID = salaryGroup.AccountVacationSalaryId;
                            CreditReferenceTypeID = 16;
                            DebitReferenceTypeID = 1;
                            break;

                        case 2:
                            description = $"قيد استحقاق مخصص  التذاكر للعام  {provision.FinYear}";
                            if (salaryGroup.AccountTicketsId == null || salaryGroup.AccountTicketsId <= 0)
                                throw new KeyNotFoundException($"يجب مراجعة حساب  تذاكر لمجموعة الرواتب رقم  {employeeAcc.SalaryGroupId}");
                            if (salaryGroup.AccountDueTicketsId == null || salaryGroup.AccountDueTicketsId <= 0)
                                throw new KeyNotFoundException($"يجب مراجعة حساب مخصص تذاكر لمجموعة الرواتب رقم  {employeeAcc.SalaryGroupId}");
                            CreditAccountID = salaryGroup.AccountDueTicketsId;
                            DebitAccountID = salaryGroup.AccountTicketsId;
                            CreditReferenceTypeID = 1;
                            DebitReferenceTypeID = 1;
                            break;
                        case 1:
                            description = $"قيد استحقاق مخصص نهاية الخدمة للعام  {provision.FinYear}";
                            if (salaryGroup.AccountEndServiceId == null || salaryGroup.AccountEndServiceId <= 0)
                                throw new KeyNotFoundException($"يجب مراجعة حساب  نهاية الخدمة لمجموعة الرواتب رقم  {employeeAcc.SalaryGroupId}");
                            if (salaryGroup.AccountDueEndServiceId == null || salaryGroup.AccountDueEndServiceId <= 0)
                                throw new KeyNotFoundException($"يجب مراجعة حساب مخصص نهاية الخدمة لمجموعة الرواتب رقم  {employeeAcc.SalaryGroupId}");
                            CreditAccountID = salaryGroup.AccountDueEndServiceId;
                            DebitAccountID = salaryGroup.AccountEndServiceId;
                            CreditReferenceTypeID = 1;
                            DebitReferenceTypeID = 1;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), "Invalid provision type");
                    }



                    // First query (Debit)
                    var debitRecord = new ProvisionsAccDto
                    {
                        AccountID = DebitAccountID,
                        Credit = 0,
                        Debit = employeeAcc.Amount,
                        Description = $"{description} {employeeAcc.EmpName}",
                        CCID = employeeAcc.CcId ?? 0,
                        CCID2 = employeeAcc.CcId2 ?? 0,
                        CCID3 = employeeAcc.CcId3 ?? 0,
                        CCID4 = employeeAcc.CcId4 ?? 0,
                        CCID5 = employeeAcc.CcId5 ?? 0,
                        ReferenceNo = 0,
                        ReferenceTypeID = DebitReferenceTypeID
                    };

                    resultList.Add(debitRecord);

                    // Second query (Credit)
                    var creditRecord = new ProvisionsAccDto
                    {
                        AccountID = CreditAccountID,
                        Credit = employeeAcc.Amount,
                        Debit = 0,
                        Description = $"{description} {employeeAcc.EmpName}",
                        CCID = employeeAcc.CcId ?? 0,
                        CCID2 = employeeAcc.CcId2 ?? 0,
                        CCID3 = employeeAcc.CcId3 ?? 0,
                        CCID4 = employeeAcc.CcId4 ?? 0,
                        CCID5 = employeeAcc.CcId5 ?? 0,
                        ReferenceNo = 0,
                        ReferenceTypeID = CreditReferenceTypeID
                    };

                    resultList.Add(creditRecord);
                }

                if (!resultList.Any())
                {
                    throw new KeyNotFoundException("No result data found for the given ID");
                }

                return resultList;
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching vacation account details.", ex);
            }
        }

    }
}
