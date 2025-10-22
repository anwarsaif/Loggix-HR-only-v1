using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrMandateRequestsMasterService : IGenericQueryService<HrMandateRequestsMasterDto, HrMandateRequestsMasterVw>, IGenericWriteService<HrMandateRequestsMasterDto, HrMandateRequestsMasterEditDto>
    {

    }

}
