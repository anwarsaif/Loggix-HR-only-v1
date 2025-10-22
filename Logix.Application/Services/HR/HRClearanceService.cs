using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.Hr;
using Logix.Domain.HR;
using Logix.Domain.Main;
using System.Data;

namespace Logix.Application.Services.HR
{
    public class HrClearanceService : GenericQueryService<HrClearance, HrClearanceDto, HrClearanceVw>, IHrClearanceService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper mapper;
        private readonly ICurrentData session;
        private readonly IWorkflowHelper workflowHelper;

        public HrClearanceService(IQueryRepository<HrClearance> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.workflowHelper = workflowHelper;
        }

        public async Task<IResult<HrClearanceDto>> Add(HrClearanceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrClearanceDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrClearanceDto>.FailAsync($"Employee Id Is Required");
            try
            {
                // check if Emp Is Exist
                var CheckEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (CheckEmpExist == null) return await Result<HrClearanceDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.BankId = "0";
                var newItem = mapper.Map<HrClearance>(entity);
                newItem.EmpId = CheckEmpExist.Id;

                var newEntity = await hrRepositoryManager.HrClearanceRepository.AddAndReturn(newItem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = mapper.Map<HrClearanceDto>(newEntity);

                return await Result<HrClearanceDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrClearanceDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }


        public async Task<IResult<HREmpClearanceSpDto>> GetData(string EmpCode, string LastWorkingDate, int ClearanceTypeId, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');

                var CheckEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (CheckEmpExist == null) return await Result<HREmpClearanceSpDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                // نفحص هل الموظف موجود ضمن فروع الموظف الحالي 
                var CHeckEmpInBranch = await hrRepositoryManager.HrEmployeeRepository.GetOneVw(x => x.IsDeleted == false && x.Isdel == false && BranchesList.Contains(x.BranchId.ToString()) && x.Id == CheckEmpExist.Id);
                if (CHeckEmpInBranch == null) return await Result<HREmpClearanceSpDto>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));

                var getEmpClearanceData = await mainRepositoryManager.StoredProceduresRepository.HR_Emp_Clearance_Sp(CheckEmpExist.Id, LastWorkingDate);
                if (ClearanceTypeId == 2)
                {
                    getEmpClearanceData.Salary_C = 0;
                    getEmpClearanceData.Allowance_C = 0;
                    getEmpClearanceData.Housing_C = 0;
                    getEmpClearanceData.Gosi = 0;
                    getEmpClearanceData.Loan = 0;
                    getEmpClearanceData.Delay = 0;
                    getEmpClearanceData.Delay_Cnt = 0;
                    getEmpClearanceData.Absence = 0;
                    getEmpClearanceData.Absence_Cnt = 0;
                    getEmpClearanceData.Penalties = 0;
                    getEmpClearanceData.Ded_Housing = 0;
                    getEmpClearanceData.DedOhad = 0;
                    getEmpClearanceData.Count_Day_Work = 0;
                    getEmpClearanceData.Deduction_tmp = 0;
                }

                return await Result<HREmpClearanceSpDto>.SuccessAsync(getEmpClearanceData, localization.GetResource1("AddSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HREmpClearanceSpDto>.FailAsync($"{exp.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrClearanceRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrClearanceDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrClearanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrClearanceDto>.SuccessAsync(mapper.Map<HrClearanceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrClearanceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrClearanceRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrClearanceDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrClearanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrClearanceDto>.SuccessAsync(mapper.Map<HrClearanceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrClearanceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrClearanceEditDto>> Update(HrClearanceEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null) return await Result<HrClearanceEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

                var item = await hrRepositoryManager.HrClearanceRepository.GetById(entity.Id);
                if (item == null) return await Result<HrClearanceEditDto>.FailAsync($"{localization.GetMessagesResource("NoIdInUpdate")}");

                var BranchesList = session.Branches.Split(',');

                var CheckEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (CheckEmpExist == null) return await Result<HrClearanceEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                // نفحص هل الموظف موجود ضمن فروع الموظف الحالي 
                if (!BranchesList.Contains(CheckEmpExist.BranchId.ToString()))
                    return await Result<HrClearanceEditDto>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                mapper.Map(entity, item);

                item.IsDeleted = false;
                item.EmpId = CheckEmpExist.Id;
                item.DateC = entity.LeaveDate;
                item.BankId = "0";

                decimal NewAmount = 0;
                decimal Housing_allowance_Value = 0.00m;

                decimal Amount = entity.AllowanceC ?? 0;
                decimal Housing = entity.HousingC ?? 0;

                int Housing_allowance = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.HousingAllowance,
                    x => x.FacilityId == session.FacilityId) ?? 0;

                if (entity.AllowancesList != null)
                {
                    foreach (var allowance in entity.AllowancesList)
                    {
                        NewAmount += allowance.NewAmount ?? 0;
                        if (allowance.AdId == Housing_allowance)
                            Housing_allowance_Value = allowance.NewAmount ?? 0;
                    }
                }

                if ((NewAmount - Housing) != Amount)
                    return await Result<HrClearanceEditDto>.FailAsync(localization.GetHrResource("DifferenceAmounts"));

                if (Housing_allowance_Value != entity.HousingC)
                    return await Result<HrClearanceEditDto>.FailAsync(localization.GetHrResource("HousingDifferenceAmounts"));

                hrRepositoryManager.HrClearanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //UpdateHR_Allowance_Deduction
                if (entity.AllowancesList != null)
                {
                    foreach (var ad in entity.AllowancesList)
                    {
                        var updateItem = await hrRepositoryManager.HrClearanceAllowanceDeductionRepository.GetOne(x => x.Id == ad.Id
                            && x.ClearanceId == item.Id && x.EmpId == CheckEmpExist.Id && x.AdId == ad.AdId && x.TypeId == 1);
                        if (updateItem != null)
                        {
                            updateItem.NewAmount = ad.NewAmount;
                            updateItem.FixedOrTemporary = 1;
                            hrRepositoryManager.HrClearanceAllowanceDeductionRepository.Update(updateItem);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrClearanceEditDto>.SuccessAsync(mapper.Map<HrClearanceEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrClearanceEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> Add(HrClearanceAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');

                var CheckEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (CheckEmpExist == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                // نفحص هل الموظف موجود ضمن فروع الموظف الحالي 
                if (!BranchesList.Contains(CheckEmpExist.BranchId.ToString()))
                    return await Result<string>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var newClearance = mapper.Map<HrClearance>(entity);
                newClearance.EmpId = CheckEmpExist.Id;
                newClearance.DateC = entity.LeaveDate;
                newClearance.BankId = "0";

                decimal NewAmount = 0;
                decimal Housing_allowance_Value = 0.00m;

                decimal Amount = entity.AllowanceC ?? 0;
                decimal Housing = entity.HousingC ?? 0;

                int Housing_allowance = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.HousingAllowance,
                    x => x.FacilityId == session.FacilityId) ?? 0;

                if (entity.AllowancesList != null)
                {
                    foreach (var item in entity.AllowancesList)
                    {
                        NewAmount += item.NewAmount ?? 0;
                        if (item.AdId == Housing_allowance)
                            Housing_allowance_Value = item.NewAmount ?? 0;
                    }
                }

                if ((NewAmount - Housing) != Amount)
                    return await Result<string>.FailAsync(localization.GetHrResource("DifferenceAmounts"));

                if (Housing_allowance_Value != entity.HousingC)
                    return await Result<string>.FailAsync(localization.GetHrResource("HousingDifferenceAmounts"));

                var AddedEntity = await hrRepositoryManager.HrClearanceRepository.AddAndReturn(newClearance);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //InsertHR_Allowance_Deduction
                if (entity.AllowancesList != null)
                {
                    foreach (var item in entity.AllowancesList)
                    {
                        HrClearanceAllowanceDeduction newItem = new()
                        {
                            FixedOrTemporary = 1,
                            ClearanceId = AddedEntity.Id,
                            EmpId = CheckEmpExist.Id,
                            Rate = item.Rate,
                            Amount = item.Amount,
                            NewAmount = item.NewAmount,
                            TypeId = 1,
                            AdId = item.AdId,
                            IsDeleted = item.IsDeleted
                        };

                        await hrRepositoryManager.HrClearanceAllowanceDeductionRepository.Add(newItem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                if (entity.VacationId > 0)
                {
                    var vaction = await hrRepositoryManager.HrVacationsRepository.GetOne(x => x.VacationId == entity.VacationId);
                    if (vaction != null)
                    {
                        vaction.IsSalary = true;
                        hrRepositoryManager.HrVacationsRepository.Update(vaction);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                //  انشاء رصيد افتتاحي جديد
                if (entity.CreateBalance == true)
                {
                    var newVacationBalance = new HrVacationBalance
                    {
                        IsDeleted = false,
                        EmpId = CheckEmpExist.Id,
                        VBalanceRate = (entity.Remainingbalance / 2.5m),
                        VacationBalance = entity.Remainingbalance,
                        StartDate = entity.StartBalanceDate,
                        VacationTypeId = 1,
                        Note = entity.CreateBalanceNote ?? "",
                    };
                    await hrRepositoryManager.HrVacationBalanceRepository.AddAndReturn(newVacationBalance);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(AddedEntity.Id.ToString(), localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"{exp.Message}");
            }
        }

        public async Task<IResult<string>> PayrollTransfer(HrClearancePayrollTransferDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                //check
                if (entity == null) return await Result<string>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                //var HrClearanceItem = await hrRepositoryManager.HrClearanceRepository.GetById(entity.Id);
                var HrClearanceItem = await hrRepositoryManager.HrClearanceRepository.GetOneVw(x => x.Id == entity.Id);
                if (HrClearanceItem == null)
                    return await Result<string>.FailAsync($"ليس هناك تصفيه مستحقات اجازة بهذا الرقم");

                if (HrClearanceItem.PayrollId > 0)
                    return await Result<string>.FailAsync($"تم التحويل مسبقا");


                int BankId = entity.BankId ?? 0;
                string? MSGALL = "";

                int HousingAllowance = 0;
                int HousingDeduction = 0;
                int BadalatAllowance = 0;
                int OtherDeduction = 0;
                int TicketAllowance = 0;
                int VacationDueAllowance = 0;
                int GOSIDeduction = 0;
                //int LeaveBenefitsAllowance = 0;

                string AttStartDay = string.Empty;
                string AttEndDay = string.Empty;

                var GetHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                if (GetHrSetting != null)
                {
                    HousingAllowance = GetHrSetting.HousingAllowance ?? 0;
                    HousingDeduction = GetHrSetting.HousingDeduction ?? 0;
                    GOSIDeduction = GetHrSetting.GosiDeduction ?? 0;
                    BadalatAllowance = GetHrSetting.BadalatAllowance ?? 0;
                    OtherDeduction = GetHrSetting.OtherDeduction ?? 0;
                    TicketAllowance = GetHrSetting.TicketAllowance ?? 0;
                    VacationDueAllowance = GetHrSetting.VacationDueAllowance ?? 0;
                    //LeaveBenefitsAllowance = GetHrSetting.LeaveBenefitsAllowance ?? 0;

                    AttStartDay = GetHrSetting.MonthStartDay ?? "";
                    AttEndDay = GetHrSetting.MonthEndDay ?? "";
                }

                if (HousingAllowance == 0)
                    MSGALL += "لم يتم ربط بدل السكن في اعدادات الموارد البشرية فضلاً قم بعملية الربط اولاً";

                if (HousingDeduction == 0)
                    MSGALL += "لم يتم ربط حسم بدل السكن في اعدادات الموارد البشرية فضلاً قم بعملية الربط اولاً";

                if (GOSIDeduction == 0)
                    MSGALL += "لم يتم ربط حسميات التأمينات الأجتماعية في اعدادات الموارد البشرية فضلاً قم بعملية الربط اولاً";

                if (BadalatAllowance == 0)
                    MSGALL += "لم يتم ربط بدلات اخرى في اعدادات الموارد البشرية فضلاً قم بعملية الربط اولاً";

                if (OtherDeduction == 0)
                    MSGALL += "لم يتم ربط حسميات اخرى في اعدادات الموارد البشرية فضلاً قم بعملية الربط اولاً";

                if (TicketAllowance == 0)
                    MSGALL += "لم يتم ربط بدل تذاكر السفر في اعدادات الموارد البشرية فضلاً قم بعملية الربط اولاً";

                if (VacationDueAllowance == 0)
                    MSGALL += "لم يتم ربط بدل راتب الإجازة المستحقة في اعدادات الموارد البشرية فضلاً قم بعملية الربط اولاً";

                //if (LeaveBenefitsAllowance == 0)
                //    MSGALL += "لم يتم ربط بدل مكافأة نهاية الخدمة في اعدادات الموارد البشرية فضلاً قم بعملية الربط اولاً";

                if (!string.IsNullOrEmpty(MSGALL))
                    return await Result<string>.FailAsync(MSGALL);

                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                long appId = 0;
                //  ارسال الى سير العمل
                string _subject = $"{localization.GetHrResource("ClearingEmployeeLeaveEntitlements")}  {checkEmpExist.EmpName} {localization.GetHrResource("Amount")}  : {entity.Net}";
                var GetApp_ID = await workflowHelper.Send(session.UserId, 433, entity.AppTypeId, Subject: _subject, cancellationToken: cancellationToken);
                appId = GetApp_ID;

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                string FinancelYear = entity.ClearanceDate ?? "".Substring(0, 4);
                string MonthCode = entity.ClearanceDate ?? "".Substring(5, 2);
                string? AttStartDate; string? AttEndDate;
                //     من هنا يبداء تحويل البروسيجر الموجود في القاعدة
                if (AttEndDay == "29" || AttEndDay == "30" || AttEndDay == "31")
                {
                    if (MonthCode == "02")
                    {
                        DateTime firstDay = DateHelper.StringToDate($"{FinancelYear}/{MonthCode}/01");
                        AttEndDay = firstDay.AddMonths(1).AddDays(-1).Day.ToString("00");
                    }
                }

                if (AttEndDay == "31")
                {
                    if (MonthCode == "04" || MonthCode == "06" || MonthCode == "09" || MonthCode == "11")
                    {
                        DateTime firstDay = DateHelper.StringToDate($"{FinancelYear}/{MonthCode}/01");
                        AttEndDay = firstDay.AddMonths(1).AddDays(-1).Day.ToString("00");
                    }
                }

                if (AttStartDay != "01")
                {
                    if (MonthCode != "01")
                    {
                        int PrevMonth = int.Parse(MonthCode) - 1;
                        string PrevMonth_Str = PrevMonth.ToString().PadLeft(2, '0');
                        AttStartDate = FinancelYear + "/" + PrevMonth_Str + "/" + AttStartDay;
                        AttEndDate = FinancelYear + "/" + MonthCode + "/" + AttEndDay;
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(FinancelYear) - 1;
                        string PrevFinancelYear_Str = PrevFinancelYear.ToString();
                        AttStartDate = PrevFinancelYear_Str + "/12/" + AttStartDay;
                        AttEndDate = FinancelYear + "/01/" + AttEndDay;
                    }
                }
                else
                {
                    AttStartDate = FinancelYear + "/" + MonthCode + "/" + AttStartDay;
                    AttEndDate = FinancelYear + "/" + MonthCode + "/" + AttEndDay;
                }

                long MsCode = 0;
                var getMaxMsCode = await hrRepositoryManager.HrPayrollRepository.GetAll(x => x.MsCode);
                if (getMaxMsCode.Any())
                    MsCode = getMaxMsCode.Max(x => Convert.ToInt64(x)) + 1;
                else
                    MsCode = 1;

                var PayrollCls = new HrPayroll
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = session.UserId,
                    MsDate = entity.ClearanceDate,
                    MsMonth = MonthCode,
                    MsMothTxt = DateHelper.GetMonthName(Convert.ToInt32(MonthCode)),
                    MsTitle = "مسير تصفية مستحقات إجازة",
                    FinancelYear = Convert.ToInt32(FinancelYear),
                    State = 1,
                    AppId = appId,
                    PayrollTypeId = (HrClearanceItem.ClearanceType == 1) ? 1 : 5,
                    FacilityId = (int?)session.FacilityId,
                    BranchId = 0,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    MsCode = MsCode,
                };

                var newEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(PayrollCls);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                //من هنا ينتهي تحويل البروسيجر الموجود في القاعدة

                decimal allowance = 0;
                decimal Salary_C = 0;
                decimal Deduction = 0;

                if (decimal.TryParse(HrClearanceItem.SalaryC.ToString(), out Salary_C))
                    Salary_C = Math.Round(Salary_C, 2);

                if (decimal.TryParse(HrClearanceItem.TotalAllowance.ToString(), out allowance))
                    allowance = allowance - Salary_C;

                if (decimal.TryParse(HrClearanceItem.TotalDeduction.ToString(), out Deduction))
                    Deduction = Math.Round(Deduction, 2);

                int CountDayWork = 0;
                if (HrClearanceItem.ClearanceType == 1)
                    CountDayWork = (HrClearanceItem.CountDayWork ?? 0) + (HrClearanceItem.VacationAccountDay ?? 0);
                else if (HrClearanceItem.ClearanceType == 2)
                    CountDayWork = HrClearanceItem.VacationAccountDay ?? 0;

                HRPayrollDStoredProcedureDto PayrollDCls = new();
                PayrollDCls.Emp_ID = checkEmpExist.Id;
                PayrollDCls.MS_ID = newEntity.MsId;

                if (!string.IsNullOrEmpty(HrClearanceItem.Absence.ToString()))
                {
                    PayrollDCls.Absence = HrClearanceItem.Absence;
                    Deduction -= (HrClearanceItem.Absence ?? 0);
                }
                else PayrollDCls.Absence = 0;

                PayrollDCls.Allowance = allowance;

                if (!string.IsNullOrEmpty(HrClearanceItem.Delay.ToString()))
                {
                    PayrollDCls.Delay = HrClearanceItem.Delay;
                    Deduction -= (HrClearanceItem.Delay ?? 0);
                }
                else PayrollDCls.Delay = 0;

                PayrollDCls.BankId = BankId;
                PayrollDCls.Count_Day_Work = CountDayWork;
                PayrollDCls.CreatedBy = session.UserId;
                PayrollDCls.CreatedOn = DateTime.Now;
                PayrollDCls.Emp_Account_No = HrClearanceItem.AccountNo;

                if (!string.IsNullOrEmpty(HrClearanceItem.Loan.ToString()))
                {
                    PayrollDCls.Loan = HrClearanceItem.Loan;
                    Deduction -= (HrClearanceItem.Loan ?? 0);
                }
                else PayrollDCls.Loan = 0;

                if (!string.IsNullOrEmpty(HrClearanceItem.SalaryC.ToString()))
                    PayrollDCls.Salary = HrClearanceItem.SalaryC;
                else
                    PayrollDCls.Salary = 0;

                PayrollDCls.Salary_Orignal = HrClearanceItem.Salary;
                PayrollDCls.Commission = 0;
                PayrollDCls.Mandate = 0;
                PayrollDCls.OverTime = 0;
                PayrollDCls.H_OverTime = 0;

                if (!string.IsNullOrEmpty(HrClearanceItem.Penalties.ToString()))
                {
                    PayrollDCls.Penalties = HrClearanceItem.Penalties;
                    Deduction -= (HrClearanceItem.Penalties ?? 0);
                }
                else PayrollDCls.Penalties = 0;

                PayrollDCls.Deduction = Deduction;
                PayrollDCls.Net = HrClearanceItem.Net;

                PayrollDCls.Cnt_Absence = 0;
                PayrollDCls.IncomeTax = 0;
                PayrollDCls.DelayHourByDay = 0;
                PayrollDCls.CMDTYPE = 1;

                var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(PayrollDCls);
                if (PDID <= 0)
                    return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");

                //////  اضافة لعدم ارجاع البروسيجر لرقم المسير التفصيلي 
                //var getPDID = hrRepositoryManager.HrPayrollDRepository.Entities.Max(x => x.MsdId);
                //PDID = getPDID;

                //// بدل السكن
                //if (!string.IsNullOrEmpty(HrClearanceItem.HousingC.ToString()))
                //{
                //    var PayrollAllowanceDeduction = new HrPayrollAllowanceDeduction();
                //    PayrollAllowanceDeduction.EmpId = checkEmpExist.Id;
                //    PayrollAllowanceDeduction.AdId = HousingAllowance;
                //    PayrollAllowanceDeduction.MsId = newEntity.MsId;
                //    PayrollAllowanceDeduction.AdValue = HrClearanceItem.HousingC;
                //    PayrollAllowanceDeduction.Debit = 0;
                //    PayrollAllowanceDeduction.Credit = HrClearanceItem.HousingC;
                //    PayrollAllowanceDeduction.AdValueOrignal = HrClearanceItem.Housing;
                //    PayrollAllowanceDeduction.MsdId = PDID;
                //    PayrollAllowanceDeduction.CreatedBy = session.UserId;
                //    PayrollAllowanceDeduction.CreatedOn = DateTime.Now;
                //    PayrollAllowanceDeduction.FixedOrTemporary = 1;
                //    var PayrollAllowanceDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(PayrollAllowanceDeduction);
                //    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //    allowance -= HrClearanceItem.HousingC ?? 0;
                //}


                //  بدل مستحقات الإجازة
                if (!string.IsNullOrEmpty(HrClearanceItem.DayClearanceAmount.ToString()))
                {
                    var PayrollAllowanceDeduction = new HrPayrollAllowanceDeduction
                    {
                        AdId = VacationDueAllowance,
                        MsId = newEntity.MsId,
                        AdValue = HrClearanceItem.DayClearanceAmount,
                        Debit = 0,
                        Credit = HrClearanceItem.DayClearanceAmount,
                        AdValueOrignal = HrClearanceItem.DayClearanceAmount,
                        MsdId = PDID,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        FixedOrTemporary = 1,
                        //EmpId = checkEmpExist.Id
                    };

                    await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.Add(PayrollAllowanceDeduction);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    allowance -= HrClearanceItem.DayClearanceAmount ?? 0;
                }

                // بدل مستحقات التذاكر
                if (!string.IsNullOrEmpty(HrClearanceItem.TickDueTotal.ToString()))
                {
                    var PayrollAllowanceDeduction = new HrPayrollAllowanceDeduction
                    {
                        AdId = TicketAllowance,
                        MsId = newEntity.MsId,
                        AdValue = HrClearanceItem.TickDueTotal,
                        Debit = 0,
                        Credit = HrClearanceItem.TickDueTotal,
                        AdValueOrignal = HrClearanceItem.TickDueTotal,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        MsdId = PDID,
                        FixedOrTemporary = 1,
                        //EmpId = checkEmpExist.Id
                    };

                    await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.Add(PayrollAllowanceDeduction);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    allowance -= HrClearanceItem.TickDueTotal ?? 0;
                }

                //  البدلات الأخرى
                var getAllowances = await hrRepositoryManager.HrClearanceAllowanceDeductionRepository.GetAllVw(x =>
                     x.ClearanceId == entity.Id && x.EmpId == HrClearanceItem.EmpId && x.IsDeleted == false);
                foreach (var getAllowance in getAllowances)
                {
                    var PayrollAllowanceDeduction = new HrPayrollAllowanceDeduction
                    {
                        AdId = getAllowance.AdId,
                        MsId = newEntity.MsId,
                        AdValue = getAllowance.NewAmount,
                        Debit = 0,
                        Credit = getAllowance.NewAmount,
                        AdValueOrignal = getAllowance.Amount,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        MsdId = PDID,
                        FixedOrTemporary = 1
                    };

                    await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.Add(PayrollAllowanceDeduction);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    allowance -= getAllowance.NewAmount ?? 0;
                }

                // حسم التأمينات الإجتماعية
                if (!string.IsNullOrEmpty(HrClearanceItem.Gosi.ToString()))
                {
                    var PayrollAllowanceDeduction = new HrPayrollAllowanceDeduction
                    {
                        AdId = GOSIDeduction,
                        MsId = newEntity.MsId,
                        AdValue = HrClearanceItem.Gosi,
                        Debit = HrClearanceItem.Gosi,
                        Credit = 0,
                        AdValueOrignal = HrClearanceItem.Gosi,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        MsdId = PDID,
                        FixedOrTemporary = 1,
                        //EmpId = checkEmpExist.Id
                    };

                    await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.Add(PayrollAllowanceDeduction);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                // حسم بدل السكن المقدم
                if (!string.IsNullOrEmpty(HrClearanceItem.DedHousing.ToString()))
                {
                    var PayrollAllowanceDeduction = new HrPayrollAllowanceDeduction
                    {
                        AdId = HousingDeduction,
                        MsId = newEntity.MsId,
                        AdValue = HrClearanceItem.DedHousing,
                        Debit = HrClearanceItem.DedHousing,
                        Credit = 0,
                        AdValueOrignal = HrClearanceItem.Housing,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        MsdId = PDID,
                        FixedOrTemporary = 1,
                        //EmpId = checkEmpExist.Id
                    };

                    await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.Add(PayrollAllowanceDeduction);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                //   حسم العهودات الأخرى
                if (!string.IsNullOrEmpty(HrClearanceItem.DedOhad.ToString()))
                {
                    var PayrollAllowanceDeduction = new HrPayrollAllowanceDeduction
                    {
                        AdId = OtherDeduction,
                        MsId = newEntity.MsId,
                        AdValue = HrClearanceItem.DedOhad,
                        Debit = HrClearanceItem.DedOhad,
                        Credit = 0,
                        AdValueOrignal = HrClearanceItem.DedOhad,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        MsdId = PDID,
                        FixedOrTemporary = 1,
                        //EmpId = checkEmpExist.Id
                    };

                    await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.Add(PayrollAllowanceDeduction);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                // حسميات اخرى 
                if (!string.IsNullOrEmpty(HrClearanceItem.OtherDeduction.ToString()))
                {
                    var PayrollAllowanceDeduction = new HrPayrollAllowanceDeduction
                    {
                        AdId = OtherDeduction,
                        MsId = newEntity.MsId,
                        AdValue = HrClearanceItem.OtherDeduction,
                        Debit = HrClearanceItem.OtherDeduction,
                        Credit = 0,
                        AdValueOrignal = HrClearanceItem.OtherDeduction,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        MsdId = PDID,
                        FixedOrTemporary = 1,
                        //EmpId = checkEmpExist.Id,
                    };
                    await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.Add(PayrollAllowanceDeduction);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                var PayrollNoteItem = new HrPayrollNote
                {
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    MsId = newEntity.MsId,
                    StateId = 1,
                    Note = "",
                };
                await hrRepositoryManager.HrPayrollNoteRepository.Add(PayrollNoteItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // update payrollId in HrClearance
                var clearanceItem = await hrRepositoryManager.HrClearanceRepository.GetById(entity.Id);
                if (clearanceItem != null)
                {
                    clearanceItem.PayrollId = newEntity.MsId;
                    hrRepositoryManager.HrClearanceRepository.Update(clearanceItem);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                // ارسال المرفقات الى المسير
                var newPayrollFile = new SysFile
                {
                    PrimaryKey = newEntity.MsId,
                    FileName = "نموذج تصفية الإجازة",
                    TableId = 37,
                    FileType = 0,
                    FileUrl = "/Apps/HR/Crystalreport/Report_Viewer.aspx?Rep_ID=8&ID=" + entity.Id.ToString(),
                    FileExt = "",
                    FileDescription = "",
                    FileDate = HrClearanceItem.DateC,
                    SourceFile = "",
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    FacilityId = 0,
                };
                await mainRepositoryManager.SysFileRepository.Add(newPayrollFile);
                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync("تم استخراج المسير بنجاح");
            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message.ToString());
            }
        }

        public async Task<IResult<List<HrClearanceVw>>> Search(HrClearanceFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                filter.BranchId ??= 0; filter.DeptId ??= 0; filter.LocationId ??= 0; filter.ClearanceType ??= 0;
                var BranchesList = session.Branches.Split(',').Select(int.Parse).ToList();

                var items = await hrRepositoryManager.HrClearanceRepository.GetAllVw(x => x.IsDeleted == false
                && ((filter.BranchId == 0 && BranchesList.Contains(x.BranchId ?? 0)) || (filter.BranchId > 0 && x.BranchId == filter.BranchId))
                && (filter.ClearanceType == 0 || x.ClearanceType == filter.ClearanceType)
                && (string.IsNullOrEmpty(filter.empCode) || x.EmpCode == filter.empCode)
                && (filter.DeptId == 0 || x.DeptId == filter.DeptId)
                && (filter.LocationId == 0 || x.Location == filter.LocationId)


                );

                if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
                {
                    DateTime startDate = DateHelper.StringToDate(filter.FromDate);
                    DateTime endDate = DateHelper.StringToDate(filter.ToDate);

                    items = items.Where(x => !string.IsNullOrEmpty(x.DateC) &&
                    (DateHelper.StringToDate(x.DateC) >= startDate) && (DateHelper.StringToDate(x.DateC) <= endDate));
                }

                items = items.OrderByDescending(x => x.DateC);
                return await Result<List<HrClearanceVw>>.SuccessAsync(items.ToList());
            }
            catch (Exception ex)
            {
                return await Result<List<HrClearanceVw>>.FailAsync(ex.Message);
            }
        }


        public async Task<IResult<DataTable>> HR_Payroll_Clearance_Sp(string EmpCode, string EmpName, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_Clearance_Sp(EmpCode, EmpName);
                return await Result<DataTable>.SuccessAsync(result, "");
            }
            catch (Exception ex)
            {
                return await Result<DataTable>.FailAsync(ex.Message.ToString());
            }
        }

        public async Task<IResult<string>> Add2(HrClearanceAddDto2 entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');

                var CheckEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (CheckEmpExist == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                // نفحص هل الموظف موجود ضمن فروع الموظف الحالي 
                if (!BranchesList.Contains(CheckEmpExist.BranchId.ToString()))
                    return await Result<string>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                HrClearance newClearance = new()
                {
                    EmpId = CheckEmpExist.Id,
                    DateC = entity.LeaveDate,
                    ClearanceType = entity.ClearanceType,
                    BasicSalary = entity.BasicSalary,
                    Housing = entity.Housing,
                    Allowances = entity.Allowances,
                    Deduction = entity.Deduction,
                    TotalSalary = entity.TotalSalary,
                    LastVacationDate = entity.LastVacationDate,
                    LastVacationType = entity.LastVacationType,
                    VacationDaysYear = entity.VacationDaysYear,
                    LocationId = 0,
                    DepId = 0,
                    BankId = "0",
                    Iban = entity.Iban,
                    LastSalaryDate = entity.LastSalaryDate,
                    VacationSdate = entity.VacationSdate,
                    VacationEdate = entity.VacationEdate,
                    VacationAccountDay = entity.VacationAccountDay ?? 0,
                    VacationDayWithSalary = entity.VacationDayWithSalary ?? 0,
                    VacationDayWithoutSalary = entity.VacationDayWithoutSalary ?? 0,
                    VacationBalance = entity.VacationBalance,
                    VacationBalanceAmount = entity.VacationBalanceAmount,
                    SalaryC = 0,
                    HousingC = 0,
                    AllowanceC = 0,
                    OtherAllowance = entity.OtherAllowance,
                    OtherAllowanceNote = entity.OtherAllowanceNote,
                    DayClearanceAmount = entity.DayClearanceAmount,
                    DayClearance = entity.DayClearance,
                    TickDueTotal = entity.TickDueTotal,
                    TickDueCnt = entity.TickDueCnt,
                    TickDueAmount = entity.TickDueAmount,
                    TotalAllowance = entity.TotalAllowance,
                    DedHousing = 0,
                    Loan = 0,
                    Gosi = 0,
                    GosiNote = "0",
                    DedOhad = entity.DedOhad,
                    DedOhadNote = entity.DedOhadNote,
                    Delay = 0,
                    DelayCnt = 0,
                    Absence = 0,
                    AbsenceCnt = 0,
                    Penalties = 0,
                    TotalDeduction = entity.TotalDeduction,
                    Net = entity.Net,
                    Note = entity.Note,
                    LastWorkingDay = entity.LeaveDate,
                    OtherDeduction = 0,
                    OtherDeductionNote = "",
                    BranchId = 0,
                    LastVacationEdate = "",

                    VacationId = 0,
                    CountDayWork = 0
                };

                var AddedEntity = await hrRepositoryManager.HrClearanceRepository.AddAndReturn(newClearance);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.ClearanceMonthList != null && entity.ClearanceMonthList.Any())
                {
                    List<HrClearanceMonth> months = new();
                    foreach (var month in entity.ClearanceMonthList)
                    {
                        HrClearanceMonth newMonth = new()
                        {
                            Absence = month.Absence,
                            Allowance = month.Allowance,
                            AllowanceOther = month.AllowancesOther,
                            ClearanceId = AddedEntity.Id,
                            Commission = month.Commission,
                            CountDayWork = month.CountDayWork ?? 0,
                            DayAbsence = month.DayAbsence ?? 0,
                            DayPrevMonth = month.DayPrevMonth ?? 0,
                            Deduction = month.Deduction,
                            DeductionOther = month.DeductionOther,
                            Delay = month.Delay,
                            DueDayWork = month.DueDayWork,
                            DuePrevMonth = month.PrevMonth,
                            FacilityId = Convert.ToInt32(session.FacilityId),
                            FinancelYear = month.FinancialYear ?? 0,
                            HExtraTime = month.HExtraTime,
                            Loan = month.Loan,
                            MsDate = month.MsDate,
                            MsMonth = month.Month,
                            MDelay = month.MDelay ?? 0,
                            Note = month.Note,
                            Penalties = month.Penalties,
                            Salary = month.Salary,
                            ExtraTime = month.ExtraTime
                        };

                        months.Add(newMonth);
                    }

                    await hrRepositoryManager.HrClearanceMonthRepository.Addlist(months);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(AddedEntity.Id.ToString(), localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"{exp.Message}");
            }
        }

        public async Task<IResult<string>> Edit2(HrClearanceAddDto2 entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null) return await Result<string>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

                var item = await hrRepositoryManager.HrClearanceRepository.GetById(entity.Id);
                if (item == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("NoIdInUpdate")}");

                var CheckEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (CheckEmpExist == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                // نفحص هل الموظف موجود ضمن فروع الموظف الحالي 
                var BranchesList = session.Branches.Split(',');
                if (!BranchesList.Contains(CheckEmpExist.BranchId.ToString()))
                    return await Result<string>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));

                item.EmpId = CheckEmpExist.Id;
                item.DateC = entity.LeaveDate;
                item.ClearanceType = entity.ClearanceType;
                item.BasicSalary = entity.BasicSalary;
                item.Housing = entity.Housing;
                item.Allowances = entity.Allowances;
                item.Deduction = entity.Deduction;
                item.TotalSalary = entity.TotalSalary;
                item.LastVacationDate = entity.LastVacationDate;
                item.LastVacationType = entity.LastVacationType;
                item.VacationDaysYear = entity.VacationDaysYear;
                item.LocationId = 0;
                item.DepId = 0;
                item.BankId = "0";
                item.Iban = entity.Iban;
                item.LastSalaryDate = entity.LastSalaryDate;
                item.VacationSdate = entity.VacationSdate;
                item.VacationEdate = entity.VacationEdate;
                item.VacationAccountDay = entity.VacationAccountDay ?? 0;
                item.VacationDayWithSalary = entity.VacationDayWithSalary ?? 0;
                item.VacationDayWithoutSalary = entity.VacationDayWithoutSalary ?? 0;
                item.VacationBalance = entity.VacationBalance;
                item.VacationBalanceAmount = entity.VacationBalanceAmount;
                item.SalaryC = 0;
                item.HousingC = 0;
                item.AllowanceC = 0;
                item.OtherAllowance = entity.OtherAllowance;
                item.OtherAllowanceNote = entity.OtherAllowanceNote;
                item.DayClearanceAmount = entity.DayClearanceAmount;
                item.DayClearance = entity.DayClearance;
                item.TickDueTotal = entity.TickDueTotal;
                item.TickDueCnt = entity.TickDueCnt;
                item.TickDueAmount = entity.TickDueAmount;
                item.TotalAllowance = entity.TotalAllowance;
                item.DedHousing = 0;
                item.Loan = 0;
                item.Gosi = 0;
                item.GosiNote = "0";
                item.DedOhad = entity.DedOhad;
                item.DedOhadNote = entity.DedOhadNote;
                item.Delay = 0;
                item.DelayCnt = 0;
                item.Absence = 0;
                item.AbsenceCnt = 0;
                item.Penalties = 0;
                item.TotalDeduction = entity.TotalDeduction;
                item.Net = entity.Net;
                item.Note = entity.Note;

                hrRepositoryManager.HrClearanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<string>.SuccessAsync(item.Id.ToString(), localization.GetMessagesResource("success"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"{exp.Message}");
            }
        }
    }
}
