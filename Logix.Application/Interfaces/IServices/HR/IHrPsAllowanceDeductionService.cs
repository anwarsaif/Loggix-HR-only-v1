using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPsAllowanceDeductionService : IGenericQueryService<HrPsAllowanceDeductionDto, HrPsAllowanceDeductionVw>, IGenericWriteService<HrPsAllowanceDeductionDto, HrPsAllowanceDeductionEditDto>
    {

    }


}
