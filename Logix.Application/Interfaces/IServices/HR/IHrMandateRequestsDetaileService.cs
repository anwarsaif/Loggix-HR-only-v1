using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrMandateRequestsDetaileService : IGenericQueryService<HrMandateRequestsDetaileDto, HrMandateRequestsDetailesVw>, IGenericWriteService<HrMandateRequestsDetaileDto, HrMandateRequestsDetaileEditDto>
    {

    }

}
