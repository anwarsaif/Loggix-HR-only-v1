using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrStructureRepository : GenericRepository<HrStructure>, IHrStructureRepository
    {
        public HrStructureRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<HrStructure>> AddList(IEnumerable<HrStructure> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<HrStructure, bool>> expression, Expression<Func<HrStructure, TResult>> selector, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HrStructureVw>> GetAllVw()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HrStructureVw>> GetAllVw(Expression<Func<HrStructureVw, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HrStructureVw>> GetAllVw(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HrStructureVw>> GetAllVw(Expression<Func<HrStructureVw, bool>> expression, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<HrStructureVw?> GetOneVw(Expression<Func<HrStructureVw, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
