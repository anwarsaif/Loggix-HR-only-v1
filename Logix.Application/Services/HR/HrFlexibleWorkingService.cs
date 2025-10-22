using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Logix.Application.Services.HR
{
    public class HrFlexibleWorkingService : GenericQueryService<HrFlexibleWorking, HrFlexibleWorkingDto, HrFlexibleWorkingVw>, IHrFlexibleWorkingService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrFlexibleWorkingService(IQueryRepository<HrFlexibleWorking> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrFlexibleWorkingDto>> Add(HrFlexibleWorkingDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrFlexibleWorkingEditDto>> Update(HrFlexibleWorkingEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<IEnumerable<HrFlexibleWorkingResultDto>>> SearchInAdd(HrFlexibleWorkingMasterFilterDto filter)
        {
            try
            {
                // Parse and split Branch IDs
                var BranchesList = filter.BranchIds?.Split(',').Select(int.Parse).ToList();

                // Fetch attendance data
                var attendanceData = await hrRepositoryManager.HrAttendanceRepository.GetAllFromView(
                    x => x.IsDeleted == false &&
                        x.PayrollType == 2 &&
                        (string.IsNullOrEmpty(filter.BranchIds) || BranchesList.Contains((int)x.BranchId)) &&
                        (filter.Branch == 0 || x.BranchId == filter.Branch) &&
                        (filter.Location == 0 || x.Location == filter.Location) &&
                        (filter.Dept == 0 || x.DeptId == filter.Dept)
                );

                var attendanceList = attendanceData.ToList();

                // Fetch flexible working data
                var flexibleWorkingData = await hrRepositoryManager.HrFlexibleWorkingRepository.GetAll(
                    x => x.IsDeleted == false
                );

                var filteredFlexibleWorkingData = flexibleWorkingData.ToList();

                // Perform filtering
                var result = attendanceList
                    .Where(x => !filteredFlexibleWorkingData.Any(f => f.EmpId == x.EmpId && x.DayDateGregorian == f.AttendanceDate) &&
                                x.TimeIn.HasValue && x.TimeOut.HasValue)
                    .Select(x =>
                    {
                        var timeInString = x.TimeIn.HasValue ? x.TimeIn.Value.ToString("HH:mm", CultureInfo.InvariantCulture) : null;
                        var timeOutString = x.TimeOut.HasValue ? x.TimeOut.Value.ToString("HH:mm", CultureInfo.InvariantCulture) : null;
                        var actualMinutes = (timeInString != null && timeOutString != null) ? DateHelper.CalculateMinutesDifference(timeInString, timeOutString) : 0;
                        int totalMinutes = (x.TimeIn.HasValue && x.TimeOut.HasValue) ? (int)(x.TimeOut.Value - x.TimeIn.Value).TotalMinutes : 0;

                        return new HrFlexibleWorkingResultDto
                        {
                            Minutes = actualMinutes,
                            AttendanceId = x.AttendanceId,
                            HourCost = x.HourCost,
                            ActualHours = $"{(actualMinutes / 60):D2}:{(actualMinutes % 60):D2}",
                            ActualMinute = actualMinutes,
                            DayName = session.Language == 1 ? x.DayName : x.DayName2,
                            DayDateGregorian = x.DayDateGregorian,
                            EmpCode = x.EmpCode,
                            EmpName = session.Language == 1 ? x.EmpName : x.EmpName2,
                            DailyWorkingHours = x.DailyWorkingHours,
                            TimeInString = timeInString,
                            TimeOutString = timeOutString,
                            EmpId = x.EmpId,
                        };
                    }).ToList();

                return result.Any()
                    ? await Result<IEnumerable<HrFlexibleWorkingResultDto>>.SuccessAsync(result)
                    : await Result<IEnumerable<HrFlexibleWorkingResultDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception ex)
            {
                return await Result<IEnumerable<HrFlexibleWorkingResultDto>>.FailAsync($"An error occurred while processing your request.{ex.Message}");
            }
        }

        public async Task<IResult<string>> ApproveWork(List<long> Ids, CancellationToken cancellationToken = default)
        {

            try
            {
                int Count = 0;

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var singleRecord in Ids)
                {
                    var item = await hrRepositoryManager.HrFlexibleWorkingRepository.GetById(singleRecord);
                    if (item != null)
                    {
                        item.StatusId = 2;
                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrFlexibleWorkingRepository.Update(item);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        Count = Count + 1;
                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync("  تمت عملية الإعتماد لعدد " + Count + $" حركة بنجاح");
            }
            catch (Exception)
            {

                return await Result<string>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }
    }
}