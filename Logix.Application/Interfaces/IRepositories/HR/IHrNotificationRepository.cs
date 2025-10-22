using Logix.Application.DTOs.HR;
using Logix.Domain.HR;


namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrNotificationRepository : IGenericRepository<HrNotification>
    {
        Task<object> LoadNotifications(string Subject, long FacilityId);

    }

}
