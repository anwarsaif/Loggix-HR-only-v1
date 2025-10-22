using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrIncrementsAllowanceDeductionService : IGenericQueryService<HrIncrementsAllowanceDeductionDto, HrIncrementsAllowanceDeductionVw>, IGenericWriteService<HrIncrementsAllowanceDeductionDto, HrIncrementsAllowanceDeductionEditDto>
    {
    }
    }
