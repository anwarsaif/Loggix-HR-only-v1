using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrMandateLocationMasterService : IGenericQueryService<HrMandateLocationMasterDto, HrMandateLocationMasterVw>, IGenericWriteService<HrMandateLocationMasterDto, HrMandateLocationMasterEditDto>
    {

    }

}
