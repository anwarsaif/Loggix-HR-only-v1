using AutoMapper;
using Logix.Application.Interfaces.IRepositories;
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
    public class HrPsDeductionVwService : GenericQueryService<HrPsDeductionVw, HrPsDeductionVw, HrPsDeductionVw>, IHrPsDeductionVwService
    {
        public HrPsDeductionVwService(IQueryRepository<HrPsDeductionVw> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
        }

        public Task<IResult<HrPsDeductionVw>> Add(HrPsDeductionVw entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrPsDeductionVw>> Update(HrPsDeductionVw entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
