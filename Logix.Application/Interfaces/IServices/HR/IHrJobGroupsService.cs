using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
	public interface IHrJobGroupsService : IGenericQueryService<HrJobGroupsDto, HrJobGroupsVw>, IGenericWriteService<HrJobGroupsDto, HrJobGroupsEditDto>
    {

    }


}
