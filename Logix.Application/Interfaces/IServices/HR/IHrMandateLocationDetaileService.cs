using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrMandateLocationDetaileService : IGenericQueryService<HrMandateLocationDetaileDto, HrMandateLocationDetailesVw>, IGenericWriteService<HrMandateLocationDetaileDto, HrMandateLocationDetaileEditDto>
    {

    }

}
