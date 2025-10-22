using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrProvisionsEmployeeAccVwRepository : IGenericRepository<HrProvisionsEmployeeAccVw>
    {
        Task<List<ProvisionsAccDto>> GetAccDetailsAsync(long id,int type);

    }

}
