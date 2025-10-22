using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrInsuranceEmpService : IGenericQueryService<HrInsuranceEmpDto, HrInsuranceEmpVw>, IGenericWriteService<HrInsuranceEmpDto, HrInsuranceEmpEditDto>

    {

    }   
   
}
