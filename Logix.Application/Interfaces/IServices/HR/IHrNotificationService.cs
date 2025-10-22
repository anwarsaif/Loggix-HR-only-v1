using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrNotificationService : IGenericQueryService<HrNotificationDto, HrNotificationsVw>, IGenericWriteService<HrNotificationDto, HrNotificationEditDto>
    {
        Task<IResult<object>> LoadNotifications(string Subject, long FacilityId);
        Task<IResult<object>> SendDisciplinaryDeletionNotice(long EmpId, string CaseName);
        Task<IResult<string>> AddNotificationsReply(HrNotificationsReplyDto entity,CancellationToken cancellationToken=default);

    }


}
