using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Data;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrSalaryGroupAccountRepository : GenericRepository<HrSalaryGroupAccount>, IHrSalaryGroupAccountRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentData session;
        public HrSalaryGroupAccountRepository(ApplicationDbContext context, ICurrentData session) : base(context)
        {
            this.context = context;
            this.session = session;
        }
       public async Task<IResult<CheckSalaryGroupDto>> CheckSalaryGroup(long msid)
        {
            CheckSalaryGroupDto result = new CheckSalaryGroupDto();
            var BranchesList = session.Branches.Split(',');
            bool Chachallowance = true;
            bool ChachDeduction = true;

            string? MsgError = "";
            int MsgErrorAccount = 0;
            int MsgErrorAllowance = 0;
            int MsgErrorDeduction = 0;
            var getPayrollDitems =await context.HrPayrollDVw.Where(x => BranchesList.Contains(x.BranchId.ToString()) && x.IsDeleted == false && x.MsId == msid).ToListAsync();
            var getSalaryGroupsAccount = context.HrSalaryGroupAccounts.Where(x => x.IsDeleted == false).ToList();

            foreach (var item in getPayrollDitems)
            {
                if (string.IsNullOrEmpty(item.SalaryGroupId.ToString()) || Convert.ToInt32(item.SalaryGroupId) <= 0)
                {
                    result.check = false;
                    MsgError += item.EmpCode + ",";
                }
                //=======================================التشيك على حسابات المجموعات

                if (Convert.ToInt32(item.SalaryGroupId) > 0)
                {
                    //======================================== التشيك على الحسابات الرئيسية

                    var getHrSalaryGroup = context.HrSalaryGroupVws.Where(x => x.Id == item.SalaryGroupId).FirstOrDefault();
                    //==================== حساب مصروف الراتب

                    if (getHrSalaryGroup?.AccountSalaryId == 0)
                    {
                        if (item.Salary > 0)
                        {
                            MsgErrorAccount++;
                        }
                    }

                    //==================== حساب الرواتب المستحقة

                    if (getHrSalaryGroup?.AccountDueSalaryId == 0)
                    {
                        if (item.Salary > 0)
                        {
                            MsgErrorAccount++;
                        }
                    }

                    // ==================== حساب السلف
                    if (getHrSalaryGroup?.AccountLoanId == 0)
                    {
                        if (item.Loan > 0)
                        {
                            MsgErrorAccount++;
                        }
                    }
                    // ==================== حساب مصروف البدلات
                    if (getHrSalaryGroup?.AccountAllowancesId == 0)
                    {
                        if (item.Allowance + item.Commission + item.OverTime > 0)
                        {
                            MsgErrorAccount++;
                        }
                    }

                    // ==================== حساب مصروف الإضافي
                    if (getHrSalaryGroup?.AccountOverTimeId == 0)
                    {
                        if (item.OverTime > 0)
                        {
                            MsgErrorAccount++;
                        }
                    }
                    //==================== حساب إيرادات الحسمبات 
                    if (getHrSalaryGroup?.AccountDeductionId == 0)
                    {
                        if (item.Absence + item.Delay + item.Loan + item.Deduction + item.Penalties > 0)
                        {
                            MsgErrorAccount++;
                        }
                    }

                    //============================================== نهاية تشيك الحسابات الرئيسية


                    //============================================== ,,,,الحسابات الفرعية البدلات  الحسميات
                    var getAllowanceDeduction =await context.HrPayrollAllowanceVws.Where(x => x.MsdId == msid && x.IsDeleted == false && x.Credit > 0).ToListAsync();
                    if (getAllowanceDeduction != null)
                    {
                        foreach (var allowanceItem in getAllowanceDeduction)
                        {
                            Chachallowance = false;

                            var SalaryGroupsAccount = getSalaryGroupsAccount.Where(x => x.TypeId == 1 && x.GroupId == item.SalaryGroupId);

                            foreach (var SingleSalaryGroupsAccount in SalaryGroupsAccount)
                            {
                                if (SingleSalaryGroupsAccount.AdId == allowanceItem.AdId)
                                {
                                    Chachallowance = true;

                                }
                            }
                            if (Chachallowance == false)
                            {
                                MsgErrorAllowance += 1;
                            }

                        }

                        foreach (var DeductionItem in getAllowanceDeduction)
                        {
                            ChachDeduction = false;

                            var SalaryGroupsAccount = getSalaryGroupsAccount.Where(x => x.TypeId == 2 && x.GroupId == item.SalaryGroupId);

                            foreach (var SingleSalaryGroupsAccount in SalaryGroupsAccount)
                            {
                                if (SingleSalaryGroupsAccount.AdId == DeductionItem.AdId)
                                {
                                    ChachDeduction = true;

                                }
                            }
                            if (ChachDeduction == false)
                            {
                                MsgErrorDeduction += 1;
                            }

                        }

                    }

                }
            }
            if (!string.IsNullOrEmpty(MsgError))
            {
                MsgError = MsgError.Substring(0, MsgError.Length - 1);
                result.Message += " لا يمكن إنشاء قيد الرواتب  فضلاً تأكد من مجموعات الراواتب للموظفين اولاً" + "<br>(" + msid + ")";
            }

            if (MsgErrorAccount > 0)
            {
                result.check = false;
                result.Message += " لا يمكن إنشاء قيد الرواتب  فضلاً تأكد من الحسابات الرئيسية في مجموعات الرواتب  ";
            }

            if (MsgErrorAllowance > 0)
            {
                result.check = false;
                result.Message += " لا يمكن إنشاء قيد الرواتب  فضلاً تأكد من الحسابات الفرعية(البدلات) في مجموعات الرواتب  ";
            }

            if (MsgErrorDeduction > 0)
            {
                result.check = false;
                result.Message += " لا يمكن إنشاء قيد الرواتب  فضلاً تأكد من الحسابات الفرعية(الحسميات) في مجموعات الرواتب  ";
            }
            return await Result<CheckSalaryGroupDto>.SuccessAsync(result);

        }
    }
}
