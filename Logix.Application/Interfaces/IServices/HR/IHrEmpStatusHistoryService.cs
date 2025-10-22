using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrEmpStatusHistoryService : IGenericQueryService<HrEmpStatusHistoryDto, HrEmpStatusHistoryVw>, IGenericWriteService<HrEmpStatusHistoryDto, HrEmpStatusHistoryEditDto>
    {
		Task<IResult<List<HRRPEmpStatusHistoryFilterDto>>> Search(HRRPEmpStatusHistoryFilterDto filter, CancellationToken cancellationToken = default);

	}
}
