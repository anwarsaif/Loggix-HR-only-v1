using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrPayrollCostcenterService : GenericQueryService<HrPayrollCostcenter, HrPayrollCostcenterDto, HrPayrollCostcenterVw>, IHrPayrollCostcenterService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;


        public HrPayrollCostcenterService(IQueryRepository<HrPayrollCostcenter> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session, ILocalizationService localization, ISysConfigurationAppHelper sysConfigurationAppHelper) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
            this.session = session;
            this.localization = localization;
        }

        public Task<IResult<HrPayrollCostcenterDto>> Add(HrPayrollCostcenterDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrPayrollCostcenterEditDto>> Update(HrPayrollCostcenterEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
    }