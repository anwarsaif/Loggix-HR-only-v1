using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrExpenseService : IGenericQueryService<HrExpenseDto, HrExpensesVw>, IGenericWriteService<HrExpenseDto, HrExpenseEditDto>
    {
        Task<IResult<string>> Add(HrExpenseAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> CreateExpensesEntry(HrCreateExpensesEntryDto entity, CancellationToken cancellationToken = default);

    }

}
