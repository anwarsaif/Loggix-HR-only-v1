using Logix.Domain.Hr;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrPayrollRepository : IGenericRepository<HrPayroll>
    {
        Task<HrPayroll> AddPayroll(HrPayroll hrPayroll, CancellationToken cancellationToken);
        Task ChangeStatusPayrollTrans(HrPayrollNote hrPayrollNote, CancellationToken cancellationToken);
    }

}
