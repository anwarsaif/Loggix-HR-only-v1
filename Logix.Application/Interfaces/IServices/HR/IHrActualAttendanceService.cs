using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrActualAttendanceService : IGenericQueryService<HrActualAttendanceDto, HrActualAttendanceVw>, IGenericWriteService<HrActualAttendanceDto, HrActualAttendanceDto>
    {
        //Task<IResult<List<HrActualAttendanceReportDto>>> GetAttendanceDetailsReportForEmployee(HrCheckInOutFilterDto filter, CancellationToken cancellationToken = default);
        //Task<IResult<List<HrActualAttendanceReportDto>>> GetAttendanceTotalReportForEmployee(HrCheckInOutFilterDto filter, CancellationToken cancellationToken = default);
		Task<IResult<List<HrActualAttendanceReportDto>>> Search(HrCheckInOutFilterDto filter, CancellationToken cancellationToken = default);

	}

}
