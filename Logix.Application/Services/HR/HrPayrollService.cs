using System.Globalization;
using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.Hr;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrPayrollService : GenericQueryService<HrPayroll, HrPayrollDto, HrPayrollVw>, IHrPayrollService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IAccRepositoryManager accRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;
        private readonly IWorkflowHelper workflowHelper;

        public HrPayrollService(IQueryRepository<HrPayroll> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session,
            ILocalizationService localization,
            ISysConfigurationAppHelper sysConfigurationAppHelper,
            IAccRepositoryManager accRepositoryManager,
            IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
            this.session = session;
            this.localization = localization;
            this.accRepositoryManager = accRepositoryManager;
            this.workflowHelper = workflowHelper;
        }

        public Task<IResult<HrPayrollDto>> Add(HrPayrollDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrPayrollRepository.GetOne(x => x.MsId == Id);
                if (item == null) return await Result<HrPayrollDto>.FailAsync($"{localization.GetMessagesResource("NoIdInDelete")}");

                var PropertyValue = await sysConfigurationAppHelper.GetValue(310, session.FacilityId);
                if (PropertyValue == "1")
                {
                    if (item.State == 4)
                    {
                        return await Result<HrPayrollDto>.FailAsync($"{localization.GetMessagesResource("RequestApprovedAuthorizedPersonAndCannotDeleted")}");
                    }
                }

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(Id), 24);
                if (status == 2)
                {
                    return await Result<HrPayrollDto>.FailAsync($"{localization.GetResource1("ThePayrollCannotDeleted")}");
                }
                else
                {
                    await accRepositoryManager.AccJournalMasterRepository.DeleteJournalWithDetailsbyReference(Id, 24);
                    await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                item.IsDeleted = true;
                hrRepositoryManager.HrPayrollRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // start of delelte payroll_D 
                var payrollDItem = await hrRepositoryManager.HrPayrollDRepository.GetAll(x => x.MsId == Id);
                if (payrollDItem.Any())
                {
                    foreach (var singlepayrollDIte in payrollDItem)
                    {
                        singlepayrollDIte.IsDeleted = true;
                    }

                    hrRepositoryManager.HrPayrollDRepository.UpdateAll(payrollDItem);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                // BEGIN of delelte HRLoanInstallment 
                var loanInstallmentPayment = await hrRepositoryManager.HrLoanInstallmentPaymentRepository.GetAll(x => x.PayrollId == Id && x.IsDeleted == false);
                var LoanInstallmentID = loanInstallmentPayment.Select(x => x.LoanInstallmentId).ToList();
                var getLoanInstallment = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(x => LoanInstallmentID.Contains(x.Id));
                if (getLoanInstallment.Any())
                {
                    foreach (var singleLoanInstallment in getLoanInstallment)
                    {
                        singleLoanInstallment.IsPaid = false;
                    }

                    hrRepositoryManager.HrLoanInstallmentRepository.UpdateAll(getLoanInstallment);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                // Begin of delelte HRLoanInstallment_Payment 
                if (loanInstallmentPayment.Any())
                {
                    foreach (var singleLoanInstallmentPayment in loanInstallmentPayment)
                    {
                        singleLoanInstallmentPayment.IsDeleted = true;
                    }

                    hrRepositoryManager.HrLoanInstallmentPaymentRepository.UpdateAll(loanInstallmentPayment);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrPayrollDto>.SuccessAsync(_mapper.Map<HrPayrollDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPayrollDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<HrPayrollEditDto>> Update(HrPayrollEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPayrollEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrPayrollRepository.GetById(entity.MsId);

                if (item == null) return await Result<HrPayrollEditDto>.FailAsync("the Payroll Not Found");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.DueDate = entity.DueDate;
                item.MsTitle = entity.MsTitle;
                item.PaymentDate = entity.PaymentDate;
                hrRepositoryManager.HrPayrollRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, item.MsId, 37);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrPayrollEditDto>.SuccessAsync(_mapper.Map<HrPayrollEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrPayrollEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<IEnumerable<HRPayrollManuallCreateSpDto>>> getHR_Payroll_Create2_Sp(HRPayrollCreate2SpFilterDto filter)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_Create2_Sp(filter);
                return await Result<IEnumerable<HRPayrollManuallCreateSpDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRPayrollManuallCreateSpDto>>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<IEnumerable<HRPayrollCreate2SpDto>>> getHR_Payroll_Create_Sp(HRPayrollCreateSpFilterDto filter)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_Create_Sp(filter);
                return await Result<IEnumerable<HRPayrollCreate2SpDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRPayrollCreate2SpDto>>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<string>> AddNewPayroll(HrPayrollAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == entity.FacilityId);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (entity.MsMonth == "02" || entity.MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{entity.MsMonth}-{entity.FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(entity.MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{entity.MsMonth}-{entity.FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (Convert.ToInt32(AttStartDay) != 1 && Convert.ToInt32(AttStartDay) != 01)
                {
                    if (Convert.ToInt32(entity.MsMonth) != 1 && Convert.ToInt32(entity.MsMonth) != 01)
                    {
                        int PrevMonth = int.Parse(entity.MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{entity.FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(entity.FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{entity.FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttStartDay}";
                    AttEndDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttEndDay}";
                }

                var MsMonthTxt = DateHelper.GetMonthName(Convert.ToInt32(entity.MsMonth));

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var newPayrollEntity = new HrPayroll
                {
                    MsCode = getMaxCode + 1,
                    MsDate = entity.MsDate,
                    MsTitle = entity.MsTitle,
                    MsMonth = entity.MsMonth,
                    MsMothTxt = MsMonthTxt,
                    FinancelYear = entity.FinancelYear,
                    State = entity.State,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = entity.FacilityId,
                    PayrollTypeId = entity.PayrollTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = entity.BranchId,
                    AppId = 0,
                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                // End Of Add To HrPayroll


                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                var getFromHrAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(x => x.IsDeleted == false && x.FixedOrTemporary == 1);
                foreach (var item in entity.SpDtos)
                {
                    var empData = getFromEmployees.Where(x => x.EmpId == item.Emp_ID).FirstOrDefault();
                    if (empData == null)
                        return await Result<string>.FailAsync($"the employee of Id : {item.Emp_ID} not found");
                    var newPayrollDEntity = new HrPayrollD
                    {
                        MsId = AddedPayrollEntity.MsId,
                        EmpId = empData.Id,
                        Absence = item.Absence,
                        Allowance = item.Allowance,
                        Deduction = item.Deduction,
                        Delay = item.Delay,
                        BankId = item.Bank_ID,
                        CountDayWork = item.Attendance,
                        EmpAccountNo = item.Account_No,
                        Loan = item.Loan,
                        Salary = item.Salary,
                        Commission = item.Commission,
                        OverTime = item.OverTime,
                        Mandate = item.Mandate,
                        HOverTime = item.H_OverTime,
                        Penalties = item.Penalties,
                        Net = ((item.Salary ?? 0) + (item.Allowance ?? 0) + (item.Commission ?? 0) + (item.OverTime ?? 0) - (item.Absence ?? 0) - (item.Delay ?? 0) - (item.Loan ?? 0) - (item.Deduction ?? 0) - (item.Penalties ?? 0)),
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        AllowanceOrignal = getFromHrAllowanceDeduction.Where(x => x.TypeId == 1 && x.EmpId == empData.Id).Sum(x => x.Amount) ?? 0,
                        DeductionOrignal = getFromHrAllowanceDeduction.Where(x => x.TypeId == 2 && x.EmpId == empData.Id).Sum(x => x.Amount) ?? 0,
                        Iban = empData.Iban,
                        BranchId = empData.BranchId,
                        LocationId = empData.Location,
                        DeptId = empData.DeptId,
                        FacilityId = empData.FacilityId,
                        WagesProtection = empData.WagesProtection,
                        SalaryOrignal = empData.Salary, //  يتم تعديلها في البروسيجر
                        CcId = empData.CcId ?? 0,
                        SalaryGroupId = empData.SalaryGroupId,
                        PaymentTypeId = empData.PaymentTypeId,
                    };
                    var AddedPayrollDEntity = await hrRepositoryManager.HrPayrollDRepository.AddAndReturn(newPayrollDEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    // this code for add allowance and deduction 
                    var GetHRPSDeduction = await hrRepositoryManager.HrPsDeductionVwRepository.GetAll(x => x.PsId == item.ID);
                    foreach (var singlePSAllowanceDeduction in GetHRPSDeduction)
                    {
                        var newPSDeductionEntity = new HrPayrollAllowanceDeduction
                        {
                            AdId = singlePSAllowanceDeduction.AdId,
                            MsId = AddedPayrollEntity.MsId,
                            AdValue = singlePSAllowanceDeduction.Amount,
                            AdValueOrignal = singlePSAllowanceDeduction.AmountOrginal,
                            Debit = singlePSAllowanceDeduction.Amount,
                            Credit = 0,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            FixedOrTemporary = singlePSAllowanceDeduction.FixedOrTemporary,
                            MsdId = AddedPayrollDEntity.MsdId

                        };
                        var AddPSAllowanceDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(newPSDeductionEntity);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                    var GetHRPSAllowance = await hrRepositoryManager.HrPsDeductionVwRepository.GetAll(x => x.PsId == item.ID);
                    foreach (var singlePSAllowanceDeduction in GetHRPSAllowance)
                    {
                        var newPSDeductionEntity = new HrPayrollAllowanceDeduction
                        {
                            AdId = singlePSAllowanceDeduction.AdId,
                            MsId = AddedPayrollEntity.MsId,
                            AdValue = singlePSAllowanceDeduction.Amount,
                            AdValueOrignal = singlePSAllowanceDeduction.AmountOrginal,
                            Debit = 0,
                            Credit = singlePSAllowanceDeduction.Amount,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            FixedOrTemporary = singlePSAllowanceDeduction.FixedOrTemporary,
                            MsdId = AddedPayrollDEntity.MsdId

                        };
                        var AddPSAllowanceDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(newPSDeductionEntity);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                // End Of Add To HrPayrollD

                // Begin ChangeStatus_Payroll_Trans
                var newHrPayrollNote = new HrPayrollNote
                {
                    MsId = AddedPayrollEntity.MsId,
                    StateId = 1,
                    Note = "",
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                };
                var AddedPayrollNoteEntity = await hrRepositoryManager.HrPayrollNoteRepository.AddAndReturn(newHrPayrollNote);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //كما هو موجود بال sp
                var ms = await hrRepositoryManager.HrPayrollRepository.GetById(AddedPayrollEntity.MsId);
                if (ms == null) return await Result<string>.FailAsync("the Payroll Not Found");
                ms.State = newHrPayrollNote.StateId;
                ms.ModifiedBy = newHrPayrollNote.CreatedBy;
                ms.ModifiedOn = newHrPayrollNote.CreatedOn;
                hrRepositoryManager.HrPayrollRepository.Update(ms);

                // تحديث السلف الى مدفوعة بعد اعتماد المسير
                if (entity.State == 4)
                {
                    var LoanInstallmentID = await hrRepositoryManager.HrLoanInstallmentPaymentRepository.GetAll(x => x.Id, x => x.PayrollId == AddedPayrollEntity.MsId && x.IsDeleted == false);
                    var getLoanInstallment = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(x => LoanInstallmentID.Contains(x.Id));

                    if (getLoanInstallment.Count() > 0)
                    {
                        foreach (var singleLoanInstallment in getLoanInstallment)
                        {
                            singleLoanInstallment.IsPaid = true;
                            singleLoanInstallment.ModifiedBy = session.UserId;
                            singleLoanInstallment.ModifiedOn = DateTime.Now;
                        }
                        hrRepositoryManager.HrLoanInstallmentRepository.UpdateAll(getLoanInstallment);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                // End ChangeStatus_Payroll_Trans



                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in addNewPayroll at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in addNewPayroll at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        private async Task<AllowanceDeductionCheckResult> CheckAllowanceandDeductionAsync(HrPayrollAddDto entity)
        {
            var result = new AllowanceDeductionCheckResult();
            string degit = await sysConfigurationAppHelper.GetValue(71, Convert.ToInt64(entity.FacilityId ?? 0));

            foreach (var rowPayroll in entity.SpDtos)
            {
                string empCode = rowPayroll.Emp_ID;
                decimal allowance = rowPayroll.Allowance ?? 0;
                decimal deduction = rowPayroll.Deduction ?? 0;
                long empId = rowPayroll.ID;
                int attendance = rowPayroll.Attendance ?? 0;

                var allowances = await hrRepositoryManager.HrAllowanceVwRepository.GetAllowanceFixedAndTemporary(empId, entity.FinancelYear, entity.MsMonth, attendance);
                decimal calculatedAllowance = allowances.Sum(r => r.Amount ?? 0);

                if (!string.IsNullOrEmpty(degit))
                    calculatedAllowance = Math.Round(calculatedAllowance, Convert.ToInt32(degit));

                if (allowance != calculatedAllowance)
                {
                    result.AllowanceMismatch += empCode + ",";
                    result.IsValid = false;
                }

                var deductions = await hrRepositoryManager.HrDeductionVwRepository.GetDeductionFixedAndTemporary(empId, entity.FinancelYear, entity.MsMonth, attendance);
                decimal calculatedDeduction = deductions.Sum(r => r.Amount ?? 0);

                if (!string.IsNullOrEmpty(degit))
                    calculatedDeduction = Math.Round(calculatedDeduction, Convert.ToInt32(degit));

                if (deduction != calculatedDeduction)
                {
                    result.DeductionMismatch += empCode + ",";
                    result.IsValid = false;
                }
            }

            return result;
        }

        public async Task<IResult<string>> AddNewAutomaticPayroll1(HrPayrollAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                if (entity.SpDtos.Count <= 0) return await Result<string>.FailAsync("الرجاء اختيار بيانات");

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                long? AppID = 0;
                var configCheck = await sysConfigurationAppHelper.GetValue(330, Convert.ToInt64(entity.FacilityId ?? 0));
                if (configCheck != "2")
                {
                    var checkResult = await CheckAllowanceandDeductionAsync(entity);
                    if (!checkResult.IsValid)
                    {
                        string message = "";

                        if (!string.IsNullOrEmpty(checkResult.AllowanceMismatch))
                        {
                            message += $"اجمالي البدلات لا يساوي  تفاصيل البدلات  للموظفين  {checkResult.AllowanceMismatch}";
                        }

                        if (!string.IsNullOrEmpty(checkResult.DeductionMismatch))
                        {
                            message += $"\nاجمالي الحسميات لا يساوي  تفاصيل الحسميات  للموظفين  {checkResult.DeductionMismatch}";
                        }

                        return await Result<string>.FailAsync(message);
                    }
                }

                entity.AppTypeId ??= 0;

                var empID = Convert.ToInt64(session.EmpId);

                //  ارسال الى سير العمل
                string subject = entity.IsForAll ? (entity.MsTitle ?? "") : "";
                var GetApp_ID = await workflowHelper.Send(empID, 168, entity.AppTypeId, Subject: subject);
                AppID = GetApp_ID;

                //  التشييك على السالب  و التشييك على حماية الأجور ونسبة الخصم في نفس الدوارة
                string Negitive = "";
                string WagesProtection = "";
                var WagesProtectionRateBasicSalary = await sysConfigurationAppHelper.GetValue(52, Convert.ToInt64(entity.FacilityId ?? 0));
                if (string.IsNullOrEmpty(WagesProtectionRateBasicSalary)) WagesProtectionRateBasicSalary = "100";

                foreach (var item in entity.SpDtos)
                {
                    if ((item.Salary + item.Allowance + item.OverTime + item.Commission - item.Absence - item.Delay - item.Loan - item.Deduction - item.Penalties) < 0)
                        Negitive += item.Emp_ID + ",";
                    if ((item.Absence + item.Delay + item.Loan + item.Deduction + item.Penalties) > (item.BasicSalary * Convert.ToDecimal(WagesProtectionRateBasicSalary) / 100))
                        WagesProtection += item.Emp_ID + ",";
                }
                if (Negitive.Length > 0) return await Result<string>.FailAsync($"{localization.GetHrResource("NegitaveSalaryIssue")} \n {Negitive.TrimEnd(',')}");
                if (WagesProtection.Length > 0) return await Result<string>.FailAsync($"{localization.GetHrResource("OverDeductionIssue")} \n {WagesProtection.TrimEnd(',')}");
                int Digit = 2;
                var DigitProperty = await sysConfigurationAppHelper.GetValue(71, Convert.ToInt64(entity.FacilityId ?? 0));
                if (!string.IsNullOrEmpty(DigitProperty))
                {
                    Digit = Convert.ToInt32(DigitProperty);
                }

                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == entity.FacilityId);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (entity.MsMonth == "02" || entity.MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{entity.MsMonth}-{entity.FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(entity.MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{entity.MsMonth}-{entity.FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (AttStartDay != "01")
                {
                    if (entity.MsMonth != "01")
                    {
                        int PrevMonth = int.Parse(entity.MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{entity.FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(entity.FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{entity.FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttStartDay}";
                    AttEndDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttEndDay}";
                }

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var theCode = (getMaxCode ?? 0) + 1;
                var MsMonthTxt = DateHelper.GetMonthName(Convert.ToInt32(entity.MsMonth));

                var newPayrollEntity = new HrPayroll
                {
                    MsCode = theCode,
                    MsDate = entity.MsDate,
                    MsTitle = entity.MsTitle,
                    MsMonth = entity.MsMonth,
                    MsMothTxt = MsMonthTxt,
                    FinancelYear = entity.FinancelYear,
                    State = entity.State,
                    CreatedBy = session.UserId,
                    FacilityId = entity.FacilityId,
                    PayrollTypeId = entity.PayrollTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = entity.BranchId ?? 0,
                    AppId = AppID,
                    Posted = false
                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                //var getFromHrAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(x => x.IsDeleted == false && x.FixedOrTemporary == 1);
                foreach (var item in entity.SpDtos)
                {
                    if (item.ID == 0) { continue; }

                    var empData = getFromEmployees.Where(x => x.Id == item.ID).FirstOrDefault();
                    if (empData == null)
                        return await Result<string>.FailAsync($"the employee of Id : {item.Emp_ID} not found");
                    var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                    {
                        MS_ID = AddedPayrollEntity.MsId,
                        Emp_ID = item.ID,
                        Absence = item.Absence ?? 0,
                        Allowance = item.Allowance ?? 0,
                        Deduction = item.Deduction ?? 0,
                        Delay = item.Delay ?? 0,
                        BankId = item.Bank_ID ?? 0,
                        Count_Day_Work = item.Attendance ?? 0,
                        Emp_Account_No = item.Account_No ?? "",
                        Loan = item.Loan ?? 0,
                        Salary = item.Salary ?? 0,
                        Commission = item.Commission ?? 0,
                        OverTime = item.OverTime ?? 0,
                        Mandate = item.Mandate ?? 0,
                        H_OverTime = item.H_OverTime ?? 0,
                        Penalties = item.Penalties ?? 0,
                        Net = ((item.Salary ?? 0) + (item.Allowance ?? 0) + (item.Commission ?? 0) + (item.OverTime ?? 0) + (item.Mandate ?? 0) - (item.Absence ?? 0) - (item.Delay ?? 0) - (item.Loan ?? 0) - (item.Deduction ?? 0) - (item.Penalties ?? 0)),
                        CreatedBy = session.UserId,
                        CMDTYPE = 1
                    };
                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(newPayrollDEntity);
                    if (PDID <= 0)
                        return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");
                    var getPDID = hrRepositoryManager.HrPayrollDRepository.Entities.Max(x => x.MsdId);
                    PDID = getPDID;
                    //// this code for add allowance and deduction 

                    var getDeductionData = await hrRepositoryManager.HrDeductionVwRepository.GetDeductionFixedAndTemporary(item.ID, entity.FinancelYear, entity.MsMonth, item.Attendance);
                    if (getDeductionData.Count() > 0)
                    {
                        foreach (var singleDeductionData in getDeductionData)
                        {
                            var newductionEntity = new HrPayrollAllowanceDeduction
                            {
                                AdId = singleDeductionData.AdId,
                                MsId = AddedPayrollEntity.MsId,
                                AdValue = Math.Round(singleDeductionData.Amount ?? 0, Digit),
                                AdValueOrignal = singleDeductionData.OriginalAmount,
                                Debit = Math.Round(singleDeductionData.Amount ?? 0, Digit),
                                Credit = 0,
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,
                                FixedOrTemporary = singleDeductionData.FixedOrTemporary,
                                MsdId = PDID
                            };
                            var AddDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(newductionEntity);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }

                    var getAllowanceData = await hrRepositoryManager.HrAllowanceVwRepository.GetAllowanceFixedAndTemporary(item.ID, entity.FinancelYear, entity.MsMonth, item.Attendance);
                    if (getAllowanceData.Count() > 0)
                    {
                        foreach (var singleAllowanceData in getAllowanceData)
                        {
                            var newductionEntity = new HrPayrollAllowanceDeduction
                            {
                                AdId = singleAllowanceData.AdId,
                                MsId = AddedPayrollEntity.MsId,
                                AdValue = Math.Round(singleAllowanceData.Amount ?? 0, Digit),
                                AdValueOrignal = singleAllowanceData.OriginalAmount,
                                Debit = 0,
                                Credit = Math.Round(singleAllowanceData.Amount ?? 0, Digit),
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,
                                FixedOrTemporary = singleAllowanceData.FixedOrTemporary,
                                MsdId = PDID
                            };
                            var AddDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(newductionEntity);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }
                }
                // End Of Add To HrPayrollD

                // Begin ChangeStatus_Payroll_Trans
                var newHrPayrollNote = new HrPayrollNote
                {
                    MsId = AddedPayrollEntity.MsId,
                    StateId = 1,
                    Note = "",
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                };
                var AddedPayrollNoteEntity = await hrRepositoryManager.HrPayrollNoteRepository.AddAndReturn(newHrPayrollNote);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // تحديث السلف الى مدفوعة بعد اعتماد المسير
                if (entity.State == 4)
                {
                    var LoanInstallmentID = await hrRepositoryManager.HrLoanInstallmentPaymentRepository.GetAll(x => x.LoanInstallmentId, x => x.PayrollId == AddedPayrollEntity.MsId && x.IsDeleted == false);
                    var getLoanInstallment = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(x => LoanInstallmentID.Contains(x.Id));
                    if (getLoanInstallment.Count() > 0)
                    {
                        foreach (var singleLoanInstallment in getLoanInstallment)
                        {
                            singleLoanInstallment.IsPaid = true;
                        }
                        hrRepositoryManager.HrLoanInstallmentRepository.UpdateAll(getLoanInstallment);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                // End ChangeStatus_Payroll_Trans

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in addNewPayroll at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in addNewPayroll at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> CheckJoinWorkForPayroll(HRPayrollCreateSpFilterDto entity, CancellationToken cancellationToken)
        {
            try
            {
                string EndDate = "";
                string checkEmpJoinWork = "";
                if (entity.FacilityID <= 0 || entity.FacilityID == null)
                {
                    entity.FacilityID = Convert.ToInt32(session.FacilityId);
                }
                if (entity.FacilityID < 1) entity.FacilityID = 1;
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == entity.FacilityID);
                EndDate = $"{entity.FinancelYear}/{entity.MSMonth}/{getFromHrSetting.MonthEndDay}";
                if (entity.MSMonth.Length == 1)
                {
                    entity.MSMonth = "0" + entity.MSMonth;
                }
                List<long?> getEmpIdsListFromHrPayrollD = new();
                List<long?> getEmpIdsListFromHrVacations = new();
                var getFromHrPayrollD = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(x => x.MsMonth == entity.MSMonth && x.FinancelYear.ToString() == entity.FinancelYear && x.IsDeleted == false && x.PayrollTypeId == 1);
                if (getFromHrPayrollD.Any())
                {
                    getEmpIdsListFromHrPayrollD = getFromHrPayrollD.Select(x => x.EmpId).ToList();
                }

                var getEmpIdsListFromVacation = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.NeedJoinRequest == true && (x.VacationRdate == null || x.VacationRdate == ""));
                if (getEmpIdsListFromVacation.Count() >= 0)
                {
                    var FilteredData = getEmpIdsListFromVacation.Where(x => DateHelper.StringToDate(x.VacationEdate) <= DateHelper.StringToDate(EndDate));
                    if (FilteredData.Any())
                    {
                        getEmpIdsListFromHrVacations = FilteredData.Select(x => x.EmpId).ToList();
                    }
                }
                var BranchesList = session.Branches.Split(',');
                var ContractTypeIds = (entity.ContractTypeID ?? "").Split(',');
                entity.DeptID ??= 0; entity.Location ??= 0; entity.BRANCHID ??= 0; entity.FacilityID ??= 0;
                entity.PaymentTypeID ??= 0; entity.SalaryGroupID ??= 0; entity.SponsorsID ??= 0; entity.WagesProtection ??= 0;

                var getHrEmployee = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(x => x.Isdel == false && x.IsDeleted == false
                && x.StatusId != 2 && x.StopSalary == false
                && (entity.DeptID == 0 || x.DeptId == entity.DeptID)
                && (entity.Location == 0 || x.Location == entity.Location)
                && (entity.BRANCHID == 0 || x.BranchId == entity.BRANCHID)
                && (BranchesList.Length == 0 || BranchesList.Contains(x.BranchId.ToString()))
                && (string.IsNullOrEmpty(entity.EmpCode) || x.EmpId == entity.EmpCode)
                && (entity.FacilityID == 0 || x.FacilityId == entity.FacilityID)
                && (string.IsNullOrEmpty(entity.ContractTypeID) || ContractTypeIds.Contains(x.ContractTypeId.ToString()))
                && (entity.PaymentTypeID == 0 || x.PaymentTypeId == entity.PaymentTypeID)
                && (entity.SalaryGroupID == 0 || x.SalaryGroupId == entity.SalaryGroupID)
                && (entity.SponsorsID == 0 || x.SponsorsId == entity.SponsorsID)
                && (entity.WagesProtection == 0 || x.WagesProtection == entity.WagesProtection)
                && (!getEmpIdsListFromHrPayrollD.Contains(x.Id))
                && getEmpIdsListFromHrVacations.Contains(x.Id));

                if (getHrEmployee.Any())
                {
                    foreach (var item in getHrEmployee)
                    {
                        checkEmpJoinWork += item.EmpId + ",";
                    }
                }

                return await Result<string>.SuccessAsync(checkEmpJoinWork, "", 200);
            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }
        }

        /// <summary>
        /// تستخدم الدالة للفلترة في شاشة اضافة مسير عمولات
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IResult<IEnumerable<HrPreparationSalariesVw>>> CommissionPayrollSearch(HRPayrollCreateSpFilterDto entity)
        {
            try
            {
                string EndDate = "";
                DateTime firstDayOfNextMonth = new DateTime(Convert.ToInt32(entity.FinancelYear), Convert.ToInt32(entity.MSMonth), 1).AddMonths(1);
                DateTime lastDayOfMonth = firstDayOfNextMonth.AddDays(-1);
                var monthEnd = lastDayOfMonth.Day.ToString();
                if (monthEnd.Length == 1) monthEnd = "0" + monthEnd;

                EndDate = $"{entity.FinancelYear}/{entity.MSMonth}/{monthEnd}";
                if (entity.MSMonth.Length == 1)
                {
                    entity.MSMonth = "0" + entity.MSMonth;
                }
                List<long?> getEmpIdsListFromHrPayrollD = new List<long?>();
                var getFromHrPayrollD = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(x => x.MsMonth == entity.MSMonth && x.FinancelYear.ToString() == entity.FinancelYear && x.IsDeleted == false && x.PayrollTypeId == 2);
                if (getFromHrPayrollD.Any())
                {
                    getEmpIdsListFromHrPayrollD = getFromHrPayrollD.Select(x => x.EmpId).ToList();
                }


                var getHrPreparingSalaries = await hrRepositoryManager.HrPreparationSalaryRepository.GetAllFromView(x =>
                x.IsDeleted == false &&
                x.PayrollTypeId == 2 &&
               (x.FinancelYear.ToString() == entity.FinancelYear) &&
                (string.IsNullOrEmpty(entity.MSMonth) || Convert.ToInt32(x.MsMonth) == Convert.ToInt32(entity.MSMonth)) &&
                (entity.DeptID == null || entity.DeptID == 0 || x.DeptId == entity.DeptID) &&
                (entity.Location == null || entity.Location == 0 || x.Location == entity.Location) &&
                (entity.BRANCHID == null || entity.BRANCHID == 0 || x.BranchId == entity.BRANCHID) &&
                (string.IsNullOrEmpty(entity.EmpCode) || x.EmpCode.ToLower() == entity.EmpCode.ToLower()) &&
                (string.IsNullOrEmpty(entity.EmpName) || x.EmpName.ToLower() == entity.EmpName.ToLower()) &&
                (entity.FacilityID == null || entity.FacilityID == 0 || x.FacilityId == entity.FacilityID) &&
                ((entity.NationalityID == 0) || (entity.NationalityID == 1 && new List<int?>(13).Contains(x.NationalityId)) || (entity.NationalityID == 2 && !new List<int?>(13).Contains(x.NationalityId))) &&
                !getEmpIdsListFromHrPayrollD.Contains(x.EmpId)
                );

                return await Result<IEnumerable<HrPreparationSalariesVw>>.SuccessAsync(getHrPreparingSalaries, "", 200);


            }
            catch (Exception ex)
            {

                return await Result<IEnumerable<HrPreparationSalariesVw>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<string>> AddNewCommissionPayroll(HrCommissionPayrollAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == entity.FacilityID);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (entity.MsMonth == "02" || entity.MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{entity.MsMonth}-{entity.FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(entity.MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{entity.MsMonth}-{entity.FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (AttStartDay != "01")
                {
                    if (entity.MsMonth != "01")
                    {
                        int PrevMonth = int.Parse(entity.MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{entity.FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(entity.FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{entity.FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttStartDay}";
                    AttEndDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttEndDay}";
                }

                long? AppID = 0;
                entity.AppTypeId ??= 0;

                var empID = Convert.ToInt64(session.EmpId);
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 742, entity.AppTypeId);
                AppID = GetApp_ID;

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var newPayrollEntity = new HrPayroll
                {
                    MsCode = getMaxCode + 1,
                    MsDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                    MsTitle = "مسير عمولات",
                    MsMonth = entity.MsMonth,
                    FinancelYear = entity.FinancelYear,
                    State = 1,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = entity.FacilityID,
                    PayrollTypeId = 2,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = entity.BRANCHID,
                    AppId = AppID

                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // End Of Add To HrPayroll


                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                foreach (var item in entity.DetailsDto)
                {
                    var empDate = getFromEmployees.Where(x => x.Id == item.EmpId).FirstOrDefault();
                    if (empDate == null)
                    {
                        continue;

                    }
                    var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                    {
                        Emp_ID = item.EmpId,
                        MS_ID = AddedPayrollEntity.MsId,
                        Absence = 0,
                        Allowance = 0,
                        Deduction = 0,
                        Delay = 0,
                        BankId = item.BankId,
                        Count_Day_Work = 0,
                        CreatedBy = session.UserId,
                        Emp_Account_No = item.AccountNo,
                        Loan = 0,
                        Salary = 0,
                        Salary_Orignal = 0,
                        Commission = item.Commission,
                        OverTime = 0,
                        Mandate = 0,
                        H_OverTime = 0,
                        Penalties = 0,
                        Net = item.Commission,
                        CMDTYPE = 1
                    };
                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(newPayrollDEntity);
                    if (PDID <= 0)
                        return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");

                }


                // End Of Add To HrPayrollD

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in addNewCommissionPayroll at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in addNewCommissionPayroll at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        /// <summary>
        /// اضافة مسير صرف مستحقات اخرى
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<IResult<string>> AddNewPaymentDues(HrPaymentDueAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var MsDate = DateHelper.StringToDate(entity.MSDate);
                string MsMonth = MsDate.Month.ToString("D2");
                int FinancelYear = MsDate.Year;
                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (MsMonth == "02" || MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (AttStartDay != "01")
                {
                    if (MsMonth != "01")
                    {
                        int PrevMonth = int.Parse(MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{FinancelYear}/{MsMonth}/{AttStartDay}";
                    AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                }
                entity.AppTypeId ??= 0;
                long? AppID = 0;
                var empID = Convert.ToInt64(session.EmpId);
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 900, entity.AppTypeId);
                AppID = GetApp_ID;

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var newPayrollEntity = new HrPayroll
                {
                    MsCode = getMaxCode + 1,
                    MsDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                    MsTitle = entity.MSTitle,
                    MsMonth = MsMonth,
                    MsMothTxt = DateHelper.GetMonthName(Convert.ToInt32(MsMonth)),
                    FinancelYear = FinancelYear,
                    State = 1,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = (int?)session.FacilityId,
                    PayrollTypeId = entity.PayrolllTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = session.BranchId,
                    AppId = AppID,
                    Posted = false

                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // End Of Add To HrPayroll


                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                foreach (var item in entity.DetailsDto)
                {
                    var empDate = getFromEmployees.Where(x => x.EmpId == item.EmpCode).FirstOrDefault();
                    if (empDate == null)
                    {
                        return await Result<string>.FailAsync($"There is No Employee With This Code {item.EmpCode}");

                    }
                    var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                    {
                        Emp_ID = empDate.Id,
                        MS_ID = AddedPayrollEntity.MsId,
                        Absence = 0,
                        Allowance = item.Amount,
                        Deduction = 0,
                        Delay = 0,
                        BankId = item.BankId,
                        Count_Day_Work = 0,
                        CreatedBy = session.UserId,
                        Emp_Account_No = item.AccountNo,
                        Loan = 0,
                        Salary = 0,
                        Salary_Orignal = 0,
                        Commission = 0,
                        OverTime = 0,
                        Mandate = 0,
                        H_OverTime = 0,
                        Penalties = 0,
                        Net = item.Amount,
                        Refrance_No = item.Id ?? 0,
                        CMDTYPE = 1
                    };
                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(newPayrollDEntity);
                    if (PDID <= 0)
                        return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");

                }
                // End Of Add To HrPayrollD
                // Begin ChangeStatus_Payroll_Trans
                var newHrPayrollNote = new HrPayrollNote
                {
                    MsId = AddedPayrollEntity.MsId,
                    StateId = entity.State,
                    Note = "",
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                };
                var AddedPayrollNoteEntity = await hrRepositoryManager.HrPayrollNoteRepository.AddAndReturn(newHrPayrollNote);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                AddedPayrollEntity.State = entity.State;
                AddedPayrollEntity.ModifiedBy = session.UserId;
                AddedPayrollEntity.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrPayrollRepository.Update(AddedPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, AddedPayrollEntity.MsId, 37);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in AddNewPaymentDues at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in AddNewPaymentDues at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public async Task<IResult<IEnumerable<HRPreparationSalariesLoanDto>>> getHR_Preparation_Salaries_Loan_SP(HRPreparationSalariesLoanFilterDto filter)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Preparation_Salaries_Loan_SP(filter);
                return await Result<IEnumerable<HRPreparationSalariesLoanDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRPreparationSalariesLoanDto>>.FailAsync($"EXP in HR_Preparation_Salaries_Loan_SP at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrPayrollApprovedDto>> BindApprovedPayroll(long msId)
        {
            try
            {

                HrPayrollApprovedDto result = new HrPayrollApprovedDto();
                var getHrPayroll = await hrRepositoryManager.HrPayrollRepository.GetOne(x => x.MsId == msId);
                if (getHrPayroll != null)
                {
                    result.MSMonth = getHrPayroll.MsMonth;
                    result.FacilityID = getHrPayroll.FacilityId;
                    result.FinancelYear = getHrPayroll.FinancelYear;
                    result.BRANCHID = getHrPayroll.BranchId;
                    var getPayrollDitems = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e =>
                e.IsDeleted == false &&
                (result.FinancelYear == null || result.FinancelYear == 0 || result.FinancelYear == e.FinancelYear) &&
                (result.FacilityID == null || result.FacilityID == 0 || result.FacilityID == e.FacilityId) &&
                (result.DeptID == null || result.DeptID == 0 || result.DeptID == e.DeptId) &&
                (result.BRANCHID == null || result.BRANCHID == 0 || result.BRANCHID == e.BranchId) &&
                (result.Location == null || result.Location == 0 || result.Location == e.Location) &&
                (result.SponsorsID == null || result.SponsorsID == 0 || result.SponsorsID == e.SponsorsId) &&
                (string.IsNullOrEmpty(result.MSMonth) || Convert.ToInt32(result.MSMonth) == Convert.ToInt32(e.MsMonth)));
                    result.hrPayrollDVws = getPayrollDitems.ToList();

                    //  Get JCode By ReferenceNo
                    var getJCode = await accRepositoryManager.AccJournalMasterRepository.GetOne(x => x.ReferenceNo == msId && x.FlagDelete == false && x.DocTypeId == 24);
                    if (getJCode != null)
                    {
                        result.TxtJCode = getJCode.JCode;

                    }
                    return await Result<HrPayrollApprovedDto>.SuccessAsync(result, "", 200);

                }
                return await Result<HrPayrollApprovedDto>.FailAsync("the Payroll No tFound");


            }
            catch (Exception exp)
            {

                return await Result<HrPayrollApprovedDto>.FailAsync($"EXP in HrPayrollService at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                ;
            }
        }

        public async Task<IResult<string>> AddNewApprovedPayroll1(HrPayrollApprovedFilterAndAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");
            try
            {
                // التأكد من مجموعة الرواتب

                var checkSalaryGroup = await hrRepositoryManager.HrSalaryGroupAccountRepository.CheckSalaryGroup(entity.MSId);
                if (!checkSalaryGroup.Succeeded || !checkSalaryGroup.Data.check)
                {
                    return await Result<string>.FailAsync("تأكد من مجموعةو الرواتب");
                }

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(entity.MSId), 24);

                if (status == 2)
                {
                    return await Result<string>.FailAsync(localization.GetMessagesResource("ThePayrollCannotBeModifiedToBePosted"));
                }
                var StatusId = await accRepositoryManager.AccFacilityRepository.GetOne(x => x.Posting, x => x.FacilityId == session.FacilityId);
                long? PeriodId = 0;
                var GetPreiodIDByDate = await accRepositoryManager.AccPeriodsRepository.GetAll(x => (x.PeriodStartDateGregorian != null && x.PeriodEndDateGregorian != null) && x.FlagDelete == false && x.PeriodState == 1 && x.FacilityId == session.FacilityId);
                if (GetPreiodIDByDate.Count() > 0)
                {
                    var filteredResult = GetPreiodIDByDate.Where(x => DateHelper.StringToDate(x.PeriodStartDateGregorian) <= DateHelper.StringToDate(entity.OperationDate) && DateHelper.StringToDate(x.PeriodEndDateGregorian) >= DateHelper.StringToDate(entity.OperationDate));
                    if (filteredResult.Count() > 0)
                    {
                        PeriodId = filteredResult.Select(x => x.PeriodId).FirstOrDefault();

                    }
                }


                var MsMonthTxt = DateHelper.GetMonthName(Convert.ToInt32(entity.MSMonth));

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var Description = $"  {localization.GetMessagesResource("Recordingsalariesformonth")} " + MsMonthTxt + $"  {localization.GetMessagesResource("Foryear")} " + entity.FinancelYear;


                // التقيد في المحاسبي
                var newACCJournalMaster = new AccJournalMaster
                {
                    JCode = "0",
                    JDateHijri = entity.OperationDate,
                    JDateGregorian = entity.OperationDate,
                    Amount = 0,
                    AmountWrite = "",
                    JDescription = Description,
                    JTime = "",
                    PaymentTypeId = 2,
                    PeriodId = PeriodId,
                    StatusId = StatusId,
                    InsertUserId = (int?)session.UserId,
                    InsertDate = DateTime.Now,
                    FinYear = session.FinYear,
                    FacilityId = session.FacilityId,
                    DocTypeId = 24,//'نوع القيد قيد الرواتب
                    ReferenceNo = entity.MSId,
                    JBian = Description,
                    ChequNo = null,
                    BankId = 0,
                    CcId = (entity.BRANCHID == 0 || entity.BRANCHID == null) ? session.BranchId : entity.BRANCHID,
                    ChequDateHijri = null,
                    CurrencyId = 1,
                    ExchangeRate = 1
                };
                var AddedACCJournalMaster = await accRepositoryManager.AccJournalMasterRepository.AddAndReturn(newACCJournalMaster);
                await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var GetHRPayrollDTrans = await hrRepositoryManager.HrPayrollDRepository.GetHrPayrollDTrans(entity.MSId, Convert.ToInt64(entity.FacilityID));
                var Facility = await accRepositoryManager.AccJournalMasterRepository.GetById(AddedACCJournalMaster.JId);
                var allACCAccounts = await accRepositoryManager.AccAccountRepository.GetAll(x => x.IsDeleted == false && x.FacilityId == Facility.FacilityId);
                foreach (var PayrollDTrans in GetHRPayrollDTrans)
                {
                    //  start of add Journal Details
                    if (PayrollDTrans.AccountID == 0)
                    {
                        return await Result<string>.FailAsync($"الحساب الموجود في طرف القيد غير موجود في قائمة الحسابات.");
                    }
                    var isAccountExist = allACCAccounts.Where(x => x.AccAccountId == PayrollDTrans.AccountID);
                    if (isAccountExist.Count() <= 0)
                    {
                        return await Result<string>.FailAsync($"الحساب الموجود في طرف القيد غير موجود في قائمة الحسابات لهذه المنشآة {PayrollDTrans.AccountID}");
                    }
                    // start of add to AccJournalMasterDetails

                    var AccountCurrency = allACCAccounts.Where(x => x.AccAccountId == PayrollDTrans.AccountID).Select(x => x.CurrencyId).FirstOrDefault();
                    // End of add to AccJournalMasterDetails
                    var newAccJournalDetaile = new AccJournalDetaile
                    {
                        JId = AddedACCJournalMaster.JId,
                        AccAccountId = PayrollDTrans.AccountID,
                        Credit = PayrollDTrans.Credit,
                        Debit = PayrollDTrans.Debit,
                        Description = PayrollDTrans.Description,
                        CreatedBy = (int?)session.UserId,
                        CreatedOn = DateTime.Now,
                        CcId = PayrollDTrans.CCId,
                        EmpId = PayrollDTrans.EmpId,
                        ReferenceNo = PayrollDTrans.ReferenceNo,
                        ReferenceTypeId = PayrollDTrans.ReferenceTypeID,
                        JDateGregorian = entity.OperationDate,
                        CurrencyId = AccountCurrency,
                        ExchangeRate = 1
                    };
                    await accRepositoryManager.AccJournalDetaileRepository.AddAndReturn(newAccJournalDetaile);
                    await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    //  End of add Journal Details add


                }
                var IsCopyfileForACC = await sysConfigurationAppHelper.GetValue(253, session.FacilityId);
                if (IsCopyfileForACC == "1")
                {
                    var getSysFiles = await mainRepositoryManager.SysFileRepository.GetAll(x => x.TableId == 37 && x.PrimaryKey == entity.MSId);
                    if (getSysFiles != null)
                    {
                        foreach (var file in getSysFiles)
                        {
                            var newJournalMasterFile = new AccJournalMasterFile
                            {
                                JId = newACCJournalMaster.JId,
                                FileName = file.FileName,
                                FileType = 0,
                                FileUrl = file.FileUrl,
                                FileExt = file.FileExt,
                                FileDescription = "",
                                FileDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                                SourceFile = "",
                                CreatedBy = session.UserId
                            };
                            await accRepositoryManager.AccJournalMasterFileRepository.AddAndReturn(newJournalMasterFile);
                            await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }
                    }
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(AddedACCJournalMaster.JId.ToString(), $"{localization.GetMessagesResource("payrollentryhasbeencreatedinFinance")}", 200);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResult<string>> ChangeStatusPayroll(ApproveRejectPayrollDto entity, int state, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity.MsId <= 0)
                {
                    return await Result<string>.FailAsync("يجب ارسال رقم المسير");
                }
                // check if payroll is found in database
                var isPayrollExist = await hrRepositoryManager.HrPayrollRepository.GetById(entity.MsId);
                if (isPayrollExist == null)
                {
                    return await Result<string>.FailAsync($"ليس هناك مسير مطابق");
                }
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var newHrPayrollNote = new HrPayrollNote
                {
                    MsId = entity.MsId,
                    StateId = state,
                    Note = entity.Note,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                };
                var AddedPayrollNoteEntity = await hrRepositoryManager.HrPayrollNoteRepository.AddAndReturn(newHrPayrollNote);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                isPayrollExist.State = state;
                isPayrollExist.ModifiedBy = session.UserId;
                isPayrollExist.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrPayrollRepository.Update(isPayrollExist);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // تحديث السلف الى مدفوعة بعد اعتماد المسير
                if (state == 4)
                {
                    var LoanInstallmentID = await hrRepositoryManager.HrLoanInstallmentPaymentRepository.GetAll(x => x.LoanInstallmentId, x => x.PayrollId == entity.MsId && x.IsDeleted == false);
                    var getLoanInstallment = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(x => LoanInstallmentID.Contains(x.Id));

                    if (getLoanInstallment.Count() > 0)
                    {
                        foreach (var singleLoanInstallment in getLoanInstallment)
                        {
                            singleLoanInstallment.IsPaid = true;
                            singleLoanInstallment.ModifiedBy = session.UserId;
                            singleLoanInstallment.ModifiedOn = DateTime.Now;
                        }
                        hrRepositoryManager.HrLoanInstallmentRepository.UpdateAll(getLoanInstallment);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }


                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, entity.MsId, 37);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                // End ChangeStatus_Payroll_Trans
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("Success"), 200);
            }
            catch (Exception)
            {

                return await Result<string>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult<string>> ChangeStatusPayroll(string msIds, int state, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(msIds))
                {
                    return await Result<string>.FailAsync("يجب ارسال رقم المسير");
                }
                // check if payroll is found in database
                var ids = msIds.Split(",");
                //var isPayrollExist = await hrRepositoryManager.HrPayrollRepository.GetById(entity.MsId);
                foreach (var id in ids)
                {
                    var isPayrollExist = await hrRepositoryManager.HrPayrollRepository.GetById(Convert.ToInt64(id));

                    if (isPayrollExist == null)
                    {
                        return await Result<string>.FailAsync($"ليس هناك مسير مطابق");
                    }
                    await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                    var newHrPayrollNote = new HrPayrollNote
                    {
                        MsId = Convert.ToInt64(id),
                        StateId = state,
                        //Note = entity.Note,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                    };
                    var AddedPayrollNoteEntity = await hrRepositoryManager.HrPayrollNoteRepository.AddAndReturn(newHrPayrollNote);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    isPayrollExist.State = state;
                    isPayrollExist.ModifiedBy = session.UserId;
                    isPayrollExist.ModifiedOn = DateTime.Now;
                    hrRepositoryManager.HrPayrollRepository.Update(isPayrollExist);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    // تحديث السلف الى مدفوعة بعد اعتماد المسير
                    if (state == 4)
                    {
                        var LoanInstallmentID = await hrRepositoryManager.HrLoanInstallmentPaymentRepository.GetAll(x => x.LoanInstallmentId, x => x.PayrollId == Convert.ToInt64(id) && x.IsDeleted == false);
                        var getLoanInstallment = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(x => LoanInstallmentID.Contains(x.Id));

                        if (getLoanInstallment.Count() > 0)
                        {
                            foreach (var singleLoanInstallment in getLoanInstallment)
                            {
                                singleLoanInstallment.IsPaid = true;
                                singleLoanInstallment.ModifiedBy = session.UserId;
                                singleLoanInstallment.ModifiedOn = DateTime.Now;
                            }
                            hrRepositoryManager.HrLoanInstallmentRepository.UpdateAll(getLoanInstallment);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("تم اعتماد مسير الراتب من قبل المدير العام"), 200);
            }
            catch (Exception)
            {

                return await Result<string>.FailAsync(localization.GetResource1("AddError"));
            }
        }


        public async Task<IResult<IEnumerable<HRPayrollAdvancedResultDto>>> HR_Payroll_Create_Advanced_Sp(HRPayrollCreateSpFilterDto filter)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_Create_Advanced_Sp(filter);
                return await Result<IEnumerable<HRPayrollAdvancedResultDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRPayrollAdvancedResultDto>>.FailAsync($"EXP in Service in  HR_Payroll_Create_Advanced_Sp at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public async Task<IResult<string>> AddNewAdvancedPayroll(HrPayrollAdvancedAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                if (entity.SpDtos.Count <= 0) return await Result<string>.FailAsync("الرجاء اختيار بيانات");

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                long? AppID = 0;

                entity.AppTypeId ??= 0;

                var empID = Convert.ToInt64(session.EmpId);

                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 168, entity.AppTypeId);
                AppID = GetApp_ID;

                //  التشييك على السالب  و التشييك على حماية الأجور ونسبة الخصم في نفس الدوارة

                string Negitive = "";
                string WagesProtection = "";
                var WagesProtectionRateBasicSalary = await sysConfigurationAppHelper.GetValue(52, (long)entity.FacilityId);
                if (string.IsNullOrEmpty(WagesProtectionRateBasicSalary)) WagesProtectionRateBasicSalary = "100";

                foreach (var item in entity.SpDtos)
                {
                    if (item.Salary + item.Allowance + item.OverTime + item.Commission - item.Absence - item.DelayHourByDay - item.Loan - item.Deduction - item.SocialInsurance - item.Penalties < 0)
                        Negitive += item.Emp_ID + ",";
                    if ((item.Absence + item.DelayHourByDay + item.Loan + item.Deduction + item.SocialInsurance + item.Penalties) > (item.BasicSalary * Convert.ToDecimal(WagesProtectionRateBasicSalary) / 100))
                        WagesProtection += item.Emp_ID + ",";
                }
                if (Negitive.Length > 0) return await Result<string>.FailAsync($"{localization.GetHrResource("NegitaveSalaryIssue")} \n {Negitive.TrimEnd(',')}");
                if (WagesProtection.Length > 0) return await Result<string>.FailAsync($"{localization.GetHrResource("OverDeductionIssue")} \n {WagesProtection.TrimEnd(',')}");
                int Digit = 2;
                var DigitProperty = await sysConfigurationAppHelper.GetValue(71, (long)entity.FacilityId);
                if (Convert.ToInt32(DigitProperty) > 0) Digit = Convert.ToInt32(DigitProperty);


                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == entity.FacilityId);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (entity.MsMonth == "02" || entity.MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{entity.MsMonth}-{entity.FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(entity.MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{entity.MsMonth}-{entity.FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (AttStartDay != "01")
                {
                    if (entity.MsMonth != "01")
                    {
                        int PrevMonth = int.Parse(entity.MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{entity.FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(entity.FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{entity.FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttStartDay}";
                    AttEndDate = $"{entity.FinancelYear}/{entity.MsMonth}/{AttEndDay}";
                }
                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var theCode = (getMaxCode ?? 0) + 1;


                var newPayrollEntity = new HrPayroll
                {
                    MsCode = theCode,
                    MsDate = entity.MsDate,
                    MsTitle = entity.MsTitle,
                    MsMonth = entity.MsMonth,
                    MsMothTxt = DateHelper.GetMonthName(Convert.ToInt32(entity.MsMonth)),
                    FinancelYear = entity.FinancelYear,
                    State = entity.State,
                    CreatedBy = session.UserId,
                    FacilityId = entity.FacilityId,
                    PayrollTypeId = entity.PayrollTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = entity.BranchId ?? 0,
                    AppId = AppID,
                    Posted = false

                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // End Of Add To HrPayroll


                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                var getFromHrAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(x => x.IsDeleted == false && x.FixedOrTemporary == 1);
                foreach (var item in entity.SpDtos)
                {
                    var empDate = getFromEmployees.Where(x => x.Id == item.ID).FirstOrDefault();
                    if (empDate == null)
                    {
                        return await Result<string>.FailAsync($"the employee of Id : {item.Emp_ID} not found");

                    }
                    var Net = (item.Salary ?? 0) + (item.Allowance ?? 0) + (item.OverTime ?? 0) + (item.Commission ?? 0) - (item.Absence ?? 0) - (item.DelayHourByDay ?? 0) - (item.Loan ?? 0) - (item.Deduction ?? 0) - (item.SocialInsurance ?? 0) - (item.Penalties ?? 0) - (item.IncomeTax ?? 0);

                    var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                    {
                        MS_ID = AddedPayrollEntity.MsId,
                        Emp_ID = item.ID,
                        Absence = item.Absence ?? 0,
                        Allowance = item.Allowance ?? 0,
                        Deduction = item.Deduction ?? 0,
                        Delay = item.Delay ?? 0,
                        BankId = item.Bank_ID ?? 0,
                        Count_Day_Work = item.Attendance ?? 0,
                        Emp_Account_No = item.Account_No ?? "",
                        Loan = item.Loan ?? 0,
                        Salary = item.Salary ?? 0,
                        Commission = item.Commission ?? 0,
                        OverTime = item.OverTime ?? 0,
                        Mandate = item.Mandate ?? 0,
                        H_OverTime = item.H_OverTime ?? 0,
                        Penalties = item.Penalties ?? 0,
                        Net = Net,
                        CreatedBy = session.UserId,
                        CMDTYPE = 1
                    };
                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(newPayrollDEntity);
                    if (PDID <= 0)
                        return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");
                    var getPDID = hrRepositoryManager.HrPayrollDRepository.Entities.Max(x => x.MsdId);
                    PDID = getPDID;
                    //// this code for add allowance and deduction 

                    var getDeductionData = await hrRepositoryManager.HrDeductionVwRepository.GetDeductionFixedAndTemporary(item.ID, entity.FinancelYear, entity.MsMonth, item.Attendance);
                    if (getDeductionData.Count() > 0)
                    {
                        foreach (var singleDeductionData in getDeductionData)
                        {
                            var newductionEntity = new HrPayrollAllowanceDeduction
                            {
                                AdId = singleDeductionData.AdId,
                                MsId = AddedPayrollEntity.MsId,
                                AdValue = Math.Round((decimal)singleDeductionData.Amount, Digit),
                                AdValueOrignal = singleDeductionData.OriginalAmount,
                                Debit = Math.Round((decimal)singleDeductionData.Amount, Digit),
                                Credit = 0,
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,
                                FixedOrTemporary = singleDeductionData.FixedOrTemporary,
                                MsdId = PDID

                            };
                            var AddDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(newductionEntity);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }


                    var getAllowanceData = await hrRepositoryManager.HrAllowanceVwRepository.GetAllowanceFixedAndTemporary(item.ID, entity.FinancelYear, entity.MsMonth, item.Attendance);
                    if (getAllowanceData.Count() > 0)
                    {
                        foreach (var singleAllowanceData in getAllowanceData)
                        {
                            var newductionEntity = new HrPayrollAllowanceDeduction
                            {
                                AdId = singleAllowanceData.AdId,
                                MsId = AddedPayrollEntity.MsId,
                                AdValue = Math.Round((decimal)singleAllowanceData.Amount, Digit),
                                AdValueOrignal = singleAllowanceData.OriginalAmount,
                                Debit = Math.Round((decimal)singleAllowanceData.Amount, Digit),
                                Credit = 0,
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,
                                FixedOrTemporary = singleAllowanceData.FixedOrTemporary,
                                MsdId = PDID
                            };
                            var AddDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(newductionEntity);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }


                }


                // End Of Add To HrPayrollD

                // Begin ChangeStatus_Payroll_Trans



                var newHrPayrollNote = new HrPayrollNote
                {
                    MsId = AddedPayrollEntity.MsId,
                    StateId = 1,
                    Note = "",
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                };
                var AddedPayrollNoteEntity = await hrRepositoryManager.HrPayrollNoteRepository.AddAndReturn(newHrPayrollNote);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // تحديث السلف الى مدفوعة بعد اعتماد المسير
                if (entity.State == 4)
                {
                    var LoanInstallmentID = await hrRepositoryManager.HrLoanInstallmentPaymentRepository.GetAll(x => x.Id, x => x.PayrollId == AddedPayrollEntity.MsId && x.IsDeleted == false);
                    var getLoanInstallment = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(x => LoanInstallmentID.Contains(x.Id));

                    if (getLoanInstallment.Count() > 0)
                    {
                        foreach (var singleLoanInstallment in getLoanInstallment)
                        {
                            singleLoanInstallment.IsPaid = true;
                            singleLoanInstallment.ModifiedBy = session.UserId;
                            singleLoanInstallment.ModifiedOn = DateTime.Now;
                        }
                        hrRepositoryManager.HrLoanInstallmentRepository.UpdateAll(getLoanInstallment);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                // End ChangeStatus_Payroll_Trans



                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in addNewPayroll at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in addNewPayroll at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        #region مسير خارج دوام


        /// <summary>
        /// اضافة مسير خارج دوام
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<IResult<string>> PayrollOverTimeAdd(PayrollOverTimeAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var MsDate = DateHelper.StringToDate(entity.MSDate);
                string MsMonth = MsDate.Month.ToString("D2");
                int FinancelYear = MsDate.Year;
                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (MsMonth == "02" || MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (AttStartDay != "01")
                {
                    if (MsMonth != "01")
                    {
                        int PrevMonth = int.Parse(MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{FinancelYear}/{MsMonth}/{AttStartDay}";
                    AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                }
                entity.AppTypeId ??= 0;
                long? AppID = 0;
                var empID = Convert.ToInt64(session.EmpId);
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 900, entity.AppTypeId);
                AppID = GetApp_ID;

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var newPayrollEntity = new HrPayroll
                {
                    MsCode = getMaxCode + 1,
                    MsDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                    MsTitle = entity.MSTitle,
                    MsMonth = MsMonth,
                    MsMothTxt = DateHelper.GetMonthName(Convert.ToInt32(MsMonth)),
                    FinancelYear = FinancelYear,
                    State = entity.State,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = (int?)session.FacilityId,
                    PayrollTypeId = entity.PayrolllTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = session.BranchId,
                    AppId = AppID,
                    Posted = false

                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // End Of Add To HrPayroll


                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                foreach (var item in entity.DetailsDto)
                {
                    var empDate = getFromEmployees.Where(x => x.EmpId == item.EmpCode).FirstOrDefault();
                    if (empDate == null)
                    {
                        return await Result<string>.FailAsync($"There is No Employee With This Code {item.EmpCode}");

                    }
                    var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                    {
                        Emp_ID = empDate.Id,
                        MS_ID = AddedPayrollEntity.MsId,
                        Absence = 0,
                        Allowance = 0,
                        Deduction = 0,
                        Delay = 0,
                        BankId = item.BankId,
                        Count_Day_Work = 0,
                        CreatedBy = session.UserId,
                        Emp_Account_No = item.AccountNo,
                        Loan = 0,
                        Salary = 0,
                        Salary_Orignal = 0,
                        Commission = 0,
                        OverTime = item.Net,
                        Mandate = 0,
                        H_OverTime = item.Cnt,
                        Penalties = 0,
                        Net = item.Net,
                        Refrance_No = item.Id ?? 0,
                        CMDTYPE = 1
                    };
                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(newPayrollDEntity);
                    if (PDID <= 0)
                        return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");

                }
                // End Of Add To HrPayrollD
                // Begin ChangeStatus_Payroll_Trans
                var newHrPayrollNote = new HrPayrollNote
                {
                    MsId = AddedPayrollEntity.MsId,
                    StateId = entity.State,
                    Note = "",
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                };
                var AddedPayrollNoteEntity = await hrRepositoryManager.HrPayrollNoteRepository.AddAndReturn(newHrPayrollNote);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                AddedPayrollEntity.State = entity.State;
                AddedPayrollEntity.ModifiedBy = session.UserId;
                AddedPayrollEntity.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrPayrollRepository.Update(AddedPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, AddedPayrollEntity.MsId, 37);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in PayrollOverTimeAdd at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        #endregion

        #region مسير بدل سكن


        /// <summary>
        /// اضافة مسير بدل سكن
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<IResult<string>> PayrollHousingAllowanceAdd(PayrollHousingAllowancesAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var MsDate = DateHelper.StringToDate(entity.MSDate);
                string MsMonth = MsDate.Month.ToString("D2");
                int FinancelYear = MsDate.Year;
                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (MsMonth == "02" || MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (AttStartDay != "01")
                {
                    if (MsMonth != "01")
                    {
                        int PrevMonth = int.Parse(MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{FinancelYear}/{MsMonth}/{AttStartDay}";
                    AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                }
                entity.AppTypeId ??= 0;
                long? AppID = 0;
                var empID = Convert.ToInt64(session.EmpId);
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 900, entity.AppTypeId);
                AppID = GetApp_ID;

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var newPayrollEntity = new HrPayroll
                {
                    MsCode = getMaxCode + 1,
                    MsDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                    MsTitle = entity.MSTitle,
                    MsMonth = MsMonth,
                    MsMothTxt = DateHelper.GetMonthName(Convert.ToInt32(MsMonth)),
                    FinancelYear = FinancelYear,
                    State = entity.State,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = (int?)session.FacilityId,
                    PayrollTypeId = entity.PayrolllTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = session.BranchId,
                    AppId = AppID,
                    Posted = false

                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // End Of Add To HrPayroll


                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                foreach (var item in entity.DetailsDto)
                {
                    var empDate = getFromEmployees.Where(x => x.EmpId == item.EmpCode).FirstOrDefault();
                    if (empDate == null)
                    {
                        return await Result<string>.FailAsync($"There is No Employee With This Code {item.EmpCode}");

                    }
                    var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                    {
                        Emp_ID = empDate.Id,
                        MS_ID = AddedPayrollEntity.MsId,
                        Absence = 0,
                        Allowance = item.Net,
                        Deduction = 0,
                        Delay = 0,
                        BankId = empDate.BankId,
                        Count_Day_Work = 0,
                        CreatedBy = session.UserId,
                        Emp_Account_No = empDate.AccountNo,
                        Loan = 0,
                        Salary = 0,
                        Salary_Orignal = 0,
                        Commission = 0,
                        OverTime = 0,
                        Mandate = 0,
                        H_OverTime = 0,
                        Penalties = 0,
                        Net = item.Net,
                        Refrance_No = 0,
                        CMDTYPE = 1
                    };
                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(newPayrollDEntity);
                    if (PDID <= 0)
                        return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");
                    var getPDID = hrRepositoryManager.HrPayrollDRepository.Entities.Max(x => x.MsdId);
                    PDID = getPDID;

                    long? AdId = 0;
                    if (getFromHrSetting != null)
                    {
                        AdId = getFromHrSetting.HousingAllowance;

                    }
                    // this code for add allowance 
                    var newPSDeductionEntity = new HrPayrollAllowanceDeduction
                    {
                        AdId = AdId,
                        MsId = AddedPayrollEntity.MsId,
                        AdValue = item.Net,
                        AdValueOrignal = item.Net,
                        Debit = 0,
                        Credit = item.Net,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        FixedOrTemporary = 2,
                        MsdId = PDID

                    };
                    var AddPSAllowanceDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(newPSDeductionEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }
                // End Of Add To HrPayrollD

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, AddedPayrollEntity.MsId, 37);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in PayrollOverTimeAdd at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        #endregion


        #region    مسير انتداب


        /// <summary>
        /// اضافة مسير انتداب
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<IResult<string>> PayrollMandateAdd(PayrollMandateAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var MsDate = DateHelper.StringToDate(entity.MSDate);
                string MsMonth = MsDate.Month.ToString("D2");
                int FinancelYear = MsDate.Year;
                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (MsMonth == "02" || MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (AttStartDay != "01")
                {
                    if (MsMonth != "01")
                    {
                        int PrevMonth = int.Parse(MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{FinancelYear}/{MsMonth}/{AttStartDay}";
                    AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                }
                entity.AppTypeId ??= 0;
                long? AppID = 0;
                var empID = Convert.ToInt64(session.EmpId);
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 900, entity.AppTypeId);
                AppID = GetApp_ID;

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var newPayrollEntity = new HrPayroll
                {
                    MsCode = getMaxCode + 1,
                    MsDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                    MsTitle = entity.MSTitle,
                    MsMonth = MsMonth,
                    MsMothTxt = DateHelper.GetMonthName(Convert.ToInt32(MsMonth)),
                    FinancelYear = FinancelYear,
                    State = entity.State,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = (int?)session.FacilityId,
                    PayrollTypeId = entity.PayrolllTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = session.BranchId,
                    AppId = AppID,
                    Posted = false

                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // End Of Add To HrPayroll


                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                var getFromMandate = await hrRepositoryManager.HrMandateRepository.GetAll(x => x.IsDeleted == false);

                foreach (var item in entity.DetailsDto)
                {
                    var MandateData = getFromMandate.Where(x => x.Id == item).FirstOrDefault();
                    if (MandateData == null)
                        return await Result<string>.FailAsync($"There is No Mandate Data With This Code {item}");
                    var empDate = getFromEmployees.Where(x => x.Id == MandateData.EmpId).FirstOrDefault();
                    if (empDate == null)
                        return await Result<string>.FailAsync($"There is No Employee With This Id {MandateData.EmpId}");

                    var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                    {
                        Emp_ID = MandateData.EmpId,
                        MS_ID = AddedPayrollEntity.MsId,
                        Absence = 0,
                        Allowance = MandateData.TransportAmount,
                        Deduction = 0,
                        Delay = 0,
                        BankId = empDate.BankId,
                        Count_Day_Work = MandateData.NoOfNight,
                        CreatedBy = session.UserId,
                        Emp_Account_No = empDate.AccountNo,
                        Loan = 0,
                        Salary = 0,
                        Salary_Orignal = 0,
                        Commission = 0,
                        OverTime = 0,
                        Mandate = (MandateData.ActualExpenses - MandateData.TransportAmount),
                        H_OverTime = 0,
                        Penalties = 0,
                        Net = (MandateData.ActualExpenses - MandateData.TransportAmount),
                        Refrance_No = MandateData.Id,
                        Note = MandateData.Note,
                        CMDTYPE = 1
                    };
                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(newPayrollDEntity);
                    if (PDID <= 0)
                        return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");
                    var getPDID = hrRepositoryManager.HrPayrollDRepository.Entities.Max(x => x.MsdId);
                    PDID = getPDID;

                    long? AdId = 0;
                    if (getFromHrSetting != null)
                    {
                        AdId = getFromHrSetting.TransportAllowance;

                    }
                    // this code for add allowance 
                    var newPSDeductionEntity = new HrPayrollAllowanceDeduction
                    {
                        AdId = AdId,
                        MsId = AddedPayrollEntity.MsId,
                        AdValue = MandateData.TransportAmount,
                        AdValueOrignal = MandateData.TransportAmount,
                        Debit = 0,
                        Credit = MandateData.TransportAmount,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        FixedOrTemporary = 1,
                        MsdId = PDID

                    };
                    var AddPSAllowanceDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(newPSDeductionEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }
                // End Of Add To HrPayrollD

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, AddedPayrollEntity.MsId, 37);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in PayrollOverTimeAdd at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        #endregion




        #region   مسير تذاكر مستحقة


        /// <summary>
        /// اضافة مسير تذاكر مستحقة
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<IResult<string>> PayrollTicketAllowanceAdd(PayrollTicketAllowancesAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var MsDate = DateHelper.StringToDate(entity.MSDate);
                string MsMonth = MsDate.Month.ToString("D2");
                int FinancelYear = MsDate.Year;
                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (MsMonth == "02" || MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (AttStartDay != "01")
                {
                    if (MsMonth != "01")
                    {
                        int PrevMonth = int.Parse(MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{FinancelYear}/{MsMonth}/{AttStartDay}";
                    AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                }
                entity.AppTypeId ??= 0;
                long? AppID = 0;
                var empID = Convert.ToInt64(session.EmpId);
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 900, entity.AppTypeId);
                AppID = GetApp_ID;

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var newPayrollEntity = new HrPayroll
                {
                    MsCode = getMaxCode + 1,
                    MsDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                    MsTitle = entity.MSTitle,
                    MsMonth = MsMonth,
                    MsMothTxt = DateHelper.GetMonthName(Convert.ToInt32(MsMonth)),
                    FinancelYear = FinancelYear,
                    State = entity.State,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = (int?)session.FacilityId,
                    PayrollTypeId = entity.PayrolllTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = session.BranchId,
                    AppId = AppID,
                    Posted = false

                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // End Of Add To HrPayroll


                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                foreach (var item in entity.DetailsDto)
                {
                    var empDate = getFromEmployees.Where(x => x.EmpId == item.EmpCode).FirstOrDefault();
                    if (empDate == null)
                    {
                        return await Result<string>.FailAsync($"There is No Employee With This Code {item.EmpCode}");

                    }
                    var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                    {
                        Emp_ID = empDate.Id,
                        MS_ID = AddedPayrollEntity.MsId,
                        Absence = 0,
                        Allowance = item.Net,
                        Deduction = 0,
                        Delay = 0,
                        BankId = empDate.BankId,
                        Count_Day_Work = 0,
                        CreatedBy = session.UserId,
                        Emp_Account_No = empDate.AccountNo,
                        Loan = 0,
                        Salary = 0,
                        Salary_Orignal = 0,
                        Commission = 0,
                        OverTime = 0,
                        Mandate = 0,
                        H_OverTime = 0,
                        Penalties = 0,
                        Net = item.Net,
                        Refrance_No = 0,
                        CMDTYPE = 1
                    };
                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(newPayrollDEntity);
                    if (PDID <= 0)
                        return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");
                    var getPDID = hrRepositoryManager.HrPayrollDRepository.Entities.Max(x => x.MsdId);
                    PDID = getPDID;

                    long? AdId = 0;
                    if (getFromHrSetting != null)
                    {
                        AdId = getFromHrSetting.TicketAllowance;

                    }
                    // this code for add allowance 
                    var newPSDeductionEntity = new HrPayrollAllowanceDeduction
                    {
                        AdId = AdId,
                        MsId = AddedPayrollEntity.MsId,
                        AdValue = item.Net,
                        AdValueOrignal = item.Net,
                        Debit = 0,
                        Credit = item.Net,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        FixedOrTemporary = 2,
                        MsdId = PDID

                    };
                    var AddPSAllowanceDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(newPSDeductionEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }
                // End Of Add To HrPayrollD

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, AddedPayrollEntity.MsId, 37);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in PayrollOverTimeAdd at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        #endregion



        #region مسير خارج دوام يدوي


        /// <summary>
        ///  مسير خارج دوام يدوي
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<IResult<string>> PayrollOverTime2Add(PayrollOverTime2AddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var MsDate = DateHelper.StringToDate(entity.MSDate);
                string MsMonth = MsDate.Month.ToString("D2");
                int FinancelYear = MsDate.Year;
                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (MsMonth == "02" || MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (AttStartDay != "01")
                {
                    if (MsMonth != "01")
                    {
                        int PrevMonth = int.Parse(MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{FinancelYear}/{MsMonth}/{AttStartDay}";
                    AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                }
                entity.AppTypeId ??= 0;
                long? AppID = 0;
                var empID = Convert.ToInt64(session.EmpId);
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 900, entity.AppTypeId);
                AppID = GetApp_ID;

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var newPayrollEntity = new HrPayroll
                {
                    MsCode = getMaxCode + 1,
                    MsDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                    MsTitle = entity.MSTitle,
                    MsMonth = MsMonth,
                    MsMothTxt = DateHelper.GetMonthName(Convert.ToInt32(MsMonth)),
                    FinancelYear = FinancelYear,
                    State = entity.State,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = (int?)session.FacilityId,
                    PayrollTypeId = entity.PayrolllTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = session.BranchId,
                    AppId = AppID,
                    Posted = false

                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // End Of Add To HrPayroll


                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                foreach (var item in entity.DetailsDto)
                {
                    var empData = getFromEmployees.Where(x => x.EmpId == item.EmpCode).FirstOrDefault();
                    if (empData == null)
                    {
                        return await Result<string>.FailAsync($"There is No Employee With This Code {item.EmpCode}");

                    }
                    var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                    {
                        Emp_ID = empData.Id,
                        MS_ID = AddedPayrollEntity.MsId,
                        Absence = 0,
                        Allowance = 0,
                        Deduction = 0,
                        Delay = 0,
                        BankId = empData.BankId,
                        Count_Day_Work = item.WorkDaysCount,
                        CreatedBy = session.UserId,
                        Emp_Account_No = empData.AccountNo,
                        Loan = 0,
                        Salary = 0,
                        Salary_Orignal = empData.Salary,
                        Commission = 0,
                        OverTime = item.Net,
                        Mandate = 0,
                        H_OverTime = item.HOverTime,
                        Penalties = 0,
                        Net = item.Net,
                        Refrance_No = 0,
                        CMDTYPE = 1
                    };
                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(newPayrollDEntity);
                    if (PDID <= 0)
                        return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");

                }
                // End Of Add To HrPayrollD
                // Begin ChangeStatus_Payroll_Trans
                var newHrPayrollNote = new HrPayrollNote
                {
                    MsId = AddedPayrollEntity.MsId,
                    StateId = entity.State,
                    Note = "",
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                };
                var AddedPayrollNoteEntity = await hrRepositoryManager.HrPayrollNoteRepository.AddAndReturn(newHrPayrollNote);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                AddedPayrollEntity.State = entity.State;
                AddedPayrollEntity.ModifiedBy = session.UserId;
                AddedPayrollEntity.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrPayrollRepository.Update(AddedPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, AddedPayrollEntity.MsId, 37);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in PayrollOverTime2Add at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        #endregion





        #region   مسير دوام مرن


        /// <summary>
        /// اضافة مسير دوام مرن
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<IResult<string>> PayrollFlexibleWorkingAdd(PayrollFlexibleWorkingAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var MsDate = DateHelper.StringToDate(entity.MSDate);
                string MsMonth = MsDate.Month.ToString("D2");
                int FinancelYear = MsDate.Year;
                // Begin Of Add To HrPayroll
                string? AttEndDate;
                string? AttStartDate;
                string? AttStartDay;
                string? AttEndDay;
                string? PrevMonthStr;
                string? PrevFinancelYearStr;
                //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
                var getFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                AttStartDay = getFromHrSetting.MonthStartDay;
                AttEndDay = getFromHrSetting.MonthEndDay;
                //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

                if (new[] { "29", "30", "31" }.Contains(AttEndDay))
                {
                    if (MsMonth == "02" || MsMonth == "2")
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }
                if (AttEndDay == "31")
                {
                    if (new[] { "04", "06", "09", "11" }.Contains(MsMonth))
                    {
                        DateTime date = DateTime.ParseExact($"01-{MsMonth}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                        AttEndDay = previousMonthEndDate.ToString("dd");
                    }
                }


                if (AttStartDay != "01")
                {
                    if (MsMonth != "01")
                    {
                        int PrevMonth = int.Parse(MsMonth) - 1;
                        PrevMonthStr = PrevMonth.ToString("D2");
                        AttStartDate = $"{FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(FinancelYear.ToString()) - 1;
                        PrevFinancelYearStr = PrevFinancelYear.ToString();
                        AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                        AttEndDate = $"{FinancelYear}/01/{AttEndDay}";
                    }
                }
                else
                {
                    AttStartDate = $"{FinancelYear}/{MsMonth}/{AttStartDay}";
                    AttEndDate = $"{FinancelYear}/{MsMonth}/{AttEndDay}";
                }
                entity.AppTypeId ??= 0;
                long? AppID = 0;
                var empID = Convert.ToInt64(session.EmpId);
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 900, entity.AppTypeId);
                AppID = GetApp_ID;

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var newPayrollEntity = new HrPayroll
                {
                    MsCode = getMaxCode + 1,
                    MsDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                    MsTitle = entity.MSTitle,
                    MsMonth = MsMonth,
                    MsMothTxt = DateHelper.GetMonthName(Convert.ToInt32(MsMonth)),
                    FinancelYear = FinancelYear,
                    State = entity.State,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = (int?)session.FacilityId,
                    PayrollTypeId = entity.PayrolllTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = session.BranchId,
                    AppId = AppID,
                    Posted = false

                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // End Of Add To HrPayroll


                // Begin Of Add To HrPayrollD
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                var getFromFlexibleWorking = await hrRepositoryManager.HrFlexibleWorkingRepository.GetAllFromView(x => x.IsDeleted == false);

                foreach (var item in entity.DetailsDto)
                {
                    var FlexibleWorkingData = getFromFlexibleWorking.Where(x => x.Id == item).FirstOrDefault();
                    if (FlexibleWorkingData == null)
                        return await Result<string>.FailAsync($"There is No Flexible Working for this Id {item}");
                    var empData = getFromEmployees.Where(x => x.Id == FlexibleWorkingData.EmpId).FirstOrDefault();
                    if (empData == null)
                        return await Result<string>.FailAsync($"There is No Employee With This Id {empData.EmpId}");

                    var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                    {
                        Emp_ID = FlexibleWorkingData.EmpId,
                        MS_ID = AddedPayrollEntity.MsId,
                        Absence = 0,
                        Allowance = 0,
                        Deduction = 0,
                        Delay = 0,
                        BankId = empData.BankId,
                        Count_Day_Work = 0,
                        CreatedBy = session.UserId,
                        Emp_Account_No = empData.AccountNo,
                        Loan = 0,
                        Salary = 0,
                        Salary_Orignal = 0,
                        Commission = 0,
                        OverTime = FlexibleWorkingData.TotalPrice,
                        Mandate = 0,
                        H_OverTime = FlexibleWorkingData.ActualMinute,
                        Penalties = 0,
                        Net = FlexibleWorkingData.TotalPrice,
                        Refrance_No = FlexibleWorkingData.Id,
                        Note = "",
                        CMDTYPE = 1
                    };
                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(newPayrollDEntity);
                    if (PDID <= 0)
                        return await Result<string>.FailAsync($"{localization.GetResource1("AddError")}");
                    var getPDID = hrRepositoryManager.HrPayrollDRepository.Entities.Max(x => x.MsdId);
                    PDID = getPDID;

                }
                // End Of Add To HrPayrollD

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, AddedPayrollEntity.MsId, 37);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in PayrollOverTimeAdd at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        //public Task<IResult<IEnumerable<HRPayrollManuallCreateSpDto>>> getHR_Payroll_Create2_Sp(HRPayrollCreate2SpFilterDto filter)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IResult<string>> AddNewPayroll(HrPayrollAddDto entity, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IResult> Remove(long Id, int stateId, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IResult<IEnumerable<HRPayrollCreate2SpDto>>> getHR_Payroll_Create_Sp(HRPayrollCreateSpFilterDto filter)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IResult<HrPayrollDto>> Add(HrPayrollDto entity, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IResult<HrPayrollEditDto>> Update(HrPayrollEditDto entity, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}


        #endregion

    }


}