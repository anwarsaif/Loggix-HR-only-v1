using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
	public interface IHrJobLevelsAllowanceDeductionRepository : IGenericRepository<HrJobLevelsAllowanceDeduction>
    {
		Task<IEnumerable<HrJobLevelsAllowanceDeduction>> GetByJobIdAndTypeAsync(long jobId, int typeId);
	} 
   
}
