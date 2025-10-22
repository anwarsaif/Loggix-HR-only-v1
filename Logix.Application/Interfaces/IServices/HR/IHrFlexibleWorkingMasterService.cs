using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrFlexibleWorkingMasterService : IGenericQueryService<HrFlexibleWorkingMasterDto, HrFlexibleWorkingMasterDto>, IGenericWriteService<HrFlexibleWorkingMasterDto, HrFlexibleWorkingMasterDto>
    {
        Task<IResult<string>> Add(HrFlexibleWorkingMasterAddDto entity, CancellationToken cancellationToken = default);

    }

}
