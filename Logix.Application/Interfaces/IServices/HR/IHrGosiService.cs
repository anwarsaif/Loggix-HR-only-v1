using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrGosiService : IGenericQueryService<HrGosiDto, HrGosiVw>, IGenericWriteService<HrGosiDto, HrGosiEditDto>
    {
        Task<IResult> Remove(long Id, long GosiId, CancellationToken cancellationToken = default);
        Task<IResult<AccJournalMaster>> CreateDue(HrCreateDueDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> Add(HrGosiAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<EmployeeGosiDto>>> getEmployeeData(EmployeeGosiSearchtDto filter);
        Task<IResult<IEnumerable<HrEmployeeGosiReportDto>>> GetEmployeeGosiReportInf(HrEmployeeGosiReportFilterDto filter);
        Task<IResult<string>> UpdateGosiEmployee(HrGosiEditDto entity, CancellationToken cancellationToken = default);

    }

}
