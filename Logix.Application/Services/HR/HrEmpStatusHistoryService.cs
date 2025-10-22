using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Globalization;

namespace Logix.Application.Services.HR
{
    public class HrEmpStatusHistoryService : GenericQueryService<HrEmpStatusHistory, HrEmpStatusHistoryDto, HrEmpStatusHistoryVw>, IHrEmpStatusHistoryService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IWorkflowHelper workflowHelper;

        public HrEmpStatusHistoryService(IQueryRepository<HrEmpStatusHistory> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
            this.workflowHelper = workflowHelper;
        }

        public Task<IResult<HrEmpStatusHistoryDto>> Add(HrEmpStatusHistoryDto entity, CancellationToken cancellationToken = default)
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

		public async Task<IResult<List<HRRPEmpStatusHistoryFilterDto>>> Search(HRRPEmpStatusHistoryFilterDto filter, CancellationToken cancellationToken = default)
		{
			List<HRRPEmpStatusHistoryFilterDto> results = new List<HRRPEmpStatusHistoryFilterDto>();

			try
			{
				var BranchesList = session.Branches.Split(',');

				var employees = await hrRepositoryManager.HrEmpStatusHistoryRepository.GetAllVw(e => e.IsDeleted == false && e.StatusId == 1 && BranchesList.Contains(e.BranchId.ToString()));
				var filteredEmployees = employees.Where(e =>
						(string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower())) &&
						(string.IsNullOrEmpty(filter.empCode) || e.EmpId == filter.empCode) &&
						(filter.StatusId == 0 || filter.StatusId == null || e.StatusId == filter.StatusId)

					);
				foreach (var item in filteredEmployees)
				{

					var employeeDto = new HRRPEmpStatusHistoryFilterDto
					{
						empCode = item.EmpId,
						EmpName = item.EmpName,
						UserName = item.UserFullname,
						OldStatus = item.StatusOldName,
						NewStatus = item.StatusNewName,
						Reason = item.Note,
						Tdate = item.CreatedOn.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
					};

					results.Add(employeeDto);
				}
				if (results.Count > 0) return await Result<List<HRRPEmpStatusHistoryFilterDto>>.SuccessAsync(results, "");
				return await Result<List<HRRPEmpStatusHistoryFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));


			}
			catch (Exception ex)
			{
				return await Result<List<HRRPEmpStatusHistoryFilterDto>>.FailAsync(ex.Message);
			}
		}

		public Task<IResult<HrEmpStatusHistoryEditDto>> Update(HrEmpStatusHistoryEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
    }