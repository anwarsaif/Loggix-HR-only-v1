using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrExpensesTypeService : IGenericQueryService<HrExpensesTypeDto, HrExpensesTypeVw>, IGenericWriteService<HrExpensesTypeDto, HrExpensesTypeEditDto>
    {

    }

}
