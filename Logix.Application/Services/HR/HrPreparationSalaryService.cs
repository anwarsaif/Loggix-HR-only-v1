using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using Logix.Domain.OPM;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Globalization;
using static QuestPDF.Helpers.Colors;

namespace Logix.Application.Services.HR
{
    public class HrPreparationSalaryService : GenericQueryService<HrPreparationSalary, HrPreparationSalaryDto, HrPreparationSalariesVw>, IHrPreparationSalaryService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IDbFunctionsRepository dbFunctionsRepository;
        public HrPreparationSalaryService(IQueryRepository<HrPreparationSalary> queryRepository, IMainRepositoryManager mainRepositoryManager, ILocalizationService localization, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, IDbFunctionsRepository dbFunctionsRepository) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.dbFunctionsRepository = dbFunctionsRepository;
        }
        public async Task<IResult<HrPreparationSalaryDto>> Add(HrPreparationSalaryDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPreparationSalaryDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrPreparationSalaryDto>.FailAsync($"Employee Id Is Required");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                // check if Emp Is Exist
                var item = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (item == null) return await Result<HrPreparationSalaryDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (item.StatusId == 2) return await Result<HrPreparationSalaryDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                var newItem = _mapper.Map<HrPreparationSalary>(entity);
                newItem.EmpId = item.Id;
                newItem.CreatedBy = session.UserId;
                newItem.FacilityId = (int?)session.FacilityId;
                var CheckPreparationSalaries = await hrRepositoryManager.HrPreparationSalaryRepository.GetAllFromView(x => x.IsDeleted == false && x.FinancelYear == entity.FinancelYear && x.MsMonth == entity.MsMonth && x.EmpId == item.Id);
                if (CheckPreparationSalaries.Count() > 0) return await Result<HrPreparationSalaryDto>.FailAsync($"تم إعداد راتب للموظف  {entity.EmpCode} لهذا الشهر مسبقاً");
                var newEntity = await hrRepositoryManager.HrPreparationSalaryRepository.AddAndReturn(newItem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var getHrSettting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                if (getHrSettting != null)
                {
                    if (getHrSettting.UpdetDepLocExl == 1)
                    {
                        //   تحديث الأقسام والمواقع


                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        item.IsDeleted = false;
                        item.Location = (int?)entity.Location;
                        item.DeptId = (int?)entity.DeptId;
                        mainRepositoryManager.InvestEmployeeRepository.Update(item);
                        await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                }
                var getAllowance = await hrRepositoryManager.HrAllowanceVwRepository.GetAll(x => x.IsDeleted == false && x.EmpId == item.Id && x.Status == true && x.FixedOrTemporary == 1 && x.TypeId == 1);
                foreach (var singleAllowanceitem in getAllowance)
                {
                    var HRPSAllowance = new HrPsAllowanceDeduction
                    {
                        PsId = newEntity.Id,
                        EmpId = item.Id,
                        TypeId = 1,
                        FixedOrTemporary = 1,
                        Debit = 0,
                        CreatedBy = session.UserId,
                        Credit = singleAllowanceitem.Amount ?? 0 * entity.CountDayWork / Convert.ToDecimal(entity.DaysOfmonth),
                        AmountOrginal = singleAllowanceitem.Amount ?? 0,
                        AdId = singleAllowanceitem.AdId
                    };
                    await hrRepositoryManager.HrPsAllowanceDeductionRepository.Add(HRPSAllowance);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                var getDeduction = await hrRepositoryManager.HrDeductionVwRepository.GetAll(x => x.IsDeleted == false && x.EmpId == item.Id && x.Status == true && x.FixedOrTemporary == 1 && x.TypeId == 2);
                foreach (var singleDeductionitem in getDeduction)
                {
                    var HRPSDeduction = new HrPsAllowanceDeduction
                    {
                        PsId = newEntity.Id,
                        EmpId = item.Id,
                        TypeId = 2,
                        FixedOrTemporary = 1,
                        Debit = singleDeductionitem.Amount ?? 0,
                        CreatedBy = session.UserId,
                        Credit = 0,
                        AmountOrginal = singleDeductionitem.Amount ?? 0,
                        AdId = singleDeductionitem.AdId
                    };
                    await hrRepositoryManager.HrPsAllowanceDeductionRepository.Add(HRPSDeduction);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                var entityMap = _mapper.Map<HrPreparationSalaryDto>(newEntity);

                return await Result<HrPreparationSalaryDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrPreparationSalaryDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrPreparationSalaryRepository.GetById(Id);
                if (item == null) return Result<HrPreparationSalaryDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrPreparationSalaryRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPreparationSalaryDto>.SuccessAsync(_mapper.Map<HrPreparationSalaryDto>(item), localization.GetMessagesResource("Deletesuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPreparationSalaryDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrPreparationSalaryRepository.GetById(Id);
                if (item == null) return Result<HrPreparationSalaryDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrPreparationSalaryRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPreparationSalaryDto>.SuccessAsync(_mapper.Map<HrPreparationSalaryDto>(item), localization.GetMessagesResource("Deletesuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPreparationSalaryDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrPreparationSalaryEditDto>> Update(HrPreparationSalaryEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPreparationSalaryEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (entity.EmpId <= 0) return await Result<HrPreparationSalaryEditDto>.FailAsync($"Employee Id Is Required");

            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrPreparationSalaryEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrPreparationSalaryRepository.GetById(entity.Id);

                if (item == null) return await Result<HrPreparationSalaryEditDto>.FailAsync("the Record Is Not Found");

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                entity.EmpId = checkEmpExist.Id;
                _mapper.Map(entity, item);
                item.FacilityId = (int?)session.FacilityId;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.EmpId = checkEmpExist.Id;
                hrRepositoryManager.HrPreparationSalaryRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPreparationSalaryEditDto>.SuccessAsync(_mapper.Map<HrPreparationSalaryEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrPreparationSalaryEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> Remove(List<long> Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var Singleitem in Id)
                {
                    var item = await hrRepositoryManager.HrPreparationSalaryRepository.GetById(Singleitem);
                    if (item == null) return Result<string>.Fail($"--- there is no Data with this id: {Singleitem}---");
                    item.IsDeleted = true;
                    item.ModifiedOn = DateTime.Now;
                    item.ModifiedBy = session.UserId;
                    hrRepositoryManager.HrPreparationSalaryRepository.Update(item);

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync("تمت عملية الحذف لجمع العناصر المحددة", 200);

            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<AddPreparationSalariesResultDto>> AddUsingExcel(List<HrPreparationSalaryDto> entities, CancellationToken cancellationToken = default)
        {

            if (entities == null) return await Result<AddPreparationSalariesResultDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            AddPreparationSalariesResultDto result = new AddPreparationSalariesResultDto();

            int? SucceeededCount = 0;
            try
            {
                var getHrSettting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);

                var OverTime = getHrSettting.OverTime ?? 0;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var getAllowances = await hrRepositoryManager.HrAllowanceVwRepository.GetAll(x => x.IsDeleted == false && x.Status == true && x.FixedOrTemporary == 1 && x.TypeId == 1);
                var getDeductions = await hrRepositoryManager.HrDeductionVwRepository.GetAll(x => x.IsDeleted == false && x.Status == true && x.FixedOrTemporary == 1 && x.TypeId == 2);

                foreach (var item in entities)
                {
                    decimal TotalAllowance = 0;
                    decimal TotalDeduction = 0;
                    var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == item.EmpCode && x.IsDeleted == false && x.Isdel == false);
                    if (checkEmpExist == null)
                    {
                        result.IDNotEqual += item.EmpCode + ",";
                        continue;
                    }
                    if (checkEmpExist.StatusId == 2)
                    {
                        result.NotActive += item.EmpCode + ",";
                        continue;
                    }
                    ////////////////////////this cod efor get emp salary and financial data//////////////////////////
                    var GetHREmpAllData = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.Id == checkEmpExist.Id && x.IsDeleted == false && x.Isdel == false);
                    TotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(checkEmpExist.Id);
                    TotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalDeduction(checkEmpExist.Id);

                    ////////////////////////////////////////////////
                    var CheckPreparationSalaries = await hrRepositoryManager.HrPreparationSalaryRepository.GetAllFromView(x => x.IsDeleted == false && x.FinancelYear == item.FinancelYear && x.MsMonth == item.MsMonth && x.EmpId == checkEmpExist.Id);
                    if (CheckPreparationSalaries.Count() > 0)
                    {
                        result.IsExist += item.EmpCode + ",";
                        continue;

                    }
                    var DailyWorkinghours = checkEmpExist.DailyWorkingHours;
                    var DayAbsence = item.DayAbsence;

                    if (DayAbsence > 0)
                    {
                        decimal Amount = 0;
                        Amount = await dbFunctionsRepository.ApplyPolicies(session.FacilityId, 3, checkEmpExist.Id);
                        item.Absence = (Amount / Convert.ToInt32(item.DaysOfmonth)) * DayAbsence;
                    }
                    else
                    {
                        item.Absence = 0;
                    }
                    if (item.DayPrevMonth > 0)
                    {
                        item.DuePrevMonth = ((GetHREmpAllData.Salary + TotalAllowance - TotalDeduction) / Convert.ToInt32(item.DaysOfmonth)) * item.DayPrevMonth;// معادلة احتساب الايام المرحلة
                    }
                    else
                    {
                        item.DuePrevMonth = 0;
                    }
                    var newHrPreparingSalary = new HrPreparationSalary
                    {
                        Salary = GetHREmpAllData.Salary,
                        Allowance = TotalAllowance,
                        Deduction = TotalDeduction,
                        EmpId = checkEmpExist.Id,
                        DeptId = checkEmpExist.DeptId,
                        Location = checkEmpExist.Location,
                        ExtraTime = Math.Round((decimal)((GetHREmpAllData.Salary ?? 0 / Convert.ToInt32(item.DaysOfmonth) / DailyWorkinghours) * item.HExtraTime * OverTime), 2), // معادلة احتساب خارج الدوام
                        Delay = Math.Round((decimal)(((GetHREmpAllData.Salary ?? 0 / Convert.ToInt32(item.DaysOfmonth)) / DailyWorkinghours / 60) * item.MDelay), 2), //  معادلة احتساب التأخرات
                        MsMonth = item.MsMonth,
                        FinancelYear = item.FinancelYear,
                        CountDayWork = item.CountDayWork,
                        DueDayWork = ((GetHREmpAllData.Salary + TotalAllowance - TotalDeduction) / Convert.ToInt32(item.DaysOfmonth)) * item.CountDayWork, // 'معادلة احتساب ايام العمل 
                        DayAbsence = item.DayAbsence,
                        Absence = item.Absence,
                        DuePrevMonth = item.DuePrevMonth,
                        AllowanceOther = item.AllowanceOther,
                        DeductionOther = item.DeductionOther,
                        CreatedBy = session.UserId,
                        Commission = item.Commission,
                        Note = item.Note,
                        PayrollTypeId = 1,
                        Penalties = item.Penalties,
                        MsDate = item.MsDate,
                        FacilityId = item.FacilityId,
                    };
                    var newEntity = await hrRepositoryManager.HrPreparationSalaryRepository.AddAndReturn(newHrPreparingSalary);

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    if (getHrSettting != null)
                    {
                        if (getHrSettting.UpdetDepLocExl == 1 && item.Location > 0 && item.DeptId > 0)
                        {
                            //   تحديث الأقسام والمواقع
                            checkEmpExist.ModifiedBy = session.UserId;
                            checkEmpExist.ModifiedOn = DateTime.Now;
                            checkEmpExist.IsDeleted = false;
                            checkEmpExist.Location = (int?)item.Location;
                            checkEmpExist.DeptId = (int?)item.DeptId;
                            mainRepositoryManager.InvestEmployeeRepository.Update(checkEmpExist);
                            await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }

                    }
                    var getAllowance = getAllowances.Where(x => x.EmpId == item.Id);
                    foreach (var singleAllowanceitem in getAllowance)
                    {
                        var HRPSAllowance = new HrPsAllowanceDeduction
                        {
                            PsId = newEntity.Id,
                            EmpId = item.Id,
                            TypeId = 1,
                            FixedOrTemporary = 1,
                            Debit = 0,
                            CreatedBy = session.UserId,
                            Credit = singleAllowanceitem.Amount ?? 0 * item.CountDayWork / Convert.ToDecimal(item.DaysOfmonth),
                            AmountOrginal = singleAllowanceitem.Amount ?? 0,
                            AdId = singleAllowanceitem.AdId
                        };
                        await hrRepositoryManager.HrPsAllowanceDeductionRepository.Add(HRPSAllowance);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }
                    var getDeduction = getDeductions.Where(x => x.EmpId == item.Id);
                    foreach (var singleDeductionitem in getDeduction)
                    {
                        var HRPSDeduction = new HrPsAllowanceDeduction
                        {
                            PsId = newEntity.Id,
                            EmpId = item.Id,
                            TypeId = 2,
                            FixedOrTemporary = 1,
                            Debit = singleDeductionitem.Amount ?? 0,
                            CreatedBy = session.UserId,
                            Credit = 0,
                            AmountOrginal = singleDeductionitem.Amount ?? 0,
                            AdId = singleDeductionitem.AdId
                        };
                        await hrRepositoryManager.HrPsAllowanceDeductionRepository.Add(HRPSDeduction);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }
                    SucceeededCount++;
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                if (!string.IsNullOrEmpty(result.NotActive)) result.NotActive = $" الموظفين التاليين حالتهم انهاء خدمة  : {result.NotActive.TrimEnd(',')}";
                if (!string.IsNullOrEmpty(result.IDNotEqual)) result.IDNotEqual = $" الموظفين التاليين ارقامهم غير متطابقة مع هوياتهم  :  {result.IDNotEqual.TrimEnd(',')}";
                if (!string.IsNullOrEmpty(result.EmpNotExists)) result.EmpNotExists = $" الموظفين التاليين ارقامهم لاتوجد لهم ارقام وظيفية على النظام  : {result.EmpNotExists.TrimEnd(',')}";
                if (!string.IsNullOrEmpty(result.IsExist)) result.IsExist = $"تم إعداد راتب للموظفين  لهذا الشهر مسبقاً وهم  : {result.IsExist.TrimEnd(',')}";
                if (SucceeededCount > 0) result.Saved = $"تم استيراد  {SucceeededCount}  سجل ";

                return await Result<AddPreparationSalariesResultDto>.SuccessAsync(result, "", 200);


            }
            catch (Exception)
            {

                return await Result<AddPreparationSalariesResultDto>.FailAsync($"حدث خطاء اثناء عملية الاعداد ");
            }
        }
        public async Task<IResult<HrPreparationSalaryEditDto>> Update(HrPreparationCommissionUpdateDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPreparationSalaryEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrPreparationSalaryEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrPreparationSalaryRepository.GetById(entity.Id);

                if (item == null) return await Result<HrPreparationSalaryEditDto>.FailAsync("the Record Is Not Found");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.EmpId = checkEmpExist.Id;
                item.DayPrevMonth = 0;
                item.DuePrevMonth = 0;
                item.DueDayWork = 0;
                item.Penalties = 0;
                item.MsDate = entity.MsDate;
                hrRepositoryManager.HrPreparationSalaryRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPreparationSalaryEditDto>.SuccessAsync(_mapper.Map<HrPreparationSalaryEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrPreparationSalaryEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<HrPreparationSalaryDto>> PreparationCommissionAdd(HrPreparationSalaryDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPreparationSalaryDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrPreparationSalaryDto>.FailAsync($"Employee Id Is Required");
            try
            {
                var getHrSettting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                // check if Emp Is Exist
                var item = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (item == null) return await Result<HrPreparationSalaryDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                entity.EmpId = item.Id;
                entity.CreatedBy = session.UserId;

                var CheckPreparationSalaries = await hrRepositoryManager.HrPreparationSalaryRepository.GetAllFromView(x => x.IsDeleted == false && x.FinancelYear == entity.FinancelYear && x.MsMonth == entity.MsMonth && x.EmpId == item.Id);
                if (CheckPreparationSalaries.Count() > 0) return await Result<HrPreparationSalaryDto>.FailAsync($"تم إعداد راتب للموظف  {entity.EmpCode} لهذا الشهر مسبقاً");
                var NewHrPreparationSalary = new HrPreparationSalary
                {
                    EmpId = item.Id,
                    MsDate = entity.MsDate,
                    FinancelYear = entity.FinancelYear,
                    MsMonth = entity.MsMonth,
                    CreatedBy = session.UserId,
                    Salary = 0,
                    Allowance = 0,
                    Deduction = 0,
                    Absence = 0,
                    Delay = 0,
                    Loan = 0,
                    DeductionOther = 0,

                    ExtraTime = 0,

                    CountDayWork = 0,

                    DayAbsence = 0,

                    HExtraTime = 0,
                    MDelay = 0,

                    AllowanceOther = 0,

                    DayPrevMonth = 0,

                    DuePrevMonth = 0,

                    DueDayWork = 0,
                    Penalties = 0,
                    PayrollTypeId = 2,
                    DeptId = entity.DeptId,
                    Location = entity.Location,
                    Note = entity.Note,
                    Commission = entity.Commission ?? 0,
                };

                var newEntity = await hrRepositoryManager.HrPreparationSalaryRepository.AddAndReturn(NewHrPreparationSalary);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (getHrSettting != null)
                {
                    if (getHrSettting.UpdetDepLocExl == 1 && entity.Location > 0 && entity.DeptId > 0)
                    {
                        //   تحديث الأقسام والمواقع
                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        item.IsDeleted = false;
                        item.Location = (int?)entity.Location;
                        item.DeptId = (int?)entity.DeptId;
                        mainRepositoryManager.InvestEmployeeRepository.Update(item);
                        await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                }
                var entityMap = _mapper.Map<HrPreparationSalaryDto>(newEntity);

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);


                return await Result<HrPreparationSalaryDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrPreparationSalaryDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }
        public async Task<IResult<AddPreparationSalariesResultDto>> AddPreparationCommisssionUsingExcel(List<HrPreparationSalaryDto> entities, CancellationToken cancellationToken = default)
        {

            if (entities == null) return await Result<AddPreparationSalariesResultDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            AddPreparationSalariesResultDto result = new AddPreparationSalariesResultDto();

            int? SucceeededCount = 0;
            try
            {
                var getHrSettting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);

                var OverTime = getHrSettting.OverTime ?? 0;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var getAllowances = await hrRepositoryManager.HrAllowanceVwRepository.GetAll(x => x.IsDeleted == false && x.Status == true && x.FixedOrTemporary == 1 && x.TypeId == 1);
                var getDeductions = await hrRepositoryManager.HrDeductionVwRepository.GetAll(x => x.IsDeleted == false && x.Status == true && x.FixedOrTemporary == 1 && x.TypeId == 2);

                foreach (var item in entities)
                {
                    decimal TotalAllowance = 0;
                    decimal TotalDeduction = 0;
                    var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == item.EmpCode && x.IsDeleted == false && x.Isdel == false);
                    if (checkEmpExist == null)
                    {
                        result.IDNotEqual += item.EmpCode + ",";
                        continue;
                    }
                    if (checkEmpExist.StatusId == 2)
                    {
                        result.NotActive += item.EmpCode + ",";
                        continue;
                    }
                    ////////////////////////this code for get emp salary and financial data//////////////////////////
                    var GetHREmpAllData = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.Id == checkEmpExist.Id && x.IsDeleted == false && x.Isdel == false);
                    TotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(checkEmpExist.Id);
                    TotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalDeduction(checkEmpExist.Id);
                    ////////////////////////////////////////////////
                    var CheckPreparationSalaries = await hrRepositoryManager.HrPreparationSalaryRepository.GetAllFromView(x => x.IsDeleted == false && x.FinancelYear == item.FinancelYear && x.MsMonth == item.MsMonth && x.EmpId == checkEmpExist.Id);
                    if (CheckPreparationSalaries.Count() > 0)
                    {
                        result.IsExist += item.EmpCode + ",";
                        continue;
                    }
                    var NewHrPreparationSalary = new HrPreparationSalary
                    {
                        MsDate = item.MsDate,
                        FinancelYear = item.FinancelYear,
                        MsMonth = item.MsMonth,
                        CreatedBy = session.UserId,
                        Salary = GetHREmpAllData.Salary,
                        Allowance = TotalAllowance,
                        Deduction = TotalDeduction,
                        EmpId = checkEmpExist.Id,
                        DeptId = checkEmpExist.DeptId,
                        Location = checkEmpExist.Location,
                        Absence = 0,
                        Delay = 0,
                        Loan = 0,
                        DeductionOther = 0,
                        ExtraTime = 0,
                        CountDayWork = 0,
                        DayAbsence = 0,
                        HExtraTime = 0,
                        MDelay = 0,
                        AllowanceOther = 0,
                        DayPrevMonth = 0,
                        DuePrevMonth = 0,
                        DueDayWork = 0,
                        Penalties = 0,
                        PayrollTypeId = 2,
                        Note = item.Note,
                        Commission = item.Commission ?? 0,
                    };
                    var newEntity = await hrRepositoryManager.HrPreparationSalaryRepository.AddAndReturn(NewHrPreparationSalary);

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    if (getHrSettting != null)
                    {
                        if (getHrSettting.UpdetDepLocExl == 1 && item.Location > 0 && item.DeptId > 0)
                        {
                            //   تحديث الأقسام والمواقع
                            checkEmpExist.ModifiedBy = session.UserId;
                            checkEmpExist.ModifiedOn = DateTime.Now;
                            checkEmpExist.IsDeleted = false;
                            checkEmpExist.Location = (int?)item.Location;
                            checkEmpExist.DeptId = (int?)item.DeptId;
                            mainRepositoryManager.InvestEmployeeRepository.Update(checkEmpExist);
                            await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }

                    }
                    var getAllowance = getAllowances.Where(x => x.EmpId == item.Id);
                    foreach (var singleAllowanceitem in getAllowance)
                    {
                        var HRPSAllowance = new HrPsAllowanceDeduction
                        {
                            PsId = newEntity.Id,
                            EmpId = item.Id,
                            TypeId = 1,
                            FixedOrTemporary = 1,
                            Debit = 0,
                            CreatedBy = session.UserId,
                            Credit = singleAllowanceitem.Amount ?? 0 * item.CountDayWork / Convert.ToDecimal(item.DaysOfmonth),
                            AmountOrginal = singleAllowanceitem.Amount ?? 0,
                            AdId = singleAllowanceitem.AdId
                        };
                        await hrRepositoryManager.HrPsAllowanceDeductionRepository.Add(HRPSAllowance);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }
                    var getDeduction = getDeductions.Where(x => x.EmpId == item.Id);
                    foreach (var singleDeductionitem in getDeduction)
                    {
                        var HRPSDeduction = new HrPsAllowanceDeduction
                        {
                            PsId = newEntity.Id,
                            EmpId = item.Id,
                            TypeId = 2,
                            FixedOrTemporary = 1,
                            Debit = singleDeductionitem.Amount ?? 0,
                            CreatedBy = session.UserId,
                            Credit = 0,
                            AmountOrginal = singleDeductionitem.Amount ?? 0,
                            AdId = singleDeductionitem.AdId
                        };
                        await hrRepositoryManager.HrPsAllowanceDeductionRepository.Add(HRPSDeduction);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }
                    SucceeededCount++;
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                if (!string.IsNullOrEmpty(result.NotActive)) result.NotActive = $" الموظفين التاليين حالتهم انهاء خدمة  : {result.NotActive.TrimEnd(',')}";
                if (!string.IsNullOrEmpty(result.IDNotEqual)) result.IDNotEqual = $" الموظفين التاليين ارقامهم غير متطابقة مع هوياتهم  :  {result.IDNotEqual.TrimEnd(',')}";
                if (!string.IsNullOrEmpty(result.EmpNotExists)) result.EmpNotExists = $" الموظفين التاليين ارقامهم لاتوجد لهم ارقام وظيفية على النظام  : {result.EmpNotExists.TrimEnd(',')}";
                if (!string.IsNullOrEmpty(result.IsExist)) result.IsExist = $"تم إعداد راتب للموظفين  لهذا الشهر مسبقاً وهم  : {result.IsExist.TrimEnd(',')}";
                if (SucceeededCount > 0) result.Saved = $"تم استيراد  {SucceeededCount}  سجل ";

                return await Result<AddPreparationSalariesResultDto>.SuccessAsync(result, "", 200);


            }
            catch (Exception)
            {

                return await Result<AddPreparationSalariesResultDto>.FailAsync($"حدث خطاء اثناء عملية الاعداد ");
            }
        }

        public async Task<IResult<string>> PreparationSalariesLoanAdd(List<HRPreparationSalariesLoanAddDto> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null) return await Result<string>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var entity in entities)
                {
                    var item = await hrRepositoryManager.HrPreparationSalaryRepository.GetById(entity.ID);

                    if (item == null) return await Result<string>.FailAsync("Some Records Not Found");

                    item.ModifiedBy = session.UserId;
                    item.ModifiedOn = DateTime.Now;
                    item.Loan = entity.Loan;
                    hrRepositoryManager.HrPreparationSalaryRepository.Update(item);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetMessagesResource("success"), 200);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> RemoveByPackage(string PackageId, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var getAllPackageData = await hrRepositoryManager.HrPreparationSalaryRepository.GetAll(x => x.IsDeleted == false && x.PackageNo == PackageId);
                foreach (var singleItem in getAllPackageData)
                {
                    singleItem.ModifiedBy = session.UserId;
                    singleItem.ModifiedOn = DateTime.Now;
                    hrRepositoryManager.HrPreparationSalaryRepository.Update(singleItem);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("DeleteSuccess"));

            }
            catch (Exception)
            {

                return await Result<string>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }


        public async Task<IResult<List<HrPayrollCompareResult?>>> PreparationSalariesPayrollCompare(HrPayrollCompareFilterDto filter, int CmdType)
        {
            try
            {
                var resul = await hrRepositoryManager.HrPreparationSalaryRepository.PreparationSalariesPayrollCompare(filter, CmdType);
                return await Result<List<HrPayrollCompareResult?>>.SuccessAsync(resul, "", 200);
            }
            catch (Exception ex)
            {

                return await Result<List<HrPayrollCompareResult>>.FailAsync(ex.Message);
            }
        }
    }

}
