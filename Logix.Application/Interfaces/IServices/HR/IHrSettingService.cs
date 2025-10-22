using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrSettingService : IGenericQueryService<HrSettingDto, HrSetting>, IGenericWriteService<HrSettingDto, HrSettingDto>
    {

    }
}
