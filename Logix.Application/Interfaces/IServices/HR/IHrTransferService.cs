using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrTransferService : IGenericQueryService<HrTransferDto, HrTransfersVw>, IGenericWriteService<HrTransferDto, HrTransferEditDto>
    {
        Task<IResult<string>> HRGetchildeDepartmentFn(long DepId);
        Task<IResult<HrTransfersAllAddDto>> AddMultipleTransfers(HrTransfersAllAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrTransfersAdd2Dto>> Add2Transfers(HrTransfersAdd2Dto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrTransfersAdd2Dto>> GetEmpDataByEmpId(string empId, CancellationToken cancellationToken = default);

		Task<IResult<List<HrTransfersVw>>> Search(HrTransferFilterDto filter, CancellationToken cancellationToken = default);


	}


}
