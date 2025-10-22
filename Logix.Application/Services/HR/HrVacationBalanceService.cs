using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrVacationBalanceService : GenericQueryService<HrVacationBalance, HrVacationBalanceDto, HrVacationBalanceVw>, IHrVacationBalanceService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;

        public HrVacationBalanceService(IQueryRepository<HrVacationBalance> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrVacationBalanceDto>> Add(HrVacationBalanceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrVacationBalanceDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrVacationBalanceDto>.FailAsync($"Employee Id Is Required");
            try
            {
                // check if Emp Is Exist
                var checkEmpExxist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExxist == null) return await Result<HrVacationBalanceDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmpExxist.StatusId == 2) return await Result<HrVacationBalanceDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                var item = _mapper.Map<HrVacationBalance>(entity);
                item.EmpId = checkEmpExxist.Id;
                item.IsDeleted = false;
                item.VBalanceRate = entity.VacationBalance / 2.5m;

                var newEntity = await hrRepositoryManager.HrVacationBalanceRepository.AddAndReturn(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrVacationBalanceDto>(newEntity);

                return await Result<HrVacationBalanceDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrVacationBalanceDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult<HrVacationEmpBalanceDto>> HR_Vacation_Balance(HrVacationEmpBalanceDto filter)
        {

            if (string.IsNullOrEmpty(filter.Emp_Code))
            {
                return await Result<HrVacationEmpBalanceDto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));
            }

            try
            {
                var item = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == filter.Emp_Code && x.IsDeleted == false && x.Isdel == false);
                if (item == null) return await Result<HrVacationEmpBalanceDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                filter.Emp_ID = item.Id;
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Vacation_Balance(filter);
                return await Result<HrVacationEmpBalanceDto>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<HrVacationEmpBalanceDto>.FailAsync($"EXP in HR_Vacation_Balance at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrVacationBalanceRepository.GetOne(x => x.VacBalanceId == Id);
                if (item == null) return Result<HrVacationBalanceDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrVacationBalanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVacationBalanceDto>.SuccessAsync(_mapper.Map<HrVacationBalanceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrVacationBalanceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrVacationBalanceRepository.GetOne(x => x.VacBalanceId == Id);
                if (item == null) return Result<HrVacationBalanceDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrVacationBalanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVacationBalanceDto>.SuccessAsync(_mapper.Map<HrVacationBalanceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrVacationBalanceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<List<HrVacationBalanceFilterDto>>> Search(HrVacationBalanceFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');
                filter.BranchId ??= 0; filter.VacationTypeId ??= 0; filter.DeptId ??= 0; filter.Location ??= 0;

                List<HrVacationBalanceFilterDto> resultList = new List<HrVacationBalanceFilterDto>();
                var items = await hrRepositoryManager.HrVacationBalanceRepository.GetAllVw(e => e.IsDeleted == false
                && (string.IsNullOrEmpty(filter.EmpCode) || e.EmpCode == filter.EmpCode)
                && (filter.BranchId == 0 || e.BranchId == filter.BranchId)
                && (filter.VacationTypeId == 0 || e.VacationTypeId == filter.VacationTypeId)
                && (filter.DeptId == 0 || e.DeptId == filter.DeptId)
                && (filter.Location == 0 || e.Location == filter.Location)
                && BranchesList.Contains(e.BranchId.ToString())
                );
                if (items != null)
                {
                    if (items.Any())
                    {
                        foreach (var item in items)
                        {
                            var newRecord = new HrVacationBalanceFilterDto
                            {
                                Id = item.VacBalanceId,
                                EmpCode = item.EmpCode,
                                EmpName = item.EmpName,
                                EmpName2 = item.EmpName2,
                                LocationName = item.LocationName,
                                LocationName2 = item.LocationName2,
                                DepName = item.DepName,
                                DepName2 = item.DepName2,
                                BraName = item.BraName,
                                BraName2 = item.BraName2,
                                StartDate = item.StartDate,
                                VacationBalance = item.VacationBalance,
                                Note = item.Note,
                                VacationTypeName = item.VacationTypeName,
                                VacationTypeName2 = item.VacationTypeName2,
                            };
                            resultList.Add(newRecord);
                        }
                        if (resultList.Any())
                            return await Result<List<HrVacationBalanceFilterDto>>.SuccessAsync(resultList, "");
                        return await Result<List<HrVacationBalanceFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
                    }
                    return await Result<List<HrVacationBalanceFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
                }
                return await Result<List<HrVacationBalanceFilterDto>>.FailAsync("errror");
            }
            catch (Exception ex)
            {
                return await Result<List<HrVacationBalanceFilterDto>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<HrVacationBalanceEditDto>> Update(HrVacationBalanceEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null) return await Result<HrVacationBalanceEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrVacationBalanceEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                //if (checkEmpExist.StatusId == 2) return await Result<HrVacationBalanceEditDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                var item = await hrRepositoryManager.HrVacationBalanceRepository.GetById(entity.VacBalanceId);
                if (item == null) return await Result<HrVacationBalanceEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

                _mapper.Map(entity, item);
                item.VBalanceRate = entity.VacationBalance / 2.5m;
                item.EmpId = checkEmpExist.Id;

                hrRepositoryManager.HrVacationBalanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVacationBalanceEditDto>.SuccessAsync(_mapper.Map<HrVacationBalanceEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrVacationBalanceEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<HrVacationBalanceALLFilterDto>>> VacationBalanceALL(HrVacationBalanceALLSendFilterDto filter)
        {
            try
            {
                // prepare filter
                filter.Location ??= 0; filter.DeptId ??= 0; filter.BranchId ??= 0; filter.NationalityId ??= 0;
                filter.VacationTypeId ??= 0; filter.JobCatagoriesId ??= 0;

                if (filter.BranchId > 0) filter.BranchsId = null;
                else filter.BranchsId = session.Branches;
                if (string.IsNullOrEmpty(filter.EmpId)) filter.EmpId = null;
                if (string.IsNullOrEmpty(filter.EmpName)) filter.EmpName = null;

                var result = await mainRepositoryManager.StoredProceduresRepository.HR_VacationBalance_SP(filter);
                return await Result<IEnumerable<HrVacationBalanceALLFilterDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HrVacationBalanceALLFilterDto>>.FailAsync($"EXP at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        //public async Task<IResult<List<HrVacationEmpBalanceDto>>> VacationEmpBalanceSearch(HrVacationEmpBalanceDto filter, CancellationToken cancellationToken = default)
        //{
        //	try
        //          {
        //              var chkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == filter.Emp_Code && x.IsDeleted == false && x.Isdel == false);

        //              List<HrVacationEmpBalanceDto> resultList = new List<HrVacationEmpBalanceDto>();
        //              var result = await mainRepositoryManager.StoredProceduresRepository.HR_Vacation_Balance(filter);

        //              result.Emp_Code = chkEmp.EmpId;
        //              result.emp_name = session.Language == 1 ? chkEmp.EmpName : chkEmp.EmpName2;
        //              resultList.Add(result);

        //              return await Result< List<HrVacationEmpBalanceDto>>.SuccessAsync(resultList, "");
        //          }
        //	catch (Exception ex)
        //	{
        //		return await Result<List<HrVacationEmpBalanceDto>>.FailAsync(ex.Message);
        //	}
        //}

        public async Task<IResult<List<HrVacationEmpBalanceDto>>> VacationEmpBalanceSearch(HrVacationEmpBalanceDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.Emp_Code))
                {
                    return await Result<List<HrVacationEmpBalanceDto>>.FailAsync("Employee code cannot be empty.");
                }

                var chkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == filter.Emp_Code && x.IsDeleted == false && x.Isdel == false);
                if (chkEmp == null)
                {
                    return await Result<List<HrVacationEmpBalanceDto>>.FailAsync("Employee not found.");
                }

                List<HrVacationEmpBalanceDto> resultList = new List<HrVacationEmpBalanceDto>();
                filter.Emp_ID = chkEmp.Id;
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Vacation_Balance(filter);

                result.Emp_Code = chkEmp.EmpId;
                result.emp_name = session.Language == 1 ? chkEmp.EmpName : chkEmp.EmpName2;
                resultList.Add(result);

                return await Result<List<HrVacationEmpBalanceDto>>.SuccessAsync(resultList, "");
            }
            catch (Exception ex)
            {
                return await Result<List<HrVacationEmpBalanceDto>>.FailAsync(ex.Message);
            }
        }
    }
}
