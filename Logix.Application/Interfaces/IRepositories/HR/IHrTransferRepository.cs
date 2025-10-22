using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrTransferRepository : IGenericRepository<HrTransfer,HrTransfersVw>
    {
        Task<string> HRGetchildeDepartmentFn(long DepID);

    }

}
