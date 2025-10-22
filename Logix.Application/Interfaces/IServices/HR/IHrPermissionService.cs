using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPermissionService : IGenericQueryService<HrPermissionDto, HrPermissionsVw>, IGenericWriteService<HrPermissionDto, HrPermissionEditDto>
    {
		Task<IResult<List<HrPermissionFilterDto>>> Search(HrPermissionFilterDto filter, CancellationToken cancellationToken = default);

	}
}
