using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrVisitScheduleLocationService : IGenericQueryService<HrVisitScheduleLocationDto, HrVisitScheduleLocationVw>, IGenericWriteService<HrVisitScheduleLocationDto, HrVisitScheduleLocationEditDto>
    {
        Task<int> GetNewVisitCount(long groupId, string branchesId, CancellationToken cancellationToken = default);
    }
}
