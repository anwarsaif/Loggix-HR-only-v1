using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace Logix.Application.Services.HR
{
    public class HrActualAttendanceService : GenericQueryService<HrActualAttendance, HrActualAttendanceDto, HrActualAttendanceVw>, IHrActualAttendanceService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrActualAttendanceService(IQueryRepository<HrActualAttendance> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrActualAttendanceDto>> Add(HrActualAttendanceDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private async Task<IResult<List<HrActualAttendanceReportDto>>> GetAttendanceDetailsReportForEmployee(HrCheckInOutFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                // Fetch all data from the repository
                var query = await hrRepositoryManager.HrActualAttendanceRepository.GetAllFromView(x => x.Date != null && x.Date != "" && (string.IsNullOrEmpty(filter.EmpCode) || x.EmpCode == filter.EmpCode));

                if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
                {
                    var fromDate = DateHelper.StringToDate(filter.FromDate);
                    var toDate = DateHelper.StringToDate(filter.ToDate);
                    query = query.Where(a => a.Checktimein >= fromDate && a.Checktimein <= toDate);
                }
                var result = query.Select(x => new HrActualAttendanceReportDto
                {
                    EmpName = x.EmpName,
                    EmpCode = x.EmpCode,
                    DepName = x.DepName,
                    BraName = x.BraName,
                    CHECKTIMEIN2 = x.Checktimein.HasValue ? x.Checktimein.Value.ToString("HH:mm:ss") : "",
                    CHECKTIMEOut2 = x.Checktimeout.HasValue ? x.Checktimeout.Value.ToString("HH:mm:ss") : "",
                    TotalTime = FormatTotalTime(x.Checktimein, x.Checktimeout),
                    Date = x.Date ?? "",
                    DayName = x.DayName
                }).OrderByDescending(a => a.EmpId).ThenByDescending(a => DateHelper.StringToDate(a.Date)).ToList();
                return await Result<List<HrActualAttendanceReportDto>>.SuccessAsync(result, (result.Count() > 0) ? "" : localization.GetResource1("NosearchResult"));
            }
            catch (Exception exp)
            {
                return await Result<List<HrActualAttendanceReportDto>>.FailAsync($"EXP in GetAttendanceDetailsReportForEmployee at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        private string FormatTotalTime(DateTime? checkTimeIn, DateTime? checkTimeOut)
        {
            if (checkTimeIn.HasValue && checkTimeOut.HasValue)
            {
                var totalMinutes = (checkTimeOut.Value - checkTimeIn.Value).TotalMinutes;
                var hours = (int)(totalMinutes / 60);
                var minutes = (int)(totalMinutes % 60);
                return $"{hours:D2}:{minutes:D2}";
            }
            return "00:00";
        }

        private int CalculateTotalSecondsIN(DateTime? checkTimeIn, DateTime? checkTimeOut)
        {
            if (checkTimeIn.HasValue && checkTimeOut.HasValue)
            {
                return (int)(checkTimeOut.Value - checkTimeIn.Value).TotalMinutes;
            }
            return 0;
        }


        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrActualAttendanceDto>> Update(HrActualAttendanceDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }



        private async Task<IResult<List<HrActualAttendanceReportDto>>> GetAttendanceTotalReportForEmployee(HrCheckInOutFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                filter.TotalTimeOut ??= 0;
                filter.TotalTimeIn ??= 0;

                // Fetch attendance records from view
                var attendanceViewQuery = await hrRepositoryManager.HrActualAttendanceRepository.GetAllFromView(x => x.Date != null &&
                    (string.IsNullOrEmpty(filter.EmpCode) || x.EmpCode == filter.EmpCode));

                // Apply date filter if provided
                if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
                {
                    var fromDate = DateHelper.StringToDate(filter.FromDate);
                    var toDate = DateHelper.StringToDate(filter.ToDate);
                    attendanceViewQuery = attendanceViewQuery.Where(a => a.Checktimein >= fromDate && a.Checktimein <= toDate);
                }

                // Fetch attendance details from the repository
                var attendancesQuery = await hrRepositoryManager.HrAttendanceRepository.GetAll(y => y.IsDeleted == false);

                // Apply time filters
                if (filter.TotalTimeOut != 0 || filter.TotalTimeIn != 0)
                {
                    attendanceViewQuery = attendanceViewQuery.Where(a =>
                        attendancesQuery.Any(y => y.EmpId == a.EmpId && y.DayDateGregorian == a.Date && y.TimeIn != null && y.TimeOut != null));
                }

                // Final query to calculate and format time
                var result = (from attendance in attendanceViewQuery
                              let timeIn = attendancesQuery.Where(y => y.EmpId == attendance.EmpId && y.DayDateGregorian == attendance.Date)
                                                           .OrderBy(y => y.TimeIn).FirstOrDefault()?.TimeIn
                              let timeOut = attendancesQuery.Where(y => y.EmpId == attendance.EmpId && y.DayDateGregorian == attendance.Date)
                                                           .OrderBy(y => y.TimeOut).FirstOrDefault()?.TimeOut
                              let totalMinutes = (timeIn.HasValue && timeOut.HasValue) ? (int)(timeOut.Value - timeIn.Value).TotalMinutes : 0
                              let totalSecondIN = CalculateTotalSecondsIN(attendance.Checktimein, attendance.Checktimeout)

                              select new HrActualAttendanceReportDto
                              {
                                  EmpCode = attendance.EmpCode,
                                  DepName = attendance.DepName ?? "",
                                  BraName = attendance.BraName ?? "",
                                  EmpName = attendance.EmpName ?? "",
                                  EmpId = attendance.EmpId,
                                  CHECKTIMEIN2 = attendance.Checktimein?.ToString("HH:mm:ss") ?? "",
                                  CHECKTIMEOut2 = attendance.Checktimeout?.ToString("HH:mm:ss") ?? "",
                                  TotalTime = FormatTotalTime(attendance.Checktimein, attendance.Checktimeout),
                                  Date = attendance.Date ?? "",
                                  DayName = attendance.DayName,
                                  TimeIn = timeIn?.ToString("HH:mm:ss"),
                                  TimeOut = timeOut?.ToString("HH:mm:ss"),
                                  TotalMinutes = totalMinutes,
                                  TotalSecondIN = totalSecondIN,

                                  FormattedTotalTime = totalMinutes != 0
                                      ? string.Format("{0:00}:{1:00}", totalMinutes / 60, totalMinutes % 60)
                                      : "00:00",
                                  FormattedWorkTime = totalMinutes != 0
                                      ? string.Format("{0:00}:{1:00}", totalSecondIN / 60, totalSecondIN % 60)
                                      : "00:00",
                                  FormattedExitTime = totalMinutes != 0
                                      ? string.Format("{0:00}:{1:00}", (totalMinutes - totalSecondIN) / 60, (totalMinutes - totalSecondIN) % 60)
                                      : "00:00"
                              })
                            .GroupBy(x => new { x.EmpId, x.Date, x.EmpName, x.BraName, x.DepName, x.DayName, x.EmpCode })
                            .Where(g => g.Any() &&
                                        (filter.TotalTimeOut == 0 ||
                                         g.Sum(a => a.TotalMinutes) - g.Sum(a => a.TotalSecondIN) >= filter.TotalTimeOut) &&
                                        (filter.TotalTimeIn == 0 ||
                                         g.Sum(a => a.TotalSecondIN) >= filter.TotalTimeIn))
                            .SelectMany(g => g)
                            .OrderByDescending(a => a.EmpId)
                            .ThenByDescending(a => DateHelper.StringToDate(a.Date))
                            .ToList();

                return await Result<List<HrActualAttendanceReportDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception exp)
            {
                var errorMessage = $"Exception in GetAttendanceTotalReportForEmployee at ({this.GetType()}), Message: {exp.Message}";
                if (exp.InnerException != null)
                {
                    errorMessage += $" --- Inner Exception: {exp.InnerException.Message}";
                }

                return await Result<List<HrActualAttendanceReportDto>>.FailAsync(errorMessage);
            }
        }

		public async Task<IResult<List<HrActualAttendanceReportDto>>> Search(HrCheckInOutFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				if (filter.ReportType == 1)
				{
					var result = await GetAttendanceDetailsReportForEmployee(filter);
					return result;

				}
				else if (filter.ReportType == 2)
				{
					var result = await GetAttendanceTotalReportForEmployee(filter);
					return result;

				}
				else
				{
					return await Result<List<HrActualAttendanceReportDto>>.FailAsync("يجب اختيار نوع التقرير اما  تفصيلي او اجمالي");

				}
			}
			catch (Exception ex)
			{
				return await Result<List<HrActualAttendanceReportDto>>.FailAsync(ex.Message);
			}
		}
	}
}