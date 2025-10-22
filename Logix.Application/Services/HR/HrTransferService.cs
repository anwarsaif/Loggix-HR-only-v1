using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using DocumentFormat.OpenXml.Vml.Office;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Logix.Application.Services.HR
{
    public class HrTransferService : GenericQueryService<HrTransfer, HrTransferDto, HrTransfersVw>, IHrTransferService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IPMRepositoryManager pmRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        public HrTransferService(IQueryRepository<HrTransfer> queryRepository, IMainRepositoryManager mainRepositoryManager, IPMRepositoryManager pmRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.pmRepositoryManager = pmRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;

        }

        public async Task<IResult<HrTransferDto>> Add(HrTransferDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrTransferDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            
            try
            {
                int? Job_Catagories_ID = 0;
                int? Program_ID = 0;
                decimal? Net_Salary = 0;
                decimal TotalAllowance = 0;
                decimal TotalDeduction = 0;
                var checkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrTransferDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));


                if (checkEmp.StatusId == 2)
                {
                    return await Result<HrTransferDto>.FailAsync(localization.GetMessagesResource("EmployeeAlreadyTerminated"));

                }
                var GetHREmpAllData = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.Id == checkEmp.Id);
                if (GetHREmpAllData == null)
                {
                    return await Result<HrTransferDto>.FailAsync(localization.GetMessagesResource("EmployeeNotFoundInList"));
                }

                TotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(checkEmp.Id);
                TotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalDeduction(checkEmp.Id);
                Program_ID = GetHREmpAllData.ProgramId;
                Job_Catagories_ID = GetHREmpAllData.JobCatagoriesId;
                Net_Salary = GetHREmpAllData.Salary + TotalAllowance - TotalDeduction;

                // التشييك على الضوابط التشغيلية
                var projectId = await mainRepositoryManager.SysDepartmentRepository.GetOne(p => p.Id == entity.TransLocationTo);
                var Allow = await pmRepositoryManager.PMProjectsOperationalControlRepository.GetAll(a => a.IsDeleted == false && a.ProjectId == projectId.ProjectId && a.JobCatagoriesId == Job_Catagories_ID);
                var AllowInJob = Allow.Sum(a => a.CountOfEmplyee ?? 0);
                var Current = await hrRepositoryManager.HrEmployeeRepository.GetAll(c => (c.StatusId == 1 || c.StatusId == 10) && c.IsDeleted == false && c.Location == entity.TransLocationTo && c.JobCatagoriesId == Job_Catagories_ID);
                var CurrentInjob = Current.Count();
                if (AllowInJob != 0 && AllowInJob < CurrentInjob + 1)
                {
                    var resultMessage = localization.GetMessagesResource("TheNumberOfEmployeesOnLocationForTheSpecifiedJobExceedsTheNumberAllowedInTheOperationalControls")
                        + localization.GetMessagesResource("NumberOfPositionsAllowedForTheSite") + AllowInJob.ToString()
                        + localization.GetMessagesResource("NumberOfEmployeesOnTheJobUnderProcess") + CurrentInjob.ToString();

                    return await Result<HrTransferDto>.FailAsync(resultMessage);
                }

                // التشييك على رواتب الوظائف والبرامج
                var jobsSalary = await pmRepositoryManager.PMJobsSalaryVwRepository.GetOne(j => j.IsDeleted == false && j.ProjectId == projectId.ProjectId && j.JobCatagoriesId == Job_Catagories_ID && j.ProgramId == Program_ID);
                var maxSalary = 0.00m;
                if (jobsSalary != null)
                {
                    maxSalary = jobsSalary.Maxsalary ?? 0.00m;
                }
                if (maxSalary != 0 && maxSalary < Net_Salary)
                {
                    var resultMessage = localization.GetMessagesResource("TheEmployeesOnLocationSalaryForTheSpecifiedPositionExceedsTheSalaryAllowedInTheOperationalControls")
                        + localization.GetMessagesResource("TheHighestSalaryForAnEmployeeOnTheLocationMustNotExceedTheMaximumLimitWhichIs") + maxSalary.ToString();
                    return await Result<HrTransferDto>.FailAsync(resultMessage);
                }
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.EmpId = checkEmp.Id;
                entity.IsDeleted = false;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var newEntity = await hrRepositoryManager.HrTransferRepository.AddAndReturn(_mapper.Map<HrTransfer>(entity));

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.ShitID != 0)
                {
                    //  حذف الوردية الحالية واسناد الوردية الجديدة
                    var getEmployeeAttShift = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOne(x => x.IsDeleted == false && x.EmpId == checkEmp.Id);
                    // اذا وجدت وردية نقوم بالتعديل مالم سنقوم بالاضافة
                    if (getEmployeeAttShift != null)
                    {
                        getEmployeeAttShift.ModifiedBy = session.UserId;
                        getEmployeeAttShift.ModifiedOn = DateTime.Now;
                        getEmployeeAttShift.IsDeleted = true;
                        hrRepositoryManager.HrAttShiftEmployeeRepository.Update(getEmployeeAttShift);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                    var addNewAttShift = new HrAttShiftEmployee
                    {
                        EmpId = Convert.ToInt64(checkEmp.Id),
                        CreatedBy = session.UserId,
                        BeginDate = entity.TransferDate,
                        EndDate = "2027/12/30",
                        ShitId = entity.ShitID,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                    };

                    var addNewAttShiftItem = await hrRepositoryManager.HrAttShiftEmployeeRepository.AddAndReturn(addNewAttShift);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                var entityMap = _mapper.Map<HrTransferDto>(newEntity);

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrTransferDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrTransferDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<HrTransfersAllAddDto>> AddMultipleTransfers(HrTransfersAllAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrTransfersAllAddDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            if (!entity.EmpIds.Any()) return await Result<HrTransfersAllAddDto>.FailAsync(localization.GetMessagesResource("NoEmployeeHasBeenSelected"));

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var empIdItem in entity.EmpIds)
                {
                    var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == empIdItem && e.IsDeleted == false && e.Isdel == false);
                    if (investEmployees == null)
                        return await Result<HrTransfersAllAddDto>.FailAsync($"{localization.GetMessagesResource("EmployeeNotFoundInList")} : {empIdItem}");

                    if (investEmployees.StatusId == 2)
                        return await Result<HrTransfersAllAddDto>.FailAsync(localization.GetMessagesResource("EmployeeAlreadyTerminated"));

                    entity.EmpId = investEmployees.Id;
                    entity.CreatedBy = session.UserId;
                    entity.CreatedOn = DateTime.Now;
                    entity.IsDeleted = false;

                    var newEntity = await hrRepositoryManager.HrTransferRepository.AddAndReturn(_mapper.Map<HrTransfer>(entity));
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    investEmployees.DeptId = entity.TransDepartmentTo;
                    investEmployees.Location = entity.TransLocationTo;
                    investEmployees.BranchId = entity.BranchIdTo;
                    investEmployees.ModifiedBy = session.UserId;
                    investEmployees.ModifiedOn = DateTime.Now;
                    mainRepositoryManager.InvestEmployeeRepository.Update(investEmployees);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrTransfersAllAddDto>.SuccessAsync($"{localization.GetMessagesResource("TheProcessofAddingNumofEmployeesWasCompletedSuccessfully1")} {entity.EmpIds.Count()}, {localization.GetMessagesResource("TheProcessofAddingNumofEmployeesWasCompletedSuccessfully2")}");

            }
            catch (Exception exp)
            {

                return await Result<HrTransfersAllAddDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exp.Message}");
            }

        }
        public async Task<IResult<string>> HRGetchildeDepartmentFn(long Id)
        {
            try
            {
                var result = await hrRepositoryManager.HrTransferRepository.HRGetchildeDepartmentFn(Id);

                return await Result<string>.SuccessAsync(result);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in HRGetchildeDepartmentFn  ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrTransferRepository.GetById(Id);
                if (item == null) return Result<HrTransferDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrTransferRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrTransferDto>.SuccessAsync(_mapper.Map<HrTransferDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrTransferDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrTransferRepository.GetById(Id);
                if (item == null) return Result<HrTransferDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrTransferRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrTransferDto>.SuccessAsync(_mapper.Map<HrTransferDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrTransferDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrTransferEditDto>> Update(HrTransferEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrTransferEditDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                // check if Emp Is Exist
                var checkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrTransferEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrTransferRepository.GetOne(x => x.Id == entity.Id);
                if (item == null) return await Result<HrTransferEditDto>.FailAsync($"{localization.GetMessagesResource("NoDataWithId")}: {entity.Id}");

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                entity.EmpId = checkEmp.Id;

                _mapper.Map(entity, item);
                item.IsDeleted = false;
                hrRepositoryManager.HrTransferRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrTransferEditDto>.SuccessAsync(_mapper.Map<HrTransferEditDto>(item), localization.GetResource1("UpdateSuccess"));

            }
            catch (Exception exc)
            {
                return await Result<HrTransferEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<HrTransfersAdd2Dto>> Add2Transfers(HrTransfersAdd2Dto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrTransfersAdd2Dto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                int? Job_Catagories_ID = 0;
                int? Program_ID = 0;
                decimal? Net_Salary = 0;
                decimal TotalAllowance = 0;
                decimal TotalDeduction = 0;
                long? IncrementId = 0;

                var checkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCode.ToString() && e.IsDeleted == false && e.Isdel == false);
                
                if (checkEmp == null) return await Result<HrTransfersAdd2Dto>.FailAsync(localization.GetResource1("EmployeeNotFound"));


                if (checkEmp.StatusId == 2)
                {
                    return await Result<HrTransfersAdd2Dto>.FailAsync(localization.GetMessagesResource("EmployeeAlreadyTerminated"));

                }
                var GetHREmpAllData = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.Id == checkEmp.Id);
                if (GetHREmpAllData == null)
                {
                    return await Result<HrTransfersAdd2Dto>.FailAsync(localization.GetMessagesResource("EmployeeNotFoundInList"));
                }

                TotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(checkEmp.Id);
                TotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalDeduction(checkEmp.Id);
                Program_ID = GetHREmpAllData.ProgramId;
                Job_Catagories_ID = GetHREmpAllData.JobCatagoriesId;
                Net_Salary = GetHREmpAllData.Salary + TotalAllowance - TotalDeduction;

                // التشييك على الضوابط التشغيلية
                var projectId = await mainRepositoryManager.SysDepartmentRepository.GetOne(p => p.Id == entity.TransLocationTo);
                var Allow = await pmRepositoryManager.PMProjectsOperationalControlRepository.GetAll(a => a.IsDeleted == false && a.ProjectId == projectId.ProjectId && a.JobCatagoriesId == Job_Catagories_ID);
                var AllowInJob = Allow.Sum(a => a.CountOfEmplyee ?? 0);
                var Current = await hrRepositoryManager.HrEmployeeRepository.GetAll(c => (c.StatusId == 1 || c.StatusId == 10) && c.IsDeleted == false && c.Location == entity.TransLocationTo && c.JobCatagoriesId == Job_Catagories_ID);
                var CurrentInjob = Current.Count();
                if (AllowInJob != 0 && AllowInJob < CurrentInjob + 1)
                {
                    var resultMessage = localization.GetMessagesResource("TheNumberOfEmployeesOnLocationForTheSpecifiedJobExceedsTheNumberAllowedInTheOperationalControls")
                        + localization.GetMessagesResource("NumberOfPositionsAllowedForTheSite") + AllowInJob.ToString()
                        + localization.GetMessagesResource("NumberOfEmployeesOnTheJobUnderProcess") + CurrentInjob.ToString();

                    return await Result<HrTransfersAdd2Dto>.FailAsync(resultMessage);
                }

                // التشييك على رواتب الوظائف والبرامج
                var jobsSalary = await pmRepositoryManager.PMJobsSalaryVwRepository.GetOne(j => j.IsDeleted == false && j.ProjectId == projectId.ProjectId && j.JobCatagoriesId == Job_Catagories_ID && j.ProgramId == Program_ID);
                var maxSalary = 0.00m;
                if (jobsSalary != null)
                {
                    maxSalary = jobsSalary.Maxsalary ?? 0.00m;
                }
                if (maxSalary != 0 && maxSalary < Net_Salary)
                {
                    var resultMessage = localization.GetMessagesResource("TheEmployeesOnLocationSalaryForTheSpecifiedPositionExceedsTheSalaryAllowedInTheOperationalControls")
                        + localization.GetMessagesResource("TheHighestSalaryForAnEmployeeOnTheLocationMustNotExceedTheMaximumLimitWhichIs") + maxSalary.ToString();
                    return await Result<HrTransfersAdd2Dto>.FailAsync(resultMessage);
                }
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.EmpId = checkEmp.Id;
                entity.IsDeleted = false;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var newEntity = await hrRepositoryManager.HrTransferRepository.AddAndReturn(_mapper.Map<HrTransfer>(entity));

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var newIncrementsItem = new HrIncrement
                {
                    EmpId = checkEmp.Id,
                    IncreaseDate = entity.TransferDate,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    Salary = entity.Salary,
                    Allowances = entity.AllowancesAmount,
                    Deductions = entity.DeductionsAmount,
                    IncreaseAmount = entity.IncreaseAmount,
                    StartDate = entity.TransferDate,
                    NewSalary = entity.NewSalary,
                    Note = entity.Note,
                    ApplyType = 1,
                };

                var IncrementResult = await hrRepositoryManager.HrIncrementRepository.AddAndReturn(newIncrementsItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                IncrementId = IncrementResult.Id;

                //  هنا نقوم بتحديث الراتب في جدول التوظيف 
                if (checkEmp != null)
                {
                    checkEmp.ModifiedBy = session.UserId;
                    checkEmp.ModifiedOn = DateTime.Now;
                    checkEmp.DeptId = entity.TransDepartmentTo;
                    checkEmp.Location = entity.TransLocationTo;
                    checkEmp.BranchId = entity.TransBranchIdTo;
                    checkEmp.Salary = entity.NewSalary;
                    checkEmp.LevelId = (int?)IncrementResult.NewLevelId;
                    checkEmp.DegreeId = (int?)IncrementResult.NewGradId;
                    checkEmp.JobId = (int?)IncrementResult.NewJobId;
                    checkEmp.JobCatagoriesId = (int?)IncrementResult.NewCatJobId;
                    mainRepositoryManager.InvestEmployeeRepository.Update(checkEmp);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                //  معالجة البدلات
                if (entity.allowance.Count() > 0)
                {

                    foreach (var allowanceitem in entity.allowance)
                    {
                        var newIncrementsAllowanceDeduction = new HrIncrementsAllowanceDeduction
                        {
                            IncrementId = IncrementId,
                            Rate = allowanceitem.AllowanceRate,
                            Amount = allowanceitem.AllowanceAmount,
                            TypeId = 1,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            NewRate = 0,
                            NewAmount = allowanceitem.NewAmount,
                            AdId = allowanceitem.AddId,
                        };
                        if (allowanceitem.IsDeleted == false)
                        {
                            newIncrementsAllowanceDeduction.Status = false;
                            var newHrIncrementAllowance = await hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.AddAndReturn(newIncrementsAllowanceDeduction);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                                var updateAllowanceDeduction = new HrAllowanceDeduction
                                {
                                    TypeId = 1,
                                    AdId = allowanceitem.AddId,
                                    Rate = 0,
                                    Amount = allowanceitem.NewAmount,
                                    CreatedBy = session.UserId,
                                    CreatedOn = DateTime.Now,
                                    EmpId = checkEmp.Id,
                                    FixedOrTemporary = 1,
                                    MAdId = 0,
                                    Id = allowanceitem.Id,
                                };
                                if (updateAllowanceDeduction.Id == 0)
                                {
                                    updateAllowanceDeduction.Note = localization.GetMessagesResource("AddFromTransfer");

                                    var ad = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(updateAllowanceDeduction);
                                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                }
                                else
                                {
                                    var tracked = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(allowanceitem.Id);

                                    if (tracked != null)
                                    {
                                        tracked.Amount = allowanceitem.NewAmount;
                                        tracked.Rate = 0;
                                        tracked.ModifiedBy = session.UserId;
                                        tracked.ModifiedOn = DateTime.Now;
                                        tracked.TypeId = 1;
                                        tracked.FixedOrTemporary = 1;
                                        tracked.MAdId = 0;
                                        tracked.Note = localization.GetMessagesResource("UpdateFromTransfer");

                                        hrRepositoryManager.HrAllowanceDeductionRepository.Update(tracked);
                                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    }
                                }
                            }
                        else
                        {
                            var getAllDed = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(allowanceitem.Id);
                            if (getAllDed != null)
                            {
                                getAllDed.IsDeleted = true;
                                getAllDed.ModifiedBy = session.UserId;
                                getAllDed.ModifiedOn = DateTime.Now;

                                hrRepositoryManager.HrAllowanceDeductionRepository.Update(getAllDed);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                        }
                    }
                }

                //  معالجة الحسميات
                if (entity.deduction.Count() > 0)
                {

                    foreach (var deductionItem in entity.deduction)
                    {
                        var newIncrementsAllowanceDeduction = new HrIncrementsAllowanceDeduction
                        {
                            IncrementId = IncrementId,
                            Rate = deductionItem.DeductionRate,
                            Amount = deductionItem.DeductionAmount,
                            TypeId = 2,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            NewRate = 0,
                            NewAmount = deductionItem.NewAmount,
                            AdId = deductionItem.AddId,
                        };
                        if (deductionItem.IsDeleted == false)
                        {
                            newIncrementsAllowanceDeduction.Status = false;
                            var newHrIncrementAllowance = await hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.AddAndReturn(newIncrementsAllowanceDeduction);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                                var updateAllowanceDeduction = new HrAllowanceDeduction
                                {
                                    TypeId = 2,
                                    AdId = deductionItem.AddId,
                                    Rate = 0,
                                    Amount = deductionItem.NewAmount,
                                    CreatedBy = session.UserId,
                                    CreatedOn = DateTime.Now,
                                    EmpId = checkEmp.Id,
                                    FixedOrTemporary = 1,
                                    MAdId = 0,
                                    Id = deductionItem.Id,
                                };
                                if (updateAllowanceDeduction.Id == 0)
                                {
                                    updateAllowanceDeduction.Note = localization.GetMessagesResource("AddFromTransfer");

                                    var ad = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(updateAllowanceDeduction);
                                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                }
                                else
                                {
                                    var tracked = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(deductionItem.Id);

                                    if (tracked != null)
                                    {
                                        tracked.Amount = deductionItem.NewAmount;
                                        tracked.Rate = 0;
                                        tracked.ModifiedBy = session.UserId;
                                        tracked.ModifiedOn = DateTime.Now;
                                        tracked.TypeId = 2;
                                        tracked.FixedOrTemporary = 1;
                                        tracked.MAdId = 0;
                                        tracked.Note = localization.GetMessagesResource("UpdateFromTransfer");

                                        hrRepositoryManager.HrAllowanceDeductionRepository.Update(tracked);
                                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    }
                                }
                            }
                        else
                        {
                            var getAllDed = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(deductionItem.Id);
                            if (getAllDed != null)
                            {
                                getAllDed.IsDeleted = true;
                                getAllDed.ModifiedBy = session.UserId;
                                getAllDed.ModifiedOn = DateTime.Now;

                                hrRepositoryManager.HrAllowanceDeductionRepository.Update(getAllDed);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                        }
                    }
                }

                //////////////////////////////////////////////////////////////////
                if (entity.ShitID > 0)
                {
                    //  حذف الوردية الحالية واسناد الوردية الجديدة
                    var getEmployeeAttShift = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOne(x => x.IsDeleted == false && x.EmpId == checkEmp.Id);
                    // اذا وجدت وردية نقوم بالتعديل مالم سنقوم بالاضافة
                    if (getEmployeeAttShift != null)
                    {
                        getEmployeeAttShift.ModifiedBy = session.UserId;
                        getEmployeeAttShift.ModifiedOn = DateTime.Now;
                        getEmployeeAttShift.IsDeleted = true;
                        hrRepositoryManager.HrAttShiftEmployeeRepository.Update(getEmployeeAttShift);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                    else
                    {
                        var addNewAttShift = new HrAttShiftEmployee
                        {
                            ModifiedBy = session.UserId,
                            ModifiedOn = DateTime.Now,
                            IsDeleted = false,
                            ShitId = entity.ShitID,
                            EndDate = "2027/12/30",
                            BeginDate = entity.TransferDate
                        };
                        var addNewAttShiftItem = await hrRepositoryManager.HrAttShiftEmployeeRepository.AddAndReturn(addNewAttShift);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                //////////////////////////////////////////////////////////////////
                var entityMap = _mapper.Map<HrTransfersAdd2Dto>(newEntity);

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrTransfersAdd2Dto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrTransfersAdd2Dto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<HrTransfersAdd2Dto>> GetEmpDataByEmpId(string empId, CancellationToken cancellationToken = default)
        {
            decimal TotalAllowance = 0;
            decimal TotalDeduction = 0;
            List<HrAllowanceVM> allAllowanceList = new List<HrAllowanceVM>();
            List<HrDeductionVM> allDeductionList = new List<HrDeductionVM>();
            var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == empId && e.IsDeleted == false && e.Isdel == false);
            if (investEmployees == null)
            {
                return await Result<HrTransfersAdd2Dto>.FailAsync("  الموظف غير موجود في قائمة الموظفين");
            }
            var GetHREmpAllData = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.Id == investEmployees.Id);
            if (GetHREmpAllData == null)
            {
                return await Result<HrTransfersAdd2Dto>.FailAsync("  الموظف غير موجود في قائمة الموظفين");
            }
            var getTotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(e => e.EmpId == investEmployees.Id && e.IsDeleted == false && e.TypeId == 1 && e.FixedOrTemporary == 1);
            if (getTotalAllowance != null)
            {
                foreach (var item in getTotalAllowance)
                {
                    TotalAllowance += (item.Amount != null ? item.Amount.Value : 0);
                    var allAllowance = new HrAllowanceVM
                    {
                        Id = item.Id,
                        AllowanceAmount = item.Amount,
                        AllowanceRate = item.Rate,
                        AddId = item.AdId
                    };
                    allAllowanceList.Add(allAllowance);
                }
            }
            var getTotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(e => e.EmpId == investEmployees.Id && e.IsDeleted == false && e.TypeId == 2 && e.FixedOrTemporary == 1);
            if (getTotalDeduction != null)
            {
                foreach (var item in getTotalDeduction)
                {
                    TotalDeduction += (item.Amount != null ? item.Amount.Value : 0);
                    var alldeduction = new HrDeductionVM
                    {
                        Id = item.Id,
                        DeductionAmount = item.Amount,
                        DeductionRate = item.Rate,
                        AddId = item.AdId
                    };
                    allDeductionList.Add(alldeduction);
                }
            }

            var empDataResult = new HrTransfersAdd2Dto
            {
                Salary = GetHREmpAllData.Salary,
                EmpCode = GetHREmpAllData.EmpId,
                NetSalary = GetHREmpAllData.Salary + TotalAllowance - TotalDeduction,
                AllowancesAmount = TotalAllowance,
                DeductionsAmount = TotalDeduction,
                EmpName = GetHREmpAllData.EmpName,
                TransDepartmentFrom = GetHREmpAllData.DeptId,
                TransBranchIdfrom = GetHREmpAllData.BranchId,
                TransLocationFrom = GetHREmpAllData.Location,
                deduction = allDeductionList,
                allowance = allAllowanceList
            };
            return await Result<HrTransfersAdd2Dto>.SuccessAsync(empDataResult);

        }

		public async Task<IResult<List<HrTransfersVw>>> Search(HrTransferFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				var BranchesList = session.Branches.Split(',');
                filter.DeptId ??= 0;
                filter.BranchId ??= 0;
                filter.LocationId ??= 0;
                filter.LocationFromId ??= 0;
                filter.LocationToId ??= 0;
                filter.TransDepartmentFrom ??= 0;
                filter.TransDepartmentTo ??= 0;
                filter.BranchFromId ??= 0;
                filter.BranchToId ??= 0;
				//List<HrTransferVM> resultList = new List<HrTransferVM>();
				var items = await hrRepositoryManager.HrTransferRepository.GetAllVw(e => e.IsDeleted == false && 
                (string.IsNullOrEmpty(filter.EmpId) || e.EmpCode == filter.EmpId) &&
                (string.IsNullOrEmpty(filter.EmpName) || e.EmpName == filter.EmpName) &&
                (filter.DeptId == 0 || e.DeptId == filter.DeptId) &&
                (filter.BranchId == 0 || e.BranchId == filter.BranchId || e.BranchId == filter.BranchFromId) &&
                (BranchesList == null || BranchesList.Contains(e.BranchId.ToString())) &&
                (filter.LocationId == 0 || e.Location == filter.LocationId) &&
                (filter.LocationFromId == 0 || e.TransLocationFrom == filter.LocationFromId) &&
                (filter.LocationToId == 0 || e.TransLocationTo == filter.LocationToId) &&
                (filter.TransDepartmentFrom == 0 || e.TransDepartmentFrom == filter.TransDepartmentFrom) &&
                (filter.TransDepartmentTo == 0 || e.TransDepartmentTo == filter.TransDepartmentTo) &&
                (filter.BranchToId == 0 || e.BranchIdTo == filter.BranchToId)
                
                );
				if (items != null)
				{
                    if (items.Count() > 0)
					{
						var res = items.AsQueryable();

						if (!string.IsNullOrEmpty(filter.FromDate))
						{
							res = res.Where(r => r.TransferDate != null && DateHelper.StringToDate(r.TransferDate) >= DateHelper.StringToDate(filter.FromDate));
						}
						if (!string.IsNullOrEmpty(filter.ToDate))
						{
							res = res.Where(r => r.TransferDate != null && DateHelper.StringToDate(r.TransferDate) <= DateHelper.StringToDate(filter.ToDate));
						}
						if (items.Any())
							return await Result<List<HrTransfersVw>>.SuccessAsync(items.ToList(), "");
						return await Result<List<HrTransfersVw>>.SuccessAsync(items.ToList(), localization.GetResource1("NosearchResult"));
					}
					return await Result<List<HrTransfersVw>>.SuccessAsync(items.ToList(), localization.GetResource1("NosearchResult"));
				}
				return await Result<List<HrTransfersVw>>.FailAsync("items.Status.message");
			}
			catch (Exception ex)
			{
				return await Result<List<HrTransfersVw>>.FailAsync(ex.Message);
			}
		}
	}

}
