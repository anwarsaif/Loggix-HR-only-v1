using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.Repositories.HR
{
	public class HrProvisionsMedicalInsuranceEmployeeAccVwRepository : GenericRepository<HrProvisionsMedicalInsuranceEmployeeAccVw>, IHrProvisionsMedicalInsuranceEmployeeAccVwRepository
	{
        private readonly ApplicationDbContext context;

        public HrProvisionsMedicalInsuranceEmployeeAccVwRepository(ApplicationDbContext context) : base(context)
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
        public async Task<List<ProvisionsMedicalInsuranceEmployeeAccVwDto>> GetAccDetailsAsync(long id)
        {
            try
            {

                var description = "";
                // Fetch provision from HR_Provisions
                var provision = await context.HrProvisionsMedicalInsurances
					.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted==false);
                if (provision == null)
                {
                    throw new KeyNotFoundException("Provision not found");
                }
                // Get employee provision records from HR_Provisions_Employee_Acc_VW view
                var employeeAccRecords = await context.HrProvisionsMedicalInsuranceEmployeeAccVw
					.Where(e => e.Id == id && e.SalaryGroupId != 0)
                    .ToListAsync();

                if (!employeeAccRecords.Any())
                {
                    throw new KeyNotFoundException("No employee provision records found");
                }

                var resultList = new List<ProvisionsMedicalInsuranceEmployeeAccVwDto>();

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

                    // First query (Debit)
                    var debitRecord = new ProvisionsMedicalInsuranceEmployeeAccVwDto
                    {
                        AccountID = DebitAccountID,
                        Credit = 0,
                        Debit = employeeAcc.InsuranceAmount,
                        Description = $"{description} {employeeAcc.EmpName}",
                        CCID = employeeAcc.CcId ?? 0,
                        //CCID2 = employeeAcc.CcId2 ?? 0,
                        //CCID3 = employeeAcc.CcId3 ?? 0,
                        //CCID4 = employeeAcc.CcId4 ?? 0,
                        //CCID5 = employeeAcc.CcId5 ?? 0,
                        ReferenceNo = 0,
                        ReferenceTypeID = DebitReferenceTypeID
                    };

                    resultList.Add(debitRecord);

                    // Second query (Credit)
                    var creditRecord = new ProvisionsMedicalInsuranceEmployeeAccVwDto
					{
                        AccountID = CreditAccountID,
                        Credit = employeeAcc.InsuranceAmount,
                        Debit = 0,
                        Description = $"{description} {employeeAcc.EmpName}",
                        CCID = employeeAcc.CcId ?? 0,
                        //CCID2 = employeeAcc.CcId2 ?? 0,
                        //CCID3 = employeeAcc.CcId3 ?? 0,
                        //CCID4 = employeeAcc.CcId4 ?? 0,
                        //CCID5 = employeeAcc.CcId5 ?? 0,
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
