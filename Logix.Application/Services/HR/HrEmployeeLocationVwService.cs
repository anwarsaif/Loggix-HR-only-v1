using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrEmployeeLocationVwService : GenericQueryService<HrEmployeeLocationVw, HrEmployeeLocationVw, HrEmployeeLocationVw>, IHrEmployeeLocationVwService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrEmployeeLocationVwService(IQueryRepository<HrEmployeeLocationVw> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<List<HrEmployeeLocationVw>>> Search(HrAttLocationEmployeeFilterDto filter)
        {
            try
            {
                var branchesList = session.Branches.Split(',');
                filter.JobCatagoriesId ??= 0;
                filter.BranchId ??= 0;
                filter.DeptId ??= 0;
                filter.Location ??= 0;
                var items = await hrRepositoryManager.HrEmployeeLocationVwRepository.GetAll(e =>
                    e.IsDeleted == false &&
                    branchesList.Contains(e.BranchId.ToString()) &&
                    e.LocationId == filter.LocationId &&
                    (string.IsNullOrEmpty(filter.EmpCode) || e.EmpId == filter.EmpCode) &&
                    (string.IsNullOrEmpty(filter.EmpName) ||(e.EmpName!=null&& e.EmpName.Contains(filter.EmpName))) &&
                    (filter.BranchId == 0 || e.BranchId == filter.BranchId) &&
                    (filter.JobCatagoriesId == 0 || e.JobCatagoriesId == filter.JobCatagoriesId) &&
                    (filter.DeptId == 0 || e.DeptId == filter.DeptId) &&
                    (filter.Location == 0 || e.Location == filter.Location)
                );

                if (items == null || !items.Any())
                {
                    return await Result<List<HrEmployeeLocationVw>>.SuccessAsync(new List<HrEmployeeLocationVw>(), localization.GetResource1("NosearchResult"));
                }

                var result = items.OrderBy(c =>c.EmpIdInt).ToList();

                // Return the results
                if (result.Any())
                {
                    return await Result<List<HrEmployeeLocationVw>>.SuccessAsync(result, "");
                }

                return await Result<List<HrEmployeeLocationVw>>.SuccessAsync(new List<HrEmployeeLocationVw>(), localization.GetResource1("NosearchResult"));
            }
            catch (Exception exp)
            {
                return await Result<List<HrEmployeeLocationVw>>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

    }
}
