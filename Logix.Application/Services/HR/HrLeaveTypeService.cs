using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrLeaveTypeService : GenericQueryService<HrLeaveType, HrLeaveTypeDto, HrLeaveTypeVw>, IHrLeaveTypeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;
        private readonly IWorkflowHelper workflowHelper;

        public HrLeaveTypeService(IQueryRepository<HrLeaveType> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, ISysConfigurationAppHelper sysConfigurationAppHelper, IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
            this.workflowHelper = workflowHelper;
        }

    }
    }
