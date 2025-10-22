using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPerformanceService : IGenericQueryService<HrPerformanceDto, HrPerformanceVw>, IGenericWriteService<HrPerformanceDto, HrPerformanceEditDto>
    {
        Task<IResult<string>> SendNotificationsEvaluation(long PerformanceId, CancellationToken cancellationToken = default);
        Task<IResult<string>> SendEmployeePerformanceIndicators(long PerformanceId, CancellationToken cancellationToken = default);

    }

}
