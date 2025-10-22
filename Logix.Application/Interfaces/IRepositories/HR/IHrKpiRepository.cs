using Logix.Application.DTOs.HR;
using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrKpiRepository : IGenericRepository<HrKpi, HrKpiVw>
    {
        Task<IEnumerable<object>> GetKpiEmployeeDetailsAsync(HRKpiQueryFilterDto filter,CancellationToken cancellationToken=default);

    }
}
