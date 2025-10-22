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
    public class HrClearanceTypeService : GenericQueryService<HrClearanceType, HrClearanceTypeDto, HrClearanceTypeVw>, IHrClearanceTypeService
    {
        public HrClearanceTypeService(IQueryRepository<HrClearanceType> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
        }

        public Task<IResult<HrClearanceTypeDto>> Add(HrClearanceTypeDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrClearanceTypeDto>> Update(HrClearanceTypeDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
