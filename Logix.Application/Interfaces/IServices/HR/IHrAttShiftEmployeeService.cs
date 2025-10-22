using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using InvestEmployee = Logix.Domain.Main.InvestEmployee;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAttShiftEmployeeService : IGenericQueryService<HrAttShiftEmployeeDto, HrAttShiftEmployeeVw>, IGenericWriteService<HrAttShiftEmployeeDto, HrAttShiftEmployeeEditDto>
    {
        Task<IResult<IEnumerable<InvestEmployee>>> Search1(HrAttShiftEmployeeFilterDto filter);
        Task<IResult<IEnumerable<HrAttCheckShiftEmployeeVw>>> Search2(HrAttShiftEmployeeFilterDto filter);
        Task<IResult<string>> Cancel(List<long?>  entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> Assign(HrAttShiftEmployeeAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> AssignUsingExcel(IEnumerable<HrAttShiftEmployeeAddFromExcelDto> entity, CancellationToken cancellationToken = default);

    }


}
