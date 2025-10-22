using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPayrollAllowanceDeductionService : IGenericQueryService<HrPayrollAllowanceDeductionDto, HrPayrollAllowanceDeductionVw>, IGenericWriteService<HrPayrollAllowanceDeductionDto, HrPayrollAllowanceDeductionEditDto>
    {

    }


}
