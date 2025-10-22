using System.Globalization;
using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrAttendanceService : GenericQueryService<HrAttendance, HrAttendanceDto, HrAttendancesVw>, IHrAttendanceService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMapper mapper;
        private readonly ICurrentData session;
        private readonly ISysConfigurationAppHelper sysConfiguration;


        public HrAttendanceService(IQueryRepository<HrAttendance> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, ISysConfigurationAppHelper sysConfiguration) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.sysConfiguration = sysConfiguration;
        }

        public Task<IResult<HrAttendanceDto>> Add(HrAttendanceDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<HrAttendanceEditDto>> Update(HrAttendanceEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAttendanceEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrAttendanceEditDto>.FailAsync($"Employee Id Is Required");

            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrAttendanceEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrAttendanceRepository.GetOne(x => x.AttendanceId == entity.AttendanceId);

                if (item == null) return await Result<HrAttendanceEditDto>.FailAsync("the Attendance  Is Not Found");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.EmpId = checkEmpExist.Id;
                item.NoteIn = entity.NoteIn;
                item.NoteOut = entity.NoteOut;
                if (!string.IsNullOrEmpty(entity.TimeInString) && entity.TimeInString != "00:00" && !string.IsNullOrEmpty(entity.DayDateGregorian))
                {
                    var changedDate = DateHelper.ChangeFormatDate(entity.DayDateGregorian, entity.TimeInString);
                    item.TimeIn = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                }
                if (!string.IsNullOrEmpty(entity.TimeOutString) && entity.TimeOutString != "00:00" && !string.IsNullOrEmpty(entity.DayDateGregorian))
                {
                    var changedDate = DateHelper.ChangeFormatDate(entity.DayDateGregorian, entity.TimeOutString);
                    item.TimeOut = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                }
                if (!string.IsNullOrEmpty(entity.DefTimeInString) && entity.DefTimeInString != "00:00" && !string.IsNullOrEmpty(entity.DefTimeInString))
                {
                    var changedDate = DateHelper.ChangeFormatDate(entity.DayDateGregorian, entity.DefTimeInString);
                    item.DefTimeIn = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                }
                if (!string.IsNullOrEmpty(entity.DefTimeOutString) && entity.DefTimeOutString != "00:00" && !string.IsNullOrEmpty(entity.DefTimeOutString))
                {
                    var changedDate = DateHelper.ChangeFormatDate(entity.DayDateGregorian, entity.DefTimeOutString);
                    item.DefTimeOut = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                }
                hrRepositoryManager.HrAttendanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttendanceEditDto>.SuccessAsync(mapper.Map<HrAttendanceEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrAttendanceEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<HRAttendanceReportDto>>> getHR_Attendance_Report_SP(HRAttendanceReportFilterDto filter)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');


                if (string.IsNullOrEmpty(filter.EmpCode))
                {
                    filter.EmpCode = null;
                }
                if (string.IsNullOrEmpty(filter.EmpName))
                {
                    filter.EmpName = null;
                }

                if (filter.BranchId != null && filter.BranchId > 0)
                {
                    filter.BranchsId = "";

                }

                else
                {
                    filter.BranchId = 0;
                    filter.BranchsId = session.Branches;
                }
                if (filter.ShitId <= 0)
                {
                    filter.ShitId = 0;

                }
                filter.TimeTableId = 0;

                if (filter.DeptId == 0 || filter.DeptId == null)
                {
                    filter.DeptId = null;
                }

                if (filter.StatusId == 0 || filter.StatusId == null)
                {
                    filter.StatusId = null;
                }
                if (filter.Location == 0 || filter.Location == null)
                {
                    filter.Location = null;
                }
                if (filter.SponsorsId == 0 || filter.SponsorsId == null)
                {
                    filter.SponsorsId = null;
                }
                if (filter.AttendanceType == 0 || filter.AttendanceType == null)
                {
                    filter.AttendanceType = null;
                }
                if (!string.IsNullOrEmpty(filter.DayDateGregorian) && !string.IsNullOrEmpty(filter.DayDateGregorian2))
                {
                    filter.DayDateGregorian = filter.DayDateGregorian;
                    filter.DayDateGregorian2 = filter.DayDateGregorian2;
                }
                else
                {
                    filter.DayDateGregorian = null;
                    filter.DayDateGregorian2 = null;
                }
                filter.ManagerId = 0;
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_Report_SP(filter);
                return await Result<IEnumerable<HRAttendanceReportDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRAttendanceReportDto>>.FailAsync($"EXP in {nameof(getHR_Attendance_Report_SP)} at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> HR_Attendance_SP_CmdType_1(HrAttendanceDto entity, CancellationToken cancellationToken = default)
        {
            DateHelper.Initialize(mainRepositoryManager);
            if (entity == null) return await Result<string>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {


                //check Empid
                var EmpItem = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCode && e.IsDeleted == false);

                if (EmpItem == null) return await Result<string>.FailAsync($"{localization.GetResource1("EmployeeNotFound")}");
                if (EmpItem.StatusId == 2) return await Result<string>.FailAsync($"{localization.GetHrResource("EmpNotActive")}");
                // Check_Emp_Exists_In_Payroll
                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == EmpItem.Id && e.PayrollTypeId == 1);

                if (IfEmpExistsInPayroll.Any())
                {
                    var filterResult = IfEmpExistsInPayroll.Where(e => DateHelper.StringToDate(entity.TxtDate) >= DateHelper.StringToDate(e.StartDate) && DateHelper.StringToDate(entity.TxtDate) <= DateHelper.StringToDate(e.EndDate));
                    if (filterResult.Count() > 0)
                    {
                        return await Result<string>.FailAsync("لن تتمكن من اضافة تحضير بسبب استخراج مسير للموظف في نفس الشهر");

                    }
                }

                if (session.CalendarType == "2")
                {
                    var GDate = await DateHelper.DateFormattYYYYMMDD_H_G(entity.TxtDate);
                    var changedDate = DateHelper.ChangeFormatDate(GDate, entity.timeText);
                    entity.TimeIn = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    entity.TimeOut = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    //TimeDate = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    var changedDate = DateHelper.ChangeFormatDate(entity.TxtDate, entity.timeText);
                    entity.TimeIn = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    entity.TimeOut = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                }

                entity.DayNo = 0;

                if (session.CalendarType == "2")
                {
                    var GDate = await DateHelper.DateFormattYYYYMMDD_H_G(entity.TxtDate);
                    entity.DayDateGregorian = GDate;
                    entity.DayDateHijri = entity.TxtDate;
                }
                else
                {
                    var HDate = await DateHelper.DateFormattYYYYMMDD_G_H(entity.TxtDate);

                    entity.DayDateGregorian = entity.TxtDate;
                    entity.DayDateHijri = HDate;
                }
                entity.EmpId = EmpItem.Id;
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.LogInBy = "3";
                entity.LogOutBy = "3";
                entity.Longitude = null;
                entity.Latitude = null;

                if (entity.AttType == 1)
                {
                    entity.NoteIn = entity.Note;
                    entity.NoteOut = null;
                }
                else
                {
                    entity.NoteIn = null;
                    entity.NoteOut = entity.Note;
                }
                entity.LongitudeOut = null;
                entity.CMDTYPE = 1;
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_SP_CmdType_1(entity);
                if (result == true)
                {
                    return await Result<string>.SuccessAsync("تمت عملية التحضير بنجاح");

                }
                return await Result<string>.FailAsync("لم تتم عملية التحضير تأكد من وجود وردية للموظف او ان الموظف محضر مسبقاً");
            }
            catch (Exception)
            {
                return await Result<string>.FailAsync($"حدث خطاء اثناء عملية التحضير ");
            }
        }

        public async Task<IResult<IEnumerable<HRAttendanceReport5Dto>>> getHR_Attendance_Report5_SP(HRAttendanceReport5FilterDto filter)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_Report5_SP(filter);
                return await Result<IEnumerable<HRAttendanceReport5Dto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRAttendanceReport5Dto>>.FailAsync($"EXP in getHR_Attendance_Report5_SP at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<HRAttendanceReport4Dto>>> getHR_Attendance_Report4_SP(HRAttendanceReport4FilterDto filter)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_Report4_SP(filter);
                return await Result<IEnumerable<HRAttendanceReport4Dto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRAttendanceReport4Dto>>.FailAsync($"EXP in getHR_Attendance_Report5_SP at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<HrAttendancesFilterDto>>> AttendanceSearch(HrAttendancesFilterDto filter)
        {
            try
            {
                List<HrAttendancesFilterDto> result = new List<HrAttendancesFilterDto>();
                var BranchesList = session.Branches.Split(',');
                var items = await hrRepositoryManager.HrAttendanceRepository.GetAllFromView(x => x.IsDeleted == false && BranchesList.Contains(x.BranchId.ToString())
                && (string.IsNullOrEmpty(filter.EmpCode) || x.EmpCode == filter.EmpCode) &&
                (string.IsNullOrEmpty(filter.EmpName) || x.EmpName.ToLower().Contains(filter.EmpName.ToLower()) || x.EmpName2.ToLower().Contains(filter.EmpName.ToLower()))

                );
                if (items != null)
                {
                    if (items.Count() > 0)
                    {

                        var res = items.AsQueryable();

                        if (filter.BranchId != null && filter.BranchId > 0)
                        {
                            res = res.Where(c => c.BranchId != null && c.BranchId.Equals(filter.BranchId));
                        }

                        if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
                        {
                            res = res.Where(r => r.DayDateGregorian != null &&
                            DateHelper.StringToDate(r.DayDateGregorian) >= DateHelper.StringToDate(filter.FromDate) &&
                           DateHelper.StringToDate(r.DayDateGregorian) <= DateHelper.StringToDate(filter.ToDate));
                        }

                        if (res.Any())
                        {
                            DateHelper.Initialize(mainRepositoryManager);

                            foreach (var item in res)
                            {
                                var newItem = new HrAttendancesFilterDto
                                {
                                    Id = item.AttendanceId,
                                    EmpCode = item.EmpCode,
                                    EmpName = session.Language == 1 ? item.EmpName : item.EmpName2,
                                    theDate = session.CalendarType == "1" ? item.DayDateGregorian : await DateHelper.DateFormattYYYYMMDD_G_H(item.DayDateGregorian),
                                    DayName = item.DayName,
                                    TimeIn = item.TimeIn,
                                    TimeOut = item.TimeOut,
                                    NoteIn = item.NoteIn,
                                    NoteOut = item.NoteOut,
                                };
                                result.Add(newItem);
                            }
                            return await Result<List<HrAttendancesFilterDto>>.SuccessAsync(result, "");

                        }
                        return await Result<List<HrAttendancesFilterDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
                    }
                    return await Result<List<HrAttendancesFilterDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
                }
                return await Result<List<HrAttendancesFilterDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception ex)
            {
                return await Result<List<HrAttendancesFilterDto>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<IEnumerable<HRAddMultiAttendanceDto>>> AttendanceSearchForMultiAdd(HRAddMultiAttendanceFilterDto filter)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.AttendanceSearchForMultiAdd(filter);
                return await Result<IEnumerable<HRAddMultiAttendanceDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRAddMultiAttendanceDto>>.FailAsync($"EXP in AttendanceSearchForMultiAdd at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<AddMultiAttendanceResultDto>> MultiAdd(List<HrMultiAddDto> entities, CancellationToken cancellationToken = default)
        {

            if (entities == null) return await Result<AddMultiAttendanceResultDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            AddMultiAttendanceResultDto result = new AddMultiAttendanceResultDto();
            result.EmpWithProblems = new List<string>();
            int SavedAttendanceRecordCount = 0;
            int SavedAbsenceRecordCount = 0;
            int SavedRestRecordCount = 0;


            try
            {
                DateHelper.Initialize(mainRepositoryManager);
                DateTime TimeDate = DateTime.ParseExact(DateHelper.ChangeFormatDate(entities.FirstOrDefault().TxtDate, entities.FirstOrDefault().TimeText), "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                var getShitId = await hrRepositoryManager.HrAttShiftTimeTableRepository.GetAll(x => x.ShiftId, x => x.IsDeleted == false && x.TimeTableId == entities.FirstOrDefault().TimeTableId);
                var CheckShiftIsClose = await hrRepositoryManager.HrAttShiftCloseRepository.GetAll(x => x.Id, x => x.IsDeleted == false && x.DateClose == entities.FirstOrDefault().TxtDate && getShitId.Contains(x.ShiftId));

                if (CheckShiftIsClose.Any())
                {
                    return await Result<AddMultiAttendanceResultDto>.FailAsync($"لن تتمكن من اجراء اي تعديل على الودرية بسبب اغلاقها");

                }
                foreach (var item in entities)
                {
                    if ((item.PresentSelected == true && item.RestSelected == true && item.AbsenceSelected == true) ||
                        (item.PresentSelected == true && item.RestSelected == true) ||
                        (item.RestSelected == true && item.AbsenceSelected == true) ||
                        (item.PresentSelected == true && item.AbsenceSelected == true))
                    {
                        return await Result<AddMultiAttendanceResultDto>.FailAsync($" بعض السطور  تم فيها اختيار اكثر من خيار يجب إختيار خيار واحد فقط");
                    }
                }

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                foreach (var item in entities)
                {
                    var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == item.EmpCode && x.IsDeleted == false && x.Isdel == false);
                    if (checkEmpExist == null)
                    {
                        result.EmpWithProblems.Add($"الموظف رقم  {item.EmpCode}  غير موجود");
                        continue;
                    }

                    if (checkEmpExist.StatusId == 2)
                    {
                        result.EmpWithProblems.Add($" تم عمل انهاء خدمة للموظف رقم   {item.EmpCode}");
                        continue;
                    }
                    if (item.PresentSelected)
                    {

                        // Begin check if Emp Is Exist In Payroll
                        var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1);

                        if (IfEmpExistsInPayroll.Any())
                        {
                            var filterResult = IfEmpExistsInPayroll.Where(e => DateHelper.StringToDate(item.TxtDate) >= DateHelper.StringToDate(e.StartDate) && DateHelper.StringToDate(item.TxtDate) <= DateHelper.StringToDate(e.EndDate));
                            if (filterResult.Count() > 0)
                            {
                                result.EmpWithProblems.Add($"الموظف رقم  {item.EmpCode}  لديه مسير في نفس الشهر");
                                continue;
                            }
                        }
                        // End check if Emp Is Exist In Payroll
                        // Begin check if VacationsInSameDay2

                        var CheckIsVacationsInSameDay2 = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                        if (CheckIsVacationsInSameDay2.Count() > 0)
                        {
                            var filterResult = CheckIsVacationsInSameDay2.Where(e => DateHelper.StringToDate(item.TxtDate) >= DateHelper.StringToDate(e.VacationSdate) && DateHelper.StringToDate(item.TxtDate) <= DateHelper.StringToDate(e.VacationEdate));
                            if (filterResult.Count() > 0)
                            {
                                result.EmpWithProblems.Add($"الموظف رقم  {item.EmpCode}  في اجازة ");
                                continue;
                            }
                        }
                        // End check if VacationsInSameDay2


                        // Begin Of Insert Attendance For Employee
                        var entity = new HrAttendanceDto();
                        if (session.CalendarType == "2")
                        {
                            var GDate = await DateHelper.DateFormattYYYYMMDD_H_G(item.TxtDate);
                            var changedDate = DateHelper.ChangeFormatDate(GDate, item.TimeText);
                            entity.TimeIn = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                            entity.TimeOut = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                            entity.DayDateGregorian = GDate;
                            entity.DayDateHijri = item.TxtDate;
                        }
                        else
                        {
                            var changedDate = DateHelper.ChangeFormatDate(item.TxtDate, item.TimeText);
                            entity.TimeIn = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                            entity.TimeOut = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                            entity.DayDateGregorian = item.TxtDate;
                            var HDate = await DateHelper.DateFormattYYYYMMDD_G_H(item.TxtDate);
                            entity.DayDateHijri = HDate;
                        }
                        entity.DayNo = 0;
                        entity.EmpId = checkEmpExist.Id;
                        entity.CreatedBy = session.UserId;
                        entity.CreatedOn = DateTime.Now;
                        entity.LogInBy = "3";
                        entity.LogOutBy = "3";
                        entity.Longitude = null;
                        entity.Latitude = null;
                        if (item.AttType == 1)
                        {
                            entity.NoteIn = entity.Note;
                            entity.NoteOut = null;
                        }
                        else
                        {
                            entity.NoteIn = null;
                            entity.NoteOut = entity.Note;
                        }
                        entity.LongitudeOut = null;
                        entity.AttType = item.AttType;
                        entity.CMDTYPE = 1;
                        var AttendanceResult = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_SP_CmdType_1(entity);
                        if (AttendanceResult == true)
                        {
                            SavedAttendanceRecordCount += 1;

                        }
                        else
                        {
                            result.EmpWithProblems.Add($"  لم تتم عملية التحضير تأكد من وجود وردية للموظف رقم  {item.EmpCode} او ان الموظف محضر مسبقاً");
                        }
                        // End Of Insert Attendance For Employee

                    }
                    else
                    {
                        var getAttendance = await hrRepositoryManager.HrAttendanceRepository.GetOne(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AttType == item.AttType && x.TimeTableId == item.TimeTableId && x.DayDateGregorian == item.TxtDate);
                        if (getAttendance != null)
                        {
                            getAttendance.IsDeleted = true;
                            getAttendance.ModifiedBy = session.UserId;
                            getAttendance.ModifiedOn = DateTime.Now;
                            hrRepositoryManager.HrAttendanceRepository.Update(getAttendance);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }
                    }

                    //////////////////////////////////////////////////////////////////
                    if (item.RestSelected)
                    {
                        // Begin check if VacationsInSameDay
                        var CheckIsVacationsInSameDay = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.VacationSdate == item.TxtDate && x.VacationTypeId == 8);
                        if (!CheckIsVacationsInSameDay.Any())
                        {
                            var CheckIsVacationsInSameDay2 = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                            if (CheckIsVacationsInSameDay2.Any())
                            {
                                var filterResult = CheckIsVacationsInSameDay2.Where(e => DateHelper.StringToDate(item.TxtDate) >= DateHelper.StringToDate(e.VacationSdate) && DateHelper.StringToDate(item.TxtDate) <= DateHelper.StringToDate(e.VacationEdate));
                                if (filterResult.Any())
                                {
                                    result.EmpWithProblems.Add($"الموظف رقم  {item.EmpCode}  في اجازة ");
                                    continue;
                                }
                            }
                        }
                        var GetFromAttShiftEmployeeVW = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOneVw(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                        // here we will start add vacation
                        var HrVacations = new HrVacation
                        {
                            EmpId = checkEmpExist.Id,
                            VacationSdate = item.TxtDate,
                            VacationEdate = item.TxtDate,
                            VacationTypeId = 8,
                            Note = "",
                            VacationAccountDay = 1,
                            FinancelYear = (int?)session.FinYear,
                            IsSalary = false,
                            StatusId = 4,
                            IsDeleted = false,
                            VacationsDayTypeId = 1,
                            LocationId = checkEmpExist.Location,
                            TimeTableId = (int?)GetFromAttShiftEmployeeVW.TimeTableId,
                            ShiftId = (int?)GetFromAttShiftEmployeeVW.ShiftId

                        };
                        var newVacationEntity = await hrRepositoryManager.HrVacationsRepository.AddAndReturn(HrVacations);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        //  في حال كانت الإجازة مرضية يتم تنفيذ البروسيجر للمرضية
                        int? catId = 0;
                        var checkVacationType = await hrRepositoryManager.HrVacationsTypeRepository.GetOne(x => x.CatId, x => x.VacationTypeId == newVacationEntity.VacationTypeId);
                        catId = checkVacationType ?? 0;
                        if (catId == 2)
                        {
                            string SickleavePolicy = await sysConfiguration.GetValue(77);
                            if (SickleavePolicy == "1")
                            {
                                var ImplementStoredProcedure = await mainRepositoryManager.StoredProceduresRepository.HR_Sick_leave_Sp(checkEmpExist.Id, 2, newVacationEntity.VacationId);
                            }
                        }
                        SavedRestRecordCount += 1;


                        // End check if VacationsInSameDay
                    }
                    else
                    {
                        //DeleteHR_Vacations2
                        var getVacations = await hrRepositoryManager.HrVacationsRepository.GetOne(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.VacationSdate == item.TxtDate && x.VacationTypeId == 8);
                        if (getVacations != null)
                        {
                            getVacations.IsDeleted = true;
                            hrRepositoryManager.HrVacationsRepository.Update(getVacations);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }

                    }

                    //////////////////////////////////////////////////////////////////
                    if (item.AbsenceSelected)
                    {
                        //  Check IF EMPLOYEE Is Absence InSame Day
                        var CheckIsAbsenceInSameDay = await hrRepositoryManager.HrAbsenceRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AbsenceDate == item.TxtDate);
                        if (CheckIsAbsenceInSameDay.Any())
                        {
                            result.EmpWithProblems.Add($"الموظف رقم  {item.EmpCode}  غائب  ");
                            continue;
                        }

                        // CheckIsVacationInSameDay
                        var CheckIsVacationsInSameDay = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                        if (CheckIsVacationsInSameDay.Any())
                        {

                            var filterResult = CheckIsVacationsInSameDay.Where(e => DateHelper.StringToDate(item.TxtDate) >= DateHelper.StringToDate(e.VacationSdate) && DateHelper.StringToDate(item.TxtDate) <= DateHelper.StringToDate(e.VacationEdate));
                            if (filterResult.Any())
                            {
                                result.EmpWithProblems.Add($"الموظف رقم  {item.EmpCode}  في اجازة ");
                                continue;
                            }
                        }

                        // Begin of Add Absence
                        var HrVacations = new HrAbsence
                        {
                            EmpId = checkEmpExist.Id,
                            Type = "2",
                            Note = "",
                            AbsenceDate = item.TxtDate,
                            AbsenceTypeId = 0,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false

                        };
                        await hrRepositoryManager.HrAbsenceRepository.Add(HrVacations);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        SavedAbsenceRecordCount += 1;

                    }
                    else
                    {
                        //DeleteHR Absence2
                        var getAbsence = await hrRepositoryManager.HrAbsenceRepository.GetOne(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AbsenceDate == item.TxtDate);
                        if (getAbsence != null)
                        {
                            getAbsence.IsDeleted = true;
                            getAbsence.ModifiedBy = session.UserId;
                            getAbsence.ModifiedOn = DateTime.Now;
                            hrRepositoryManager.HrAbsenceRepository.Update(getAbsence);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }
                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                //////////// Results ////////////
                var AttendanceTrueRows = entities.Where(x => x.PresentSelected == true).Count();
                if (AttendanceTrueRows == 0)
                {
                    result.AttendanceNotSelectedMessage = $" لم يتم اختيار اي موظف للتحضير";

                }
                var RestTrueRows = entities.Where(x => x.RestSelected == true).Count();
                if (RestTrueRows == 0)
                {
                    result.RestNotSelectedMessage = $" لم يتم اختيار اي موظف للراحة";

                }
                var AbsenceTrueRows = entities.Where(x => x.AbsenceSelected == true).Count();
                if (AbsenceTrueRows == 0)
                {
                    result.RestNotSelectedMessage = $" لم يتم اختيار اي موظف للغياب";

                }
                if (SavedAttendanceRecordCount > 0)
                {
                    result.SavedAttendanceRecord = $" تمت عملية التحضير لعدد  {SavedAttendanceRecordCount}  من الموظفين بنجاح ";

                }
                if (SavedAbsenceRecordCount > 0)
                {
                    result.SavedAbsenceRecord = $" تمت عملية  تسجيل غياب لعدد  {SavedAbsenceRecordCount}  من الموظفين بنجاح ";

                }
                if (SavedRestRecordCount > 0)
                {
                    result.SavedRestRecord = $" تمت عملية تسجيل راحة لعدد  {SavedRestRecordCount}  من الموظفين بنجاح ";

                }

                /////////////////////////////////////////////
                return await Result<AddMultiAttendanceResultDto>.SuccessAsync(result, "", 200);


            }
            catch (Exception)
            {

                return await Result<AddMultiAttendanceResultDto>.FailAsync($"حدث خطاء اثناء عملية التحضير ");
            }
        }

        public async Task<IResult<AddAttendanceFromExcelResultDto>> AddAttendanceFromExcel(IEnumerable<AddAttendanceFromExcelDto> entities, CancellationToken cancellationToken = default)
        {
            AddAttendanceFromExcelResultDto result = new AddAttendanceFromExcelResultDto();
            result.EmpNotActive = new List<string>();
            result.PayrollAviable = new List<string>();
            result.EmpNotExist = new List<string>();
            result.EmpWithProblems = new List<string>();
            result.SavedRecord = new List<string>();
            int SavedRecordCount = 0;
            if (!entities.Any()) return await Result<AddAttendanceFromExcelResultDto>.FailAsync($"  تأكد من وجود بيانات في ملف الاكسل", 500);
            try
            {
                DateHelper.Initialize(mainRepositoryManager);
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var item in entities)
                {
                    // Begin check if Emp Is Exist and id active 
                    var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == item.EmpCode && x.IsDeleted == false && x.Isdel == false);
                    if (checkEmpExist == null)
                    {
                        result.EmpNotExist.Add($"الموظف رقم  {item.EmpCode}  غير موجود");
                        continue;
                    }
                    if (checkEmpExist.StatusId == 2)
                    {
                        result.EmpNotActive.Add($" تم عمل انهاء خدمة للموظف رقم   {item.EmpCode}");
                        continue;
                    }
                    //End check if Emp Is Exist and id active 

                    // Begin check if Emp Is Exist In Payroll
                    var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1);

                    if (IfEmpExistsInPayroll.Any())
                    {
                        var filterResult = IfEmpExistsInPayroll.Where(e => DateHelper.StringToDate(item.TxtDate) >= DateHelper.StringToDate(e.StartDate) && DateHelper.StringToDate(item.TxtDate) <= DateHelper.StringToDate(e.EndDate));
                        if (filterResult.Any())
                        {
                            result.PayrollAviable.Add($"الموظف رقم  {item.EmpCode}  لديه مسير في نفس الشهر");
                            continue;
                        }
                    }
                    // End check if Emp Is Exist In Payroll

                    // Begin Of Insert Attendance For Employee
                    var entity = new HrAttendanceDto();
                    if (session.CalendarType == "2")
                    {
                        var GDate = await DateHelper.DateFormattYYYYMMDD_H_G(item.TxtDate);
                        var changedDate = DateHelper.ChangeFormatDate(GDate, item.TimeText);
                        entity.TimeIn = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                        entity.TimeOut = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                        entity.DayDateGregorian = GDate;
                        entity.DayDateHijri = item.TxtDate;
                    }
                    else
                    {
                        var changedDate = DateHelper.ChangeFormatDate(item.TxtDate, item.TimeText);
                        entity.TimeIn = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                        entity.TimeOut = DateTime.ParseExact(changedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                        entity.DayDateGregorian = item.TxtDate;
                        var HDate = await DateHelper.DateFormattYYYYMMDD_G_H(item.TxtDate);
                        entity.DayDateHijri = HDate;
                    }
                    entity.DayNo = 0;

                    entity.EmpId = checkEmpExist.Id;
                    entity.CreatedBy = session.UserId;
                    entity.CreatedOn = DateTime.Now;
                    entity.LogInBy = "3";
                    entity.LogOutBy = "3";
                    entity.Longitude = null;
                    entity.Latitude = null;

                    if (item.AttType == 1)
                    {
                        entity.NoteIn = entity.Note;
                        entity.NoteOut = null;
                    }
                    else
                    {
                        entity.NoteIn = null;
                        entity.NoteOut = entity.Note;
                    }
                    entity.LongitudeOut = null;
                    entity.AttType = item.AttType;
                    entity.CMDTYPE = 1;
                    var AttendanceResult = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_SP_CmdType_1(entity);
                    if (AttendanceResult == true)
                    {
                        SavedRecordCount += 1;

                    }
                    else
                    {
                        result.EmpWithProblems.Add($"  لم تتم عملية التحضير تأكد من وجود وردية للموظف رقم  {item.EmpCode} او ان الموظف محضر مسبقاً");
                    }
                    // End Of Insert Attendance For Employee
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                if (SavedRecordCount == entities.Count())
                {
                    result.SavedRecord.Add($" تم تحضير من الموظفين عدد:  {SavedRecordCount}");
                    return await Result<AddAttendanceFromExcelResultDto>.SuccessAsync(result, "", 200);

                }
                else
                {
                    result.SavedRecord.Add($" تم تحضير من الموظفين عدد:  {SavedRecordCount}");
                    return await Result<AddAttendanceFromExcelResultDto>.SuccessAsync(result, "", 200);
                }

            }
            catch (Exception)
            {

                return await Result<AddAttendanceFromExcelResultDto>.FailAsync($"   حدث خطاء اثناء عملية التحضير  من ملف إكسل  ");

            }
        }




        public async Task<IResult<IEnumerable<HRAttendanceTotalReportDto>>> Attendance_TotalReport(HRAttendanceTotalReportFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_TotalReportManagerial_SP(filter);
                if (result.Count() > 0)
                {
                    return await Result<IEnumerable<HRAttendanceTotalReportDto>>.SuccessAsync(result, "");


                }
                return await Result<IEnumerable<HRAttendanceTotalReportDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRAttendanceTotalReportDto>>.FailAsync($"EXP in getHR_Attendance_Report5_SP at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<IEnumerable<HRAttendanceTotalReportNewSPDto>>> HR_Attendance_TotalReportNew_SP(HRAttendanceTotalReportFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.empCode))
                {
                    filter.empCode = "";
                }
                if (string.IsNullOrEmpty(filter.EmpName))
                {
                    filter.EmpName = "";
                }
                if (filter.Location <= 0 || filter.Location == null)
                {
                    filter.Location = 0;
                }

                if (string.IsNullOrEmpty(filter.From))
                {
                    filter.From = "";
                }
                if (string.IsNullOrEmpty(filter.To))
                {
                    filter.To = "";
                }
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_TotalReportNew_SP(filter);
                if (result.Count() > 0)
                {
                    return await Result<IEnumerable<HRAttendanceTotalReportNewSPDto>>.SuccessAsync(result, "");


                }
                return await Result<IEnumerable<HRAttendanceTotalReportNewSPDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRAttendanceTotalReportNewSPDto>>.FailAsync($"EXP in getHR_Attendance_Report5_SP at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<HRAttendanceReport6SP>>> HR_Attendance_Report6_SP(HRAttendanceReport6FilterSP filter, CancellationToken cancellationToken = default)
        {
            try
            {
                filter.Workinghours ??= 0;
                filter.BranchId ??= 0;
                if (filter.Workinghours <= 0)
                {
                    return await Result<IEnumerable<HRAttendanceReport6SP>>.FailAsync("يجب ادخال  قراءة ساعة العمل");
                }
                if (string.IsNullOrEmpty(filter.EmpCode))
                {
                    filter.EmpCode = null;
                }
                if (string.IsNullOrEmpty(filter.EmpName))
                {
                    filter.EmpName = "";
                }
                if (filter.Location <= 0 || filter.Location == null)
                {
                    filter.Location = 0;
                }
                if (filter.BranchId <= 0)
                {
                    filter.BranchId = 0;
                    filter.BranchsId = session.Branches;
                }
                else
                {
                    filter.BranchsId = "";
                }
                if (string.IsNullOrEmpty(filter.From) || string.IsNullOrEmpty(filter.To))
                {
                    filter.From = "";
                    filter.To = "";
                }
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_Report6_SP(filter);
                if (result.Count() > 0)
                {
                    return await Result<IEnumerable<HRAttendanceReport6SP>>.SuccessAsync(result, "");
                }
                return await Result<IEnumerable<HRAttendanceReport6SP>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRAttendanceReport6SP>>.FailAsync($"EXP in HR_Attendance_Report6_SP at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        //  الفلترة في شاشة رفع بيانات الحضور والانصراف
        public async Task<IResult<IEnumerable<HRAttendanceCheckingStaffFilterDto>>> GetEmployeesForUploadAttendances(HRAttendanceCheckingStaffFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                filter.DeptId ??= 0;
                if (string.IsNullOrEmpty(filter.Date))
                {
                    return await Result<IEnumerable<HRAttendanceCheckingStaffFilterDto>>.FailAsync("يجب ادخال التاريخ ");
                }
                List<HRAttendanceCheckingStaffFilterDto> result = new List<HRAttendanceCheckingStaffFilterDto>();

                var getSysProperty = await sysConfiguration.GetValue(147, session.FacilityId);
                var getAllEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.Isdel == false && e.IsDeleted == false && e.StatusId == 1 && e.FacilityId == session.FacilityId && e.Doappointment != null && e.Doappointment != ""
                && (string.IsNullOrEmpty(filter.empCode) || filter.empCode == e.EmpId)
                && (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower()))
                && (filter.DeptId == 0 || filter.DeptId == e.DeptId)
                );
                var filterdEmployee = getAllEmployees.Where(f => DateHelper.StringToDate(f.Doappointment) <= DateHelper.StringToDate(filter.Date));
                if (filterdEmployee.Count() <= 0) return await Result<IEnumerable<HRAttendanceCheckingStaffFilterDto>>.FailAsync(localization.GetResource1("NosearchResult"));
                if (getSysProperty == "1")
                {
                    long managerId = session.EmpId;
                    filterdEmployee = filterdEmployee.Where(f => f.ManagerId == managerId || f.Manager2Id == managerId || f.Manager3Id == managerId);
                    if (filterdEmployee.Count() <= 0) return await Result<IEnumerable<HRAttendanceCheckingStaffFilterDto>>.FailAsync(localization.GetResource1("NosearchResult"));
                }
                var AllPermission = await hrRepositoryManager.HrPermissionRepository.GetAll(p => p.IsDeleted == false && p.PermissionDate == filter.Date);
                var AllAbsence = await hrRepositoryManager.HrAbsenceRepository.GetAll(A => A.IsDeleted == false && A.AbsenceDate == filter.Date);
                var AllVacations = await hrRepositoryManager.HrVacationsRepository.GetAll(V => V.IsDeleted == false && V.VacationSdate == filter.Date);
                var NormalVacations = AllVacations.Where(v => v.VacationTypeId == 1);
                var OtherVacation = AllVacations.Where(v => v.VacationTypeId != 1);
                foreach (var item in filterdEmployee)
                {
                    var newRecord = new HRAttendanceCheckingStaffFilterDto();
                    newRecord.empCode = item.EmpId;
                    newRecord.EmpName = item.EmpName ?? item.EmpName2;


                    //  check Absence
                    var checkAbsence = AllAbsence.Where(A => A.EmpId == item.Id).FirstOrDefault();

                    if (checkAbsence != null)
                    {
                        newRecord.Absence = 1;
                    }
                    else
                    {
                        newRecord.Absence = 0;

                    }

                    //  check Permission
                    var checkPermission = AllPermission.Where(p => p.EmpId == item.Id).FirstOrDefault();

                    if (checkPermission != null)
                    {
                        newRecord.Permission = 1;
                    }
                    else
                    {
                        newRecord.Permission = 0;

                    }

                    //  check NormalVacation
                    var checkNormalVacation = NormalVacations.Where(N => N.EmpId == item.Id).FirstOrDefault();

                    if (checkNormalVacation != null)
                    {
                        newRecord.NormalVacation = checkNormalVacation.VacationTypeId;
                    }
                    else
                    {
                        newRecord.NormalVacation = 0;

                    }

                    //  check OtherVacation
                    var checkOtherVacation = OtherVacation.Where(O => O.EmpId == item.Id).FirstOrDefault();

                    if (checkOtherVacation != null)
                    {
                        newRecord.OtherVacation = checkOtherVacation.VacationTypeId;
                    }
                    else
                    {
                        newRecord.OtherVacation = 0;

                    }

                    result.Add(newRecord);
                }

                if (result.Count() > 0)
                {
                    return await Result<IEnumerable<HRAttendanceCheckingStaffFilterDto>>.SuccessAsync(result, "");
                }
                return await Result<IEnumerable<HRAttendanceCheckingStaffFilterDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRAttendanceCheckingStaffFilterDto>>.FailAsync($"EXP in GetEmployeesForUploadAttendances at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        //   رفع بيانات الحضور والانصراف
        public async Task<IResult<string>> UploadAttendances(HRAttendanceUploadDto obj, CancellationToken cancellationToken = default)
        {

            if (obj == null) return await Result<string>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync();
                var getAllEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false && x.StatusId == 1);
                var getAllAttShiftEmployee = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetAllVw(x => x.IsDeleted == false);
                foreach (var item in obj.Data)
                {
                    var checkEmpExist = getAllEmployees.Where(x => x.EmpId == item.empCode).FirstOrDefault();
                    if (checkEmpExist == null) return await Result<string>.FailAsync($"الموظف  رقم    {item.empCode} غير موجود");
                    long? TimeTableID = getAllAttShiftEmployee.Where(x => x.EmpId == checkEmpExist.Id).Select(a => a.TimeTableId).FirstOrDefault();
                    if (item.Absence == 1)
                    {
                        var CheckIsAbsenceInSameDay = await hrRepositoryManager.HrAbsenceRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AbsenceDate == obj.Date);
                        if (CheckIsAbsenceInSameDay.Count() > 0) return await Result<string>.FailAsync($"الموظف  رقم    {item.empCode} لديه غياب في نفس التاريخ");

                        if (CheckIsAbsenceInSameDay.Count() == 0)
                        {
                            // CheckIsVacationInSameDay
                            var CheckIsVacationsInSameDay = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                            if (CheckIsVacationsInSameDay.Count() > 0)
                            {

                                var filterResult = CheckIsVacationsInSameDay.Where(e => DateHelper.StringToDate(obj.Date) >= DateHelper.StringToDate(e.VacationSdate) && DateHelper.StringToDate(obj.Date) <= DateHelper.StringToDate(e.VacationEdate));
                                if (filterResult.Count() > 0) return await Result<string>.FailAsync($"الموظف  رقم    {item.empCode} في اجازة");
                                if (filterResult.Count() == 0)
                                {
                                    var newAbsence = new HrAbsence
                                    {
                                        AbsenceTypeId = 0,
                                        AbsenceDate = obj.Date,
                                        IsDeleted = false,
                                        CreatedBy = session.UserId,
                                        CreatedOn = DateTime.Now,
                                        Type = "2",
                                        Note = "",
                                        EmpId = checkEmpExist.Id,
                                        TimeTableId = (int?)TimeTableID,
                                        LocationId = checkEmpExist.Location
                                    };
                                    await hrRepositoryManager.HrAbsenceRepository.Add(newAbsence);
                                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                }
                            }
                            continue;
                        }
                    }
                    else
                    {
                        //DeleteHR_Absence2
                        var getAbsence = await hrRepositoryManager.HrAbsenceRepository.GetOne(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.AbsenceDate == obj.Date);
                        if (getAbsence != null)
                        {
                            getAbsence.ModifiedOn = DateTime.Now;
                            getAbsence.ModifiedBy = session.UserId;
                            getAbsence.IsDeleted = true;
                            hrRepositoryManager.HrAbsenceRepository.Update(getAbsence);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }
                    }

                    if (item.NormalVacation == 1)
                    {
                        // Begin check if VacationsInSameDay2

                        var CheckIsVacationsInSameDay2 = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id);
                        if (CheckIsVacationsInSameDay2.Count() > 0)
                        {
                            var filterResult = CheckIsVacationsInSameDay2.Where(e => DateHelper.StringToDate(obj.Date) >= DateHelper.StringToDate(e.VacationSdate) && DateHelper.StringToDate(obj.Date) <= DateHelper.StringToDate(e.VacationEdate));
                            if (filterResult.Count() > 0) return await Result<string>.FailAsync($"الموظف  رقم    {item.empCode} في اجازة");

                            if (filterResult.Count() == 0)
                            {
                                var newVacation = new HrVacation
                                {

                                    IsDeleted = false,
                                    Note = "",
                                    VacationSdate = obj.Date,
                                    VacationEdate = obj.Date,
                                    EmpId = checkEmpExist.Id,
                                    VacationTypeId = 1,
                                    VacationAccountDay = 1,
                                    IsSalary = false,
                                    StatusId = 4,
                                    VacationsDayTypeId = 1,
                                    FinancelYear = session.FinyearGregorian,
                                    LocationId = checkEmpExist.Location
                                };
                                await hrRepositoryManager.HrVacationsRepository.Add(newVacation);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                            }
                        }
                        // End check if VacationsInSameDay2
                        continue;
                    }
                    else
                    {     //DeleteHRVacation
                        var getVacation = await hrRepositoryManager.HrVacationsRepository.GetOne(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.VacationSdate == obj.Date && x.VacationTypeId == 1);
                        if (getVacation != null)
                        {
                            getVacation.IsDeleted = true;
                            hrRepositoryManager.HrVacationsRepository.Update(getVacation);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }

                    }



                    if (item.Permission == 1)
                    {

                        var CheckPermission = await hrRepositoryManager.HrPermissionRepository.GetAll(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.PermissionDate == obj.Date);
                        if (CheckPermission.Count() > 0) return await Result<string>.FailAsync($"الموظف  رقم    {item.empCode} لديه استئذان في نفس التاريخ");

                        if (CheckPermission.Count() == 0)
                        {

                            var newPermission = new HrPermission
                            {

                                IsDeleted = false,
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,
                                Note = "",
                                EmpId = checkEmpExist.Id,
                                DetailsReason = "",
                                ContactNumber = "",
                                LeaveingTime = "08:00",
                                EstimatedTimeReturn = "16:00",
                                Type = 2,
                                PermissionDate = obj.Date

                            };
                            await hrRepositoryManager.HrPermissionRepository.Add(newPermission);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                        }
                        continue;
                    }
                    else
                    {
                        //DeleteHRPermission
                        var getPermission = await hrRepositoryManager.HrPermissionRepository.GetOne(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.PermissionDate == obj.Date);
                        if (getPermission != null)
                        {
                            getPermission.ModifiedOn = DateTime.Now;
                            getPermission.ModifiedBy = session.UserId;
                            getPermission.IsDeleted = true;
                            hrRepositoryManager.HrPermissionRepository.Update(getPermission);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }

                    }


                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync();

                return await Result<string>.SuccessAsync($"{localization.GetResource1("ActionSuccess")}");

            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IResult<IEnumerable<AttendanceSummaryDto>>> GetAttendanceReportByDate(AttendanceSummaryFilter filter, CancellationToken cancellationToken = default)
        {

            try
            {
                decimal TimeAttAllDay = 0;
                filter.DeptId ??= 0;
                filter.Location ??= 0;
                filter.AttendanceType ??= 0;
                filter.FingerprintType ??= 0;
                filter.ManagerId ??= 0;
                filter.StatusId ??= 0;
                filter.TimeTableId ??= 0;
                var BranchesList = session.Branches.Split(',');
                var query = await hrRepositoryManager.HrAttendanceRepository.GetAllFromView(x => x.IsDeleted == false && BranchesList.Contains(x.BranchId.ToString())
                && (string.IsNullOrEmpty(filter.EmpName) || x.EmpName.ToLower().Contains(filter.EmpName.ToLower()))
                && (string.IsNullOrEmpty(filter.EmpCode) || x.EmpCode == filter.EmpCode)
                && (filter.TimeTableId == 0 || x.TimeTableId == filter.TimeTableId)
                && (filter.ManagerId == 0 || x.ManagerId == filter.TimeTableId)
                && (filter.Location == 0 || x.Location == filter.Location)
                && (filter.DeptId == 0 || x.DeptId == filter.DeptId)
                && (filter.AttendanceType == 0 || x.AttType == filter.AttendanceType)
                && (filter.FingerprintType == 0 || (filter.FingerprintType == 1 && x.TimeOut != null) || (filter.FingerprintType == 2 && x.TimeOut == null))
                );

                if (filter.BranchId > 0)
                {
                    query = query.Where(a => a.BranchId == filter.BranchId);
                }
                if (!string.IsNullOrEmpty(filter.DayDateGregorian) && !string.IsNullOrEmpty(filter.DayDateGregorian))
                {
                    if (session.CalendarType == "2")
                    {
                        var startDateG = Bahsas.HijriToGreg(filter.DayDateGregorian);
                        var endDateG = Bahsas.HijriToGreg(filter.DayDateGregorian2);
                        var startDate = DateHelper.StringToDate(startDateG);
                        var endDate = DateHelper.StringToDate(endDateG);
                        query = query.Where(a => DateHelper.StringToDate(a.DayDateGregorian) >= startDate && DateHelper.StringToDate(a.DayDateGregorian) <= endDate);

                    }
                    else
                    {
                        var startDate = DateHelper.StringToDate(filter.DayDateGregorian);
                        var endDate = DateHelper.StringToDate(filter.DayDateGregorian2);
                        query = query.Where(a => DateHelper.StringToDate(a.DayDateGregorian) >= startDate && DateHelper.StringToDate(a.DayDateGregorian) <= endDate);

                    }
                }
                if (filter.StatusId > 0)
                {
                    switch (filter.StatusId.Value)
                    {
                        case 1:
                            query = query.Where(a => a.AllowTimeIn == true);
                            break;
                        case 2:
                            query = query.Where(a => a.TimeOut == null);
                            break;
                        case 3:
                            query = query.Where(a => a.TimeOut != null);
                            break;
                    }
                }
                var result = query
                    .GroupBy(a => new { a.EmpId, a.EmpCode, a.DayName, a.DayDateGregorian, a.EmpName, a.LocationName, a.BraName, a.AttendanceTypeName, a.TimeTableName, a.DailyWorkingHours })
                    .Select(g => new AttendanceSummaryDto
                    {
                        EmpId = g.Key.EmpId,
                        EmpCode = g.Key.EmpCode ?? "",
                        DayName = g.Key.DayName ?? "",
                        DayDateGregorian = g.Key.DayDateGregorian ?? "",
                        EmpName = g.Key.EmpName ?? "",
                        LocationName = g.Key.LocationName ?? "",
                        BranchName = g.Key.BraName ?? "",
                        AttendanceTypeName = g.Key.AttendanceTypeName ?? "",
                        TimeTableName = g.Key.TimeTableName,
                        DailyWorkingHours = g.Key.DailyWorkingHours,
                        TotalTime = g.Sum(a => (a.TimeOut.HasValue ? (a.TimeOut.Value - a.TimeIn.Value).TotalMinutes : 0)),
                        TotalTimeWorkShift = g.Sum(a => (a.OffDutyTime.HasValue ? (a.OffDutyTime.Value - a.OnDutyTime.Value).TotalMinutes : 0)),
                        TimeIn = g.FirstOrDefault() != null && g.FirstOrDefault().TimeIn.HasValue ? g.FirstOrDefault().TimeIn.Value.ToString(@"hh\:mm\:ss") : string.Empty,
                        TimeOut = g.FirstOrDefault() != null && g.FirstOrDefault().TimeOut.HasValue ? g.FirstOrDefault().TimeOut.Value.ToString(@"hh\:mm\:ss") : string.Empty,
                        DefTimeIn = g.FirstOrDefault() != null && g.FirstOrDefault().DefTimeIn.HasValue ? g.FirstOrDefault().DefTimeIn.Value.ToString(@"hh\:mm\:ss") : string.Empty,
                        DefTimeOut = g.FirstOrDefault() != null && g.FirstOrDefault().DefTimeOut.HasValue ? g.FirstOrDefault().DefTimeOut.Value.ToString(@"hh\:mm\:ss") : string.Empty
                    })
                    .OrderByDescending(a => a.DayDateGregorian)
                    .ToList();
                var getAllDelay = await hrRepositoryManager.HrDelayRepository.GetAllFromView(x => x.IsDeleted == false && x.DelayDate != "" && x.DelayDate != null);
                var getAllPermissions = await hrRepositoryManager.HrPermissionRepository.GetAllFromView(x => x.IsDeleted == false && x.PermissionDate != "" && x.PermissionDate != null && x.FacilityId == session.FacilityId);

                foreach (var item in result)
                {
                    double totalTimeDelay = 0;
                    double totalTimePermission = 0;
                    var getDelay = getAllDelay.Where(x => x.EmpCode == item.EmpCode && DateHelper.StringToDate(x.DelayDate) >= DateHelper.StringToDate(item.DayDateGregorian) && DateHelper.StringToDate(x.DelayDate) <= DateHelper.StringToDate(item.DayDateGregorian));
                    foreach (var Delayitem in getDelay)
                    {
                        var TotalMinutes = 0;
                        var delayTime = Delayitem.DelayTime;
                        totalTimeDelay += delayTime.Value.TotalMinutes;
                    }
                    item.TotalDelay = GetStringTimeFromMinutes((decimal)totalTimeDelay);
                    var getPermission = getAllPermissions.Where(x => x.EmpCode == item.EmpCode && DateHelper.StringToDate(x.PermissionDate) >= DateHelper.StringToDate(item.DayDateGregorian) && DateHelper.StringToDate(x.PermissionDate) <= DateHelper.StringToDate(item.DayDateGregorian));

                    foreach (var Permissionitem in getPermission)
                    {
                        var difference = TimeSpan.Parse(Permissionitem.EstimatedTimeReturn) - TimeSpan.Parse(Permissionitem.LeaveingTime);
                        totalTimePermission += difference.TotalMinutes;
                    }
                    item.TotalPermission = GetStringTimeFromMinutes((decimal)totalTimePermission);

                    decimal totalTime = (decimal)item.TotalTime;
                    decimal workHours = 0;

                    if (filter.HoursReadings == 0)
                    {
                        workHours = (decimal)item.TotalTimeWorkShift;
                    }
                    else if (filter.HoursReadings == 1)
                    {
                        workHours = item.DailyWorkingHours ?? 0;
                    }
                    else
                    {
                        workHours = Convert.ToDecimal(filter.AttendanceHours) * 60;
                    }

                    item.TotalTimeFormatted = GetStringTimeFromMinutes(totalTime);
                    double txtTotal = (double)workHours - ((double)totalTime + totalTimePermission + totalTimeDelay);
                    item.UnregisteredDefault = GetStringTimeFromMinutes((decimal)(txtTotal > 0 ? txtTotal : 0));
                    item.WorkHoursFormatted = GetStringTimeFromMinutes(workHours);

                    TimeAttAllDay += totalTime;
                }

                if (result.Count > 0)
                {
                    return await Result<IEnumerable<AttendanceSummaryDto>>.SuccessAsync(result, "");
                }
                return await Result<IEnumerable<AttendanceSummaryDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));

            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<AttendanceSummaryDto>>.FailAsync($"EXP in GetAttendanceReportByDate at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public async Task<IResult<string>> AddDelayFromReport(List<AddDelayDto> entities, CancellationToken cancellationToken = default)
        {

            try
            {
                string ResultMessage = string.Empty;
                int Count = 0;

                var getAllEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(x => x.Isdel == false && x.IsDeleted == false);
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var entity in entities)
                {

                    var getEmployeeData = getAllEmployees.Where(x => x.EmpId == entity.EmpCode).FirstOrDefault();
                    if (getEmployeeData == null)
                    {
                        ResultMessage += $"الموظق رقم {entity.EmpCode ?? string.Empty} غير موجود " + " , ";
                        continue;
                    }
                    if (string.IsNullOrEmpty(entity.UnregisteredDefault) || entity.UnregisteredDefault == "00:00")
                    {
                        ResultMessage += $"ليس هناك تقصير غير مسجل للموظف  {entity.EmpCode ?? string.Empty} " + " , ";
                        continue;
                    }
                    if (string.IsNullOrEmpty(entity.DayDateGregorian))
                    {
                        ResultMessage += $"ليس هناك تاريخ اعتماد للموظف  {entity.EmpCode ?? string.Empty} " + " , ";
                        continue;
                    }
                    var newDelay = new HrDelay
                    {
                        EmpId = getEmployeeData.Id,
                        IsDeleted = false,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        DelayDate = entity.DayDateGregorian,
                        TypeId = 2,
                        Note = "اجمالي من تقرير اعتماد التاخير",
                        DelayTime = TimeSpan.Parse(entity.UnregisteredDefault)
                    };
                    await hrRepositoryManager.HrDelayRepository.Add(newDelay);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    Count = Count + 1;
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                if (Count > 0)
                {
                    ResultMessage = localization.GetResource1("ActionSuccess") + "  لعدد " + Count + " من الحقول " + ResultMessage;
                    return await Result<string>.SuccessAsync(ResultMessage);

                }
                return await Result<string>.SuccessAsync(ResultMessage);

            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in AddDelayFromReport at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }



        public async Task<IResult<string>> HR_Reaset_Attendance_SP(HrAttendanceResetDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"Error in Reset of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Reaset_Attendance_SP(entity);
                return await Result<string>.SuccessAsync("تمت عملية التحضير بنجاح");

            }
            catch (Exception)
            {
                return await Result<string>.FailAsync($"حدث خطاء اثناءإعادة أرسال البصمة ");
            }
        }





        private string GetStringTimeFromMinutes(decimal time)
        {
            int totalIntegerMinutes = (int)time;
            int hours = totalIntegerMinutes / 60;
            int minutes = totalIntegerMinutes % 60;
            string timeElapsedTotal = (hours.ToString().Length == 1 ? "0" + hours.ToString() : hours.ToString()) + ":" + (minutes.ToString().Length == 1 ? "0" + minutes.ToString() : minutes.ToString());
            return timeElapsedTotal;
        }



        public async Task<IResult<IEnumerable<HRAttendanceTotalReportSPDto>>> HR_Attendance_TotalReport_SP(HRAttendanceTotalReportSPFilterDto filter)
        {
            try
            {
                var Branchs = session.Branches.Split(',');
                filter.BranchsId = string.Join(",", Branchs);
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_TotalReport_SP(filter);
                return await Result<IEnumerable<HRAttendanceTotalReportSPDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HRAttendanceTotalReportSPDto>>.FailAsync($"EXP in {nameof(getHR_Attendance_Report_SP)} at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<List<HRAttendanceReport5Dto>>> Search(HRAttendanceReport5FilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                filter.CalendarType = Convert.ToInt32(session.CalendarType);
                filter.Language = Convert.ToInt32(session.Language);
                if (string.IsNullOrEmpty(filter.From) || string.IsNullOrEmpty(filter.To))
                {
                    filter.From = null;
                    filter.To = null;
                }
                if (!string.IsNullOrEmpty(filter.EmpCode))
                {

                    var getEmpId = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.Isdel == false && x.IsDeleted == false && x.EmpId == filter.EmpCode);
                    if (getEmpId != null)
                    {
                        filter.EmpId = getEmpId.Id;
                    }
                }


                var items = await getHR_Attendance_Report5_SP(filter);
                if (items != null)
                {
                    if (items.Data.Count() > 0)
                    {
                        return await Result<List<HRAttendanceReport5Dto>>.SuccessAsync(items.Data.ToList(), "");
                    }
                    return await Result<List<HRAttendanceReport5Dto>>.SuccessAsync(items.Data.ToList(), localization.GetResource1("NosearchResult"));
                }
                return await Result<List<HRAttendanceReport5Dto>>.FailAsync(items.Status.message);
            }
            catch (Exception ex)
            {
                return await Result<List<HRAttendanceReport5Dto>>.FailAsync(ex.Message);
            }
        }
    }
}