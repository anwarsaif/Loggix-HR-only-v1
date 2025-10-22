using AutoMapper;
using DocumentFormat.OpenXml.Vml.Office;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.ACC;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Emit;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace Logix.Application.Services.HR
{
    public class HrAbsenceService : GenericQueryService<HrAbsence, HrAbsenceDto, HrAbsenceVw>, IHrAbsenceService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILocalizationService localization;
        private readonly ICurrentData session;
        private readonly ISysConfigurationAppHelper configuration;

        public HrAbsenceService(IQueryRepository<HrAbsence> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session, ILocalizationService localization, ISysConfigurationAppHelper configuration) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;
            this.localization = localization;
            this.session = session;
            this.configuration = configuration;
        }

        public async Task<IResult<HrAbsenceDto>> Add(HrAbsenceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAbsenceDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                var item = _mapper.Map<HrAbsence>(entity);
                var newEntity = await hrRepositoryManager.HrAbsenceRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrAbsenceDto>(newEntity);


                return await Result<HrAbsenceDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrAbsenceDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }


        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrAbsenceRepository.GetById(Id);
                if (item == null) return Result<HrAbsenceDto>.Fail($"--- there is no Data with this id: {Id}---");

                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrAbsenceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAbsenceDto>.SuccessAsync(_mapper.Map<HrAbsenceDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrAbsenceDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrAbsenceRepository.GetById(Id);
            if (item == null) return Result<HrAbsenceDto>.Fail($"--- there is no Data with this id: {Id}---");

            try
            {
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrAbsenceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAbsenceDto>.SuccessAsync(_mapper.Map<HrAbsenceDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrAbsenceDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> NewRemove(long AbsenceId, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrAbsenceRepository.GetById(AbsenceId);
                if (item == null) return Result<HrAbsenceDto>.Fail($"{localization.GetMessagesResource("NoDataWithId")} : {AbsenceId}");
                // Check_Emp_Exists_In_Payroll
                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == item.EmpId && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                if (IfEmpExistsInPayroll.Any())
                {
                    var msDateStr = item.AbsenceDate;

                    var filterResult = IfEmpExistsInPayroll
                                    .Where(e =>
                                    {
                                        // تحويل التواريخ مع التحقق من الصحة
                                        bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                        bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                        bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                        return isDateValid && isStartDateValid && isEndDateValid &&
                                               msDate >= startDate && msDate <= endDate;
                                    })
                                    .ToList();
                    if (filterResult.Any())
                    {
                        return await Result<HrAbsenceDto>.FailAsync(localization.GetMessagesResource("AbsenceLinkedToPayroll"));

                    }
                }

                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrAbsenceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAbsenceDto>.SuccessAsync(_mapper.Map<HrAbsenceDto>(item), localization.GetResource1("DeleteSuccess"));

            }
            catch (Exception)
            {
                return await Result<HrAbsenceDto>.FailAsync(localization.GetResource1("DeleteFail"));
            }
        }

        public async Task<IResult> Remove(List<long> AbsenceIds, CancellationToken cancellationToken = default)
        {

            try
            {
                if (!AbsenceIds.Any())
                {
                    return await Result<string>.FailAsync(localization.GetMessagesResource("SelectRecordsTobeDeleted"));
                }
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var row in AbsenceIds)
                {
                    var item = await hrRepositoryManager.HrAbsenceRepository.GetById(row);
                    if (item == null) return Result<string>.Fail(localization.GetMessagesResource("NoDataWithId") + " : " + row);
                    // Check_Emp_Exists_In_Payroll
                    var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == item.EmpId && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));


                    if (IfEmpExistsInPayroll.Any())
                    {
                        var msDateStr = item.AbsenceDate;

                        var filterResult = IfEmpExistsInPayroll
                                        .Where(e =>
                                        {
                                            // تحويل التواريخ مع التحقق من الصحة
                                            bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                            bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                            bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                            return isDateValid && isStartDateValid && isEndDateValid &&
                                                   msDate >= startDate && msDate <= endDate;
                                        })
                                        .ToList();
                        if (filterResult.Any())
                        {
                            var getEmpCode = await mainRepositoryManager.InvestEmployeeRepository.GetOne(i => i.EmpId, i => i.Id == item.EmpId && i.Isdel == false && i.IsDeleted == false);

                            return await Result<string>.InformationAsync($" {localization.GetMessagesResource("AbsenceLinkedToPayroll")}  {getEmpCode} ");
                        }
                    }

                    item.IsDeleted = true;
                    item.ModifiedOn = DateTime.Now;
                    item.ModifiedBy = (int)session.UserId;
                    hrRepositoryManager.HrAbsenceRepository.Update(item);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);


                return await Result<string>.SuccessAsync("", $"{localization.GetResource1("DeleteSuccess")} {AbsenceIds.Count} ");

            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrAbsenceDto>> Update(HrAbsenceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAbsenceDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");
            try
            {
                var item = await hrRepositoryManager.HrAbsenceRepository.GetById(entity.Id);

                if (item == null) return await Result<HrAbsenceDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(entity, item);

                hrRepositoryManager.HrAbsenceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAbsenceDto>.SuccessAsync(_mapper.Map<HrAbsenceDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrAbsenceDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> DisciplinaryCaseIDChanged(string EmpCode, string AbsenceDate, int DisciplinaryCaseID, CancellationToken cancellationToken = default)
        {
            int? RepeatCount = 0;
            decimal DeductedRate = 0.00m;
            decimal? DeductedAmount = 0.00m;
            int? ActionType = 0;
            if (string.IsNullOrEmpty(EmpCode))
            {
                return await Result<string>.SuccessAsync("there is no EmpCode passed");
            }

            try
            {
                var checkEmpId = await mainRepositoryManager.InvestEmployeeRepository.GetOne(i => i.EmpId == EmpCode && i.Isdel == false && i.IsDeleted == false);

                if (checkEmpId != null)
                {
                    var Total = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 3, checkEmpId.Id);
                    string NumberofRepetitionDays = await configuration.GetValue(117, session.FacilityId);
                    if (string.IsNullOrEmpty(NumberofRepetitionDays))
                    {
                        NumberofRepetitionDays = "0";
                    }
                    var DueDate2 = DateHelper.StringToDate(AbsenceDate).AddDays(-Convert.ToDouble(NumberofRepetitionDays)).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    // عدد المخالفات الغياب خلال 180 يوم

                    var GetFromHRDisciplinaryCaseAction = await hrRepositoryManager.HrDisciplinaryCaseActionRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpId.Id && x.DisciplinaryCaseId == DisciplinaryCaseID);
                    if (GetFromHRDisciplinaryCaseAction.Any())
                    {
                        var filterDate = GetFromHRDisciplinaryCaseAction.Where(x => x.DueDate != null
                        && DateHelper.StringToDate(x.DueDate) >= DateHelper.StringToDate(DueDate2) &&
                        DateHelper.StringToDate(x.DueDate) <= DateHelper.StringToDate(AbsenceDate)
                        ).ToList();
                        if (filterDate.Any())
                        {
                            RepeatCount = filterDate.Count;
                        }
                        else
                        {
                            RepeatCount = 0;
                        }
                    }
                    else
                    {
                        RepeatCount = 0;
                    }
                    var GetFromHRDisciplinaryRule = await hrRepositoryManager.HrDisciplinaryRuleRepository.GetOne(x => x.DisciplinaryCaseId == DisciplinaryCaseID && (RepeatCount + 1 >= x.ReptFrom && RepeatCount + 1 <= x.ReptTo));
                    DeductedRate = (GetFromHRDisciplinaryRule.DeductedRate) ?? 0m;
                    ActionType = GetFromHRDisciplinaryRule.ActionType;
                    DeductedAmount = Math.Round((decimal)(GetFromHRDisciplinaryRule.DeductedRate * Total / 30));
                    return await Result<object>.SuccessAsync(new { DeductedAmount = DeductedAmount, DeductedRate = DeductedRate, RepeatCount = RepeatCount, ActionType = ActionType }, "Result:", 200);

                }
                else
                {
                    return await Result<string>.SuccessAsync(localization.GetResource1("EmployeeNotFound"));

                }

            }
            catch (Exception exp)
            {
                return await Result<String>.FailAsync($"{exp.Message}");
            }
        }

        public async Task<IResult<HrAbsenceDto>> AddSingleAbsence(HrAbsenceAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {

                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(i => i.EmpId == entity.EmpCode && i.Isdel == false && i.IsDeleted == false);

                if (checkEmpExist == null)
                {
                    return await Result<HrAbsenceDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                }
                if (checkEmpExist.StatusId == 2)
                {
                    return await Result<HrAbsenceDto>.FailAsync(localization.GetResource1("EmpNotActive"));

                }
                var GetTimeTableID = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOneVw(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);


                //CheckIsAbsenceInSameDay
                var CheckIsAbsenceInSameDay = await hrRepositoryManager.HrAbsenceRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AbsenceDate == entity.AbsenceDate);
                if (CheckIsAbsenceInSameDay.Any())
                {
                    return await Result<HrAbsenceDto>.FailAsync("تم تسجيل غياب لهذا الموظف سابقاً - لن تتمكن من تسجيل اكثر من غياب في نفس اليوم");

                }

                // CheckIsVacationInSameDay
                var CheckIsVacationsInSameDay = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                if (CheckIsVacationsInSameDay.Any())
                {
                    var absenceDateStr = entity.AbsenceDate;
                    var payrollList = CheckIsVacationsInSameDay.ToList(); // جلب البيانات أولاً

                    var filterResult = payrollList
                                    .Where(e =>
                                    {
                                        // تحويل التواريخ مع التحقق من الصحة
                                        bool isAbsenceDateValid = DateTime.TryParseExact(absenceDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime absenceDate);
                                        bool isStartDateValid = DateTime.TryParseExact(e.VacationSdate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                        bool isEndDateValid = DateTime.TryParseExact(e.VacationEdate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                        return isAbsenceDateValid && isStartDateValid && isEndDateValid &&
                                               absenceDate >= startDate && absenceDate <= endDate;
                                    })
                                    .ToList();
                    //var filterResult = CheckIsVacationsInSameDay.Where(e => DateHelper.SafeParseDate(entity.AbsenceDate) >= DateHelper.SafeParseDate(e.VacationSdate) && DateHelper.SafeParseDate(entity.AbsenceDate) <= DateHelper.SafeParseDate(e.VacationEdate));
					if (filterResult.Any())
                    {
                        return await Result<HrAbsenceDto>.FailAsync("تم تسجيل إجازة لهذا الموظف سابقاً - لن تتمكن من تسجيل غياب في نفس اليوم");

                    }
                }


                //CheckIsDelayInSameDay
                var CheckIsDelayInSameDay = await hrRepositoryManager.HrDelayRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.DelayDate == entity.AbsenceDate);
                if (CheckIsDelayInSameDay.Any())
                {
                    return await Result<HrAbsenceDto>.FailAsync("تم تسجيل تأخر لهذا الموظف سابقاً - لن تتمكن من تسجيل غياب في نفس اليوم");

                }
                //CheckDateAbsencesIsBeforeDOAppointment

                var CheckDateAbsencesIsBeforeDOAppointment = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.IsDeleted == false && x.Id == checkEmpExist.Id && x.Doappointment != null);
                if (DateHelper.StringToDate(CheckDateAbsencesIsBeforeDOAppointment.Doappointment) > DateHelper.StringToDate(entity.AbsenceDate))
                {
                    return await Result<HrAbsenceDto>.FailAsync("تاريخ الغياب قبل تاريخ التعيين للموظف ");

                }
                // Check_Emp_Exists_In_Payroll
                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                if (IfEmpExistsInPayroll.Any())
                {

                    var msDateStr = entity.AbsenceDate;

                    var filterResult = IfEmpExistsInPayroll
                                    .Where(e =>
                                    {
                                        // تحويل التواريخ مع التحقق من الصحة
                                        bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                        bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                        bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                        return isDateValid && isStartDateValid && isEndDateValid &&
                                               msDate >= startDate && msDate <= endDate;
                                    })
                                    .ToList();
                    if (filterResult.Any())
                    {
                        return await Result<HrAbsenceDto>.FailAsync("لن تتمكن من اضافة غياب بسبب استخراج مسير للموظف في نفس الشهر");
                    }
                }


                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var AddAbsence = new HrAbsence
                {
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    Type = entity.Type.ToString(),
                    AbsenceTypeId = 0,
                    AbsenceDate = entity.AbsenceDate,
                    Note = entity.Note,
                    EmpId = checkEmpExist.Id,
                    LocationId = checkEmpExist.Location,
                    TimeTableId = Convert.ToInt32(GetTimeTableID?.TimeTableId)
                };
                var newAbsenceEntity = await hrRepositoryManager.HrAbsenceRepository.AddAndReturn(AddAbsence);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);



                if (entity.DisciplinaryCaseId > 0 && entity.ApplyPlenties)
                {
                    var AddDisciplinaryCaseAction = new HrDisciplinaryCaseAction
                    {
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        StatusId = 1,
                        DisciplinaryCaseId = entity.DisciplinaryCaseId,
                        DeductedAmount = entity.DeductedAmount,
                        DeductedRate = entity.DeductedRate,
                        EmpId = checkEmpExist.Id,
                        CountRept = entity.CountRept,
                        Description = entity.Note,
                        DueDate = entity.AbsenceDate,
                        ActionType = entity.ActionType,
                        VisitScheduleDId = 0

                    };
                    await hrRepositoryManager.HrDisciplinaryCaseActionRepository.Add(AddDisciplinaryCaseAction);

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                var entityMap = _mapper.Map<HrAbsenceDto>(newAbsenceEntity);

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrAbsenceDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exp)
            {

                return await Result<HrAbsenceDto>.FailAsync($"{localization.GetResource1("ErrorOccurredDuring")}    {exp.Message}");
            }
        }
        public async Task<IResult<AddAbsenceFromExcelResultDto>> AddAbsenceFromExcel(List<HrAbsenceAddDto> entities, CancellationToken cancellationToken = default)
        {
            AddAbsenceFromExcelResultDto result = new AddAbsenceFromExcelResultDto();
            int? SavedCount = 0;
            List<string> NotFoundCount = new List<string>();
            List<string> NotActiveCount = new List<string>();
            List<string> PayrollCount = new List<string>();
            List<string> VacationCount = new List<string>();
            List<string> DelayCount = new List<string>();
            List<string> AbsenceCount = new List<string>();
            List<string> DOAppointmentCount = new List<string>();
            int? RepeatCount = 0;
            try
            {
                if (!entities.Any())
                {
                    return await Result<AddAbsenceFromExcelResultDto>.FailAsync("لا يوجد بيانات في ملف الإكسل ");

                }
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                foreach (var SingleAsenceItem in entities)
                {
                    var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(i => i.EmpId == SingleAsenceItem.EmpCode && i.Isdel == false && i.IsDeleted == false);

                    if (checkEmpExist == null)
                    {
                        NotFoundCount.Add(SingleAsenceItem.EmpCode);
                        continue;
                    }

                    if (checkEmpExist.StatusId == 2)
                    {
                        NotActiveCount.Add(SingleAsenceItem.EmpCode);
                        continue;
                    }
                    var GetTimeTableID = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOneVw(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);


                    //CheckIsAbsenceInSameDay
                    var CheckIsAbsenceInSameDay = await hrRepositoryManager.HrAbsenceRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AbsenceDate == SingleAsenceItem.AbsenceDate);
                    if (CheckIsAbsenceInSameDay.Any())
                    {
                        AbsenceCount.Add(SingleAsenceItem.EmpCode);
                        continue;
                    }

                    // CheckIsVacationInSameDay
                    var CheckIsVacationsInSameDay = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                    if (CheckIsVacationsInSameDay.Any())
                    {

                        var filterResult = CheckIsVacationsInSameDay.Where(e => DateHelper.StringToDate(SingleAsenceItem.AbsenceDate) >= DateHelper.StringToDate(e.VacationSdate) && DateHelper.StringToDate(SingleAsenceItem.AbsenceDate) <= DateHelper.StringToDate(e.VacationEdate));
                        if (filterResult.Any())
                        {
                            VacationCount.Add(SingleAsenceItem.EmpCode);
                            continue;
                        }
                    }


                    //CheckIsDelayInSameDay
                    var CheckIsDelayInSameDay = await hrRepositoryManager.HrDelayRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.DelayDate == SingleAsenceItem.AbsenceDate);
                    if (CheckIsDelayInSameDay.Any())
                    {
                        DelayCount.Add(SingleAsenceItem.EmpCode);
                        continue;
                    }
                    //CheckDateAbsencesIsBeforeDOAppointment

                    var CheckDateAbsencesIsBeforeDOAppointment = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.IsDeleted == false && x.Id == checkEmpExist.Id && x.Doappointment != null);
                    if (CheckDateAbsencesIsBeforeDOAppointment == null)
                    {
                        NotFoundCount.Add(SingleAsenceItem.EmpCode);
                        continue;
                    }
                    if (DateHelper.StringToDate(CheckDateAbsencesIsBeforeDOAppointment.Doappointment) > DateHelper.StringToDate(SingleAsenceItem.AbsenceDate))
                    {
                        DOAppointmentCount.Add(SingleAsenceItem.EmpCode);
                        continue;
                    }
                    // Check_Emp_Exists_In_Payroll
                    var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                    if (IfEmpExistsInPayroll.Any())
                    {

                        var msDateStr = SingleAsenceItem.AbsenceDate;

                        var filterResult = IfEmpExistsInPayroll
                                        .Where(e =>
                                        {
                                            // تحويل التواريخ مع التحقق من الصحة
                                            bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                            bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                            bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                            return isDateValid && isStartDateValid && isEndDateValid &&
                                                   msDate >= startDate && msDate <= endDate;
                                        })
                                        .ToList();
                        if (filterResult.Any())
                        {
                            PayrollCount.Add(SingleAsenceItem.EmpCode);
                            continue;
                        }
                    }



                    var AddAbsence = new HrAbsence
                    {
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        Type = SingleAsenceItem.Type.ToString(),
                        AbsenceTypeId = 0,
                        AbsenceDate = SingleAsenceItem.AbsenceDate,
                        Note = SingleAsenceItem.Note,
                        EmpId = checkEmpExist.Id,
                        LocationId = checkEmpExist.Location,
                        TimeTableId = Convert.ToInt32(GetTimeTableID?.TimeTableId)
                    };
                    var newAbsenceEntity = await hrRepositoryManager.HrAbsenceRepository.AddAndReturn(AddAbsence);

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    if (SingleAsenceItem.DisciplinaryCaseId > 0 && SingleAsenceItem.ApplyPlenties)
                    {

                        var Total = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 3, checkEmpExist.Id);
                        string NumberofRepetitionDays = await configuration.GetValue(117, session.FacilityId);
                        if (string.IsNullOrEmpty(NumberofRepetitionDays))
                        {
                            NumberofRepetitionDays = "0";
                        }
                        var DueDate2 = DateHelper.StringToDate(SingleAsenceItem.AbsenceDate).AddDays(-Convert.ToDouble(NumberofRepetitionDays)).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        // عدد المخالفات الغياب خلال 180 يوم

                        var GetFromHRDisciplinaryCaseAction = await hrRepositoryManager.HrDisciplinaryCaseActionRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.DisciplinaryCaseId == SingleAsenceItem.DisciplinaryCaseId);
                        if (GetFromHRDisciplinaryCaseAction.Any())
                        {
                            var filterDate = GetFromHRDisciplinaryCaseAction.Where(x => x.DueDate != null
                            && DateHelper.StringToDate(x.DueDate) >= DateHelper.StringToDate(DueDate2) &&
                            DateHelper.StringToDate(x.DueDate) <= DateHelper.StringToDate(SingleAsenceItem.AbsenceDate)
                            ).ToList();
                            if (filterDate.Any())
                            {
                                RepeatCount = filterDate.Count;
                            }
                            else
                            {
                                RepeatCount = 0;
                            }
                        }
                        else
                        {
                            RepeatCount = 0;
                        }
                        var GetFromHRDisciplinaryRule = await hrRepositoryManager.HrDisciplinaryRuleRepository.GetOne(x => x.DisciplinaryCaseId == SingleAsenceItem.DisciplinaryCaseId && (RepeatCount + 1 >= x.ReptFrom && RepeatCount + 1 <= x.ReptTo));



                        var AddDisciplinaryCaseAction = new HrDisciplinaryCaseAction
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            StatusId = 1,
                            DisciplinaryCaseId = SingleAsenceItem.DisciplinaryCaseId,
                            DeductedAmount = Math.Round((decimal)(GetFromHRDisciplinaryRule.DeductedRate * Total / 30)),
                            DeductedRate = (GetFromHRDisciplinaryRule.DeductedRate) ?? 0m,
                            EmpId = checkEmpExist.Id,
                            CountRept = RepeatCount,
                            Description = SingleAsenceItem.Note,
                            DueDate = SingleAsenceItem.AbsenceDate,
                            ActionType = GetFromHRDisciplinaryRule.ActionType,
                            VisitScheduleDId = 0
                        };
                        await hrRepositoryManager.HrDisciplinaryCaseActionRepository.Add(AddDisciplinaryCaseAction);

                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                    SavedCount += 1; ;


                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                if (SavedCount > 0)
                {
                    result.SavedAbsenceRecord = $" تم إضافة حالة غياب لعدد  {SavedCount}  من الموظفين بنجاح ";

                }
                if (NotFoundCount.Any())
                {
                    result.EmpNotAviable = $" الموظفين المذكورين غير موجودين في قائمة الموظفين :  {string.Join(", ", NotFoundCount)} .";

                }
                if (NotActiveCount.Any())
                {
                    result.EmpNotActiveAble = $" الموظفين المذكورين لديهم انهاء خدمة مسبقاَ:  {string.Join(", ", NotActiveCount)} .";

                }
                if (AbsenceCount.Any())
                {
                    result.AbsenceDateAviable = $" يوجد غياب للموظفين في نفس التاريخ :  {string.Join(", ", AbsenceCount)} .";

                }
                if (DelayCount.Any())
                {
                    result.DelayAviable = $" تم تسجيل تأخير  للموظفين في نفس التاريخ :  {string.Join(", ", DelayCount)} .";

                }
                if (VacationCount.Any())
                {
                    result.VacationAviable = $" تم تسجيل إجازة  للموظفين في نفس التاريخ :  {string.Join(", ", VacationCount)} .";

                }
                if (PayrollCount.Any())
                {
                    result.PayrollAviable = $" تم إستخراج مسير  للموظفين في نفس الشهر :  {string.Join(", ", PayrollCount)} .";

                }
                if (DOAppointmentCount.Any())
                {
                    result.DOAppointmentAviable = $" تاريخ الغياب قبل تاريخ التعين للموظفين :  {string.Join(", ", DOAppointmentCount)} .";

                }
                return await Result<AddAbsenceFromExcelResultDto>.SuccessAsync(result, localization.GetResource1("CreateSuccess"));


            }
            catch (Exception exp)
            {

                return await Result<AddAbsenceFromExcelResultDto>.FailAsync($"{localization.GetResource1("ErrorOccurredDuring")}    {exp.Message}");
            }
        }

        public async Task<IResult<string>> AbsenceNotAttendance(AbsenceNotAttendanceDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = mainRepositoryManager.StoredProceduresRepository.Absence_NotAttendance(entity);
                if (result > 0)
                    return await Result<string>.SuccessAsync(localization.GetResource1("CreateSuccess"));
                return await Result<string>.SuccessAsync(" ليس هناك موظفين غير حاضرين");


            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in AbsenceNotAttendance at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> AbsenceForNewInterval(HrAbsenceAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                int? count = 0;
                count = entity.DaysCount;
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(i => i.EmpId == entity.EmpCode && i.Isdel == false && i.IsDeleted == false);

                if (checkEmpExist == null)
                {
                    return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                }
                if (checkEmpExist.StatusId == 2)
                {
                    return await Result<string>.FailAsync(localization.GetResource1("EmpNotActive"));

                }
                var GetTimeTableID = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOneVw(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                for (int i = 0; i <= count - 1; i++)
                {
                    var newDate = DateHelper.StringToDate(entity.AbsenceDate).AddDays(i);
                    var StringDate = DateHelper.DateToString(newDate, CultureInfo.InvariantCulture);
                    //CheckIsAbsenceInSameDay
                    var CheckIsAbsenceInSameDay = await hrRepositoryManager.HrAbsenceRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AbsenceDate == StringDate);
                    if (CheckIsAbsenceInSameDay.Any())
                    {
                        return await Result<string>.FailAsync($"{localization.GetMessagesResource("")} {StringDate}");
                    }
                    // CheckIsVacationInSameDay
                    var CheckIsVacationsInSameDay = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                    if (CheckIsVacationsInSameDay.Any())
                    {
                        var msDateStr = newDate.ToString();

                        var filterResult = CheckIsVacationsInSameDay
                                        .Where(e =>
                                        {
                                            // تحويل التواريخ مع التحقق من الصحة
                                            bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                            bool isStartDateValid = DateTime.TryParseExact(e.VacationSdate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                            bool isEndDateValid = DateTime.TryParseExact(e.VacationEdate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                            return isDateValid && isStartDateValid && isEndDateValid &&
                                                   msDate >= startDate && msDate <= endDate;
                                        })
                                        .ToList();
                        //var filterResult = CheckIsVacationsInSameDay.Where(e => newDate >= DateHelper.StringToDate(e.VacationSdate) && newDate <= DateHelper.StringToDate(e.VacationEdate));
                        if (filterResult.Any())
                        {
                            return await Result<string>.FailAsync($" تم تسجيل إجازة لهذا الموظف سابقاً - لن تتمكن من تسجيل غياب في نفس اليوم بتاريخ {StringDate}");

                        }
                    }
                    //CheckIsDelayInSameDay
                    var CheckIsDelayInSameDay = await hrRepositoryManager.HrDelayRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.DelayDate == StringDate);
                    if (CheckIsDelayInSameDay.Any())
                    {
                        return await Result<string>.FailAsync($"تم تسجيل تأخر لهذا الموظف سابقاً - لن تتمكن من تسجيل غياب في نفس اليوم بتاريخ {StringDate}");

                    }

                    //CheckDateAbsencesIsBeforeDOAppointment

                    var CheckDateAbsencesIsBeforeDOAppointment = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.IsDeleted == false && x.Id == checkEmpExist.Id && x.Doappointment != null);
                    if (DateHelper.StringToDate(CheckDateAbsencesIsBeforeDOAppointment.Doappointment) > newDate)
                    {
                        return await Result<string>.FailAsync("تاريخ الغياب قبل تاريخ التعيين للموظف ");

                    }
                    // Check_Emp_Exists_In_Payroll
                    var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                    if (IfEmpExistsInPayroll.Any())
                    {

                        var msDateStr = newDate.ToString();

                        var filterResult = IfEmpExistsInPayroll
                                        .Where(e =>
                                        {
                                            // تحويل التواريخ مع التحقق من الصحة
                                            bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                            bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                            bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                            return isDateValid && isStartDateValid && isEndDateValid &&
                                                   msDate >= startDate && msDate <= endDate;
                                        })
                                        .ToList();
                        if (filterResult.Any())
                        {
                            return await Result<string>.FailAsync($"لن تتمكن من اضافة غياب بسبب استخراج مسير للموظف في نفس الشهر {StringDate}");
                        }
                    }

                    var AddAbsence = new HrAbsence
                    {
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        Type = entity.Type.ToString(),
                        AbsenceTypeId = 0,
                        AbsenceDate = StringDate,
                        Note = entity.Note,
                        EmpId = checkEmpExist.Id,
                        LocationId = checkEmpExist.Location,
                        TimeTableId = Convert.ToInt32(GetTimeTableID?.TimeTableId)
                    };
                    var newAbsenceEntity = await hrRepositoryManager.HrAbsenceRepository.AddAndReturn(AddAbsence);

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    if (entity.DisciplinaryCaseId > 0 && entity.ApplyPlenties)
                    {
                        var AddDisciplinaryCaseAction = new HrDisciplinaryCaseAction
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            StatusId = 1,
                            DisciplinaryCaseId = entity.DisciplinaryCaseId,
                            DeductedAmount = entity.DeductedAmount,
                            DeductedRate = entity.DeductedRate,
                            EmpId = checkEmpExist.Id,
                            CountRept = entity.CountRept,
                            Description = entity.Note,
                            DueDate = StringDate,
                            ActionType = entity.ActionType,
                            VisitScheduleDId = 0

                        };
                        await hrRepositoryManager.HrDisciplinaryCaseActionRepository.Add(AddDisciplinaryCaseAction);

                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("CreateSuccess"), 200);
            }
            catch (Exception exp)
            {

                return await Result<string>.FailAsync($"{localization.GetResource1("ErrorOccurredDuring")}    {exp.Message}");
            }
        }

        public async Task<IResult<AddMultiAbsenceAddResultDto>> MultiAbsenceAdd(HrMultiAbsenceAddDto entity, CancellationToken cancellationToken = default)
        {
            AddMultiAbsenceAddResultDto result = new AddMultiAbsenceAddResultDto();
            int? SavedCount = 0;
            List<string> NotFoundCount = new List<string>();
            List<string> NotActiveCount = new List<string>();
            List<string> PayrollCount = new List<string>();
            List<string> VacationCount = new List<string>();
            List<string> DelayCount = new List<string>();
            List<string> AbsenceCount = new List<string>();
            List<string> DOAppointmentCount = new List<string>();
            int? RepeatCount = 0;
            try
            {
                if (entity == null) return await Result<AddMultiAbsenceAddResultDto>.FailAsync($"The Passed Data is Null");
                if (entity.EmpCode.Count <= 0) return await Result<AddMultiAbsenceAddResultDto>.FailAsync($"يجب تحديد موظفين");

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                foreach (var SingleEmpCode in entity.EmpCode)
                {
                    var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(i => i.EmpId == SingleEmpCode && i.Isdel == false && i.IsDeleted == false);

                    if (checkEmpExist == null)
                    {
                        NotFoundCount.Add(SingleEmpCode);
                        continue;
                    }

                    if (checkEmpExist.StatusId == 2)
                    {
                        NotActiveCount.Add(SingleEmpCode);
                        continue;
                    }
                    var GetTimeTableID = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOneVw(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);

                    //CheckIsAbsenceInSameDay
                    var CheckIsAbsenceInSameDay = await hrRepositoryManager.HrAbsenceRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AbsenceDate == entity.AbsenceDate);
                    if (CheckIsAbsenceInSameDay.Any())
                    {
                        AbsenceCount.Add(SingleEmpCode);
                        continue;
                    }

                    // CheckIsVacationInSameDay
                    var CheckIsVacationsInSameDay = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                    if (CheckIsVacationsInSameDay.Any())
                    {

                        var filterResult = CheckIsVacationsInSameDay.Where(e => DateHelper.StringToDate(entity.AbsenceDate) >= DateHelper.StringToDate(e.VacationSdate) && DateHelper.StringToDate(entity.AbsenceDate) <= DateHelper.StringToDate(e.VacationEdate));
                        if (filterResult.Any())
                        {
                            VacationCount.Add(SingleEmpCode);
                            continue;
                        }
                    }


                    //CheckIsDelayInSameDay
                    var CheckIsDelayInSameDay = await hrRepositoryManager.HrDelayRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.DelayDate == entity.AbsenceDate);
                    if (CheckIsDelayInSameDay.Any())
                    {
                        DelayCount.Add(SingleEmpCode);
                        continue;
                    }
                    //CheckDateAbsencesIsBeforeDOAppointment

                    var CheckDateAbsencesIsBeforeDOAppointment = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.IsDeleted == false && x.Id == checkEmpExist.Id && x.Doappointment != null);

                    if (CheckDateAbsencesIsBeforeDOAppointment == null)
                    {
                        NotFoundCount.Add(SingleEmpCode);
                        continue;
                    }
                    if (DateHelper.StringToDate(CheckDateAbsencesIsBeforeDOAppointment.Doappointment) > DateHelper.StringToDate(entity.AbsenceDate))
                    {
                        DOAppointmentCount.Add(SingleEmpCode);
                        continue;
                    }
                    // Check_Emp_Exists_In_Payroll
                    var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                    if (IfEmpExistsInPayroll.Any())
                    {

                        var msDateStr = entity.AbsenceDate;

                        var filterResult = IfEmpExistsInPayroll
                                        .Where(e =>
                                        {
                                            // تحويل التواريخ مع التحقق من الصحة
                                            bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                            bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                            bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                            return isDateValid && isStartDateValid && isEndDateValid &&
                                                   msDate >= startDate && msDate <= endDate;
                                        })
                                        .ToList();
                        if (filterResult.Any())
                        {
                            PayrollCount.Add(SingleEmpCode);
                            continue;
                        }
                    }



                    var AddAbsence = new HrAbsence
                    {
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        Type = entity.Type.ToString(),
                        AbsenceTypeId = 0,
                        AbsenceDate = entity.AbsenceDate,
                        Note = entity.Note,
                        EmpId = checkEmpExist.Id,
                        LocationId = checkEmpExist.Location,
                        TimeTableId = Convert.ToInt32(GetTimeTableID?.TimeTableId)
                    };
                    var newAbsenceEntity = await hrRepositoryManager.HrAbsenceRepository.AddAndReturn(AddAbsence);

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    if (entity.DisciplinaryCaseId > 0 && entity.ApplyPlenties)
                    {

                        var AddDisciplinaryCaseAction = new HrDisciplinaryCaseAction
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            StatusId = 1,
                            DisciplinaryCaseId = entity.DisciplinaryCaseId,
                            DeductedAmount = entity.DeductedAmount,
                            DeductedRate = entity.DeductedRate,
                            EmpId = checkEmpExist.Id,
                            CountRept = entity.CountRept,
                            Description = entity.Note,
                            DueDate = entity.AbsenceDate,
                            ActionType = entity.ActionType,
                            VisitScheduleDId = 0
                        };
                        await hrRepositoryManager.HrDisciplinaryCaseActionRepository.Add(AddDisciplinaryCaseAction);

                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                    SavedCount += 1;

                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                if (SavedCount > 0)
                {
                    result.SavedAbsenceRecord = $" تم إضافة حالة غياب لعدد  {SavedCount}  من الموظفين بنجاح ";

                }
                if (NotFoundCount.Any())
                {
                    result.EmpNotAviable = $" الموظفين المذكورين غير موجودين في قائمة الموظفين :  {string.Join(", ", NotFoundCount)} .";

                }
                if (NotActiveCount.Any())
                {
                    result.EmpNotActiveAble = $" الموظفين المذكورين لديهم انهاء خدمة مسبقاَ:  {string.Join(", ", NotActiveCount)} .";

                }
                if (AbsenceCount.Any())
                {
                    result.AbsenceDateAviable = $" يوجد غياب للموظفين في نفس التاريخ :  {string.Join(", ", AbsenceCount)} .";

                }
                if (DelayCount.Any())
                {
                    result.DelayAviable = $" تم تسجيل تأخير  للموظفين في نفس التاريخ :  {string.Join(", ", DelayCount)} .";

                }
                if (VacationCount.Any())
                {
                    result.VacationAviable = $" تم تسجيل إجازة  للموظفين في نفس التاريخ :  {string.Join(", ", VacationCount)} .";

                }
                if (PayrollCount.Any())
                {
                    result.PayrollAviable = $" تم إستخراج مسير  للموظفين في نفس الشهر :  {string.Join(", ", PayrollCount)} .";

                }
                if (DOAppointmentCount.Any())
                {
                    result.DOAppointmentAviable = $" تاريخ الغياب قبل تاريخ التعين للموظفين :  {string.Join(", ", DOAppointmentCount)} .";

                }
                return await Result<AddMultiAbsenceAddResultDto>.SuccessAsync(result, localization.GetResource1("CreateSuccess"));

            }
            catch (Exception)
            {

                return await Result<AddMultiAbsenceAddResultDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));

            }

        }
        public async Task<IResult<IEnumerable<HRApprovalAbsencesReportDto>>> HRApprovalAbsencesReport(HRApprovalAbsencesReportFilterDto filter)
        {
            try
            {
                var Branchs = session.Branches.Split(',');
                filter.BranchsId = string.Join(",", Branchs);
                if (filter.FromDate == null || filter.ToDate == null)
                {
                    return await Result<List<HRApprovalAbsencesReportDto>>.FailAsync("من فضلك ادخل تاريخ البداية والنهاية");
                }
                var result = await mainRepositoryManager.StoredProceduresRepository.HRApprovalAbsencesReport(filter);
                return await Result<IEnumerable<HRApprovalAbsencesReportDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRApprovalAbsencesReportDto>>.FailAsync($"EXP in {nameof(HRApprovalAbsencesReport)} at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<List<HrAbsenceFilterDto>>> Search(HrAbsenceFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                List<HrAbsenceFilterDto> result = new List<HrAbsenceFilterDto>();
                filter.BranchId ??= 0;
                filter.DeptId ??= 0;
                filter.Location ??= 0;
                filter.StatusId ??= 0;
                var BranchesList = session.Branches.Split(',');
                var items = await hrRepositoryManager.HrAbsenceRepository.GetAllVw(x => x.IsDeleted == false 
                && (filter.BranchId != 0 ? x.BranchId == filter.BranchId : BranchesList.Contains(x.BranchId.ToString()))
                && (string.IsNullOrEmpty(filter.EmpCode) || x.EmpCode == filter.EmpCode)
                && (string.IsNullOrEmpty(filter.EmpName) || x.EmpName.ToLower().Contains(filter.EmpName.ToLower()))
                && (filter.DeptId == 0 || filter.DeptId == x.DeptId)
                && (filter.Location == 0 || filter.Location == x.Location)
                && (filter.StatusId == 0 || filter.StatusId == x.StatusId)
                );
                if (items != null)
                {
                    if (items.Count() > 0)
                    {

                        var res = items.AsQueryable();
                        if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
                        {
                            res = res.Where(r => r.AbsenceDate != null &&
                            DateHelper.StringToDate(r.AbsenceDate) >= DateHelper.StringToDate(filter.FromDate) &&
                           DateHelper.StringToDate(r.AbsenceDate) <= DateHelper.StringToDate(filter.ToDate));
                        }

                        if (res.Any())
                        {

                            foreach (var item in res)
                            {
                                var newItem = new HrAbsenceFilterDto
                                {
                                    Id = item.Id,
                                    EmpCode = item.EmpCode,
                                    EmpName = session.Language == 1 ? item.EmpName : item.EmpName2,
                                    AbsenceDate = session.CalendarType == "1" ? item.AbsenceDate : Bahsas.GregorianToHijri(item.AbsenceDate),
                                    Note = item.Note,
                                    DepName = session.Language == 1 ? item.DepName : item.DepName2,
                                    LocationName = session.Language == 1 ? item.LocationName : item.LocationName2,
                                };
                                result.Add(newItem);
                            }
                            return await Result<List<HrAbsenceFilterDto>>.SuccessAsync(result, "");

                        }
                        return await Result<List<HrAbsenceFilterDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
                    }
                    return await Result<List<HrAbsenceFilterDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
                }
                return await Result<List<HrAbsenceFilterDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception ex)
            {
                return await Result<List<HrAbsenceFilterDto>>.FailAsync(ex.Message);
            }
        }
    }
}