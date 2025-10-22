using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Logix.Application.Common;
using Logix.Application.DTOs.FXA;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;

namespace Logix.Application.Services.HR
{
    public class HrOverTimeMService : GenericQueryService<HrOverTimeM, HrOverTimeMDto, HrOverTimeMVw>, IHrOverTimeMService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly ISysConfigurationAppHelper sysConfiguration;
        private readonly IPMRepositoryManager pMRepositoryManager;


        public HrOverTimeMService(IQueryRepository<HrOverTimeM> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, ISysConfigurationAppHelper sysConfiguration, IPMRepositoryManager pMRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.sysConfiguration = sysConfiguration;
            this.pMRepositoryManager = pMRepositoryManager;
        }

        public async Task<IResult<HrOverTimeMDto>> Add(HrOverTimeMDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrOverTimeMDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrOverTimeMDto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));
            try
            {
                // check if Emp Is Exist
                var checkIfExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkIfExist == null) return await Result<HrOverTimeMDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var mappedEntity = _mapper.Map<HrOverTimeM>(entity);
                mappedEntity.EmpId = checkIfExist.Id;
                mappedEntity.CreatedBy = session.UserId;
                mappedEntity.CreatedOn = DateTime.Now;

                var newEntity = await hrRepositoryManager.HrOverTimeMRepository.AddAndReturn(mappedEntity);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrOverTimeMDto>(newEntity);

                return await Result<HrOverTimeMDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {
                return await Result<HrOverTimeMDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }
        public async Task<IResult<string>> Add(HrOverTimeMAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                decimal? TotalAllowance = 0;
                // check if Emp Is Exist
                var checkIfExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkIfExist == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var newItem = new HrOverTimeM();

                if (!(string.IsNullOrEmpty(entity.ProjectCode)))
                {
                    var GetProjectId = await pMRepositoryManager.PMProjectsRepository.GetPMProjectIdByCode(entity.ProjectCode, session.FacilityId);

                    if (GetProjectId == null)
                        return await Result<string>.FailAsync($"رقم المشروع غير موجود في قائمة المشاريع فضلاً تأكد من الرقم الصحيح");
                    newItem.ProjectId = GetProjectId.ProjectId;
                }
                else
                {
                    newItem.ProjectId = 0;
                }
                newItem.EmpId = checkIfExist.Id;
                newItem.CreatedBy = session.UserId;
                newItem.CreatedOn = DateTime.Now;
                newItem.RefranceId = entity.RefranceId ?? "0";
                newItem.DateFrom = entity.DateFrom;
                newItem.DateTo = entity.DateTo;
                newItem.DateTran = entity.DateTran;
                newItem.PaymentType = entity.PaymentType;
                newItem.Note = entity.Note;
                newItem.CntHoursTotal = entity.CntHoursTotal ?? 0;
                newItem.CntHoursDay = entity.CntHoursDay ?? 0;
                newItem.CntHoursMonth = entity.CntHoursMonth ?? 0;
                var newEntity = await hrRepositoryManager.HrOverTimeMRepository.AddAndReturn(newItem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.hrOverTimeDDtos.Count <= 0)
                {
                    return await Result<string>.FailAsync($"قم بإضافة ساعات الإضافي اولاً");

                }
                var getSysProperty = await sysConfiguration.GetValue(132, session.FacilityId);
                var getAllAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e => e.EmpId == checkIfExist.Id && e.IsDeleted == false && e.TypeId == 1 && e.FixedOrTemporary == 1);
                if (getAllAllowance != null)
                {
                    foreach (var allowanceitem in getAllAllowance)
                    {
                        TotalAllowance += (allowanceitem.Amount != null ? allowanceitem.Amount.Value : 0);
                    }


                }
                foreach (var singleItem in entity.hrOverTimeDDtos)
                {
                    if (!string.IsNullOrEmpty(getSysProperty))
                    {
                        if (singleItem.Hours > Convert.ToDecimal(getSysProperty))
                        {
                            return await Result<string>.FailAsync($"الساعات المدخله تجاوز الساعات المسموح بها");

                        }

                    }
                    decimal? total = singleItem.OverTimeHCost * singleItem.Hours * singleItem.Amount;
                    if (entity.Type == 3)
                    {
                        var totalSalary = checkIfExist.Salary + TotalAllowance;
                        var Salary = checkIfExist.Salary;
                        total = (singleItem.Hours * (totalSalary / 30 / checkIfExist.DailyWorkingHours)) + (singleItem.Hours / 2 * (Salary / 30 / checkIfExist.DailyWorkingHours));
                    }

                    var NEWTimeDDtos = new HrOverTimeD
                    {
                        IsDeleted = false,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IdM = newEntity.Id,
                        OverTimeHCost = singleItem.OverTimeHCost,
                        OverTimeTybe = singleItem.OverTimeTybe,
                        Hours = singleItem.Hours,
                        Amount = singleItem.Amount,
                        Total = Math.Round((decimal)total, 2),
                        Description = singleItem.Description,
                        CurrencyId = singleItem.CurrencyId,
                        OverTimeDate = singleItem.OverTimeDate,
                    };

                    await hrRepositoryManager.HrOverTimeDRepository.Add(NEWTimeDDtos);

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 101);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync("item added successfully", 200);
            }
            catch (Exception exc)
            {
                return await Result<string>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }



        /// <summary>
        /// this code to remove overtime from Main and its connected rows in HrOverTimeM Table
        /// </summary>
        /// <param name="Id">the id of row which we want delete it</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                // Begin of delete HrOverTimeM

                var HrOverTimeMItem = await hrRepositoryManager.HrOverTimeMRepository.GetById(Id);
                if (HrOverTimeMItem == null) return Result<HrOverTimeMDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                HrOverTimeMItem.ModifiedBy = session.UserId;
                HrOverTimeMItem.ModifiedOn = DateTime.Now;
                HrOverTimeMItem.IsDeleted = true;

                hrRepositoryManager.HrOverTimeMRepository.Update(HrOverTimeMItem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                // end of delete HrOverTimeM


                // Begin of delete HrOverTimeD
                var HrOverTimeDItem = await hrRepositoryManager.HrOverTimeDRepository.GetAll(x => x.IdM == Id);
                if (HrOverTimeDItem != null)
                {
                    foreach (var item in HrOverTimeDItem)
                    {
                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        item.IsDeleted = true;
                        hrRepositoryManager.HrOverTimeDRepository.Update(item);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                // end of delete HrOverTimeD


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrOverTimeMDto>.SuccessAsync(_mapper.Map<HrOverTimeMDto>(HrOverTimeMItem), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrOverTimeMDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                // Begin of delete HrOverTimeM

                var HrOverTimeMItem = await hrRepositoryManager.HrOverTimeMRepository.GetById(Id);
                if (HrOverTimeMItem == null) return Result<HrOverTimeMDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                HrOverTimeMItem.ModifiedBy = session.UserId;
                HrOverTimeMItem.ModifiedOn = DateTime.Now;
                HrOverTimeMItem.IsDeleted = true;

                hrRepositoryManager.HrOverTimeMRepository.Update(HrOverTimeMItem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                // end of delete HrOverTimeM


                // Begin of delete HrOverTimeD
                var HrOverTimeDItem = await hrRepositoryManager.HrOverTimeDRepository.GetAll(x => x.IdM == Id);
                if (HrOverTimeDItem != null)
                {
                    foreach (var item in HrOverTimeDItem)
                    {
                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        item.IsDeleted = true;
                        hrRepositoryManager.HrOverTimeDRepository.Update(item);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                // end of delete HrOverTimeD


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrOverTimeMDto>.SuccessAsync(_mapper.Map<HrOverTimeMDto>(HrOverTimeMItem), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrOverTimeMDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrOverTimeMEditDto>> Update(HrOverTimeMEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrOverTimeMEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrOverTimeMEditDto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrOverTimeMEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrOverTimeMRepository.GetById(entity.Id);

                if (item == null) return await Result<HrOverTimeMEditDto>.FailAsync("the Dependent Is Not Found");

                if (!(string.IsNullOrEmpty(entity.ProjectCode)))
                {
                    var GetProjectId = await pMRepositoryManager.PMProjectsRepository.GetPMProjectIdByCode(entity.ProjectCode, session.FacilityId);

                    if (GetProjectId == null)
                        return await Result<HrOverTimeMEditDto>.FailAsync($"رقم المشروع غير موجود في قائمة المشاريع فضلاً تأكد من الرقم الصحيح");
                    item.ProjectId = GetProjectId.ProjectId;
                }
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.EmpId = checkEmpExist.Id;
                item.RefranceId = entity.RefranceId ?? "0";
                item.DateFrom = entity.DateFrom;
                item.DateTo = entity.DateTo;
                item.DateTo = entity.DateTo;
                item.DateTran = entity.DateTran;
                item.PaymentType = entity.PaymentType ?? 0;
                item.CntHoursDay = entity.CntHoursDay ?? 0;
                item.CntHoursMonth = entity.CntHoursMonth ?? 0;
                item.CntHoursTotal = entity.CntHoursTotal ?? 0;
                hrRepositoryManager.HrOverTimeMRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                {
                    if (entity.hrOverTimeDDtos != null && entity.hrOverTimeDDtos.Count() > 0)
                        foreach (var SingleDetails in entity.hrOverTimeDDtos)
                        {
                            if (SingleDetails.IsDeleted == true)
                            {
                                var HrOverTimeDItem = await hrRepositoryManager.HrOverTimeDRepository.GetOne(x => x.Id == SingleDetails.Id);
                                if (HrOverTimeDItem == null)
                                {
                                    return await Result<HrOverTimeMEditDto>.FailAsync($"تفصيل الوقت الاضافي  غير موجود {SingleDetails.Id}");

                                }
                                HrOverTimeDItem.ModifiedBy = session.UserId;
                                HrOverTimeDItem.ModifiedOn = DateTime.Now;
                                HrOverTimeDItem.IsDeleted = true;
                                hrRepositoryManager.HrOverTimeDRepository.Update(HrOverTimeDItem);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                        }
                }
                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, entity.Id, 101);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrOverTimeMEditDto>.SuccessAsync(_mapper.Map<HrOverTimeMEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrOverTimeMEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> AddUsingExcel(HrOverTimeMAddUsingExcelDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                if (!entity.hrOverTimeDDtos.Any()) return await Result<string>.FailAsync($"{localization.GetMessagesResource("dataRequire")}");
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var newDetailitem in entity.hrOverTimeDDtos)
                {
                    if (string.IsNullOrEmpty(newDetailitem.EmpCode)) return await Result<string>.FailAsync($"Employee Id Is Required");

                    var item = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == newDetailitem.EmpCode && x.IsDeleted == false && x.Isdel == false);
                    if (item == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                    //////////////////////////////////////////////   ///
                    var BranchesList = session.Branches.Split(',');
                    var result = new HrEmpSalaryAndOverTimeDto();
                    decimal TotalAllowance = 0;
                    decimal TotalDeduction = 0;
                    var getFromHrEmployee = await hrRepositoryManager.HrEmployeeRepository.GetOne(e => e.EmpId == newDetailitem.EmpCode);
                    if (getFromHrEmployee == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                    TotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(getFromHrEmployee.Id);
                    TotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalDeduction(getFromHrEmployee.Id);
                    result.BasicSalary = getFromHrEmployee.Salary;
                    result.TotalSalary = getFromHrEmployee.Salary + TotalAllowance;
                    result.DailyWorkingHours = getFromHrEmployee.DailyWorkingHours;
                    if (entity.Type == 1)
                    {
                        newDetailitem.Amount = Math.Round((decimal)(getFromHrEmployee.Salary ?? 0 / 30 / getFromHrEmployee.DailyWorkingHours ?? 1 * entity.OverTimeHCost), 2);
                    }
                    if (entity.Type == 2)
                    {
                        newDetailitem.Amount = Math.Round((decimal)((getFromHrEmployee.Salary ?? 0 + TotalAllowance - TotalDeduction) / 30 / getFromHrEmployee.DailyWorkingHours ?? 1 * entity.OverTimeHCost), 2);
                    }
                    if (entity.Type == 3)
                    {
                        decimal? TotalSalary = getFromHrEmployee.Salary + TotalAllowance;
                        decimal? Salary = getFromHrEmployee.Salary;
                        newDetailitem.Amount = ((newDetailitem.Hours * (TotalSalary / 30 / getFromHrEmployee.DailyWorkingHours)) + (newDetailitem.Hours / 2 * (Salary / 30 / getFromHrEmployee.DailyWorkingHours))) / newDetailitem.Hours;


                    }
                    if (entity.Type == 4)
                    {
                        var ObjPolicy = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 1, getFromHrEmployee.Id);
                        newDetailitem.Amount = Math.Round((decimal)(ObjPolicy / 30 / (getFromHrEmployee.DailyWorkingHours ?? 1)), 2);

                    }

                    //////////////////////////////////////////////   ///
                    var newHrOverTimeM = new HrOverTimeM
                    {

                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        EmpId = item.Id,
                        RefranceId = newDetailitem.RefranceId,
                        DateTran = entity.DateTran,
                        Note = entity.Note,
                        PaymentType = entity.PaymentType,
                    };

                    var newEntity = await hrRepositoryManager.HrOverTimeMRepository.AddAndReturn(_mapper.Map<HrOverTimeM>(entity));

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    var NEWTimeDDtos = new HrOverTimeD
                    {
                        IsDeleted = false,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IdM = newEntity.Id,
                        OverTimeHCost = newDetailitem.OverTimeHCost,
                        OverTimeTybe = newDetailitem.OverTimeTybe,
                        Hours = newDetailitem.Hours,
                        Amount = newDetailitem.Amount,
                        Total = Math.Round((decimal)(newDetailitem.Hours ?? 0 * newDetailitem.Amount ?? 0), 2),
                        Description = newDetailitem.Description,
                        CurrencyId = 1,
                        OverTimeDate = newDetailitem.OverTimeDate,
                    };

                    await hrRepositoryManager.HrOverTimeDRepository.Add(NEWTimeDDtos);

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync("item added successfully", 200);

            }
            catch (Exception exc)
            {
                return await Result<string>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<HrEmpSalaryAndOverTimeDto>> getEmpSalaryAndOverData(string empCode, int TypeId, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');
                var result = new HrEmpSalaryAndOverTimeDto();
                decimal TotalAllowance = 0;
                decimal TotalDeduction = 0;
                result.OverTimeHCost = 1.5m;
                var getFromHrEmployee = await hrRepositoryManager.HrEmployeeRepository.GetOne(e => e.EmpId == empCode);
                if (getFromHrEmployee == null) return await Result<HrEmpSalaryAndOverTimeDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                var getData = await hrRepositoryManager.HrEmployeeRepository.GetOneVw(x => x.IsDeleted == false && x.Isdel == false && BranchesList.Contains(x.BranchId.ToString()) && x.Id == getFromHrEmployee.Id);
                if (getData == null)
                {
                    return await Result<HrEmpSalaryAndOverTimeDto>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));
                }
                var getOverTime = await hrRepositoryManager.HrSettingRepository.GetOne(X => X.FacilityId == session.FacilityId);
                if (getOverTime != null)
                {
                    if (getOverTime.OverTime > 0)
                        result.OverTimeHCost = getOverTime.OverTime;
                }
                TotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(getFromHrEmployee.Id);
                TotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalDeduction(getFromHrEmployee.Id);
                result.BasicSalary = getFromHrEmployee.Salary;
                result.TotalSalary = getFromHrEmployee.Salary + TotalAllowance;
                result.DailyWorkingHours = getFromHrEmployee.DailyWorkingHours;
                if (TypeId == 1)
                {
                    result.Amount = Math.Round((decimal)((getFromHrEmployee.Salary ?? 0) / 30 / (getFromHrEmployee.DailyWorkingHours ?? 1)), 2);
                }
                if (TypeId == 2)
                {
                    result.Amount = Math.Round((decimal)(((getFromHrEmployee.Salary ?? 0) + TotalAllowance) / 30 / (getFromHrEmployee.DailyWorkingHours ?? 1)), 2);
                }
                if (TypeId == 3)
                {
                    decimal? HourFormTotal = 0;
                    decimal? HourFromBasic = 0;
                    result.OverTimeHCost = 1.5m;
                    HourFormTotal = Math.Round((decimal)(((getFromHrEmployee.Salary ?? 0) + TotalAllowance) / 30 / (getFromHrEmployee.DailyWorkingHours ?? 1)), 2);
                    HourFromBasic = Math.Round((decimal)(getFromHrEmployee.Salary ?? 0) / 30 / (getFromHrEmployee.DailyWorkingHours ?? 1), 2);
                    result.Amount = Math.Round((decimal)(HourFormTotal + (HourFromBasic / 2)), 2);

                }
                if (TypeId == 4)
                {
                    var ObjPolicy = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 1, getFromHrEmployee.Id);
                    result.Amount = Math.Round((decimal)(ObjPolicy / 30 / (getFromHrEmployee.DailyWorkingHours ?? 1)), 2);

                }
                return await Result<HrEmpSalaryAndOverTimeDto>.SuccessAsync(result, "Success");
            }
            catch (Exception)
            {

                return await Result<HrEmpSalaryAndOverTimeDto>.FailAsync($"تأكد من اكتمال بيانات الراتب للموظف");
            }
        }

        public async Task<IResult<string>> Add4(HrOverTimeMAdd4Dto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                if (entity.hrOverTimeMAdd4DetailsDto.Count() > 0)
                {

                    foreach (var item in entity.hrOverTimeMAdd4DetailsDto)
                    {
                        var EmpIsExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == item.EmpCode && x.IsDeleted == false && x.Isdel == false);
                        if (EmpIsExist == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));


                        var addNew = new HrOverTimeM
                        {
                            EmpId = EmpIsExist.Id,
                            CreatedOn = DateTime.Now,
                            CreatedBy = session.UserId,
                            DateFrom = entity.DateFrom,
                            DateTo = entity.DateTo,
                            DateTran = entity.DateTran,
                            RefranceId = entity.RefranceId,
                            PaymentType = entity.PaymentType,
                            CntHoursTotal = item.CntHoursTotal ?? 0,
                            CntHoursMonth = item.CntHoursMonth,
                            CntHoursDay = item.CntHoursDay,
                            Note = entity.Note
                        };
                        await hrRepositoryManager.HrOverTimeMRepository.Add(addNew);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                    return await Result<string>.SuccessAsync("item added successfully", 200);
                }
                else
                {
                    return await Result<string>.FailAsync("You Must Choose at Least One Employee");

                }
            }
            catch (Exception exc)
            {
                return await Result<string>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<List<HrGetAttendanceButtonResult>>> GetAttendanceData(HrGetAttendanceButtonClickDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<List<HrGetAttendanceButtonResult>>.FailAsync(($"{localization.GetMessagesResource("dataRequire")}"));

            var result = new List<HrGetAttendanceButtonResult>();
            IQueryable<HrOverTimeMVw> overtimeList = Enumerable.Empty<HrOverTimeMVw>().AsQueryable();

            try
            {
                decimal? Amount = 0m;
                decimal? HoursOV = 0m;
                double hoursIn;
                double hoursOut;
                double hoursInW;
                double hoursOutW;
                double? hours = 0;
                double? hoursW = 0;

                decimal? OverTime_H_Cost = 0m;
                var getOverTimeFromHrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(o => o.OverTime, X => X.FacilityId == session.FacilityId);
                if (getOverTimeFromHrSetting != null)
                {
                    OverTime_H_Cost = getOverTimeFromHrSetting;
                }

                var getOverTimeData = await hrRepositoryManager.HrOverTimeMRepository.GetAllFromView(x => x.IsDeleted == false && !string.IsNullOrEmpty(x.DateFrom) && !string.IsNullOrEmpty(x.DateTo) && (entity.Location == null || x.Location == entity.Location) && (string.IsNullOrEmpty(entity.RefranceID) || x.RefranceId == entity.RefranceID) && (entity.DeptID == null || x.DeptId == entity.DeptID) && (entity.BRANCHID == null || x.BranchId == entity.BRANCHID) && (string.IsNullOrEmpty(entity.EmpCode) || x.EmpCode == entity.EmpCode) && (string.IsNullOrEmpty(entity.EmpName) || x.EmpName.ToLower().Contains(entity.EmpName) || x.EmpName2.ToLower().Contains(entity.EmpName)));
                if (getOverTimeData.Count() > 0)
                {
                    overtimeList = getOverTimeData.Where(r =>
                            (DateHelper.StringToDate(entity.DateFrom) >= DateHelper.StringToDate(r.DateFrom) && DateHelper.StringToDate(entity.DateFrom) <= DateHelper.StringToDate(r.DateTo)) ||
                           (DateHelper.StringToDate(entity.DateTo) >= DateHelper.StringToDate(r.DateFrom) && DateHelper.StringToDate(entity.DateTo) <= DateHelper.StringToDate(r.DateTo))
                           ).AsQueryable();
                }
                if (overtimeList.Any())
                {
                    foreach (var singleItem in overtimeList)
                    {
                        var filterDtoForProcedure = new HRAttendanceReport4FilterDto
                        {
                            EmpCode = singleItem.EmpCode,
                            DayDateGregorian = entity.DateFrom,
                            DayDateGregorian2 = entity.DateTo,
                            BranchID = 0,
                            TimeTableID = 0,
                            ManagerID = 0,
                            AttendanceType = 0,
                            SponsorsID = 0,
                            DeptID = 0,
                            Location = 0,
                            ShitID = 0,
                            StatusID = 0,
                            BranchsID = null,
                            EmpName = entity.EmpName
                        };
                        var dt_Att = await mainRepositoryManager.StoredProceduresRepository.HR_Attendance_Report4_SP(filterDtoForProcedure); // Assuming GetAtt() returns a List<HRAttendanceReport4Dto>
                        decimal? Cnt_Hours_Total = singleItem.CntHoursTotal;

                        foreach (var RowAtt in dt_Att)
                        {
                            if (singleItem.EmpCode == RowAtt.Emp_Code)
                            {

                                var GetEmpSalary = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.Isdel == false && x.IsDeleted == false && x.EmpId == singleItem.EmpCode);
                                if (GetEmpSalary == null)
                                {
                                    return await Result<List<HrGetAttendanceButtonResult>>.FailAsync($"الموظف رقم  {singleItem.EmpCode}  غير موجود");

                                }
                                Amount = Math.Round(Convert.ToDecimal(GetEmpSalary.Salary) / 30 / Convert.ToDecimal(GetEmpSalary.DailyWorkingHours), 2);
                                if (DateHelper.StringToDate(RowAtt.Day_Date_Gregorian) >= DateHelper.StringToDate(singleItem.DateFrom) &&
                                   DateHelper.StringToDate(RowAtt.Day_Date_Gregorian) <= DateHelper.StringToDate(singleItem.DateTo))
                                {

                                    HoursOV = 0;
                                    if (!string.IsNullOrEmpty(RowAtt.Time_In.ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(RowAtt.Time_Out.ToString()))
                                        {
                                            if (!string.IsNullOrEmpty(RowAtt.Def_Time_In.ToString()))
                                            {
                                                if (!string.IsNullOrEmpty(RowAtt.Def_Time_Out.ToString()))
                                                {
                                                    var Time_In = (DateTime)RowAtt.Time_In;
                                                    var Time_Out = (DateTime)RowAtt.Time_Out;
                                                    if (DBNull.Value.Equals(Time_Out))
                                                    {
                                                        Time_Out = Time_In;
                                                    }
                                                    var Def_Time_In = (DateTime)RowAtt.Def_Time_In;
                                                    var Def_Time_Out = (DateTime)RowAtt.Def_Time_Out;
                                                    hoursIn = Time_In.Hour + Time_In.Minute / 60;
                                                    hoursOut = Time_Out.Hour + Time_Out.Minute / 60;
                                                    hoursInW = Def_Time_In.Hour + Def_Time_In.Minute / 60;
                                                    hoursOutW = Def_Time_Out.Hour + Def_Time_Out.Minute / 60;
                                                    hours = (hoursOut - hoursIn);
                                                    hoursW = (hoursOutW - hoursInW);
                                                    var getOverTime = await hrRepositoryManager.HrAttTimeTableRepository.GetOne(x => x.Overtime, x => x.Id == RowAtt.TimeTable_ID);
                                                    var OverTime = Convert.ToBoolean(getOverTime);
                                                    if (OverTime)
                                                    {
                                                        hoursIn = Time_In.Hour + Time_In.Minute / 60;
                                                        hoursOut = Time_Out.Hour + Time_Out.Minute / 60;
                                                        var totelhours = Time_Out - Time_In;
                                                        var totalhourstime = (Convert.ToInt32((totelhours.Hours * 60 + totelhours.Minutes / 60)) + (totelhours.Hours * 60 + totelhours.Minutes) % 60) / 60;
                                                        HoursOV = totalhourstime;
                                                    }
                                                    else
                                                    {
                                                        if (hoursW < hours)
                                                        {
                                                            HoursOV = (decimal?)(hours - hoursW);
                                                        }
                                                        else
                                                        {
                                                            HoursOV = 0;
                                                        }
                                                    }

                                                    if (HoursOV > Cnt_Hours_Total)
                                                    {
                                                        HoursOV = Cnt_Hours_Total;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (HoursOV == 0)
                                    {
                                        continue;
                                    }
                                    var overTimeDataItem = new HrGetAttendanceButtonResult
                                    {

                                        IdM = singleItem.Id,
                                        OverTimeTybe = 1,
                                        OverTimeHCost = OverTime_H_Cost,
                                        Amount = Amount,
                                        Description = "سحب من التحضير",
                                        CurrencyId = 1,
                                        OverTimeDate = RowAtt.Day_Date_Gregorian,
                                        Total = Math.Round((decimal)(OverTime_H_Cost * HoursOV * Amount), 2),
                                        Hours = HoursOV,
                                        EmpCode = singleItem.EmpCode,
                                        AssignmentHours = Cnt_Hours_Total,
                                        CurrencyName = "SR",
                                        EmpName = singleItem.EmpName,
                                        RefranceId = singleItem.RefranceId,
                                        HoursWork = hoursW < hours ? hours - hoursW : 0

                                    };

                                    result.Add(overTimeDataItem);
                                }
                            }
                        }
                    }

                }
                else
                {
                    return await Result<List<HrGetAttendanceButtonResult>>.FailAsync(localization.GetResource1("NosearchResult"));

                }
            }
            catch (Exception ex)
            {
                return await Result<List<HrGetAttendanceButtonResult>>.FailAsync(ex.Message.ToString());
            }
            if (result.Any())
                return await Result<List<HrGetAttendanceButtonResult>>.SuccessAsync(result, $"{result.Count}item Retrieved");
            return await Result<List<HrGetAttendanceButtonResult>>.FailAsync(localization.GetResource1("NosearchResult"));

        }

        public async Task<IResult<string>> AddListOfOverTimeD(IEnumerable<HrOverTimeDDto> entities, CancellationToken cancellationToken = default)
        {

            if (entities.Any()) return await Result<string>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                foreach (var entity in entities)
                {
                    var newItem = new HrOverTimeD
                    {
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        IdM = entity.IdM,
                        OverTimeTybe = entity.OverTimeTybe,
                        OverTimeHCost = entity.OverTimeHCost,
                        Hours = entity.Hours,
                        Amount = entity.Amount,
                        Total = entity.Total,
                        Description = entity.Description,
                        CurrencyId = entity.CurrencyId,
                        OverTimeDate = entity.OverTimeDate,

                    };
                    await hrRepositoryManager.HrOverTimeDRepository.Add(newItem);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync($"{entities.Count()} item added successfully", 200);
            }
            catch (Exception exc)
            {

                return await Result<string>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }



        }

		public async Task<IResult<List<HrOverTimeMVw>>> Search(HrOverTimeMFilterDto filter, CancellationToken cancellationToken = default)
		{
            try 
            { 
			filter.BranchId ??= 0;
			filter.DeptId ??= 0;
			filter.Location ??= 0;
			var BranchesList = session.Branches.Split(',');
			var items = await hrRepositoryManager.HrOverTimeMRepository.GetAllVw(e => e.IsDeleted == false &&
			BranchesList.Contains(e.BranchId.ToString()) &&
			e.DateFrom != "" &&
			e.DateTo != "" &&
			(string.IsNullOrEmpty(filter.EmpCode) || e.EmpCode == filter.EmpCode) &&
			(string.IsNullOrEmpty(filter.EmpName) || (e.EmpName != null && e.EmpName.ToLower().Contains(filter.EmpName.ToLower()))) &&
			(string.IsNullOrEmpty(filter.RefranceId) || e.RefranceId == filter.RefranceId) &&
			(filter.BranchId == 0 || e.BranchId == filter.BranchId) &&
			(filter.DeptId == 0 || e.DeptId == filter.DeptId) &&
			(filter.Location == 0 || e.Location == filter.Location)

			);
			if (items == null)
				return await Result<List<HrOverTimeMVw>>.FailAsync("errrrors");


			if (items.Count() <= 0)
				return await Result<List<HrOverTimeMVw>>.SuccessAsync(items.ToList(), localization.GetResource1("NosearchResult"));


			var res = items.AsQueryable();

			if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
			{
				var FromDate = DateHelper.StringToDate(filter.FromDate);
				var ToDate = DateHelper.StringToDate(filter.ToDate);
				res = res.Where(r =>
				(DateHelper.StringToDate(r.DateFrom) >= FromDate && DateHelper.StringToDate(r.DateFrom) <= ToDate) ||
			   (DateHelper.StringToDate(r.DateTo) >= FromDate && DateHelper.StringToDate(r.DateTo) <= ToDate)
			   );
			}

			if (res.Any())
				return await Result<List<HrOverTimeMVw>>.SuccessAsync(res.ToList(), "");
			return await Result<List<HrOverTimeMVw>>.SuccessAsync(res.ToList(), localization.GetResource1("NosearchResult"));
		}
         catch (Exception ex)
          {
                return await Result<List<HrOverTimeMVw>>.FailAsync(ex.Message);
	}
}
	}

}
