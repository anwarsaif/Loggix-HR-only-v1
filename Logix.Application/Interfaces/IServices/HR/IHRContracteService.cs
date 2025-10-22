using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrContracteService : IGenericQueryService<HrContracteDto, HrContractesVw>, IGenericWriteService<HrContracteDto, HrContracteEditDto>
    {
        Task<IResult<HrContracteAdd2Dto>> Add2(HrContracteAdd2Dto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrContracteAdd3Dto>> Add3(HrContracteAdd3Dto entity, CancellationToken cancellationToken = default);
        
    }

}
