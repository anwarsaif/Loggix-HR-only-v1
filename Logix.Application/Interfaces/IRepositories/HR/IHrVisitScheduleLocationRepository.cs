using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrVisitScheduleLocationRepository : IGenericRepository<HrVisitScheduleLocation>
    {
        Task<int> GetNewVisitCount(long groupId, string branchesId);
    }
}
