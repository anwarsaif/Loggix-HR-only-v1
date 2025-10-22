using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrContractsDeductionVwRepository : GenericRepository<HrContractsDeductionVw>, IHrContractsDeductionVwRepository
    {
        public HrContractsDeductionVwRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
