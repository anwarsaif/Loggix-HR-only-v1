using AutoMapper;
using DocumentFormat.OpenXml.Vml.Office;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Globalization;
using IResult = Logix.Application.Wrapper.IResult;

namespace Logix.Application.Services.HR
{
    public class HrDelayService : GenericQueryService<HrDelay, HrDelayDto, HrDelayVw>, IHrDelayService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;


        public HrDelayService(IQueryRepository<HrDelay> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
        IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session, ILocalizationService localization, ISysConfigurationAppHelper sysConfigurationAppHelper) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrDelayDto>> Add(HrDelayDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDelayDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrDelayDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                if (checkEmpExist.StatusId == 2) return await Result<HrDelayDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                if (IfEmpExistsInPayroll.Any())
                {

                    var msDateStr = entity.DelayDate;

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
                        return await Result<HrDelayDto>.FailAsync("لن تتمكن من اضافة تأخر بسبب استخراج مسير للموظف في نفس الشهر");
                    }
                }


                var checkIfAbsence = await hrRepositoryManager.HrAbsenceRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AbsenceDate == entity.DelayDate);
                if (checkIfAbsence.Any()) return await Result<HrDelayDto>.FailAsync("تم تسجيل غياب لهذا الموظف سابقاً - لن تتمكن من تسجيل غياب وتأخر في نفس اليوم");

                // Add ":00" if DelayTimeString doesn't contain ":"
                if (!string.IsNullOrEmpty(entity.DelayTimeString) && !entity.DelayTimeString.Contains(":")) { entity.DelayTimeString += ":00"; }
                var itemMapping = _mapper.Map<HrDelay>(entity);
                itemMapping.EmpId = checkEmpExist.Id;
                TimeSpan DelayTime = TimeSpan.Parse(entity.DelayTimeString);
                itemMapping.DelayTime = DelayTime;
                itemMapping.CreatedBy = session.UserId;
                itemMapping.CreatedOn = DateTime.Now;
                itemMapping.IsDeleted = false;
                var newEntity = await hrRepositoryManager.HrDelayRepository.AddAndReturn(itemMapping);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDelayDto>(newEntity);


                return await Result<HrDelayDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {

                return await Result<HrDelayDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message} {localization.GetResource1("ErrorOccurredDuring")}");
            }
        }

        public async Task<IResult<HrDelayDto>> Add(HrDelayAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDelayDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrDelayDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                if (checkEmpExist.StatusId == 2) return await Result<HrDelayDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                if (IfEmpExistsInPayroll.Any())
                {
                    var msDateStr = entity.DelayDate;

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
					//var filterResult = IfEmpExistsInPayroll.Where(e => DateHelper.StringToDate(entity.DelayDate) >= DateHelper.StringToDate(e.StartDate) && DateHelper.StringToDate(entity.DelayDate) <= DateHelper.StringToDate(e.EndDate));
					if (filterResult.Any())
                        {
                            return await Result<HrDelayDto>.FailAsync("لن تتمكن من اضافة تأخر بسبب استخراج مسير للموظف في نفس الشهر");
                        }
                 }
                


                var checkIfAbsence = await hrRepositoryManager.HrAbsenceRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AbsenceDate == entity.DelayDate);
                if (checkIfAbsence.Any()) return await Result<HrDelayDto>.FailAsync("تم تسجيل غياب لهذا الموظف سابقاً - لن تتمكن من تسجيل غياب وتأخر في نفس اليوم");

                if (!string.IsNullOrEmpty(entity.DelayTimeString) && !entity.DelayTimeString.Contains(":")) { entity.DelayTimeString += ":00"; }

                var itemMapping = _mapper.Map<HrDelay>(entity);
                itemMapping.EmpId = checkEmpExist.Id;
                TimeSpan DelayTime = TimeSpan.Parse(entity.DelayTimeString);
                itemMapping.DelayTime = DelayTime;
                itemMapping.CreatedBy = session.UserId;
                itemMapping.CreatedOn = DateTime.Now;
                itemMapping.IsDeleted = false;
                var newEntity = await hrRepositoryManager.HrDelayRepository.AddAndReturn(itemMapping);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDelayDto>(newEntity);


                return await Result<HrDelayDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {

                return await Result<HrDelayDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message} {localization.GetResource1("ErrorOccurredDuring")}");
            }
        }

        public async Task<IResult<HrDelayNonCheckoutDto>> DelayNonCheckout(HrDelayNonCheckoutDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                string? startDate = entity.FromDate;
                string? endDate = entity.ToDate;
                var getPropertyValue = await sysConfigurationAppHelper.GetValue(19);
                if (getPropertyValue == "2")
                {
                    var getFromSysCalendarStartDate = await mainRepositoryManager.SysCalendarRepository.GetOne(x => x.HDate == entity.FromDate);
                    if (getFromSysCalendarStartDate != null)
                    {
                        startDate = getFromSysCalendarStartDate.GDate;
                    }
                    var getFromSysCalendarEndDate = await mainRepositoryManager.SysCalendarRepository.GetOne(x => x.HDate == entity.ToDate);
                    if (getFromSysCalendarEndDate != null)
                    {
                        endDate = getFromSysCalendarEndDate.GDate;
                    }
                }

                var getfromHrAttendances = await hrRepositoryManager.HrAttendanceRepository.GetAll(a => a.IsDeleted == false && a.TimeOut == null);
                var attendanceData = getfromHrAttendances.Where(x => DateHelper.StringToDate(x.DayDateGregorian) >= DateHelper.StringToDate(startDate) && DateHelper.StringToDate(x.DayDateGregorian) <= DateHelper.StringToDate(endDate));
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                if (attendanceData != null)
                {
                    foreach (var item in attendanceData)
                    {
                        var getElement = await hrRepositoryManager.HrAttendanceRepository.GetById(item.AttendanceId);
                        if (getElement != null)
                        {
                            var getfromAttendanceTimeTables = await hrRepositoryManager.HrAttTimeTableRepository.GetOne(t => t.Id == getElement.TimeTableId);
                            var DefTimeOutString = getElement.DayDateGregorian + " " + getfromAttendanceTimeTables.OffDutyTime;
                            var DefTimeOutDateTime = DateTime.ParseExact(DefTimeOutString, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                            //DateTime.Parse(DefTimeOutString);
                            getElement.TimeOut = getElement.TimeIn;
                            getElement.DefTimeOut = DefTimeOutDateTime;
                            getElement.NoteOut = "";
                            getElement.ModifiedBy = session.UserId;
                            getElement.ModifiedOn = DateTime.Now;
                            getElement.LogOutBy = getElement.LogInBy;
                            hrRepositoryManager.HrAttendanceRepository.Update(getElement);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                        else
                        {
                            return await Result<HrDelayNonCheckoutDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));

                        }

                    }
                    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                    return await Result<HrDelayNonCheckoutDto>.SuccessAsync(localization.GetResource1("CreateSuccess"));

                }
                return await Result<HrDelayNonCheckoutDto>.SuccessAsync("");

            }
            catch (Exception)
            {

                return await Result<HrDelayNonCheckoutDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult<HrDelayDto>> DeleteAllSelected(List<long> Ids, CancellationToken cancellationToken = default)
        {
            if (Ids.Count() <= 0) return Result<HrDelayDto>.Fail($"قم بتحديد السجلات المراد حذفها");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                foreach (var item in Ids)
                {
                    var checkDelayExist = await hrRepositoryManager.HrDelayRepository.GetOne(e => e.Id == item);
                    if (checkDelayExist == null) return Result<HrDelayDto>.Fail($"--- there is no Data with this id: {item}---");
                    var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkDelayExist.EmpId && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                    if (IfEmpExistsInPayroll.Any())
                    {
						var absenceDateStr = checkDelayExist.DelayDate;
						var payrollList = IfEmpExistsInPayroll.ToList(); // جلب البيانات أولاً

						var filterResult = payrollList
										.Where(e =>
										{
											// تحويل التواريخ مع التحقق من الصحة
											bool isAbsenceDateValid = DateTime.TryParseExact(absenceDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime absenceDate);
											bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
											bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

											return isAbsenceDateValid && isStartDateValid && isEndDateValid &&
												   absenceDate >= startDate && absenceDate <= endDate;
										})
										.ToList();
						//var filterResult = IfEmpExistsInPayroll.Where(e => DateHelper.StringToDate(checkDelayExist.DelayDate) >= DateHelper.StringToDate(e.StartDate) && DateHelper.StringToDate(checkDelayExist.DelayDate) <= DateHelper.StringToDate(e.EndDate));
						if (filterResult.Any())
                            {
                               

                            return await Result<HrDelayDto>.SuccessAsync("لم يتم حذف التأخرات بسبب ارتباط التأخرات بمسير رواتب للموظف ");


                        }
                    }
                    // هنا يحق لنا الحذف 
                    checkDelayExist.IsDeleted = true;
                    checkDelayExist.ModifiedOn = DateTime.Now;
                    checkDelayExist.ModifiedBy = session.UserId;
                    hrRepositoryManager.HrDelayRepository.Update(checkDelayExist);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrDelayDto>.SuccessAsync(localization.GetResource1("DeleteSuccess"));

            }
            catch (Exception exp)
            {

                return await Result<HrDelayDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var checkDelayExist = await hrRepositoryManager.HrDelayRepository.GetOne(e => e.Id == Id);
                if (checkDelayExist == null) return Result<HrDelayDto>.Fail($"--- there is no Data with this id: {Id}---");
                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkDelayExist.EmpId && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                if (IfEmpExistsInPayroll.Any())
                {
                 var absenceDateStr = checkDelayExist.DelayDate;
					var payrollList = IfEmpExistsInPayroll.ToList(); // جلب البيانات أولاً

					var filterResult = payrollList
									.Where(e =>
									{
										// تحويل التواريخ مع التحقق من الصحة
										bool isAbsenceDateValid = DateTime.TryParseExact(absenceDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime absenceDate);
										bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
										bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

										return isAbsenceDateValid && isStartDateValid && isEndDateValid &&
											   absenceDate >= startDate && absenceDate <= endDate;
									})
									.ToList();
                    //var filterResult = IfEmpExistsInPayroll.Where(e => DateHelper.StringToDate(checkDelayExist.DelayDate) >= DateHelper.StringToDate(e.StartDate) && DateHelper.StringToDate(checkDelayExist.DelayDate) <= DateHelper.StringToDate(e.EndDate));
                    if (filterResult.Any())
                    {
                   

                        return await Result.SuccessAsync("لم يتم حذف التأخرات بسبب ارتباط التأخرات بمسير رواتب للموظف ");

                    }
                }
                // هنا يحق لنا الحذف 
                checkDelayExist.IsDeleted = true;
                checkDelayExist.ModifiedOn = DateTime.Now;
                checkDelayExist.ModifiedBy = session.UserId;
                hrRepositoryManager.HrDelayRepository.Update(checkDelayExist);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDelayDto>.SuccessAsync(_mapper.Map<HrDelayDto>(checkDelayExist), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {

                return await Result<HrDelayDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrDelayEditDto>> Update(HrDelayEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDelayEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");
            try
            {

                var item = await hrRepositoryManager.HrDelayRepository.GetById(entity.Id);

                if (item == null) return await Result<HrDelayEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(entity, item);

                hrRepositoryManager.HrDelayRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDelayEditDto>.SuccessAsync(_mapper.Map<HrDelayEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrDelayEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> MakeApprove(string EmpCode, string ApproveDate, string HoursMins, CancellationToken cancellationToken = default)
        {
            try
            {
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                // Check if Emp Is Absence In Same Day
                var CheckIsAbsence = await hrRepositoryManager.HrAbsenceRepository.GetAll(x => x.IsDeleted == false && x.AbsenceDate == ApproveDate && x.EmpId == checkEmpExist.Id);
                if (CheckIsAbsence.Count() > 0) return await Result<string>.FailAsync(localization.GetMessagesResource("EmployeeAbsencePreviouslyRecorded"));

                // Check if Emp Is Delay In Same Day
                var CheckIsDelay = await hrRepositoryManager.HrDelayRepository.GetAll(x => x.IsDeleted == false && x.DelayDate == ApproveDate && x.EmpId == checkEmpExist.Id);
                if (CheckIsDelay.Count() > 0) return await Result<string>.FailAsync(localization.GetMessagesResource("DelayRecordedForEmployeeCannotRecordDelay"));
                var DelayEntity = new HrDelay();
                DelayEntity.IsDeleted = false;
                DelayEntity.CreatedBy = session.UserId;
                DelayEntity.CreatedOn = DateTime.Now;
                DelayEntity.Note = localization.GetMessagesResource("TotalFromReport");
                DelayEntity.EmpId = checkEmpExist.Id;
                DelayEntity.DelayDate = ApproveDate;

                TimeSpan timespan = TimeSpan.Parse(HoursMins);
                string output = "";
                short i = 0;
                short j;
                if (timespan.TotalHours > 23)
                {
                    i = (short)timespan.TotalDays;
                    for (j = 0; j <= i; j++)
                    {
                        DateTime startdate = DateTime.Now.AddDays(-j); // Assuming TxtDateDly.Text is the current date
                        DelayEntity.DelayDate = startdate.ToString("yyyy/MM/dd");
                        if (j == i)
                        {
                            output = "0" + timespan.ToString("h\\:mm");
                            DelayEntity.DelayTime = TimeSpan.Parse(output);
                            await hrRepositoryManager.HrDelayRepository.Add(DelayEntity);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }
                        else
                        {
                            output = "24:00";
                            DelayEntity.DelayTime = TimeSpan.Parse(output);
                            await hrRepositoryManager.HrDelayRepository.Add(DelayEntity);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }
                }
                else
                {
                    output = timespan.ToString().Substring(0, 5);
                    DelayEntity.DelayTime = TimeSpan.Parse(output);
                    await hrRepositoryManager.HrDelayRepository.Add(DelayEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }



                return await Result<string>.SuccessAsync(localization.GetResource1("CreateSuccess"));

            }
            catch (Exception exp)
            {

                return await Result<string>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");

            }
        }
        private async Task<decimal> ApplyPoliciesAsync(long facilityId, long policyId, long? empId)
        {
            // Initialize amounts
            decimal totalDeductionAmount = 0;
            decimal totalAllowanceAmount = 0;
            decimal totalDeductionCustomAmount = 0;
            decimal totalAllawanceCustomAmount = 0;

            // Fetch policy details
            var policy = await hrRepositoryManager.HrPolicyRepository.GetOne(p => p.FacilityId == facilityId && p.IsDeleted == false && p.PolicieId == policyId);
            if (policy == null)
            {
                // Default settings based on policyId
                int defaultRateType = policyId switch
                {
                    2 => 1,
                    3 => 2,
                    4 => 3,
                    5 => 2,
                    8 => 1,
                    _ => throw new Exception("Invalid Policy ID")
                };

                // Create a default policy object
                policy = new HrPolicy
                {
                    RateType = defaultRateType
                };
            }

            // Fetch employee salary
            var employee = await hrRepositoryManager.HrEmployeeRepository.GetOne(e => e.Id == empId);
            if (employee == null)
            {
                throw new Exception("Employee not found.");
            }

            // Calculate total allowance and deductions
            var totalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(ad => ad.IsDeleted == false && ad.EmpId == empId && ad.FixedOrTemporary == 1 && ad.TypeId == 2);
            totalDeductionAmount = totalDeduction.Sum(x => x.Amount) ?? 0;

            var totalAllawance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(ad => ad.IsDeleted == false && ad.EmpId == empId && ad.FixedOrTemporary == 1 && ad.TypeId == 1);
            totalAllowanceAmount = totalAllawance.Sum(x => x.Amount) ?? 0;

            // Calculate custom allowance and deductions
            if (!string.IsNullOrEmpty(policy.Deductions))
            {
                var deductionIds = policy.Deductions.Split(",").Select(int.Parse).ToList();
                var totalDeductionCustom = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(ad => ad.IsDeleted == false && ad.EmpId == empId && ad.FixedOrTemporary == 1 && ad.TypeId == 2 && deductionIds.Contains((int)ad.AdId));
                totalDeductionCustomAmount = totalDeductionCustom.Sum(x => x.Amount) ?? 0;
            }

            if (!string.IsNullOrEmpty(policy.Allawance))
            {
                var allowanceIds = policy.Allawance.Split(",").Select(int.Parse).ToList();
                var totalAllawanceCustom = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(ad => ad.IsDeleted == false && ad.EmpId == empId && ad.FixedOrTemporary == 1 && ad.TypeId == 1 && allowanceIds.Contains((int)ad.AdId));
                totalAllawanceCustomAmount = totalAllawanceCustom.Sum(x => x.Amount) ?? 0;
            }

            // Calculate return amount based on rate type
            decimal retAmount = policy.RateType switch
            {
                1 => employee.Salary ?? 0,
                2 => (employee.Salary ?? 0) + totalAllowanceAmount,
                3 => (employee.Salary ?? 0) + totalAllowanceAmount - totalDeductionAmount,
                4 => (policy.Salary == true ? (employee.Salary ?? 0) : 0) + totalAllawanceCustomAmount - totalDeductionCustomAmount,
                _ => throw new Exception("Invalid Rate Type")
            };

            return retAmount;
        }

        public async Task<IResult<List<HrDelayFilterDto>>> Search(HrDelayFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');

                List<HrDelayFilterDto> resultList = new List<HrDelayFilterDto>();
                var items = await hrRepositoryManager.HrDelayRepository.GetAllVw(e => e.IsDeleted == false && BranchesList.Contains(e.BranchId.ToString()) && e.FacilityId == session.FacilityId);
                if (items != null)
                {
                    if (items.Count() > 0)
                    {
                        var res = items.AsQueryable();

                        if (!string.IsNullOrEmpty(filter.EmpCode))
                        {
                            res = res.Where(r => r.EmpCode != null && r.EmpCode == filter.EmpCode);
                        }
                        if (!string.IsNullOrEmpty(filter.EmpName))
                        {
                            res = res.Where(c => (c.EmpName != null && c.EmpName.ToLower().Contains(filter.EmpName.ToLower())));
                        }
                        if (filter.LocationId != null && filter.LocationId > 0)
                        {
                            res = res.Where(c => c.Location != null && c.Location == filter.LocationId);
                        }
                        if (filter.TypeId != null && filter.TypeId > 0)
                        {
                            res = res.Where(c => c.TypeId != null && c.TypeId == filter.TypeId);
                        }
                        if (filter.BranchId != null && filter.BranchId > 0)
                        {
                            res = res.Where(c => c.BranchId != null && c.BranchId == filter.BranchId);
                        }

                        if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
                        {
                            var fromDate = DateHelper.StringToDate(filter.FromDate);
                            var toDate = DateHelper.StringToDate(filter.ToDate);
                            res = res.Where(r => r.DelayDate != null && DateHelper.StringToDate(r.DelayDate) >= fromDate && DateHelper.StringToDate(r.DelayDate) <= toDate);
                        }


                        foreach (var item in res)
                        {
                            var delayValue = await ApplyPoliciesAsync(session.FacilityId, 2, item.EmpId);

                            // Ensure item.DelayTime is TimeSpan? and convert it to decimal if needed
                            var delayTimeInMinutes = item.DelayTime.HasValue ? (decimal)item.DelayTime.Value.TotalMinutes : 0m;

                            // Assuming DailyWorkingHours is a decimal
                            var dailyWorkingHours = item.DailyWorkingHours ?? 1m; // Ensure non-zero

                            var delayTime = delayTimeInMinutes * (delayValue / 30m / dailyWorkingHours / 60m);

                            var newRecord = new HrDelayFilterDto
                            {
                                Id = item.Id,
                                EmpName = item.EmpName,
                                EmpId = item.EmpId,
                                EmpCode = item.EmpCode,
                                Note = item.Note,
                                DeptName = item.DepName,
                                LocationName = item.LocationName,
                                DelayDate = item.DelayDate,
                                DelayTime = item.DelayTime.HasValue ? item.DelayTime.Value.ToString(@"hh\:mm\:ss") : "00:00:00",
                                DelayDuration = delayTime.ToString("F2"),
                                TypeName = (item.TypeId == 2) ? "تقصير" : (item.TypeId == 1 ? "تاخير" : ""),
                            };

                            resultList.Add(newRecord);
                        }


                        if (resultList.Count() > 0)
                            return await Result<List<HrDelayFilterDto>>.SuccessAsync(resultList, "");

                        return await Result<List<HrDelayFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
                    }

                    return await Result<List<HrDelayFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
                }

                return await Result<List<HrDelayFilterDto>>.FailAsync("errrooo");
            }
            catch (Exception ex)
            {
                return await Result<List<HrDelayFilterDto>>.FailAsync(ex.Message);
            }
        }
    }
}