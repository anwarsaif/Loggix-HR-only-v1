using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPayrollCostcenterService : IGenericQueryService<HrPayrollCostcenterDto, HrPayrollCostcenterVw>, IGenericWriteService<HrPayrollCostcenterDto, HrPayrollCostcenterEditDto>
    {

    }


}
