using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrNotificationsTypeService : IGenericQueryService<HrNotificationsTypeDto, HrNotificationsTypeVw>, IGenericWriteService<HrNotificationsTypeDto, HrNotificationsTypeEditDto>
    {

    }


}
