using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrKpiService : IGenericQueryService<HrKpiDto, HrKpiVw>, IGenericWriteService<HrKpiDto, HrKpiEditDto>
    {
        Task<IResult<List<object>>> GetEmployeeKpi(HRKpiQueryFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<bool>> UpdateKpiStatus(long Id,int StatusId, CancellationToken cancellationToken = default);
		Task<IResult<List<HRRepKPIFilterDto>>> Search(HRRepKPIFilterDto filter, CancellationToken cancellationToken = default);


	}


}
