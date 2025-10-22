using AutoMapper;
using Logix.Application.DTOs.HR;
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
    public class HrCustodyItemsPropertyService : GenericQueryService<HrCustodyItemsProperty, HrCustodyItemsPropertyDto, HrCustodyItemsProperty>, IHrCustodyItemsPropertyService
    {
        public HrCustodyItemsPropertyService(IQueryRepository<HrCustodyItemsProperty> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
        }

        public Task<IResult<HrCustodyItemsPropertyDto>> Add(HrCustodyItemsPropertyDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrCustodyItemsPropertyEditDto>> Update(HrCustodyItemsPropertyEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
