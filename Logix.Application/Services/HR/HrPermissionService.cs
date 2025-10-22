using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Globalization;

namespace Logix.Application.Services.HR
{
    public class HrPermissionService : GenericQueryService<HrPermission, HrPermissionDto, HrPermissionsVw>, IHrPermissionService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        public HrPermissionService(IQueryRepository<HrPermission> queryRepository, IMainRepositoryManager mainRepositoryManager, ILocalizationService localization, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrPermissionDto>> Add(HrPermissionDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPermissionDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                if (!DateTime.TryParseExact(entity.PermissionDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var permissionDate))
                {
                    return await Result<HrPermissionDto>.FailAsync("Invalid permission date format");
                }

                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository
                    .GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);

                if (checkEmpExist == null)
                    return await Result<HrPermissionDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                if (checkEmpExist.StatusId == 2)
                    return await Result<HrPermissionDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository
                    .GetAllVw(e => e.IsDeleted == false && e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1);

                if (IfEmpExistsInPayroll.Any())
                {
                    var filterResult = IfEmpExistsInPayroll.Where(e =>
                        DateHelper.StringToDate(e.StartDate) <= permissionDate &&
                        DateHelper.StringToDate(e.EndDate) >= permissionDate);

                    if (filterResult.Any())
                    {
                        return await Result<HrPermissionDto>.FailAsync("لن تتمكن من اضافة استئذان بسبب استخراج مسير للموظف في نفس الشهر");
                    }
                }


                var item = _mapper.Map<HrPermission>(entity);
                item.EmpId = checkEmpExist.Id;
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateHelper.GetCurrentDateTime();
                item.IsDeleted = false;
                item.EstimatedTimeReturn = string.IsNullOrWhiteSpace(entity.EstimatedTimeReturn) ? "00:00:00" : DateTime.Parse(entity.EstimatedTimeReturn).ToString("HH:mm:ss");
                var newEntity = await hrRepositoryManager.HrPermissionRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrPermissionDto>(newEntity);


                return await Result<HrPermissionDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<HrPermissionDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrPermissionRepository.GetById(Id);
                if (item == null) return Result<HrPermissionDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrPermissionRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPermissionDto>.SuccessAsync(_mapper.Map<HrPermissionDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrPermissionDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrPermissionRepository.GetById(Id);
                if (item == null) return Result<HrPermissionDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateHelper.GetCurrentDateTime();
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrPermissionRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPermissionDto>.SuccessAsync(_mapper.Map<HrPermissionDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrPermissionDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

		public async Task<IResult<List<HrPermissionFilterDto>>> Search(HrPermissionFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				List<HrPermissionFilterDto> resultList = new List<HrPermissionFilterDto>();
				var items = await hrRepositoryManager.HrPermissionRepository.GetAllVw(e => e.IsDeleted == false && e.FacilityId == session.FacilityId);
				if (items != null)
				{
					if (items.Any())
					{
						var res = items.AsQueryable();

						if (!string.IsNullOrEmpty(filter.EmpName))
						{
							res = res.Where(r => r.EmpName != null && r.EmpName.Contains(filter.EmpName));
						}
						if (filter.EmpId > 0 && filter.EmpId != null)
						{
							res = res.Where(r => r.EmpCode != null && r.EmpCode == filter.EmpId.ToString());
						}
						if (filter.LocationId != null && filter.LocationId > 0)
						{
							res = res.Where(c => c.Location != null && c.Location == filter.LocationId);
						}
						if (filter.TypeId != null && filter.TypeId > 0)
						{
							res = res.Where(c => c.Type != null && c.Type == filter.TypeId);
						}
						if (filter.BranchId != null && filter.BranchId > 0)
						{
							res = res.Where(c => c.BranchId != null && c.BranchId == filter.BranchId);
						}
						if (filter.ReasonLeave != null && filter.ReasonLeave > 0)
						{
							res = res.Where(c => c.ReasonLeave != null && c.ReasonLeave == filter.ReasonLeave);
						}
						if (filter.DeptId != null && filter.DeptId > 0)
						{
							res = res.Where(c => c.DeptId != null && c.DeptId == filter.DeptId);
						}

						if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
						{
							res = res.Where(r => r.PermissionDate != null && DateHelper.StringToDate(r.PermissionDate) >= DateHelper.StringToDate(filter.FromDate) && DateHelper.StringToDate(r.PermissionDate) <= DateHelper.StringToDate(filter.ToDate));
						}

						if (!string.IsNullOrEmpty(filter.FromTime) && !string.IsNullOrEmpty(filter.ToTime))
						{
							res = res.Where(r => r.LeaveingTime != null && TimeSpan.Parse(r.LeaveingTime) >= TimeSpan.Parse(filter.FromTime) && TimeSpan.Parse(r.LeaveingTime) <= TimeSpan.Parse(filter.ToTime));
						}

						foreach (var item in res)
						{
							var newRecord = new HrPermissionFilterDto
							{
								Id = item.Id,
								Empcode = item.EmpCode,
								EmpName = item.EmpName,
								LocName = item.LocationName,
								DepName = item.DepName,
								BraName = item.BraName,
								PermissionDate = item.PermissionDate,
								TypeName = item.TypeName,
								reasonName = item.ReasonName,
								LeaveingTime = item.LeaveingTime,
								EstimatedTimeReturn = item.EstimatedTimeReturn,
								TimeDifference = item.TimeDifference,
								ContactNumber = item.ContactNumber,

							};
							resultList.Add(newRecord);
						}
						if (resultList.Any())
							return await Result<List<HrPermissionFilterDto>>.SuccessAsync(resultList, "");
						return await Result<List<HrPermissionFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
					}
					return await Result<List<HrPermissionFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
				}
				return await Result<List<HrPermissionFilterDto>>.FailAsync("errorrre");
			}
			catch (Exception ex)
			{
				return await Result<List<HrPermissionFilterDto>>.FailAsync(ex.Message);
			}
		}

		public async Task<IResult<HrPermissionEditDto>> Update(HrPermissionEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPermissionEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.Empcode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrPermissionEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                if (checkEmpExist.StatusId == 2) return await Result<HrPermissionEditDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1);
                if (IfEmpExistsInPayroll.Any())
                {
                    var filterResult = IfEmpExistsInPayroll.Where(e => DateHelper.StringToDate(entity.PermissionDate) >= DateHelper.StringToDate(e.StartDate) && DateHelper.StringToDate(entity.PermissionDate) <= DateHelper.StringToDate(e.EndDate));
                    if (filterResult.Any())
                    {
                        return await Result<HrPermissionEditDto>.FailAsync("لن تتمكن من اضافة استئذان بسبب استخراج مسير للموظف في نفس الشهر");
                    }

                }

                var item = await hrRepositoryManager.HrPermissionRepository.GetById(entity.Id);

                if (item == null) return await Result<HrPermissionEditDto>.FailAsync(localization.GetResource1("UpdateError"));

                _mapper.Map(entity, item);
                item.EmpId = checkEmpExist.Id;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateHelper.GetCurrentDateTime();
                hrRepositoryManager.HrPermissionRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPermissionEditDto>.SuccessAsync(_mapper.Map<HrPermissionEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrPermissionEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}
