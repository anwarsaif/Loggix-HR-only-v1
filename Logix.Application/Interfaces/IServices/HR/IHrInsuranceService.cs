using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrInsuranceService : IGenericQueryService<HrInsuranceDto, HrInsurance>, IGenericWriteService<HrInsuranceDto, HrInsuranceEditDto>

    {
        Task<IResult<bool>> viewInsuranceEmp(List<HrInsuranceEmpVM> viewInsuranceEmp, CancellationToken cancellationToken = default);
        Task<IResult<HrInsuranceDto>> AddInsuranceExclude(HrInsuranceDto entity, CancellationToken cancellationToken = default);

    }

}
