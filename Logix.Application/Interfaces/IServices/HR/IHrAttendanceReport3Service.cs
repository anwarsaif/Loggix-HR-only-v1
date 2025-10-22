using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAttendanceReport3Service 
    {
        Task<IResult<IEnumerable<HRAttendanceReport4Dto>>> GetAttendanceData(HRAttendanceReport4FilterDto entity, CancellationToken cancellationToken = default);

    }


}
