using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrMandateRepository : GenericRepository<HrMandate, HrMandateVw>, IHrMandateRepository
    {
        private readonly ApplicationDbContext context;

        public HrMandateRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


    }



}
