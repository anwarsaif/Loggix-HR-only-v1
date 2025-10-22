using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Microsoft.Data.SqlClient;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Data;
using Logix.Application.Wrapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.Configuration;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrDisciplinaryCaseActionRepository : GenericRepository<HrDisciplinaryCaseAction, HrDisciplinaryCaseActionVw>, IHrDisciplinaryCaseActionRepository
    {
        private readonly ApplicationDbContext context;

        public HrDisciplinaryCaseActionRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


    }
}
