using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDependentService : IGenericQueryService<HrDependentDto, HrDependentsVw>, IGenericWriteService<HrDependentDto, HrDependentEditDto>
    {

    }


}
