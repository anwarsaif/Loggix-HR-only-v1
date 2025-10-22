using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrRequestGoalsEmployeeDetailService : IGenericQueryService<HrRequestGoalsEmployeeDetailDto, HrRequestGoalsEmployeeDetailsVw>, IGenericWriteService<HrRequestGoalsEmployeeDetailDto, HrRequestGoalsEmployeeDetailDto>
    {

    } 
   
}
