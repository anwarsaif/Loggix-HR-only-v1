using AutoMapper;
using Logix.Application.Common;
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

    public class HrAllowanceDeductionTempOrFixService : GenericQueryService<HrAllowanceDeductionTempOrFix, HrAllowanceDeductionTempOrFixDto, HrAllowanceDeductionTempOrFix>, IHrAllowanceDeductionTempOrFixService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        public HrAllowanceDeductionTempOrFixService(IQueryRepository<HrAllowanceDeductionTempOrFix> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)

        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
        }


        public Task<IResult<HrAllowanceDeductionTempOrFixDto>> Add(HrAllowanceDeductionTempOrFixDto entity, CancellationToken cancellationToken = default)

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


        public Task<IResult<HrAllowanceDeductionTempOrFixDto>> Update(HrAllowanceDeductionTempOrFixDto entity, CancellationToken cancellationToken = default)

        {
            throw new NotImplementedException();
        }
    }
}
