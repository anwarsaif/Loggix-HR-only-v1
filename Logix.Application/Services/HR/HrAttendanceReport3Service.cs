using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;

namespace Logix.Application.Services.HR
{
    public class HrAttendanceReport3Service : IHrAttendanceReport3Service
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        public HrAttendanceReport3Service(IMainRepositoryManager mainRepositoryManager, ILocalizationService localization, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }
        public async Task<IResult<IEnumerable<HRAttendanceReport4Dto>>> GetAttendanceData(HRAttendanceReport4FilterDto filter, CancellationToken cancellationToken = default)
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

				if (filter.BranchID != null && filter.BranchID > 0)
				{
					filter.BranchsID = null;

				}

				else
				{
					filter.BranchID = 0;
					filter.BranchsID = session.Branches;
				}


				if (filter.DeptID == 0 || filter.DeptID == null)
				{
					filter.DeptID = 0;
				}

				if (filter.StatusID <= 0 || filter.StatusID == null)
				{
					filter.StatusID = 0;
				}
				if (filter.Location <= 0 || filter.Location == null)
				{
					filter.Location = 0;
				}

				if (filter.AttendanceType <= 0 || filter.AttendanceType == null)
				{
					filter.AttendanceType = 0;
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

				if (filter.SponsorsID == 0 || filter.SponsorsID == null)
				{
					filter.SponsorsID = 0;
				}
				filter.ManagerID = 0;
				filter.TimeTableID = 0;
				filter.ShitID = 0;
				var result = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_Report4_SP(filter);
                return await Result<IEnumerable<HRAttendanceReport4Dto>>.SuccessAsync(result);
            }
            catch (Exception ex)
            {

                return await Result<IEnumerable<HRAttendanceReport4Dto>>.FailAsync($"{ex.Message}");
            }
        }

    }

}
