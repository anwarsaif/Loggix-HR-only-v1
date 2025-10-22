using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrPermissionRepository : IGenericRepository<HrPermission, HrPermissionsVw>
    {
        Task<IEnumerable<HrPermissionsVw>> GetAllFromView(Expression<Func<HrPermissionsVw, bool>> expression);

    }

}
