using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrExpensesEmployeeService : IGenericQueryService<HrExpensesEmployeeDto, HrExpensesEmployeesVw>, IGenericWriteService<HrExpensesEmployeeDto, HrExpensesEmployeeEditDto>
    {

    }

}
