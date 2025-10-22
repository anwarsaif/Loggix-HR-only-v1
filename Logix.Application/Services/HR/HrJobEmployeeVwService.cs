using AutoMapper;
using Logix.Application.Common;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrJobEmployeeVwService : GenericQueryService<HrJobEmployeeVw, HrJobEmployeeVw, HrJobEmployeeVw>, IHrJobEmployeeVwService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;


        public HrJobEmployeeVwService(IQueryRepository<HrJobEmployeeVw> queryRepository,
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
    }
    }
    