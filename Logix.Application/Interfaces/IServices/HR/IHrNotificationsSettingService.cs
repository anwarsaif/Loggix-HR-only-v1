using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrNotificationsSettingService : IGenericQueryService<HrNotificationsSettingDto, HrNotificationsSettingVw>, IGenericWriteService<HrNotificationsSettingDto, HrNotificationsSettingEditDto>
    {

    }


}
