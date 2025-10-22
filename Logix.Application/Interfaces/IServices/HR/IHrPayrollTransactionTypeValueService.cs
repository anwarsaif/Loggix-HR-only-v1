using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPayrollTransactionTypeValueService : IGenericQueryService<HrPayrollTransactionTypeValueDto, HrPayrollTransactionTypeValuesVw>, IGenericWriteService<HrPayrollTransactionTypeValueDto, HrPayrollTransactionTypeValueEditDto>
    {
    }
}
