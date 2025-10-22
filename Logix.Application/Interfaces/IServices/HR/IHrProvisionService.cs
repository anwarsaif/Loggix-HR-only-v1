using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrProvisionService : IGenericQueryService<HrProvisionDto, HrProvisionsVw>, IGenericWriteService<HrProvisionDto, HrProvisionEditDto>
    {
        Task<IResult<List<HrProvisionEmployeeResultDto>>> GetEmployeeProvisionVacationData(ProvisionSearchOnAddFilter filter);
        Task<IResult<List<HrProvisionEmployeeResultDto>>> GetEmployeeProvisionEndOfServiceData(ProvisionSearchOnAddFilter filter);
        Task<IResult<List<HrProvisionEmployeeResultDto>>> GetEmployeeProvisionTicketData(ProvisionSearchOnAddFilter filter);
        Task<IResult<string>> CreateProvisionEntry(HrProvisionEntryAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrProvisionDto>> AddEndOfServiceProvision(HrProvisionDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrProvisionDto>> AddVacationProvision(HrProvisionDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrProvisionDto>> AddTicketProvision(HrProvisionDto entity, CancellationToken cancellationToken = default);

    }

}
