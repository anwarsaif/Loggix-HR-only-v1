using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDirectJobService : IGenericQueryService<HrDirectJobDto, HrDirectJobVw>, IGenericWriteService<HrDirectJobDto, HrDirectJobEditDto>
    {
		Task<IResult<List<HrDirectJobVw>>> Search(HrDirectJobFilterDto filter, CancellationToken cancellationToken = default);


	}


}
