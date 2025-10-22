using AutoMapper;
using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class InvestMonthService : GenericQueryService<InvestMonth, InvestMonth, InvestMonth>, IInvestMonthService
    {
        private readonly IAccRepositoryManager accRepositoryManager;

        private readonly IMapper _mapper;
        private readonly ICurrentData session;

        public InvestMonthService(IQueryRepository<InvestMonth> queryRepository, IAccRepositoryManager AccRepositoryManager, IMapper mapper, ICurrentData session) : base(queryRepository, mapper)
        {
            this.accRepositoryManager = AccRepositoryManager;
            this._mapper = mapper;

            this.session = session;
        }
    }
    
}