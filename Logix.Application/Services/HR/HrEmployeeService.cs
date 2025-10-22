using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Linq.Expressions;
using System.Security;

namespace Logix.Application.Services.HR
{
    public class HrEmployeeService : GenericQueryService<HrEmployee, HrEmployeeDto, HrEmployeeVw>, IHrEmployeeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;

        public HrEmployeeService(IQueryRepository<HrEmployee> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;

            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<IEnumerable<HrJobProgramVw>>> GetHrJobProgramVw(Expression<Func<HrJobProgramVw, bool>> expression)
        {
            try
            {
                var item = await hrRepositoryManager.HrEmployeeRepository.GetHrJobProgramVw(expression);
                if (item == null) return Result<IEnumerable<HrJobProgramVw>>.Fail($"--- there is no Data---");

                return await Result<IEnumerable<HrJobProgramVw>>.SuccessAsync(item, "");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<HrJobProgramVw>>.Fail($"Exp in getAll: {ex.Message} --- {(ex.InnerException != null ? "InnerExp: " + ex.InnerException.Message : "no inner")} .");
            }

        }


        public async Task<IResult<IEnumerable<HrAttShift>>> GetHrAttShift(Expression<Func<HrAttShift, bool>> expression)
        {
            try
            {
                var item = await hrRepositoryManager.HrEmployeeRepository.GetHrAttShift(expression);
                if (item == null) return Result<IEnumerable<HrAttShift>>.Fail($"--- there is no Data---");

                return await Result<IEnumerable<HrAttShift>>.SuccessAsync(item, "");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<HrAttShift>>.Fail($"Exp in getAll: {ex.Message} --- {(ex.InnerException != null ? "InnerExp: " + ex.InnerException.Message : "no inner")} .");
            }

        }
        public new async Task<IResult<HrEmployeeDto>> GetById(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrEmployeeRepository.GetById(Id);
                if (item == null) return Result<HrEmployeeDto>.Fail($"--- there is no Data with this id: {Id}---");
                var newEntity = _mapper.Map<HrEmployeeDto>(item);
                return await Result<HrEmployeeDto>.SuccessAsync(newEntity, "");
            }
            catch (Exception ex)
            {
                return Result<HrEmployeeDto>.Fail($"Exp in get data by Id: {ex.Message} --- {(ex.InnerException != null ? "InnerExp: " + ex.InnerException.Message : "no inner")} .");
            }

        }
        public new async Task<IResult<HrEmployeeDto>> GetById(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrEmployeeRepository.GetById(Id);
                if (item == null) return Result<HrEmployeeDto>.Fail($"--- there is no Data with this id: {Id}---");
                var newEntity = _mapper.Map<HrEmployeeDto>(item);
                return await Result<HrEmployeeDto>.SuccessAsync(newEntity, "");

            }
            catch (Exception ex)
            {
                return Result<HrEmployeeDto>.Fail($"Exp in get data by Id: {ex.Message} --- {(ex.InnerException != null ? "InnerExp: " + ex.InnerException.Message : "no inner")} .");
            }

        }
        public async Task<IResult<HrEmployeeDto>> Add(HrEmployeeDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrEmployeeDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                entity.FacilityId = (int)session.FacilityId;
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;

                var item = _mapper.Map<HrEmployee>(entity);
                var newEntity = await hrRepositoryManager.HrEmployeeRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrEmployeeDto>(newEntity);


                return await Result<HrEmployeeDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrEmployeeDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        //public async Task<IResult<HrEmployee>> Add(HrEmployeeDto entity, CancellationToken cancellationToken = default)
        //{
        //    if (entity == null) return await Result<HrEmployeeDto>.FailAsync($"Error in Add of: {GetType()}, the passed entity is NULL !!!.");

        //    try
        //    {

        //        var item = _mapper.Map<HrEmployee>(entity);
        //        var newEntity = await hrRepositoryManager.HrEmployeeRepository.AddAndReturn(item);

        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        var entityMap = _mapper.Map<HrEmployeeDto>(newEntity);


        //        return await Result<HrEmployeeDto>.SuccessAsync(entityMap, "item added successfully");
        //    }
        //    catch (Exception exc)
        //    {

        //        return await Result<HrEmployeeDto>.FailAsync($"EXP in {GetType()}, Meesage: {exc.Message}");
        //    }

        //}

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrEmployeeRepository.GetById(Id);
            if (item == null) return Result<HrEmployeeDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrEmployeeRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrEmployeeDto>.SuccessAsync(_mapper.Map<HrEmployeeDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrEmployeeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrEmployeeRepository.GetById(Id);
            if (item == null) return Result<HrEmployeeDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrEmployeeRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrEmployeeDto>.SuccessAsync(_mapper.Map<HrEmployeeDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrEmployeeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrEmployeeEditDto>> Update(HrEmployeeEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrEmployeeEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            var item = await hrRepositoryManager.HrEmployeeRepository.GetById(entity.Id);

            if (item == null) return await Result<HrEmployeeEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            hrRepositoryManager.HrEmployeeRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrEmployeeEditDto>.SuccessAsync(_mapper.Map<HrEmployeeEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrEmployeeEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<HrAttendanceReportDto>>> GetHrAttendanceReport(string EmpCode = "", long BranchId = 0, long TimeTableId = 0, int StatusId = 0, long Location = 0, long DeptId = 0, int AttendanceType = 0, int SponsorsId = 0)
        {
            var items = await hrRepositoryManager.HrEmployeeRepository.GetHrAttendanceReport(EmpCode, BranchId, TimeTableId, StatusId, Location, DeptId, AttendanceType, SponsorsId);

            if (items == null) return await Result<IEnumerable<HrAttendanceReportDto>>.FailAsync("No Lists Found");
            return await Result<IEnumerable<HrAttendanceReportDto>>.SuccessAsync(items, "Main List records retrieved");
        }

        public async Task<IResult<bool>> CHeckEmpInBranch(long? EmpID)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');

                var getData = await hrRepositoryManager.HrEmployeeRepository.GetOneVw(x => x.IsDeleted == false && x.Isdel == false && BranchesList.Contains(x.BranchId.ToString()) && x.Id == EmpID);
                if (getData != null)
                {
                    return await Result<bool>.SuccessAsync(true);

                }
                return await Result<bool>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));

            }
            catch (Exception ex)
            {

                return await Result<bool>.FailAsync(ex.Message.ToString());
            }
        }

        public async Task<IResult<List<HrEmployeeVw>>> Search(EmployeeFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');

                var items = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.IsDeleted == false && e.IsSub == false
               //&& BranchesList.Contains(e.BranchId.ToString())
               && (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower()) || e.EmpName2.ToLower().Contains(filter.EmpName.ToLower()))
               && (string.IsNullOrEmpty(filter.EmpCode) || e.EmpId == filter.EmpCode)
               && (filter.JobType == null || filter.JobType == 0 || filter.JobType == e.JobType)
               && (filter.Status == null || filter.Status == 0 || filter.Status == e.StatusId)
               && (filter.JobCatagoriesId == null || filter.JobCatagoriesId == 0 || filter.JobCatagoriesId == e.JobCatagoriesId)
               && (filter.Status == null || filter.Status == 0 || filter.Status == e.StatusId)
               && (filter.NationalityId == null || filter.NationalityId == 0 || filter.NationalityId == e.NationalityId)
               && (filter.DeptId == null || filter.DeptId == 0 || filter.DeptId == e.DeptId)
               && (string.IsNullOrEmpty(filter.IdNo) || e.IdNo == filter.IdNo)
               && (string.IsNullOrEmpty(filter.PassId) || e.PassportNo == filter.PassId)
               && (string.IsNullOrEmpty(filter.EntryNo) || e.EntryNo == filter.EntryNo)
               && (filter.Location == null || filter.Location == 0 || filter.Location == e.Location)
               && (filter.SponsorsID == null || filter.SponsorsID == 0 || filter.SponsorsID == e.SponsorsId)
               && (filter.FacilityId == null || filter.FacilityId == 0 || filter.FacilityId == e.FacilityId)
               && (filter.Level == null || filter.Level == 0 || filter.Level == e.LevelId)
               && (filter.Degree == null || filter.Degree == 0 || filter.Degree == e.DegreeId)
               && (filter.ContractType == null || filter.ContractType == 0 || filter.ContractType == e.ContractTypeId)
               && (filter.Protection == null || filter.Protection == 0 || filter.Protection == e.WagesProtection)
               && (string.IsNullOrEmpty(filter.EmpCode2) || e.EmpCode2 == filter.EmpCode2)
                );
                if (items != null)
                {
                    if (items.Count() > 0)
                    {
                        var res = items.AsQueryable();
                        if (filter.BranchId != null && filter.BranchId > 0)
                        {
                            res = res.Where(c => c.BranchId != null && c.BranchId == filter.BranchId);
                        }
                        else
                        {
                            res = res.Where(c => c.BranchId != null && BranchesList.Contains(c.BranchId.ToString()));
                        }
                        if (res.Count() > 0) return await Result<List<HrEmployeeVw>>.SuccessAsync(res.ToList());
                        return await Result<List<HrEmployeeVw>>.SuccessAsync(res.ToList(), localization.GetResource1("NosearchResult"));
                    }
                    return await Result<List<HrEmployeeVw>>.SuccessAsync(items.ToList(), localization.GetResource1("NosearchResult"));

                }

                return await Result<List<HrEmployeeVw>>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));
            }
            catch (Exception ex)
            {
                return await Result<List<HrEmployeeVw>>.FailAsync(ex.Message);

            }
        }

        public async Task<IResult<List<HrEmployeeBendingVM>>> EmployeeBendingSearch(
            HrEmployeeBendingFilterVM filter,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var EmployeeBendingList = new List<HrEmployeeBendingVM>();

                var BranchesList = session.Branches.Split(',');
                filter.BranchId ??= 0;
                filter.JobTypeId ??= 0;
                filter.DeptId ??= 0;
                filter.NationalityId ??= 0;
                filter.JobCatagoriesId ??= 0;
                filter.SponsorsID ??= 0;
                filter.FacilityId ??= 0;
                filter.LocationProjectId ??= 0;
                var getAllEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(
                    x => x.Isdel == false && x.IsDeleted == false && x.StatusId == 10
                      && (filter.JobTypeId == 0 || x.JobId == filter.JobTypeId)
                      && (filter.BranchId != 0 ? x.BranchId == filter.BranchId : BranchesList.Contains(x.BranchId.ToString()))
                      && (string.IsNullOrEmpty(filter.EmpId) || x.EmpId == filter.EmpId)
                      && (string.IsNullOrEmpty(filter.EmpName) || x.EmpName.ToLower().Contains(filter.EmpName.ToLower()) || x.EmpName2.ToLower().Contains(filter.EmpName.ToLower()))
                      && (filter.NationalityId == 0 || x.NationalityId == filter.NationalityId)
                      && (filter.JobCatagoriesId == 0 || x.JobCatagoriesId == filter.JobCatagoriesId)
                      && (filter.DeptId == 0 || x.DeptId == filter.DeptId)
                      && (string.IsNullOrEmpty(filter.IdNo) || x.IdNo == filter.IdNo)
                      && (filter.LocationProjectId == 0 || x.Location == filter.LocationProjectId)
                      && (filter.SponsorsID == 0 || x.SponsorsId == filter.SponsorsID)
                      && (filter.FacilityId == 0 || x.FacilityId == filter.FacilityId)
                      && (string.IsNullOrEmpty(filter.BorderID) || (x.EntryNo != null && x.EntryNo.ToLower().Contains(filter.BorderID.ToLower())))
                      && (string.IsNullOrEmpty(filter.PassportID) || (x.PassportNo != null && x.PassportNo.ToLower().Contains(filter.PassportID.ToLower())))

                    );

                if (getAllEmployees == null || !getAllEmployees.Any())
                    return await Result<List<HrEmployeeBendingVM>>.SuccessAsync(EmployeeBendingList);

                var acivityItems = await mainRepositoryManager.SysActivityLogRepository.GetAll(a => a.TableId == 7);

                var res = getAllEmployees.AsQueryable();

                EmployeeBendingList = res.Select(item => new HrEmployeeBendingVM
                {
                    Id = item.Id,
                    EmpId = item.EmpId,
                    EmpName = item.EmpName,
                    EmpName2 = item.EmpName2,
                    BraName = item.BraName,
                    ByName = item.ByName,
                    ContractDate = item.ContractData,
                    ContractExpiryDate = item.ContractExpiryDate,
                    IdNo = item.IdNo,
                    DepName = item.DepName,
                    LocationName = item.LocationName,
                    ReasonStatus = item.ReasonStatus,
                    StatusName = item.StatusName,
                    theDate = item.CreatedOn == null ? item.ModifiedOn.ToString() : item.CreatedOn.ToString(),
                    ActivityLogId = acivityItems.Equals(item.Id) ? acivityItems.FirstOrDefault(a => a.TablePrimarykey == item.Id).ActivityLogId : null
                }).ToList();

                return await Result<List<HrEmployeeBendingVM>>.SuccessAsync(EmployeeBendingList, "");
            }
            catch (Exception ex)
            {
                return await Result<List<HrEmployeeBendingVM>>.FailAsync(ex.Message);
            }
        }
        public async Task<IResult<List<string>>> GetchildDepartment(long DeptId, CancellationToken cancellationToken = default)
        {
            try
            {
                var getData = await mainRepositoryManager.DbFunctionsRepository.HR_Get_childe_Department_Fn(DeptId);
                return await Result<List<string>>.SuccessAsync(getData, $"", 200);
            }
            catch (Exception exp)
            {

                return await Result<List<string>>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<List<HrUnpaidEmployeesVM>>> UnpaidEmployeesSearch(HrUnpaidEmployeesFilter filter, CancellationToken cancellationToken = default)
        {
            try
            {
                if (filter.FinancelYear <= 0)
                {
                    return await Result<List<HrUnpaidEmployeesVM>>.FailAsync(" يجب اختيار السنة المالية");
                }
                if (string.IsNullOrEmpty(filter.MsMonth))
                {
                    return await Result<List<HrUnpaidEmployeesVM>>.FailAsync(" يجب اختيار الشهر ");
                }
                if (!string.IsNullOrEmpty(filter.MsMonth))
                {
                    if (int.TryParse(filter.MsMonth, out int month))
                    {
                        filter.MsMonth = month.ToString("D2");
                    }
                }
                filter.SponsorId ??= 0;
                filter.StatusId ??= 0;
                filter.NationalityId ??= 0;
                filter.JobCatagoriesId ??= 0;
                filter.Location ??= 0;
                filter.FacilityId ??= 0;
                filter.ContractType ??= 0;
                filter.WagesProtection ??= 0;
                var BranchesList = session.Branches.Split(',');
                var PayrolDetails = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(x => x.IsDeleted == false && x.PayrollTypeId == 1 && x.MsMonth == filter.MsMonth && x.FinancelYear == filter.FinancelYear);
                var empIds = PayrolDetails.Select(x => x.EmpId);
                var deptList = await GetchildDepartment((long)filter.DeptId);

                var EmpData = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(x =>
                x.IsDeleted == false
                && x.IsSub == false
                && x.Isdel == false
                && BranchesList.Contains(x.BranchId.ToString())
                && (filter.NationalityId == 0 || filter.NationalityId == x.NationalityId)
                && (filter.JobCatagoriesId == 0 || filter.JobCatagoriesId == x.JobCatagoriesId)
                && (filter.StatusId == 0 || filter.StatusId == x.StatusId)
                && (string.IsNullOrEmpty(filter.EmpName) || x.EmpName.ToLower().Contains(filter.EmpName) || x.EmpName2.ToLower().Contains(filter.EmpName))
                && (string.IsNullOrEmpty(filter.EmpId) || x.EmpId == filter.EmpId)
                && (string.IsNullOrEmpty(filter.IdNo) || filter.IdNo == x.IdNo)
                && (filter.SponsorId == 0 || filter.SponsorId == x.SponsorsId)
                && (filter.Location == 0 || filter.Location == x.Location)
                && (filter.FacilityId == 0 || filter.FacilityId == x.FacilityId)
                && (filter.ContractType == 0 || filter.ContractType == x.ContractTypeId)
                && (filter.WagesProtection == 0 || filter.WagesProtection == x.WagesProtection)
                && (string.IsNullOrEmpty(filter.EmpCode2) || filter.EmpCode2 == x.EmpCode2)
               && !empIds.Contains(x.Id)
                );
                var result = EmpData.Where(x => x.Doappointment != null && DateHelper.StringToDate(x.Doappointment) < DateHelper.StringToDate($"{filter.FinancelYear}/{filter.MsMonth}/{x.Doappointment.Substring(8, 2)}")).AsQueryable();
                if (filter.BranchId > 0)
                {
                    result = result.Where(x => x.BranchId == filter.BranchId);
                }
                if (filter.DeptId > 0)
                {
                    result = result.Where(x => (x.DeptId == filter.DeptId || deptList.Data.Contains(x.DeptId.ToString())));
                }
                if (result.Count() > 0)
                {
                    var projectedResult = result.Select(x => new HrUnpaidEmployeesVM
                    {
                        EmpId = x.EmpId,
                        EmpName = x.EmpName,
                        EmpName2 = x.EmpName2,
                        DepName = session.Language == 1 ? x.DepName : x.DepName2,
                        BraName = session.Language == 1 ? x.BraName : x.BraName2,
                        LocationName = session.Language == 1 ? x.LocationName : x.LocationName2,
                        CatName = session.Language == 1 ? x.CatName : x.CatName2,
                        StatusName = session.Language == 1 ? x.StatusName : x.StatusName2,
                        IdNo = x.IdNo,
                        EmpPhoto = x.EmpPhoto,
                        Doappointment = x.Doappointment,
                        ContractExpiryDate = x.ContractExpiryDate,
                    }).ToList();

                    return await Result<List<HrUnpaidEmployeesVM>>.SuccessAsync(projectedResult);


                }
                return await Result<List<HrUnpaidEmployeesVM>>.SuccessAsync((List<HrUnpaidEmployeesVM>)result);

            }
            catch (Exception ex)
            {
                return await Result<List<HrUnpaidEmployeesVM>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<List<HrEmployeeVw>>> HRQualificationsSearch(HrQualificationsFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                filter.BranchId ??= 0;
                filter.JobType ??= 0;
                filter.DeptId ??= 0;
                filter.NationalityId ??= 0;
                filter.JobCatagoriesId ??= 0;
                filter.Status ??= 0;
                var BranchesList = session.Branches.Split(',');

                var items = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.Isdel == false
                   && ((filter.BranchId > 0 && e.BranchId == filter.BranchId) || (BranchesList.Contains(e.BranchId.ToString())))
                   && (filter.JobType == 0 || e.JobType == filter.JobType)
                   && (filter.DeptId == 0 || e.DeptId == filter.DeptId)
                   && (filter.NationalityId == 0 || e.NationalityId == filter.NationalityId)
                   && (filter.JobCatagoriesId == 0 || e.JobCatagoriesId == filter.JobCatagoriesId)
                   && (filter.Status == 0 || e.StatusId == filter.Status)
                   && (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower()) || e.EmpName2.ToLower().Contains(filter.EmpName.ToLower()))
                   && (string.IsNullOrEmpty(filter.EmpCode) || e.EmpId == filter.EmpCode)
                   && (string.IsNullOrEmpty(filter.IdNo) || e.IdNo == filter.IdNo)
                   && (string.IsNullOrEmpty(filter.PassId) || e.PassportNo == filter.PassId)
                   && (string.IsNullOrEmpty(filter.EntryNo) || e.EntryNo == filter.EntryNo)
                );

                if (items.Count() > 0)
                {
                    return await Result<List<HrEmployeeVw>>.SuccessAsync(items.ToList());
                }
                return await Result<List<HrEmployeeVw>>.SuccessAsync(items.ToList(), localization.GetResource1("NosearchResult"));
            }
            catch (Exception ex)
            {
                return await Result<List<HrEmployeeVw>>.FailAsync(ex.Message);
            }
        }


        public async Task<IResult<List<HrEmployeeVw>>> SearchEmployeeSub(EmployeeSubFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');
                filter.BranchId ??= 0;
                filter.JobType ??= 0;
                filter.DeptId ??= 0;
                filter.NationalityId ??= 0;
                filter.JobCatagoriesId ??= 0;
                filter.StatusId ??= 0;
                filter.LocationId ??= 0;
                filter.SponsorsId ??= 0;
                filter.FacilityId ??= 0;
                filter.ParentId ??= 0;
                // Get child department IDs if DeptId filter is specified
                List<long> childDeptIds = null;
                if (filter.DeptId != 0)
                {
                    var childDeptIdsString = await mainRepositoryManager.DbFunctionsRepository.HR_Get_childe_Department_Fn(filter.DeptId.Value);
                    childDeptIds = childDeptIdsString
                        .Select(long.Parse)
                        .ToList();
                }
                var items = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.IsDeleted == false && e.IsSub == true
                   && (filter.BranchId != 0 ? e.BranchId == filter.BranchId : BranchesList.Contains(e.BranchId.ToString()))
                   && (filter.JobType == 0 || e.JobType == filter.JobType)
                   && (filter.DeptId == 0 || (e.DeptId == filter.DeptId || (childDeptIds != null && childDeptIds.Contains(e.DeptId.Value))))
                   && (filter.NationalityId == 0 || e.NationalityId == filter.NationalityId)
                   && (filter.JobCatagoriesId == 0 || e.JobCatagoriesId == filter.JobCatagoriesId)
                   && (filter.StatusId == 0 || e.StatusId == filter.StatusId)
                   && (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower()) || e.EmpName2.ToLower().Contains(filter.EmpName.ToLower()))
                   && (string.IsNullOrEmpty(filter.EmpId) || e.EmpId == filter.EmpId)
                   && (string.IsNullOrEmpty(filter.IdNo) || e.IdNo == filter.IdNo)
                   && (string.IsNullOrEmpty(filter.PassportNo) || e.PassportNo == filter.PassportNo)
                   && (string.IsNullOrEmpty(filter.EntryNo) || e.EntryNo == filter.EntryNo)
                   && (filter.LocationId == 0 || e.Location == filter.LocationId)
                   && (filter.SponsorsId == 0 || e.SponsorsId == filter.SponsorsId)
                   && (filter.FacilityId == 0 || e.FacilityId == filter.FacilityId)
                   && (filter.ParentId == 0 || e.ParentId == filter.ParentId)
                   && (string.IsNullOrEmpty(filter.CostCenterCode) || e.CostCenterCode == filter.CostCenterCode)
                   && (string.IsNullOrEmpty(filter.CostCenterName) || filter.CostCenterName.Contains(e.CostCenterName))
                );
                if (items != null)
                {
                    if (items.Count() > 0)
                    {
                        return await Result<List<HrEmployeeVw>>.SuccessAsync(items.ToList());
                    }
                    return await Result<List<HrEmployeeVw>>.SuccessAsync(items.ToList(), localization.GetResource1("NosearchResult"));

                }

                return await Result<List<HrEmployeeVw>>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));
            }
            catch (Exception ex)
            {
                return await Result<List<HrEmployeeVw>>.FailAsync(ex.Message);

            }
        }

        public async Task<IResult<HrEmployee>> GetEmpByCode(string EmpCode, long facilityId)
        {
            var items = await hrRepositoryManager.HrEmployeeRepository.GetEmpByCode(EmpCode, facilityId);
            return await Result<HrEmployee>.SuccessAsync(items);
        }

        public async Task<IResult<List<HrEmployeeFileFilterDto>>> SearchEmployeeFile(HrEmployeeFileFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                List<HrEmployeeFileFilterDto> result = new List<HrEmployeeFileFilterDto>();
                filter.DeptId ??= 0;
                filter.Location ??= 0;
                filter.BranchId ??= 0;
                filter.Status ??= 0;
                filter.JobCatagoriesId ??= 0;
                filter.JobType ??= 0;
                filter.NationalityId ??= 0;
                filter.SponsorsID ??= 0;
                var BranchesList = session.Branches.Split(',');

                var items = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.IsDeleted == false && e.Isdel == false
               && BranchesList.Contains(e.BranchId.ToString())
               && (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower()) || e.EmpName2.ToLower().Contains(filter.EmpName.ToLower()))
               && (string.IsNullOrEmpty(filter.EmpCode) || e.EmpId == filter.EmpCode)
               && (filter.JobType == 0 || filter.JobType == e.JobType)
               && (filter.JobCatagoriesId == 0 || filter.JobCatagoriesId == e.JobCatagoriesId)
               && (filter.Status == 0 || filter.Status == e.StatusId)
               && (filter.NationalityId == 0 || filter.NationalityId == e.NationalityId)
               && (filter.DeptId == 0 || filter.DeptId == e.DeptId)
               && (string.IsNullOrEmpty(filter.IdNo) || e.IdNo == filter.IdNo)
               && (string.IsNullOrEmpty(filter.PassId) || e.PassportNo == filter.PassId)
               && (string.IsNullOrEmpty(filter.EntryNo) || e.EntryNo == filter.EntryNo)
               && (filter.Location == 0 || filter.Location == e.Location)
               && (filter.SponsorsID == 0 || filter.SponsorsID == e.SponsorsId)
                );
                if (items != null)
                {
                    if (items.Count() > 0)
                    {
                        var res = items.AsQueryable();
                        if (filter.BranchId > 0)
                        {
                            res = res.Where(c => c.BranchId != null && c.BranchId == filter.BranchId);
                        }
                        if (res.Count() > 0)
                        {
                            foreach (var item in res)
                            {
                                var singleRecord = new HrEmployeeFileFilterDto
                                {
                                    Id = item.Id,
                                    EmpCode = item.EmpId,
                                    EmpName = item.EmpName ?? "",
                                    EmpName2 = item.EmpName2 ?? "",
                                    IdNo = item.IdNo,
                                    Endofcontract = item.ContractExpiryDate,
                                    DeptName = (session.Language == 1) ? item.DepName : item.DepName2,
                                    CatName = (session.Language == 1) ? item.CatName : item.CatName2,
                                    BranchName = (session.Language == 1) ? item.BraName : item.BraName2,
                                    StatusName = (session.Language == 1) ? item.StatusName : item.StatusName2,
                                    LocationName = (session.Language == 1) ? item.LocationName : item.LocationName2,
                                };
                                result.Add(singleRecord);
                            }
                            return await Result<List<HrEmployeeFileFilterDto>>.SuccessAsync(result);

                        }
                        return await Result<List<HrEmployeeFileFilterDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
                    }
                    return await Result<List<HrEmployeeFileFilterDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));

                }

                return await Result<List<HrEmployeeFileFilterDto>>.FailAsync("");
            }
            catch (Exception ex)
            {
                return await Result<List<HrEmployeeFileFilterDto>>.FailAsync(ex.Message);

            }
        }
    

    public async Task<IResult<List<HREmpIDExpireReportFilterDto>>> SearchEmpIDExpireReport(HREmpIDExpireReportFilterDto filter, CancellationToken cancellationToken = default)
    {
      try
      {
        var BranchesList = session.Branches.Split(',');

        List<HREmpIDExpireReportFilterDto> resultList = new List<HREmpIDExpireReportFilterDto>();
        var items = await hrRepositoryManager.HrEmployeeRepository.GetAll(e => e.IsDeleted == false && e.StatusId == 1 && e.Isdel == false && e.IdExpireDate != null && e.IdExpireDate != "" &&
        BranchesList.Contains(e.BranchId.ToString()) &&
        session.FacilityId == session.FacilityId &&
        (string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpId) &&
        (string.IsNullOrEmpty(filter.EmpName) || (e.EmpName != null && e.EmpName.ToLower().Contains(filter.EmpName)) || (e.EmpName2 != null && e.EmpName2.ToLower().Contains(filter.EmpName))) &&
        (filter.LocationId == 0 || filter.LocationId == null || filter.LocationId == e.Location) &&
        (filter.NationalityId == 0 || filter.NationalityId == null || filter.NationalityId == e.NationalityId)


        );
        if (items != null)
        {
          if (items.Count() > 0)
          {

            var res = items.AsQueryable();
            if (filter.BranchId != null && filter.BranchId > 0)
            {
              res = res.Where(c => c.BranchId != null && c.BranchId == filter.BranchId);
            }

            if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
            {
              res = res.Where(c => (c.IdExpireDate != null && DateHelper.StringToDate(c.IdExpireDate) >= DateHelper.StringToDate(filter.FromDate) && DateHelper.StringToDate(c.IdExpireDate) <= DateHelper.StringToDate(filter.ToDate)));
            }
            if (res.Count() >= 0)
            {
              foreach (var item in res)
              {
                int? remainingDays = 0;
                var isHijri = Bahsas.IsHijri(item.IdExpireDate, session);
                if (isHijri)
                {
                  var getEqualDate = Bahsas.HijriToGreg(item.IdExpireDate);
                  remainingDays = (DateHelper.StringToDate(getEqualDate) - DateTime.Now).Days;
                }
                else
                {
                  remainingDays = (DateHelper.StringToDate(item.IdExpireDate) - DateTime.Now).Days;
                }

                var newItem = new HREmpIDExpireReportFilterDto
                {
                  EmpCode = item.EmpId,
                  EmpName = item.EmpName,
                  IdNo = item.IdNo,
                  IDExpireDate = item.IdExpireDate,
                  RemainingDays = remainingDays,

                };
                resultList.Add(newItem);
              }
            }
            if (resultList.Count > 0)
              return await Result<List<HREmpIDExpireReportFilterDto>>.SuccessAsync(resultList);
            return await Result<List<HREmpIDExpireReportFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

          }
          return await Result<List<HREmpIDExpireReportFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

        }
        return await Result<List<HREmpIDExpireReportFilterDto>>.FailAsync("items error");

      }
      catch (Exception ex)
      {
        return await Result<List<HREmpIDExpireReportFilterDto>>.FailAsync(ex.Message);
      }
    }

    public async Task<IResult<List<RPPassportFilterDto>>> SearchRPPassport(RPPassportFilterDto filter, CancellationToken cancellationToken = default)
    {
      List<RPPassportFilterDto> results = new List<RPPassportFilterDto>();
      try
      {
        var BranchesList = session.Branches.Split(',');
        var employees = await hrRepositoryManager.HrEmployeeRepository.GetAll(e => e.IsDeleted == false && e.StatusId == 1 && e.PassExpireDate != null && e.PassExpireDate != "" && BranchesList.Contains(e.BranchId.ToString()));
        var filteredEmployees = employees
            .Where(e =>
                (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower())) &&
                (string.IsNullOrEmpty(filter.empCode) || e.EmpId == filter.empCode) &&
                (string.IsNullOrEmpty(filter.From) || string.IsNullOrEmpty(filter.To) || (DateHelper.StringToDate(e.PassExpireDate) >= DateHelper.StringToDate(filter.From) && DateHelper.StringToDate(e.PassExpireDate) <= DateHelper.StringToDate(filter.To))) &&
                (filter.BranchId == 0 || filter.BranchId == null || e.BranchId == filter.BranchId) &&
                (filter.Location == 0 || filter.Location == null || e.Location == filter.Location)

            );
        foreach (var item in filteredEmployees)
        {
          var remainingDays = (DateHelper.StringToDate(item.PassExpireDate) - DateTime.Now).Days;

          var employeeDto = new RPPassportFilterDto
          {
            empCode = item.EmpId,
            EmpName = item.EmpName ?? item.EmpName2,
            PassExpireDate = item.PassExpireDate,
            RemainingDays = remainingDays,
          };

          results.Add(employeeDto);
        }
        if (results.Count > 0) return await Result<List<RPPassportFilterDto>>.SuccessAsync(results, "");
        return await Result<List<RPPassportFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));


      }
      catch (Exception ex)
      {
        return await Result<List<RPPassportFilterDto>>.FailAsync(ex.Message);
      }
    }

    public async Task<IResult<List<HrRPContractFilterDto>>> SearchRPContract(HrRPContractFilterDto filter, CancellationToken cancellationToken = default)
    {
      try
      {

        var BranchesList = session.Branches.Split(',');
        List<HrRPContractFilterDto> resultList = new List<HrRPContractFilterDto>();
        var items = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.IsDeleted == false && BranchesList.Contains(e.BranchId.ToString()) && e.StatusId == 1 && e.ContractExpiryDate != null &&
        e.ContractExpiryDate != "" &&
        (string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpId) &&
        (string.IsNullOrEmpty(filter.DOAppointment) || filter.DOAppointment == e.Doappointment) &&
        (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName) || e.EmpName2.ToLower().Contains(filter.EmpName)) &&
        (filter.LocationId == 0 || filter.LocationId == null || filter.LocationId == e.Location) &&
        (filter.DepartmentId == 0 || filter.DepartmentId == null || filter.DepartmentId == e.DeptId) &&
        (filter.NationalityId == 0 || filter.NationalityId == null || filter.NationalityId == e.NationalityId) &&
        (filter.JobCategory == 0 || filter.JobCategory == null || filter.JobCategory == e.JobCatagoriesId) &&
        (filter.ContractTypeID == 0 || filter.ContractTypeID == null || filter.ContractTypeID == e.ContractTypeId)
        );
        if (items != null)
        {
          if (items.Count() > 0)
          {

            var res = items.AsQueryable();

            if (filter.BranchId != null && filter.BranchId > 0)
            {
              res = res.Where(c => c.BranchId != null && c.BranchId == filter.BranchId);
            }
            if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
            {
              res = res.Where(c => (c.ContractExpiryDate != null && DateHelper.StringToDate(c.ContractExpiryDate) >= DateHelper.StringToDate(filter.FromDate) && DateHelper.StringToDate(c.ContractExpiryDate) <= DateHelper.StringToDate(filter.ToDate)));
            }
            if (res.Any())
            {
              foreach (var item in res)
              {
                decimal TotalAllowance = 0;
                decimal TotalDeduction = 0;
                var getTotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(item.Id);
                if (getTotalAllowance != null) TotalAllowance = getTotalAllowance;

                var getTotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalDeduction(item.Id);
                if (getTotalDeduction != null) TotalDeduction = getTotalDeduction;
                var newItem = new HrRPContractFilterDto
                {
                  EmpCode = item.EmpId,
                  EmpName = item.EmpName,
                  IdNo = item.IdNo,
                  DOAppointment = item.Doappointment,
                  ContractExpiryDate = item.ContractExpiryDate,
                  RemainingDays = (DateHelper.StringToDate(item.ContractExpiryDate) - DateTime.Now).Days,
                  NationalityName = item.NationalityName,
                  BranchName = item.BraName,
                  DepartmentName = item.DepName,
                  LocationName = item.LocationName,
                  Salary = item.Salary,
                  NetSalary = item.Salary + TotalAllowance - TotalDeduction,
                  Deduction = TotalDeduction,
                  Allowance = TotalAllowance
                };
                resultList.Add(newItem);
              }
              if (resultList.Count > 0) return await Result<List<HrRPContractFilterDto>>.SuccessAsync(resultList);
              return await Result<List<HrRPContractFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

            }
            return await Result<List<HrRPContractFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

          }
          return await Result<List<HrRPContractFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
        }
        return await Result<List<HrRPContractFilterDto>>.FailAsync("items error");
      }
      catch (Exception ex)
      {
        return await Result<List<HrRPContractFilterDto>>.FailAsync(ex.Message);
      }
    }

    public async Task<IResult<List<RPMedicalInsuranceFilterDto>>> SearchRPMedicalInsurance(RPMedicalInsuranceFilterDto filter, CancellationToken cancellationToken = default)
    {
      List<RPMedicalInsuranceFilterDto> results = new List<RPMedicalInsuranceFilterDto>();
      try
      {
        var employees = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.IsDeleted == false && e.StatusId == 1 && e.InsuranceDateValidity != null && e.InsuranceDateValidity != "");
        var filteredEmployees = employees
            .Where(e =>
                (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower())) &&
                (string.IsNullOrEmpty(filter.empCode) || e.EmpId == filter.empCode) &&
                (string.IsNullOrEmpty(filter.From) || string.IsNullOrEmpty(filter.To) || (DateHelper.StringToDate(e.InsuranceDateValidity) >= DateHelper.StringToDate(filter.From) && DateHelper.StringToDate(e.InsuranceDateValidity) <= DateHelper.StringToDate(filter.To))) &&
                (filter.BranchId == 0 || filter.BranchId == null || e.BranchId == filter.BranchId) &&
                (filter.Location == 0 || filter.Location == null || e.Location == filter.Location)

            );
        foreach (var item in filteredEmployees)
        {
          var remainingDays = (DateHelper.StringToDate(item.InsuranceDateValidity) - DateTime.Now).Days;

          var employeeDto = new RPMedicalInsuranceFilterDto
          {
            empCode = item.EmpId,
            EmpName = item.EmpName ?? item.EmpName2,
            InuranceExpireDate = item.InsuranceDateValidity,
            RemainingDays = remainingDays,
          };

          results.Add(employeeDto);
        }
        if (results.Count > 0) return await Result<List<RPMedicalInsuranceFilterDto>>.SuccessAsync(results, "");
        return await Result<List<RPMedicalInsuranceFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));


      }
      catch (Exception ex)
      {
        return await Result<List<RPMedicalInsuranceFilterDto>>.FailAsync(ex.Message);
      }
    }

    public async Task<IResult<List<DOAppointmentFilterDto>>> SearchRPDOAppointement(DOAppointmentFilterDto filter, CancellationToken cancellationToken = default)
    {
      List<DOAppointmentFilterDto> results = new List<DOAppointmentFilterDto>();
      try
      {
        var BranchesList = session.Branches.Split(',');

        var employees = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.IsDeleted == false && e.Doappointment != null && e.Doappointment != "" && e.Isdel == false && e.FacilityId == session.FacilityId && BranchesList.Contains(e.BranchId.ToString()));
        var filteredEmployees = employees
            .Where(e =>
                (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower())) &&
                (string.IsNullOrEmpty(filter.empCode) || e.EmpId == filter.empCode) &&
                (string.IsNullOrEmpty(filter.From) || string.IsNullOrEmpty(filter.To) || (DateHelper.StringToDate(e.Doappointment) >= DateHelper.StringToDate(filter.From) && DateHelper.StringToDate(e.Doappointment) <= DateHelper.StringToDate(filter.To))) &&
                (filter.BranchId == 0 || filter.BranchId == null || e.BranchId == filter.BranchId) &&
                (filter.Location == 0 || filter.Location == null || e.Location == filter.Location) &&
                (filter.NationalityId == 0 || filter.NationalityId == null || e.NationalityId == filter.NationalityId) &&
                (filter.dept == 0 || filter.dept == null || e.DeptId == filter.dept)

            );
        foreach (var item in filteredEmployees)
        {

          var employeeDto = new DOAppointmentFilterDto
          {
            empCode = item.EmpId,
            EmpName = item.EmpName ?? item.EmpName2,
            DoAppointment = item.Doappointment,
            BranchName = session.Language == 1 ? item.BraName : item.BraName2,
            LocationName = session.Language == 1 ? item.LocationName : item.LocationName2,
            DeptName = session.Language == 1 ? item.DepName : item.DepName2,
            JobName = session.Language == 1 ? item.CatName : item.CatName2,
            Nationality = session.Language == 1 ? item.NationalityName : item.NationalityName2,

          };

          results.Add(employeeDto);
        }
        if (results.Count > 0) return await Result<List<DOAppointmentFilterDto>>.SuccessAsync(results, "");
        return await Result<List<DOAppointmentFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));


      }
      catch (Exception ex)
      {
        return await Result<List<DOAppointmentFilterDto>>.FailAsync(ex.Message);
      }
    }

    public async Task<IResult<List<RPAttendFilterDto>>> SearchRPAttend(RPAttendFilterDto filter, CancellationToken cancellationToken = default)
    {
      List<RPAttendFilterDto> results = new List<RPAttendFilterDto>();
      try
      {
        var BranchesList = session.Branches.Split(',');

        var employees = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.IsDeleted == false && e.Isdel == false && e.StatusId == 1 && e.ExcludeAttend == true && e.FacilityId == session.FacilityId);
        var filteredEmployees = employees
            .Where(e =>
                (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower())) &&
                (string.IsNullOrEmpty(filter.empCode) || e.EmpId == filter.empCode)
  );
        foreach (var item in filteredEmployees)
        {

          var employeeDto = new RPAttendFilterDto
          {
            empCode = item.EmpId,
            EmpName = item.EmpName ?? item.EmpName2,
            Id = item.Id
          };

          results.Add(employeeDto);
        }
        if (results.Count > 0) return await Result<List<RPAttendFilterDto>>.SuccessAsync(results, "");
        return await Result<List<RPAttendFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));


      }
      catch (Exception ex)
      {
        return await Result<List<RPAttendFilterDto>>.FailAsync(ex.Message);
      }
    }

    public async Task<IResult<List<RPBankFilterDto>>> SearchRPBank(RPBankFilterDto filter, CancellationToken cancellationToken = default)
    {
      List<RPBankFilterDto> results = new List<RPBankFilterDto>();
      try
      {
        var BranchesList = session.Branches.Split(',');

        var employees = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.IsDeleted == false && e.StatusId == 1 && e.Isdel == false && BranchesList.Contains(e.BranchId.ToString()));
        var filteredEmployees = employees
            .Where(e =>
                (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower())) &&
                (string.IsNullOrEmpty(filter.empCode) || e.EmpId == filter.empCode) &&
                (filter.Bank == null || filter.Bank == 0 || filter.Bank == e.BankId) &&
                (filter.Branch == null || filter.Branch == 0 || filter.Branch == e.BranchId)
  );
        foreach (var item in filteredEmployees)
        {

          var employeeDto = new RPBankFilterDto
          {
            empCode = item.EmpId,
            EmpName = item.EmpName ?? item.EmpName2,
            IBan = item.Iban,
            BankName = item.BankName
          };

          results.Add(employeeDto);
        }
        if (results.Count > 0) return await Result<List<RPBankFilterDto>>.SuccessAsync(results, "");
        return await Result<List<RPBankFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));


      }
      catch (Exception ex)
      {
        return await Result<List<RPBankFilterDto>>.FailAsync(ex.Message);
      }
    }

    public async Task<IResult<List<HrStaffSalariesAllowancesDeductionsFilterDto>>> SearchHrStaffSalariesAllowancesDeductions(HrStaffSalariesAllowancesDeductionsFilterDto filter, CancellationToken cancellationToken = default)
    {
      try
      {
        var BranchesList = session.Branches.Split(',');
        List<HrStaffSalariesAllowancesDeductionsFilterDto> resultList = new List<HrStaffSalariesAllowancesDeductionsFilterDto>();
        var items = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e => e.IsDeleted == false && e.Isdel == false && BranchesList.Contains(e.BranchId.ToString()) &&
        (string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpId) &&
        (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName) || e.EmpName2.ToLower().Contains(filter.EmpName)) &&
        (filter.LocationId == 0 || filter.LocationId == null || filter.LocationId == e.Location) &&
        (filter.Status == 0 || filter.Status == null || filter.Status == e.StatusId) &&
        (filter.DepartmentId == 0 || filter.DepartmentId == null || filter.DepartmentId == e.DeptId) &&
        (filter.NationalityId == 0 || filter.NationalityId == null || filter.NationalityId == e.NationalityId) &&
        (filter.JobCategory == 0 || filter.JobCategory == null || filter.JobCategory == e.JobCatagoriesId) &&
        (filter.JobType == null || filter.JobType == 0 || filter.JobType == e.JobType) &&
        (filter.SalaryGroup == null || filter.SalaryGroup == 0 || filter.SalaryGroup == e.SalaryGroupId) &&
        (string.IsNullOrEmpty(filter.IdNo) || e.IdNo == filter.IdNo) &&
        (string.IsNullOrEmpty(filter.PassId) || e.PassportNo == filter.PassId) &&
        (string.IsNullOrEmpty(filter.EntryNo) || e.EntryNo == filter.EntryNo)
        );
        if (items != null)
        {
          if (items.Count() > 0)
          {

            var res = items.AsQueryable();

            if (filter.BranchId != null && filter.BranchId > 0)
            {
              res = res.Where(c => c.BranchId != null && c.BranchId == filter.BranchId);
            }

            if (res.Any())
            {
              foreach (var item in res)
              {
                decimal TotalAllowance = 0;
                decimal TotalDeduction = 0;
                var getTotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(item.Id);
                if (getTotalAllowance != null) TotalAllowance = getTotalAllowance;

                var getTotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalDeduction(item.Id);
                if (getTotalDeduction != null) TotalDeduction = getTotalDeduction;
                var newItem = new HrStaffSalariesAllowancesDeductionsFilterDto
                {
                  EmpCode = item.EmpId,
                  EmpName = (session.Language == 1) ? item.EmpName : item.EmpName2,
                  NationalityName = (session.Language == 1) ? item.NationalityName : item.NationalityName2,
                  BranchName = (session.Language == 1) ? item.BraName : item.BraName2,
                  DepartmentName = (session.Language == 1) ? item.DepName : item.DepName2,
                  LocationName = (session.Language == 1) ? item.LocationName : item.LocationName2,
                  Salary = item.Salary,
                  NetSalary = (item.Salary + TotalAllowance - TotalDeduction),
                  Deduction = TotalDeduction,
                  Allowance = TotalAllowance,
                  CatName = (session.Language == 1) ? item.CatName : item.CatName2
                };
                resultList.Add(newItem);
              }
              if (resultList.Count > 0) return await Result<List<HrStaffSalariesAllowancesDeductionsFilterDto>>.SuccessAsync(resultList);
              return await Result<List<HrStaffSalariesAllowancesDeductionsFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

            }
            return await Result<List<HrStaffSalariesAllowancesDeductionsFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

          }
          return await Result<List<HrStaffSalariesAllowancesDeductionsFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
        }
        return await Result<List<HrStaffSalariesAllowancesDeductionsFilterDto>>.FailAsync("items error");
      }
      catch (Exception ex)
      {
        return await Result<List<HrStaffSalariesAllowancesDeductionsFilterDto>>.FailAsync(ex.Message);
      }
    }
  }

}
