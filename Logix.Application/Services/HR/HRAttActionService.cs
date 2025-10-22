using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
    public class HrAttActionService : GenericQueryService<HrAttAction, HrAttActionDto, HrAttAction>, IHrAttActionService
    {
        public HrAttActionService(IQueryRepository<HrAttAction> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
        }

        public Task<IResult<HrAttActionDto>> Add(HrAttActionDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrAttActionDto>> Update(HrAttActionDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
