using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrMandateService : IGenericQueryService<HrMandateDto, HrMandateVw>, IGenericWriteService<HrMandateDto, HrMandateEditDto>
    {
        Task<IResult<bool>> AddNewMandate(HrMandateAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> PayrollAdd(HRMandatePayrollAddDto entity, CancellationToken cancellationToken = default);

    }
}
