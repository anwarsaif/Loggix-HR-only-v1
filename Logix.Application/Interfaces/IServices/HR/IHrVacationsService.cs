using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrVacationsService : IGenericQueryService<HrVacationsDto, HrVacationsVw>, IGenericWriteService<HrVacationsDto, HrVacationsEditDto>
    {
        Task<decimal> Vacation_Balance_FN(string Curr_Date, long ID_Emp);
        Task<decimal> Vacation_Balance2_FN(string Curr_Date, long ID_Emp, int VacationTypeId);
        Task<IResult<HrVacationsDto>> AddVacations(HrVacationsDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrVacationsEditDto>> EditVacations(HrVacationsEditDto entity, CancellationToken cancellationToken = default);
        Task<IResult<List<HrVacationsFilterDto>>> Search(HrVacationsFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<HrVacationsFilterDto>>> VacationReportSearch(HrVacationsFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<HrRPVacationEmployeeFilterDto>>> HRRVacationEmployeeSearch(HrRPVacationEmployeeFilterDto filter, CancellationToken cancellationToken = default);
        Task<decimal> GetCountDays(string StartDate, string EndDate, int VacationTypeId);
        Task<PaginatedResult<IEnumerable<HrVacationsFilterDto>>> GetVacationReportPaginationGrouped(HrVacationsFilterDto filter, int take, long? lastSeenId = null);
    }
}
