using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPoliciesTypeService : IGenericQueryService<HrPoliciesTypeDto, HrPoliciesType>, IGenericWriteService<HrPoliciesTypeDto, HrPoliciesTypeEditDto>
    {

    }


}
