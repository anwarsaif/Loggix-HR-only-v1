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
    public class HrVacationsService : GenericQueryService<HrVacation, HrVacationsDto, HrVacationsVw>, IHrVacationsService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;

        public HrVacationsService(IQueryRepository<HrVacation> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            ILocalizationService localization,
            ISysConfigurationAppHelper sysConfigurationAppHelpe,
            IMapper mapper, ICurrentData session) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            this.localization = localization;
            _mapper = mapper;
            this.sysConfigurationAppHelper = sysConfigurationAppHelpe;
            this.session = session;
        }

        public async Task<IResult<HrVacationsDto>> Add(HrVacationsDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrVacationsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;

                var item = _mapper.Map<HrVacation>(entity);
                var newEntity = await hrRepositoryManager.HrVacationsRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrVacationsDto>(newEntity);


                return await Result<HrVacationsDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrVacationsDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }


        public async Task<IResult<HrVacationsDto>> AddVacations(HrVacationsDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrVacationsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                var vacationType = await hrRepositoryManager.HrVacationsTypeRepository.GetOne(i => i.IsDeleted == false
                    && i.VacationTypeId == entity.VacationTypeId && entity.VacationAccountDay >= i.VacationTypeMinmam && entity.VacationAccountDay <= i.VacationTypeMaxmam);
                if (vacationType == null)
                    return await Result<HrVacationsDto>.SuccessAsync(entity, "مدة الإجازة المطلوبة اكثر من مدة الاجازة المحددة لهذا النوع من الإجازات");

                //var getVacationTypeId = await hrRepositoryManager.HrVacationsTypeRepository.GetOne(X => X.ValidateBalance, i => i.VacationTypeId == entity.VacationTypeId && i.IsDeleted == false);
                if (vacationType.ValidateBalance == true && entity.VacationAccountDay > entity.Balance)
                    return await Result<HrVacationsDto>.FailAsync($"رصيد الاجازة لا يسمح");

                var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCode && e.IsDeleted == false);
                if (Employees == null)
                    return await Result<HrVacationsDto>.FailAsync("  الموظف غير موجود في قائمة الموظفين");
                else
                    entity.EmpId = Employees.Id;
                if (Employees.StatusId == 2)
                    return await Result<HrVacationsDto>.FailAsync($"{localization.GetHrResource("EmpNotActive")}");

                var checkAbsence = await hrRepositoryManager.HrAbsenceRepository.GetAll(x => x.IsDeleted == false && x.EmpId == Employees.Id);
                if (checkAbsence.Any())
                {
                    var filteredAbsence = checkAbsence.Where(x => DateHelper.StringToDate(x.AbsenceDate) >= DateHelper.StringToDate(entity.VacationSdate)
                        && DateHelper.StringToDate(x.AbsenceDate) <= DateHelper.StringToDate(entity.VacationEdate));
                    if (filteredAbsence.Any())
                    {
                        return await Result<HrVacationsDto>.FailAsync("تم تسجيل غياب لهذا الموظف سابقاً في نفس الفترة - لن تتمكن من تسجيل غياب وإجازة في نفس الوقت");
                    }
                }

                var GetSysPropertyValues = await mainRepositoryManager.SysPropertyValueRepository.GetOne(z => z.PropertyValue, e => e.FacilityId == session.FacilityId && e.PropertyId == 190);
                if (GetSysPropertyValues == "1")
                {
                    var GetVacationID = await hrRepositoryManager.HrDirectJobRepository.GetAll(z => z.VacationId, e => e.IsDeleted == false);
                    var CheckJoinWorkDate = await hrRepositoryManager.HrVacationsRepository.GetAll(i => i.EmpId == Employees.Id && i.IsDeleted == false && i.NeedJoinRequest == true
                        && i.VacationTypeId == entity.VacationTypeId && !GetVacationID.Contains(i.VacationId));
                    if (CheckJoinWorkDate.Any())
                    {
                        return await Result<HrVacationsDto>.FailAsync("لم تتم عملية الإضافة بسبب وجود اجازة للموظف تتطلب مباشرة عمل");
                    }
                }

                // Begin check if VacationsInSameDay2
                var CheckIsVacationsInSameDay2 = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == Employees.Id);
                var filterResult = CheckIsVacationsInSameDay2.Where(e => DateHelper.StringToDate(entity.VacationSdate) >= DateHelper.StringToDate(e.VacationSdate)
                    && DateHelper.StringToDate(entity.VacationSdate) <= DateHelper.StringToDate(e.VacationEdate));
                if (!filterResult.Any())
                {
                    await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                    var Vacations_Day_Type_ID = (entity.VacationsDayTypeId > 0) ? entity.VacationsDayTypeId : 1;
                    int TimeTableId = 0; int ShitId = 0;
                    var GetFromAttShiftEmployeeVW = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOneVw(x => x.IsDeleted == false && x.EmpId == Employees.Id);
                    if (GetFromAttShiftEmployeeVW != null)
                    {
                        TimeTableId = Convert.ToInt32(GetFromAttShiftEmployeeVW.TimeTableId);
                        ShitId = Convert.ToInt32(GetFromAttShiftEmployeeVW.ShitId);
                    }

                    var HrVacations = new HrVacation
                    {
                        EmpId = Employees.Id,
                        VacationSdate = entity.VacationSdate,
                        VacationEdate = entity.VacationEdate,
                        VacationTypeId = entity.VacationTypeId,
                        Note = entity.Note,
                        VacationAccountDay = entity.VacationAccountDay,
                        FinancelYear = (int?)session.FinyearGregorian,
                        IsSalary = entity.IsSalary,
                        NeedJoinRequest = entity.NeedJoinRequest,
                        StatusId = 4,
                        IsDeleted = false,
                        VacationsDayTypeId = Vacations_Day_Type_ID,
                        TransTypeId = 1,
                        LocationId = Employees.Location,
                        TimeTableId = TimeTableId,
                        ShiftId = ShitId
                    };
                    var newVacationEntity = await hrRepositoryManager.HrVacationsRepository.AddAndReturn(HrVacations);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    //  في حال كانت الإجازة مرضية يتم تنفيذ البروسيجر للمرضية
                    if (vacationType.CatId == 2)
                    {
                        string SickleavePolicy = await sysConfigurationAppHelper.GetValue(77);
                        if (SickleavePolicy == "1")
                        {
                            var ImplementStoredProcedure = await mainRepositoryManager.StoredProceduresRepository.HR_Sick_leave_Sp(Employees.Id, 2, newVacationEntity.VacationId);
                        }
                    }

                    /// اذا كانت الاجازة كحسميات
                    if (entity.ChkAddAsDeduction)
                    {
                        if (entity.DeductionType <= 0)
                            return await Result<HrVacationsDto>.FailAsync("يجب تحديد نوع الحسم ");
                        if (entity.DeductionAmount <= 0)
                            return await Result<HrVacationsDto>.FailAsync("يجب تحديد مبلغ الحسم ");

                        var newDeduction = new HrAllowanceDeduction
                        {
                            TypeId = 2,
                            AdId = entity.DeductionType,
                            Rate = 0,
                            Amount = entity.DeductionAmount,
                            CreatedBy = session.UserId,
                            EmpId = Employees.Id,
                            FixedOrTemporary = 2,
                            Note = entity.Note,
                            DueDate = entity.DueDate ?? DateTime.Now.ToString("yyyy/mm/dd", CultureInfo.InvariantCulture),
                        };
                        await hrRepositoryManager.HrAllowanceDeductionRepository.Add(newDeduction);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                    return await Result<HrVacationsDto>.SuccessAsync(_mapper.Map<HrVacationsDto>(newVacationEntity), localization.GetResource1("AddSuccess"));
                }
                else
                {
                    return await Result<HrVacationsDto>.FailAsync("لم تتم عملية الإضافة بسبب وجود اجازة بنفس التاريخ للموظف ");
                }
            }
            catch (Exception exc)
            {
                return await Result<HrVacationsDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }


        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrVacationsRepository.GetById(Id);
                if (item == null) return Result<HrVacationsDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                hrRepositoryManager.HrVacationsRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVacationsDto>.SuccessAsync(_mapper.Map<HrVacationsDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrVacationsDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrVacationsRepository.GetById(Id);
                if (item == null) return Result<HrVacationsDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                hrRepositoryManager.HrVacationsRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVacationsDto>.SuccessAsync(_mapper.Map<HrVacationsDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrVacationsDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrVacationsEditDto>> Update(HrVacationsEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrVacationsEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            var item = await hrRepositoryManager.HrVacationsRepository.GetById(entity.VacationId);

            if (item == null) return await Result<HrVacationsEditDto>.FailAsync($"--- there is no Data with this id: {entity.VacationId}---");

            _mapper.Map(entity, item);
            hrRepositoryManager.HrVacationsRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVacationsEditDto>.SuccessAsync(_mapper.Map<HrVacationsEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrVacationsEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<decimal> Vacation_Balance2_FN(string Curr_Date, long ID_Emp, int VacationTypeId)
        {
            var result = await hrRepositoryManager.HrVacationsRepository.Vacation_Balance2_FN(Curr_Date, ID_Emp, VacationTypeId);
            return result;
        }

        public async Task<decimal> Vacation_Balance_FN(string Curr_Date, long ID_Emp)
        {
            var result = await hrRepositoryManager.HrVacationsRepository.Vacation_Balance_FN(Curr_Date, ID_Emp);
            return result;
        }

        public async Task<IResult<HrVacationsEditDto>> EditVacations(HrVacationsEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrVacationsEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");
            try
            {
                var item = await hrRepositoryManager.HrVacationsRepository.GetOne(v => v.VacationId == entity.VacationId && v.IsDeleted == false);
                if (item == null) return await Result<HrVacationsEditDto>.FailAsync($"--- there is no Data with this id: {entity.VacationId}---");

                var CheckEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.Isdel == false);
                if (CheckEmpExist == null) return await Result<HrVacationsEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                var CheckIsVacationsInSameDay2 = await hrRepositoryManager.HrVacationsRepository.GetAll(x => x.IsDeleted == false && x.EmpId == item.EmpId);
                var filterResult = CheckIsVacationsInSameDay2.Where(e => DateHelper.StringToDate(entity.VacationSdate) >= DateHelper.StringToDate(e.VacationSdate)
                    && DateHelper.StringToDate(entity.VacationSdate) <= DateHelper.StringToDate(e.VacationEdate));
                if (filterResult.Count() <= 1)
                {
                    // here we will start edit vacation
                    item.VacationSdate = entity.VacationSdate;
                    item.VacationEdate = entity.VacationEdate;
                    item.VacationTypeId = entity.VacationTypeId;
                    item.Note = entity.Note ?? "";

                    //item.EmpId = CheckEmpExist.Id;
                    //item.VacationsDayTypeId = entity.VacationsDayType;

                    item.VacationAccountDay = entity.VacationAccountDay;
                    item.FinancelYear = session.FinyearGregorian;
                    item.IsSalary = entity.IsSalary;
                    item.NeedJoinRequest = entity.NeedJoinRequest;
                    item.StatusId = 4;
                    item.VacationRdate = entity.VacationRdate;

                    hrRepositoryManager.HrVacationsRepository.Update(item);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    return await Result<HrVacationsEditDto>.SuccessAsync(_mapper.Map<HrVacationsEditDto>(item), localization.GetResource1("UpdateSuccess"));
                }
                else
                {
                    return await Result<HrVacationsEditDto>.FailAsync("لم تتم عملية الحفظ بسبب وجود اجازة بنفس التاريخ للموظف ");
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrVacationsEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        private async Task<IResult<List<string>>> GetchildDepartment(long DeptId, CancellationToken cancellationToken = default)
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

        public async Task<IResult<List<HrVacationsFilterDto>>> Search(HrVacationsFilterDto filter, CancellationToken cancellationToken = default)
        {
            List<HrVacationsFilterDto> resultList = new();
            try
            {
                filter.VacationTypeId ??= 0; filter.DeptId ??= 0; filter.LocationId ??= 0; filter.BranchId ??= 0;
                filter.TransTypeId ??= 0; filter.ClearnceId ??= 0;
                var BranchesList = session.Branches.Split(',');

                var getFromClearnce = await hrRepositoryManager.HrClearanceRepository.GetAll(x => x.VacationId, X => X.IsDeleted == false);
                var deptList = new List<string>();
                if (filter.DeptId > 0)
                {
                    var getDeptList = await GetchildDepartment((long)filter.DeptId);
                    deptList = getDeptList.Data;
                }

                var items = await hrRepositoryManager.HrVacationsRepository.GetAllVw(e => e.IsDeleted == false
                && (filter.BranchId == 0 || filter.BranchId == e.BranchId)
                && BranchesList.Contains(e.BranchId.ToString())
                && (filter.VacationTypeId == 0 || filter.VacationTypeId == e.VacationTypeId)
                && (string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpCode)
                && (filter.DeptId == 0 || filter.DeptId == e.DeptId || deptList.Contains((e.DeptId ?? 0).ToString()))
                && (filter.LocationId == 0 || filter.LocationId == e.Location)
                && (
                        filter.ClearnceId == 0
                        || (filter.ClearnceId == 1 && getFromClearnce.Contains(e.VacationId))
                        || (filter.ClearnceId == 2 && !getFromClearnce.Contains(e.VacationId))
                   )
                && (filter.TransTypeId == 0 || filter.TransTypeId == e.TransTypeId)
                );
                if (items != null)
                {
                    if (items.Count() > 0)
                    {
                        var res = items.AsQueryable();

                        if (!string.IsNullOrEmpty(filter.StartDate) && !string.IsNullOrEmpty(filter.EndDate))
                        {
                            res = res.Where(c => (DateHelper.StringToDate(c.VacationSdate) >= DateHelper.StringToDate(filter.StartDate)
                                && DateHelper.StringToDate(c.VacationSdate) <= DateHelper.StringToDate(filter.EndDate))

                                || (DateHelper.StringToDate(c.VacationEdate) >= DateHelper.StringToDate(filter.StartDate)
                                && DateHelper.StringToDate(c.VacationEdate) <= DateHelper.StringToDate(filter.EndDate))
                             );
                        }

                        if (res.Count() <= 0) return await Result<List<HrVacationsFilterDto>>.SuccessAsync(new List<HrVacationsFilterDto>(), localization.GetResource1("NosearchResult"));

                        foreach (var item in res)
                        {
                            var newRecord = new HrVacationsFilterDto
                            {
                                EmpCode = item.EmpCode,
                                EmpName = session.Language == 2 ? item.EmpName2 : item.EmpName,
                                BraName = session.Language == 2 ? item.BraName2 : item.BraName,
                                LocationName = session.Language == 2 ? item.LocationName2 : item.LocationName2,
                                DeptName = session.Language == 2 ? item.DepName2 : item.DepName,
                                VacationTypeName = session.Language == 2 ? item.VacationTypeName2 : item.VacationTypeName,
                                StartDate = item.VacationSdate,
                                EndDate = item.VacationEdate,
                                VacationAccountDay = item.VacationAccountDay,
                                IsSalary = item.IsSalary,
                                Note = item.Note,
                                VacationId = item.VacationId,
                                VacationTypeId = item.VacationTypeId,
                                ApplicationId = item.ApplicationId,

                            };
                            resultList.Add(newRecord);
                        }
                        return await Result<List<HrVacationsFilterDto>>.SuccessAsync(resultList, "");
                    }
                    return await Result<List<HrVacationsFilterDto>>.SuccessAsync(new List<HrVacationsFilterDto>(), localization.GetResource1("NosearchResult"));
                }
                return await Result<List<HrVacationsFilterDto>>.FailAsync("err");
            }
            catch (Exception ex)
            {
                return await Result<List<HrVacationsFilterDto>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<List<HrVacationsFilterDto>>> VacationReportSearch(HrVacationsFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                List<HrVacationsFilterDto> result = new();
                var BranchesList = session.Branches.Split(',');

                filter.BranchId ??= 0; filter.DeptId ??= 0; filter.LocationId ??= 0; filter.VacationTypeId ??= 0;
                filter.ChkGroupByEmpAndVacation ??= false;

                var res = await hrRepositoryManager.HrVacationsRepository.GetAllVw(x => x.IsDeleted == false
                    && ((filter.BranchId == 0 && BranchesList.Contains(x.BranchId.ToString())) || (filter.BranchId > 0 && x.BranchId == filter.BranchId))
                    && (string.IsNullOrEmpty(filter.EmpCode) || x.EmpCode == filter.EmpCode)
                    && (filter.VacationTypeId == 0 || filter.VacationTypeId == x.VacationTypeId)
                    && (filter.LocationId == 0 || filter.LocationId == x.Location)
                );

                var query = res.AsQueryable();

                if (query.Any())
                {
                    if (!string.IsNullOrEmpty(filter.StartDate) && !string.IsNullOrEmpty(filter.EndDate))
                    {
                        var startDate = DateHelper.StringToDate(filter.StartDate);
                        var endDate = DateHelper.StringToDate(filter.EndDate);
                        query = query.Where(r => r.VacationSdate != null && r.VacationEdate != null &&
                            (DateHelper.StringToDate(r.VacationSdate) >= startDate && DateHelper.StringToDate(r.VacationSdate) <= endDate) &&
                            (DateHelper.StringToDate(r.VacationEdate) >= startDate && DateHelper.StringToDate(r.VacationEdate) <= endDate));
                    }

                    if (filter.ChkGroupByEmpAndVacation == true)
                    {
                        if (filter.DeptId > 0)
                        {
                            var deptList = await GetchildDepartment((long)filter.DeptId);
                            query = query.Where(x => x.DeptId == filter.DeptId || deptList.Data.Contains((x.DeptId ?? 0).ToString()));
                        }

                        var groupedQuery = query
                            .GroupBy(v => new { v.EmpId, v.VacationTypeId })
                            .Select(g => new
                            {
                                Vacation = g.OrderBy(v => DateHelper.StringToDate(v.VacationSdate)).FirstOrDefault(),
                                StartDate = g.Min(v => DateHelper.StringToDate(v.VacationSdate)),
                                EndDate = g.Max(v => DateHelper.StringToDate(v.VacationEdate)),
                                TotalVacationAccountDay = g.Sum(v => v.VacationAccountDay)
                            }).ToList();

                        result = groupedQuery.Select(x => new HrVacationsFilterDto
                        {
                            VacationId = x.Vacation.VacationId,
                            EmpCode = x.Vacation.EmpCode,
                            EmpName = x.Vacation.EmpName,
                            EmpName2 = x.Vacation.EmpName2,
                            BraName = x.Vacation.BraName,
                            BraName2 = x.Vacation.BraName2,
                            DeptName = x.Vacation.DepName,
                            DeptName2 = x.Vacation.DepName2,
                            LocationName = x.Vacation.LocationName,
                            LocationName2 = x.Vacation.LocationName2,
                            VacationTypeName = x.Vacation.VacationTypeName,
                            VacationTypeName2 = x.Vacation.VacationTypeName2,
                            StartDate = x.StartDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                            EndDate = x.EndDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                            VacationAccountDay = x.TotalVacationAccountDay
                        }).ToList();
                    }
                    else
                    {
                        if (filter.DeptId > 0)
                        {
                            query = query.Where(x => x.DeptId == filter.DeptId);
                        }

                        result = query.Select(item => new HrVacationsFilterDto
                        {
                            VacationId = item.VacationId,
                            EmpCode = item.EmpCode,
                            EmpName = item.EmpName,
                            EmpName2 = item.EmpName2,
                            BraName = item.BraName,
                            BraName2 = item.BraName2,
                            DeptName = item.DepName,
                            DeptName2 = item.DepName2,
                            LocationName = item.LocationName,
                            LocationName2 = item.LocationName2,
                            VacationTypeName = item.VacationTypeName,
                            VacationTypeName2 = item.VacationTypeName2,

                            StartDate = item.VacationSdate,
                            EndDate = item.VacationEdate,
                            VacationAccountDay = item.VacationAccountDay
                        }).ToList();
                    }
                }

                return await Result<List<HrVacationsFilterDto>>.SuccessAsync(result, "");
            }
            catch (Exception ex)
            {
                return await Result<List<HrVacationsFilterDto>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<List<HrRPVacationEmployeeFilterDto>>> HRRVacationEmployeeSearch(HrRPVacationEmployeeFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                List<HrRPVacationEmployeeFilterDto> result = new();
                var BranchesList = session.Branches.Split(',');
                filter.BranchId ??= 0; filter.DeptId ??= 0; filter.LocationId ??= 0; filter.VacationTypeId ??= 0;

                List<int> deptList = new();
                if (filter.DeptId > 0)
                {
                    var getDeptList = await GetchildDepartment((long)filter.DeptId);
                    if (getDeptList.Data.Any())
                        deptList = getDeptList.Data.Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToList();
                }

                var items = await hrRepositoryManager.HrVacationsRepository.GetAllVw(x => x.IsDeleted == false
                && ((filter.BranchId == 0 && BranchesList.Contains(x.BranchId.ToString())) || (filter.BranchId > 0 && x.BranchId == filter.BranchId))
                && (string.IsNullOrEmpty(filter.EmpCode) || x.EmpCode == filter.EmpCode)
                && (filter.VacationTypeId == 0 || filter.VacationTypeId == x.VacationTypeId)
                && (filter.LocationId == 0 || filter.LocationId == x.Location)
                && (filter.DeptId == 0 || filter.DeptId == x.DeptId || deptList.Contains(x.DeptId ?? 0))
                );

                if (items.Any())
                {
                    var res = items.OrderByDescending(x => x.EmpId).AsQueryable();

                    if (!string.IsNullOrEmpty(filter.StartDate) && !string.IsNullOrEmpty(filter.EndDate))
                    {
                        DateTime startDate = DateHelper.StringToDate(filter.StartDate);
                        DateTime endDate = DateHelper.StringToDate(filter.EndDate);

                        res = res.Where(r => r.VacationSdate != null && r.VacationEdate != null &&
                       ((DateHelper.StringToDate(r.VacationSdate) >= startDate && DateHelper.StringToDate(r.VacationSdate) <= endDate)
                       ||
                       (DateHelper.StringToDate(r.VacationEdate) >= startDate && DateHelper.StringToDate(r.VacationEdate) <= endDate)));
                    }

                    foreach (var item in res)
                    {
                        var newItem = new HrRPVacationEmployeeFilterDto
                        {
                            VacationId = item.VacationId,
                            EmpCode = item.EmpCode,
                            EmpName = item.EmpName,
                            EmpName2 = item.EmpName2,
                            BraName = item.BraName,
                            BraName2 = item.BraName2,
                            DeptName = item.DepName,
                            DeptName2 = item.DepName2,
                            LocationName = item.LocationName,
                            LocationName2 = item.LocationName2,
                            VacationTypeName = item.VacationTypeName,
                            VacationTypeName2 = item.VacationTypeName2,
                            StartDate = item.VacationSdate,
                            EndDate = item.VacationEdate,
                            VacationAccountDay = item.VacationAccountDay,
                            IsSalary = item.IsSalary,
                            VacationAlternetiveEmpNo = item.AlternativeEmpCode,
                            VacationAlternetiveEmpName = item.AlternativeEmpName,
                            Note = item.Note,
                        };
                        result.Add(newItem);
                    }
                    return await Result<List<HrRPVacationEmployeeFilterDto>>.SuccessAsync(result, "");
                }
                return await Result<List<HrRPVacationEmployeeFilterDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception ex)
            {
                return await Result<List<HrRPVacationEmployeeFilterDto>>.FailAsync(ex.Message);
            }
        }

        public async Task<decimal> GetCountDays(string StartDate, string EndDate, int VacationTypeId)
        {
            var result = await hrRepositoryManager.HrVacationsRepository.GetCountDays(StartDate, EndDate, VacationTypeId);
            return result;
        }




        public async Task<PaginatedResult<IEnumerable<HrVacationsFilterDto>>> GetVacationReportPaginationGrouped(
            HrVacationsFilterDto filter, int take, long? lastSeenId = null)
        {
            try
            {
                var branchesList = session.Branches.Split(',');

                filter.BranchId ??= 0;
                filter.DeptId ??= 0;
                filter.LocationId ??= 0;
                filter.VacationTypeId ??= 0;

                // جلب الإدارات الفرعية في حال تم اختيار إدارة
                List<string> childDepartments = new();
                if (filter.DeptId != 0)
                {
                    var departmentes = await GetchildDepartment(filter.DeptId.Value);
                    if (departmentes?.Data != null)
                        childDepartments = departmentes.Data;
                }

                // فلترة بالتواريخ
                List<DateCondition>? dateConditions = null;
                if (!string.IsNullOrEmpty(filter.StartDate) && !string.IsNullOrEmpty(filter.EndDate))
                {
                    dateConditions = new List<DateCondition>
            {
                new DateCondition
                {
                    DatePropertyName = "VacationSdate",
                    ComparisonOperator = ComparisonOperator.Between,
                    StartDateString = filter.StartDate,
                    EndDateString = filter.EndDate
                },
                new DateCondition
                {
                    DatePropertyName = "VacationEdate",
                    ComparisonOperator = ComparisonOperator.Between,
                    StartDateString = filter.StartDate,
                    EndDateString = filter.EndDate
                }
            };
                }

                // إذا لم يُطلب التجميع: ارجاع بيانات بدون GroupBy عبر الاستعلام على الـ View
                if (filter.ChkGroupByEmpAndVacation != true)
                {
                    var items = await GetAllWithPaginationVW<long>(
                        selector: v => v.VacationId,
                        expression: v =>
                            v.IsDeleted == false &&
                            ((filter.BranchId == 0 && branchesList.Contains(v.BranchId.ToString())) ||
                             (filter.BranchId > 0 && v.BranchId == filter.BranchId)) &&
                            (string.IsNullOrEmpty(filter.EmpCode) || v.EmpCode == filter.EmpCode) &&
                            (filter.VacationTypeId == 0 || filter.VacationTypeId == v.VacationTypeId) &&
                            (filter.LocationId == 0 || filter.LocationId == v.Location) &&
                            (filter.DeptId == 0 || childDepartments.Contains((v.DeptId ?? 0).ToString())),
                        take: take,
                        lastSeenId: lastSeenId,
                        dateConditions: dateConditions
                    );

                    if (!items.Succeeded)
                    {
                        return new PaginatedResult<IEnumerable<HrVacationsFilterDto>>
                        {
                            Succeeded = false,
                            Status = items.Status
                        };
                    }

                    var result = (items.Data ?? Array.Empty<HrVacationsVw>()).Select(item => new HrVacationsFilterDto
                    {
                        VacationId = item.VacationId,
                        EmpCode = item.EmpCode,
                        EmpName = item.EmpName,
                        EmpName2 = item.EmpName2,
                        BraName = item.BraName,
                        BraName2 = item.BraName2,
                        DeptName = item.DepName,
                        DeptName2 = item.DepName2,
                        LocationName = item.LocationName,
                        LocationName2 = item.LocationName2,
                        VacationTypeName = item.VacationTypeName,
                        VacationTypeName2 = item.VacationTypeName2,
                        StartDate = item.VacationSdate,
                        EndDate = item.VacationEdate,
                        VacationAccountDay = item.VacationAccountDay
                    }).ToList();

                    return new PaginatedResult<IEnumerable<HrVacationsFilterDto>>
                    {
                        Succeeded = true,
                        Data = result,
                        Status = new Message { code = 200, message = "Records retrieved successfully" },
                        PaginationInfo = items.PaginationInfo
                    };
                }

                // استدعاء الدالة العامة مع groupedQuery
                var paged = await GetAllWithCursorPaginationAsync<HrVacationsVw, long, HrVacationsFilterDto>(
                    selector: v => v.VacationId,
                    expression: v =>
                        v.IsDeleted == false &&
                        ((filter.BranchId == 0 && branchesList.Contains(v.BranchId.ToString())) ||
                         (filter.BranchId > 0 && v.BranchId == filter.BranchId)) &&
                        (string.IsNullOrEmpty(filter.EmpCode) || v.EmpCode == filter.EmpCode) &&
                        (filter.VacationTypeId == 0 || filter.VacationTypeId == v.VacationTypeId) &&
                        (filter.LocationId == 0 || filter.LocationId == v.Location) &&
                        (filter.DeptId == 0 || childDepartments.Contains((v.DeptId ?? 0).ToString())),
                    take: take,
                    lastSeenId: lastSeenId,
                    dateConditions: dateConditions,
                    groupedQuery: (IQueryable<HrVacationsVw> q) =>
                        q.GroupBy(v => new { v.EmpId, v.VacationTypeId })
                         .Select(g => new HrVacationsFilterDto
                         {
                             VacationId = g.Min(v => v.VacationId),
                             EmpCode = g.First().EmpCode,
                             EmpName = g.First().EmpName,
                             EmpName2 = g.First().EmpName2,
                             BraName = g.First().BraName,
                             BraName2 = g.First().BraName2,
                             DeptName = g.First().DepName,
                             DeptName2 = g.First().DepName2,
                             LocationName = g.First().LocationName,
                             LocationName2 = g.First().LocationName2,
                             VacationTypeName = g.First().VacationTypeName,
                             VacationTypeName2 = g.First().VacationTypeName2,
                             StartDate = g.Min(v => DateHelper.StringToDate(v.VacationSdate))
                                         .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                             EndDate = g.Max(v => DateHelper.StringToDate(v.VacationEdate))
                                         .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                             VacationAccountDay = g.Sum(v => v.VacationAccountDay ?? 0)
                         })
                );

                return paged;
            }
            catch (Exception exc)
            {
                return new PaginatedResult<IEnumerable<HrVacationsFilterDto>>
                {
                    Succeeded = false,
                    Status = new Message { code = 500, message = exc.Message }
                };
            }
        }



    }
}