using AutoMapper;
using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Domain.HR;
namespace Logix.Application.Services.HR
{
    public class HrSalaryGroupDeductionVwService : GenericQueryService<HrSalaryGroupDeductionVw, HrSalaryGroupDeductionVw, HrSalaryGroupDeductionVw>, IHrSalaryGroupDeductionVwService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;

        public HrSalaryGroupDeductionVwService(IQueryRepository<HrSalaryGroupDeductionVw> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
        }
    }
}
