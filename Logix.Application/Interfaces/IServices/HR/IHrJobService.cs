using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrJobService : IGenericQueryService<HrJobDto, HrJobVw>, IGenericWriteService<HrJobDto, HrJobEditDto>
    {

    }


}
