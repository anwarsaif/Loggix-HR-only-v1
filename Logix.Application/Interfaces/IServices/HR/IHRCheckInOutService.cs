using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrCheckInOutService : IGenericQueryService<HrCheckInOutDto, HrCheckInOutVw>, IGenericWriteService<HrCheckInOutDto, HrCheckInOutDto>
    {
        Task<IResult<string>> ChangeType(long Id, CancellationToken cancellationToken = default);
        Task<IResult<string>> UpdateCheckInOut(HrUpdateCheckINout entity, CancellationToken cancellationToken = default);
		Task<IResult<List<HrAttendanceUnknownFilterDto>>> Search(HrAttendanceUnknownFilterDto filter, CancellationToken cancellationToken = default);

	}

}
