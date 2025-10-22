using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrInsurancePolicyService : IGenericQueryService<HrInsurancePolicyDto, HrInsurancePolicy>, IGenericWriteService<HrInsurancePolicyDto, HrInsurancePolicyEditDto>

    {

    } 
   
}
