using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.Repositories.HR
{
	public class HrJobAllowanceDeductionRepository : GenericRepository<HrJobAllowanceDeduction>, IHrJobAllowanceDeductionRepository

	{
		private readonly ApplicationDbContext context;

		public HrJobAllowanceDeductionRepository(ApplicationDbContext context) : base(context)
        {
			this.context = context;
		}

		public async Task<IEnumerable<HrJobAllowanceDeduction>> GetByJobIdAndTypeAsync(long jobId, int typeId)
		{
			return await context.Set<HrJobAllowanceDeduction>()
		   .Where(j => j.JobId == jobId && j.TypeId == typeId).ToListAsync();
		}
	} 


}
