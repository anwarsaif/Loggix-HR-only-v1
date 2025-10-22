using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPolicyService : IGenericQueryService<HrPolicyDto, HrPoliciesVw>, IGenericWriteService<HrPolicyDto, HrPolicyEditDto>
    {
       // Task<decimal> ApplyPoliciesAsync(long facilityId, long policyId, long? empId);

    }


}
