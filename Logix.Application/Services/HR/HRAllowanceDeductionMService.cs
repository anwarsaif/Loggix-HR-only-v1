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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{

    public class HrAllowanceDeductionMService : GenericQueryService<HrAllowanceDeductionM, HrAllowanceDeductionMDto, HrAllowanceDeductionM>, IHrAllowanceDeductionMService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;

        public HrAllowanceDeductionMService(IQueryRepository<HrAllowanceDeductionM> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)

        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
        }

        public Task<IResult<HrAllowanceDeductionMDto>> Add(HrAllowanceDeductionMDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrAllowanceDeductionMEditDto>> Update(HrAllowanceDeductionMEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

}