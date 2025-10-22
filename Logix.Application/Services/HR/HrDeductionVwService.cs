using AutoMapper;
using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrDeductionVwService : GenericQueryService<HrDeductionVw, HrDeductionVw, HrDeductionVw>, IHrDeductionVwService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrDeductionVwService(IQueryRepository<HrDeductionVw> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
        }
    }
    }
