using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrVacationsTypeService : IGenericQueryService<HrVacationsTypeDto, HrVacationsTypeVw>, IGenericWriteService<HrVacationsTypeDto, HrVacationsTypeEditDto>
    {
        Task<IResult<HrVacationsTypeEditDto>> AddEditVacationPolicies(HrVacationsTypeEditVacationPoliciesDto entity, CancellationToken cancellationToken = default);

    }


}
