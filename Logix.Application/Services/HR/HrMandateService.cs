using System.Globalization;
using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.Hr;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrMandateService : GenericQueryService<HrMandate, HrMandateDto, HrMandateVw>, IHrMandateService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IWorkflowHelper workflowHelper;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrMandateService(IQueryRepository<HrMandate> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.workflowHelper = workflowHelper;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrMandateDto>> Add(HrMandateDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrMandateRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrMandateDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateHelper.GetCurrentDateTime();
                item.IsDeleted = true;

                hrRepositoryManager.HrMandateRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrMandateDto>.SuccessAsync(_mapper.Map<HrMandateDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrMandateDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrMandateRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrMandateDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrMandateRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrMandateDto>.SuccessAsync(_mapper.Map<HrMandateDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrMandateDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrMandateEditDto>> Update(HrMandateEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrMandateEditDto>.FailAsync("Null Entity");
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrMandateEditDto>.FailAsync($"Employee Code Is Required");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrMandateEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrMandateRepository.GetById(entity.Id);

                if (item == null) return await Result<HrMandateEditDto>.FailAsync("the Record Is Not Found");

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                _mapper.Map(entity, item);
                item.EmpId = checkEmpExist.Id;
                int nightCounts = ((DateHelper.StringToDate(entity.ToDate) - DateHelper.StringToDate(entity.FromDate)).Days);
                nightCounts = nightCounts < 0 ? 0 : nightCounts;
                var ActualExpenses = (nightCounts * entity.RatePerNight) + entity.TicketValue + entity.OtherExpenses + entity.TransportAmount;
                item.NoOfNight = nightCounts;
                item.ActualExpenses = ActualExpenses;
                hrRepositoryManager.HrMandateRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.fileDtos != null && entity.fileDtos.Any())
                {
                    await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, item.Id, 57, cancellationToken);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrMandateEditDto>.SuccessAsync(_mapper.Map<HrMandateEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrMandateEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<bool>> AddNewMandate(HrMandateAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null) return await Result<bool>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<bool>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmpExist.StatusId == 2) return await Result<bool>.FailAsync(localization.GetHrResource("EmpNotActive"));

                DateTime fromDate = DateHelper.StringToDate(entity.FromDate);
                DateTime toDate = DateHelper.StringToDate(entity.ToDate);
                //   Check employee in Mandate
                var CheckEmployeeMandate = await hrRepositoryManager.HrMandateRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id
                && !string.IsNullOrEmpty(x.FromDate) && !string.IsNullOrEmpty(x.ToDate));
                if (CheckEmployeeMandate.Any())
                {
                    var resultFilter = CheckEmployeeMandate.Where(x =>
                    ((fromDate >= DateHelper.StringToDate(x.FromDate) && fromDate <= DateHelper.StringToDate(x.ToDate)) || (toDate >= DateHelper.StringToDate(x.FromDate) && toDate <= DateHelper.StringToDate(x.ToDate)))
                    ||
                    ((DateHelper.StringToDate(x.FromDate) >= fromDate && DateHelper.StringToDate(x.FromDate) <= toDate) || (DateHelper.StringToDate(x.ToDate) >= fromDate && DateHelper.StringToDate(x.ToDate) <= toDate)));

                    if (resultFilter.Any())
                    {
                        return await Result<bool>.FailAsync(" لايمكن عمل انتداب  للموظف بسب وجود انتداب بنفس الفترة  ");
                    }
                }

                //   Check employee in vacations
                var CheckEmployeeVacations = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                if (CheckEmployeeVacations.Any())
                {
                    var resultFilter = CheckEmployeeVacations.Where(x => x.VacationSdate != null && x.VacationEdate != null &&
                    ((fromDate >= DateHelper.StringToDate(x.VacationSdate) && fromDate <= DateHelper.StringToDate(x.VacationEdate)) || (toDate >= DateHelper.StringToDate(x.VacationSdate) && toDate <= DateHelper.StringToDate(x.VacationEdate))) ||
                    ((DateHelper.StringToDate(x.VacationSdate) >= fromDate && DateHelper.StringToDate(x.VacationSdate) <= toDate) || (DateHelper.StringToDate(x.VacationEdate) >= fromDate && DateHelper.StringToDate(x.VacationEdate) <= toDate)));
                    if (resultFilter.Any())
                    {
                        return await Result<bool>.FailAsync(" لايمكن عمل انتداب  للموظف بسب وجود اجازة بنفس الفترة  ");
                    }
                }

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                entity.AppTypeId ??= 0;
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(checkEmpExist.Id, 593, entity.AppTypeId);
                long appId = GetApp_ID;
                int nightCounts = (toDate - fromDate).Days;
                nightCounts = nightCounts < 0 ? 0 : nightCounts;
                var ActualExpenses = (nightCounts * entity.RatePerNight) + entity.TicketValue + entity.OtherExpenses + entity.TransportAmount;

                var hrCompensatoryVacation = new HrMandate
                {
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    EmpId = checkEmpExist.Id,
                    IsDeleted = false,
                    AppId = appId,
                    FromLocation = entity.FromLocation,
                    ToLocation = entity.ToLocation,
                    Objective = entity.Objective,
                    VisaTravel = entity.VisaTravel,
                    TravelBy = entity.TravelBy,
                    Accommodation = entity.Accommodation,
                    NoOfNight = nightCounts,
                    RatePerNight = entity.RatePerNight,
                    OtherExpenses = entity.OtherExpenses,
                    ActualExpenses = ActualExpenses,
                    Note = entity.Note,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate,
                    TypeId = entity.TypeId,
                    TicketType = entity.TicketType,
                    TicketValue = entity.TicketValue,
                    TransportAmount = entity.TransportAmount,
                    CatId = entity.CatId,
                };
                var newEntity = await hrRepositoryManager.HrMandateRepository.AddAndReturn(hrCompensatoryVacation);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.fileDtos != null && entity.fileDtos.Any())
                {
                    await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 57, cancellationToken);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<bool>.SuccessAsync(true, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<bool>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult<string>> PayrollAdd(HRMandatePayrollAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                // CHECK MANDATE eXIST 
                var item = await hrRepositoryManager.HrMandateRepository.GetOneVw(x => x.Id == entity.Id && x.IsDeleted == false);
                if (item == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");

                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.Id == item.EmpId && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                int BankId = item.BankId ?? 0;

                var MsDate = DateHelper.StringToDate(entity.PayDate);
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

                var getMaxCode = hrRepositoryManager.HrPayrollRepository.Entities.Max(x => x.MsCode);
                var newPayrollEntity = new HrPayroll
                {
                    MsCode = getMaxCode + 1,
                    MsDate = entity.PayDate,
                    MsTitle = localization.GetHrResource("MandatePayroll"),
                    MsMonth = MsMonth,
                    MsMothTxt = DateHelper.GetMonthName(Convert.ToInt32(MsMonth)),
                    FinancelYear = FinancelYear,
                    State = entity.State,
                    CreatedBy = session.UserId,
                    CreatedOn = DateHelper.GetCurrentDateTime(),
                    FacilityId = (int?)session.FacilityId,
                    PayrollTypeId = entity.PayrolllTypeId,
                    StartDate = AttStartDate,
                    EndDate = AttEndDate,
                    BranchId = session.BranchId,
                    AppId = 0,
                    Posted = false
                };

                var AddedPayrollEntity = await hrRepositoryManager.HrPayrollRepository.AddAndReturn(newPayrollEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                // End Of Add To HrPayroll

                // Begin Of Add To HrPayrollD
                var Allowance = item.TransportAmount ?? 0;

                var newPayrollDEntity = new HRPayrollDStoredProcedureDto
                {
                    Emp_ID = item.EmpId,
                    MS_ID = AddedPayrollEntity.MsId,
                    Absence = 0,
                    Allowance = Allowance,
                    Deduction = 0,
                    Delay = 0,
                    BankId = BankId,
                    Count_Day_Work = item.NoOfNight,
                    CreatedBy = session.UserId,
                    Emp_Account_No = checkEmpExist.AccountNo ?? "",
                    Loan = 0,
                    Salary = 0,
                    Salary_Orignal = 0,
                    Commission = 0,
                    OverTime = 0,
                    Mandate = (item.ActualExpenses - Allowance),
                    H_OverTime = 0,
                    Penalties = 0,
                    Net = item.ActualExpenses,
                    Refrance_No = item.Id,
                    Note = item.Note,
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
                if (Allowance > 0)
                {
                    var newPSDeductionEntity = new HrPayrollAllowanceDeduction
                    {
                        AdId = AdId,
                        MsId = AddedPayrollEntity.MsId,
                        AdValue = Allowance,
                        AdValueOrignal = Allowance,
                        Debit = 0,
                        Credit = Allowance,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        FixedOrTemporary = 1,
                        MsdId = PDID,
                        EmpId = checkEmpExist.Id,
                        IsDeleted = false
                    };
                    var AddPSAllowanceDeductionEntity = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(newPSDeductionEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                // End Of Add To HrPayrollD

                //add Notes
                var PayrollNoteItem = new HrPayrollNote();
                PayrollNoteItem.Note = "";
                PayrollNoteItem.CreatedBy = session.UserId;
                PayrollNoteItem.CreatedOn = DateTime.Now;
                PayrollNoteItem.MsId = AddedPayrollEntity.MsId;
                PayrollNoteItem.StateId = 1;
                var PayrollNoteEntity = await hrRepositoryManager.HrPayrollNoteRepository.AddAndReturn(PayrollNoteItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // Update Manadte PayrollId
                var getMandate = await hrRepositoryManager.HrMandateRepository.GetOne(x => x.Id == entity.Id && x.IsDeleted == false);
                if (getMandate == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");

                getMandate.PayrollId = AddedPayrollEntity.MsId;
                getMandate.ModifiedBy = session.UserId;
                getMandate.ModifiedOn = DateHelper.GetCurrentDateTime();
                hrRepositoryManager.HrMandateRepository.Update(getMandate);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("ThePathExtractedSuccessfully"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in PayrollOverTimeAdd at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}