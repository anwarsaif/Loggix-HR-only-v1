using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPreparationSalaryService : IGenericQueryService<HrPreparationSalaryDto, HrPreparationSalariesVw>, IGenericWriteService<HrPreparationSalaryDto, HrPreparationSalaryEditDto>
    {
        Task<IResult<string>> Remove(List<long> Id, CancellationToken cancellationToken = default);
        Task<IResult<HrPreparationSalaryEditDto>> Update(HrPreparationCommissionUpdateDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrPreparationSalaryDto>> PreparationCommissionAdd(HrPreparationSalaryDto entity, CancellationToken cancellationToken = default);

        Task<IResult<AddPreparationSalariesResultDto>> AddUsingExcel(List<HrPreparationSalaryDto> entities, CancellationToken cancellationToken = default);
        Task<IResult<AddPreparationSalariesResultDto>> AddPreparationCommisssionUsingExcel(List<HrPreparationSalaryDto> entities, CancellationToken cancellationToken = default);
        Task<IResult<string>> PreparationSalariesLoanAdd(List<HRPreparationSalariesLoanAddDto> entities, CancellationToken cancellationToken = default);
        Task<IResult<string>> RemoveByPackage(string PackageId, CancellationToken cancellationToken = default);

        Task<IResult<List<HrPayrollCompareResult?>>> PreparationSalariesPayrollCompare(HrPayrollCompareFilterDto filter, int CmdType);


    }


}
