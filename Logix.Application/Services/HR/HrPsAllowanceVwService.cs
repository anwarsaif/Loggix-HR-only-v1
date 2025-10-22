using AutoMapper;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
    public class HrPsAllowanceVwService : GenericQueryService<HrPsAllowanceVw, HrPsAllowanceVw, HrPsAllowanceVw>, IHrPsAllowanceVwService
    {
        public HrPsAllowanceVwService(IQueryRepository<HrPsAllowanceVw> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
        }

        public Task<IResult<HrPsAllowanceVw>> Add(HrPsAllowanceVw entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrPsAllowanceVw>> Update(HrPsAllowanceVw entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
