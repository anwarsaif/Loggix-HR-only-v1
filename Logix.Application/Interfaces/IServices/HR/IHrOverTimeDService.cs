using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrOverTimeDService : IGenericQueryService<HrOverTimeDDto, HrOverTimeDVw>, IGenericWriteService<HrOverTimeDDto, HrOverTimeDEditDto>
    {

    }
}
