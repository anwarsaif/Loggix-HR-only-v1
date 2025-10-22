using System.Data;
using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrClearanceService : IGenericQueryService<HrClearanceDto, HrClearanceVw>, IGenericWriteService<HrClearanceDto, HrClearanceEditDto>
    {
        Task<IResult<HREmpClearanceSpDto>> GetData(string EmpCode, string LastWorkingDate, int ClearanceTypeId, CancellationToken cancellationToken = default);
        Task<IResult<string>> Add(HrClearanceAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> PayrollTransfer(HrClearancePayrollTransferDto entity, CancellationToken cancellationToken = default);
        Task<IResult<List<HrClearanceVw>>> Search(HrClearanceFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<DataTable>> HR_Payroll_Clearance_Sp(string EmpCode, string EmpName, CancellationToken cancellationToken = default);
        Task<IResult<string>> Add2(HrClearanceAddDto2 entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> Edit2(HrClearanceAddDto2 entity, CancellationToken cancellationToken = default);
    }
}
