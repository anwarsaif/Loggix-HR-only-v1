using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Vml.Office;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.TS;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Logix.Application.Services.HR
{
    public class HrIncrementService : GenericQueryService<HrIncrement, HrIncrementDto, HrIncrementsVw>, IHrIncrementService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IWorkflowHelper workflowHelper;



        public HrIncrementService(IQueryRepository<HrIncrement> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.workflowHelper = workflowHelper;
        }



        public async Task<IResult<HrIncrementDto>> Add(HrIncrementDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrIncrementDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpId.ToString() && x.Isdel == false);
                if (checkEmpExist == null)
                    return await Result<HrIncrementDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                if (checkEmpExist.StatusId == 2)
                    return await Result<HrIncrementDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                if (entity.NewJobId == 0)
                    return await Result<HrIncrementDto>.FailAsync(localization.GetMessagesResource("EnterNewJobCode"));

                var item = _mapper.Map<HrIncrement>(entity);

                //var applicationId = await workflowHelper.Send(session.EmpId, 2283, entity.TransTypeId);
                //var applicationId = await workflowHelper.Send(checkEmpExist.Id, 2283, entity.AppId);
                var applicationId = await workflowHelper.Send(session.EmpId, 2283, entity.AppId);
                item.EmpId = checkEmpExist.Id;
                item.AppId = (int)applicationId;
                item.IncreaseDate = entity.IncreaseDate;
                item.Salary = entity.Salary;
                item.Allowances = entity.Allowances;
                item.Deductions = entity.Deductions;
                item.IncreaseAmount = entity.IncreaseAmount;
                item.StartDate = entity.StartDate;
                item.NewSalary = entity.NewSalary;
                item.ApplyType = 1;
                item.Note = entity.Note;
                item.CurCatJobId = entity.CurCatJobId;
                item.CurJobId = entity.CurJobId;
                item.CurLevelId = entity.CurLevelId;
                item.CurGradId = entity.CurGradId;
                item.NewLevelId = entity.NewLevelId;
                item.NewGradId = entity.NewGradId;
                item.NewCatJobId = entity.NewCatJobId;
                item.NewJobId = entity.NewJobId;
                item.DecisionNo = entity.DecisionNo;
                item.DecisionDate = entity.DecisionDate;
                item.TransTypeId = 2;

                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.IsDeleted = false;

                var newEntity = await hrRepositoryManager.HrIncrementRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrIncrementDto>(newEntity);

                return await Result<HrIncrementDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {
                return await Result<HrIncrementDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrIncrementRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrIncrementDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrIncrementRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrIncrementDto>.SuccessAsync(_mapper.Map<HrIncrementDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrIncrementDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrIncrementRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrIncrementDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrIncrementRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrIncrementDto>.SuccessAsync(_mapper.Map<HrIncrementDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrIncrementDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrIncrementEditDto>> Update(HrIncrementEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null) return await Result<HrIncrementEditDto>.FailAsync(localization.GetMessagesResource("UpdateNullEntity"));

                var item = await hrRepositoryManager.HrIncrementRepository.GetById(entity.Id);

                if (item == null)
                    return await Result<HrIncrementEditDto>.FailAsync($"{localization.GetMessagesResource("NoDataWithId")} {entity.Id}");

                _mapper.Map(entity, item);
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpId.ToString() && x.Isdel == false);
                if (checkEmpExist == null)
                    return await Result<HrIncrementEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                if (checkEmpExist.StatusId == 2)
                    return await Result<HrIncrementEditDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                if (entity.NewJobId == 0)
                    return await Result<HrIncrementEditDto>.FailAsync(localization.GetMessagesResource("EnterNewJobCode"));

                //var applicationId = await workflowHelper.Send(session.EmpId, 2283, entity.AppId);
                var applicationId = await workflowHelper.Send(checkEmpExist.Id, 2283, entity.TransTypeId);
                item.EmpId = checkEmpExist.Id;
                item.AppId = (int)applicationId;
                item.IncreaseDate = entity.IncreaseDate;
                item.Salary = entity.Salary;
                item.Allowances = entity.Allowances;
                item.Deductions = entity.Deductions;
                item.IncreaseAmount = entity.IncreaseAmount;
                item.StartDate = entity.StartDate;
                item.NewSalary = entity.NewSalary;
                item.ApplyType = 1;
                item.Note = entity.Note;
                item.CurCatJobId = entity.CurCatJobId;
                item.CurJobId = entity.CurJobId;
                item.CurLevelId = entity.CurLevelId;
                item.CurGradId = entity.CurGradId;
                item.NewLevelId = entity.NewLevelId;
                item.NewGradId = entity.NewGradId;
                item.NewCatJobId = entity.NewCatJobId;
                item.NewJobId = entity.NewJobId;
                item.DecisionNo = entity.DecisionNo;
                item.DecisionDate = entity.DecisionDate;
                item.TransTypeId = 2;

                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.IsDeleted = false;

                hrRepositoryManager.HrIncrementRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrIncrementEditDto>.SuccessAsync(_mapper.Map<HrIncrementEditDto>(item), localization.GetMessagesResource("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrIncrementEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public async Task<IResult<string>> ApplyIncrement(long IncrementId, int TransTypeID, CancellationToken cancellationToken = default)
        {
            try
            {
                var StartDate = string.Empty;
                var IncrementItem = await hrRepositoryManager.HrIncrementRepository.GetOne(x => x.Id == IncrementId);
                if (IncrementItem == null) return Result<string>.Fail($"{localization.GetMessagesResource("NoIdInUpdate")}");
                StartDate = IncrementItem.StartDate;
                await mainRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var GetEmployee = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.Id == IncrementItem.EmpId && x.IsDeleted == false && x.Isdel == false);
                if (GetEmployee == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (GetEmployee.StatusId == 2) return await Result<string>.FailAsync(localization.GetResource1("EmpNotActive"));

                // Check_Emp_Exists_In_Payroll
                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == IncrementItem.EmpId && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false);

                if (IfEmpExistsInPayroll.Any())
                {

                    var msDateStr = IncrementItem.StartDate;

                    var filterResult = IfEmpExistsInPayroll
                                    .Where(e =>
                                    {
                                        // تحويل التواريخ مع التحقق من الصحة
                                        bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                        bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                        bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                        return isDateValid && isStartDateValid && isEndDateValid &&
                                               msDate >= startDate && msDate <= endDate;
                                    })
                                    .ToList();
                    if (filterResult.Any())
                    {
                        return await Result<string>.FailAsync(localization.GetMessagesResource("CannotApplyIncrementDueToPayroll"));
                    }
                }
                IncrementItem.ApplyType = 1;
                hrRepositoryManager.HrIncrementRepository.Update(IncrementItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                
                GetEmployee.Salary = IncrementItem.NewSalary;
                GetEmployee.DegreeId = (int?)IncrementItem.NewGradId;
                GetEmployee.JobId = IncrementItem.NewJobId;
                GetEmployee.LevelId = (int?)IncrementItem.NewLevelId;
                if (TransTypeID == 1)
                {
                    GetEmployee.JobCatagoriesId = (int?)IncrementItem.CurCatJobId;
                }
                else
                {
                    GetEmployee.JobCatagoriesId = (int?)IncrementItem.NewCatJobId;
                }
                GetEmployee.LastIncrementDate = StartDate;
                GetEmployee.LastPromotionDate = StartDate;
                mainRepositoryManager.InvestEmployeeRepository.Update(GetEmployee);
                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);              
                ////////////////////////////////////////////////////////////

                var getAllIncrementAllowanceDeduction = await hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.GetAll(x => x.IsDeleted == false && x.IncrementId == IncrementId);
                var getTotalAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(e => e.EmpId == GetEmployee.Id && e.IsDeleted == false && e.FixedOrTemporary == 1);

                if (getAllIncrementAllowanceDeduction.Count() > 0)
                {
                    foreach (var item in getAllIncrementAllowanceDeduction)
                    {
                        var checkIfExist = getTotalAllowanceDeduction.Where(x => x.TypeId == item.TypeId && x.AdId == item.AdId).FirstOrDefault();
                        //      تعديل البدلات الجديدة او الحسميات الجديدة التي لم تطبق عليها العلاوات

                        if (checkIfExist != null)
                        {
                            checkIfExist.Rate = item.Rate;
                            checkIfExist.Amount = item.NewAmount;
                            checkIfExist.ModifiedBy = session.UserId;
                            checkIfExist.ModifiedOn = DateTime.Now;
                            checkIfExist.IsDeleted = false;
                            hrRepositoryManager.HrAllowanceDeductionRepository.Update(checkIfExist);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                        else
                        {
                            //      اضافة البدلات الجديدة او الحسميات الجديدة التي لم تسند للموظف سابقاً
                            var newAllowanceDeduction = new HrAllowanceDeduction
                            {
                                AdId = item.AdId,
                                TypeId = item.TypeId,
                                Rate = item.Rate,
                                Amount = item.NewAmount,
                                IsDeleted = false,
                                FixedOrTemporary = 1,
                                MAdId = 0,
                                Status = true,
                                PreparationSalariesId = 0,
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,
                                EmpId = GetEmployee.Id,
                            };
                            await hrRepositoryManager.HrAllowanceDeductionRepository.Add(newAllowanceDeduction);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }
                }
                ////////////////////////////////////////////////////////////////
                await mainRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("UpdateSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in ApplyIncrement at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrIncrementDto>> Add(HrIncrementsAddDto Entity, CancellationToken cancellationToken = default)
        {
            if (Entity == null) return await Result<HrIncrementDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                long? appId = 0;
                decimal IncreaseRate = 0;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                Entity.IncreaseAmount ??= 0;
                Entity.IncrementRate ??= 0;
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == Entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrIncrementDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmpExist.StatusId == 2) return await Result<HrIncrementDto>.FailAsync(localization.GetResource1("EmpNotActive"));
                // Check_Emp_Exists_In_Payroll
                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmpExist.Id && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                if (Entity.ChkRetroactiveAmount == false)
                {
                    if (IfEmpExistsInPayroll.Count() > 0)
                    {
                        var msDateStr = Entity.StartDate;
                        var filterResult = IfEmpExistsInPayroll
                                        .Where(e =>
                                        {
                                            // تحويل التواريخ مع التحقق من الصحة
                                            bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                            bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                            bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                            return isDateValid && isStartDateValid && isEndDateValid &&
                                                   msDate >= startDate && msDate <= endDate;
                                        })
                                        .ToList();
                        if (filterResult.Count() > 0)
                        {
                            return await Result<HrIncrementDto>.FailAsync(localization.GetMessagesResource("CannotAddIncrementDueToPayroll"));
                        }
                    }
                }

                if (Entity.ChkRetroactiveAmount == true)
                {
                    if (!string.IsNullOrEmpty(Entity.DateRetroactiveAmount))
                    {
                        if (IfEmpExistsInPayroll.Count() > 0)
                        {
                            var msDateStr = Entity.DateRetroactiveAmount;

                            var filterResult = IfEmpExistsInPayroll
                                            .Where(e =>
                                            {
                                                // تحويل التواريخ مع التحقق من الصحة
                                                bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                                bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                                bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                                return isDateValid && isStartDateValid && isEndDateValid &&
                                                       msDate >= startDate && msDate <= endDate;
                                            })
                                            .ToList();
                            if (filterResult.Count() > 0)
                            {
                                return await Result<HrIncrementDto>.FailAsync(localization.GetMessagesResource("CannotProcessRetroactiveDueToPayroll"));
                            }
                        }
                    }
                    else
                    {
                        return await Result<HrIncrementDto>.FailAsync(localization.GetMessagesResource("RetroactivePayDateRequired"));
                    }
                }

                if (Entity.ApplyType == 1)
                {
                    if (Entity.DifferenceAmount != null && Entity.DifferenceAmount > 0)
                    {
                        if (IfEmpExistsInPayroll.Count() > 0)
                        {

                            var msDateStr = Entity.DeductionDate;

                            var filterResult = IfEmpExistsInPayroll
                                            .Where(e =>
                                            {
                                                // تحويل التواريخ مع التحقق من الصحة
                                                bool isDateValid = DateTime.TryParseExact(msDateStr, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime msDate);
                                                bool isStartDateValid = DateTime.TryParseExact(e.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                                bool isEndDateValid = DateTime.TryParseExact(e.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                                return isDateValid && isStartDateValid && isEndDateValid &&
                                                       msDate >= startDate && msDate <= endDate;
                                            })
                                            .ToList();
                            if (filterResult.Count() > 0)
                            {
                                return await Result<HrIncrementDto>.FailAsync(localization.GetMessagesResource("CannotAddDeductionDueToPayroll"));

                            }
                        }
                    }

                }

                Entity.AppTypeId ??= 0;


                //  ارسال الى سير العمل

                var GetApp_ID = await workflowHelper.Send(checkEmpExist.Id, 273, Entity.AppTypeId);
                appId = GetApp_ID;

                var newIncrementEntity = new HrIncrement
                {
                    AppId = (int?)appId,
                    EmpId = checkEmpExist.Id,
                    IncreaseDate = Entity.IncreaseDate,
                    Salary = checkEmpExist.Salary,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    Allowances = Entity.Allowances,
                    Deductions = Entity.Deductions,
                    IncreaseAmount = Entity.IncreaseAmount,
                    StartDate = Entity.StartDate,
                    NewSalary = (checkEmpExist.Salary ?? 0) + (Entity.IncreaseAmount ?? 0),
                    ApplyType = Entity.ApplyType,
                    Note = Entity.Note,
                    CurCatJobId = Entity.CurCatJobId ?? 0,
                    CurGradId = Entity.CurGradId ?? 0,
                    CurJobId = Entity.CurJobId ?? 0,
                    CurLevelId = Entity.CurLevelId ?? 0,
                    NewCatJobId = Entity.NewCatJobId ?? 0,
                    NewGradId = Entity.NewGradId ?? 0,
                    NewJobId = Entity.NewJobId ?? 0,
                    NewLevelId = Entity.NewLevelId ?? 0,
                    DecisionDate = Entity.DecisionDate ?? null,
                    DecisionNo = Entity.DecisionNo,
                    IsDeleted = false,
                    TransTypeId = Entity.TransTypeId
                };
                var newEntity = await hrRepositoryManager.HrIncrementRepository.AddAndReturn(newIncrementEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //  معالجة البدلات
                if (Entity.allowancesList.Count() > 0)
                {

                    foreach (var allowanceitem in Entity.allowancesList)
                    {
                        var newIncrementsAllowanceDeduction = new HrIncrementsAllowanceDeduction
                        {
                            IncrementId = newEntity.Id,
                            AdId = allowanceitem.AdId,
                            TypeId = 1,
                            Rate = allowanceitem.Rate,
                            Amount = allowanceitem.Amount,
                            IsDeleted = allowanceitem.IsDeleted ?? false,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            NewRate = 0,
                            NewAmount = allowanceitem.NewAmount,
                            AllDedId = allowanceitem.Id,
                            IsNew = allowanceitem.IsNew
                        };
                        if (newIncrementsAllowanceDeduction.IsDeleted == false)
                        {
                            if (allowanceitem.Amount == allowanceitem.NewAmount)
                            {
                                allowanceitem.IsUpdated = false;
                            }
                            else
                            {
                                allowanceitem.IsUpdated = true;
                            }
                            newIncrementsAllowanceDeduction.Status = false;
                            var newHrIncrementAllowance = await hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.AddAndReturn(newIncrementsAllowanceDeduction);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                            if (Entity.ApplyType == 1)
                            {

                                var updateAllowanceDeduction = new HrAllowanceDeduction
                                {
                                    TypeId = 1,
                                    AdId = allowanceitem.AdId,
                                    Rate = allowanceitem.Rate,
                                    Amount = allowanceitem.NewAmount,
                                    CreatedBy = session.UserId,
                                    CreatedOn = DateTime.Now,
                                    EmpId = checkEmpExist.Id,
                                    FixedOrTemporary = 1,
                                    MAdId = 0,
                                    Id = allowanceitem.Id,
                                };
                                if (updateAllowanceDeduction.Id == 0)
                                {
                                    updateAllowanceDeduction.Note = localization.GetMessagesResource("AddFromIncrement");

                                    var ad = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(updateAllowanceDeduction);
                                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    newIncrementsAllowanceDeduction.AllDedId = ad.Id;

                                    //newIncrementsAllowanceDeduction.Id = newHrIncrementAllowance.Id;
                                    hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.Update(newIncrementsAllowanceDeduction);

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
                                        tracked.TypeId = 1; // or 2 depending on section
                                        tracked.FixedOrTemporary = 1;
                                        tracked.MAdId = 0;
                                        tracked.Note = localization.GetMessagesResource("UpdateFromIncrement");

                                        hrRepositoryManager.HrAllowanceDeductionRepository.Update(tracked);
                                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (newIncrementsAllowanceDeduction.IsNew == false)
                            {
                                newIncrementsAllowanceDeduction.IsUpdated = true;

                                await hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.Add(newIncrementsAllowanceDeduction);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                if (Entity.ApplyType == 1)
                                {

                                    var getAllDed = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(allowanceitem.Id);
                                    if (getAllDed != null)
                                    {
                                        getAllDed.IsDeleted = true;
                                        getAllDed.ModifiedBy = session.UserId;

                                        hrRepositoryManager.HrAllowanceDeductionRepository.Update(getAllDed);
                                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    }
                                }
                            }
                        }
                    }
                }


                // معالجة الحسميات
                if (Entity.deductionsList.Count() > 0)
                {

                    foreach (var deductionsitem in Entity.deductionsList)
                    {
                        var newIncrementsAllowanceDeduction = new HrIncrementsAllowanceDeduction
                        {
                            IncrementId = newEntity.Id,
                            AdId = deductionsitem.AdId,
                            TypeId = 2,
                            Rate = deductionsitem.Rate,
                            Amount = deductionsitem.Amount,
                            IsDeleted = deductionsitem.IsDeleted ?? false,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            NewRate = 0,
                            NewAmount = deductionsitem.NewAmount,
                            AllDedId = deductionsitem.Id,
                            IsNew = deductionsitem.IsNew
                        };
                        if (newIncrementsAllowanceDeduction.IsDeleted == false)
                        {
                            if (deductionsitem.Amount == deductionsitem.NewAmount)
                            {
                                deductionsitem.IsUpdated = false;
                            }
                            else
                            {
                                deductionsitem.IsUpdated = true;
                            }
                            newIncrementsAllowanceDeduction.Status = false;
                            var newHrIncrementAllowance = await hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.AddAndReturn(newIncrementsAllowanceDeduction);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                            if (Entity.ApplyType == 1)
                            {

                                var updateAllowanceDeduction = new HrAllowanceDeduction
                                {
                                    TypeId = 2,
                                    AdId = deductionsitem.AdId,
                                    Rate = deductionsitem.Rate,
                                    Amount = deductionsitem.NewAmount,
                                    CreatedBy = session.UserId,
                                    CreatedOn = DateTime.Now,
                                    EmpId = checkEmpExist.Id,
                                    FixedOrTemporary = 1,
                                    MAdId = 0,
                                    Id = deductionsitem.Id,
                                };
                                if (updateAllowanceDeduction.Id == 0)
                                {
                                    updateAllowanceDeduction.Note = localization.GetMessagesResource("AddFromIncrement");

                                    var ad = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(updateAllowanceDeduction);
                                    
                                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    newIncrementsAllowanceDeduction.AllDedId = ad.Id;
                                    //newIncrementsAllowanceDeduction.Id = newHrIncrementAllowance.Id;
                                    hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.Update(newIncrementsAllowanceDeduction);

                                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                }
                                else
                                {
                                    //updateAllowanceDeduction.Note = localization.GetMessagesResource("UpdateFromIncrement");

                                    //hrRepositoryManager.HrAllowanceDeductionRepository.Update(updateAllowanceDeduction);
                                    //await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    var tracked = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(deductionsitem.Id);

                                    if (tracked != null)
                                    {
                                        tracked.Amount = deductionsitem.NewAmount;
                                        tracked.Rate = 0;
                                        tracked.ModifiedBy = session.UserId;
                                        tracked.ModifiedOn = DateTime.Now;
                                        tracked.TypeId = 2;
                                        tracked.FixedOrTemporary = 1;
                                        tracked.MAdId = 0;
                                        tracked.Note = localization.GetMessagesResource("UpdateFromIncrement");

                                        hrRepositoryManager.HrAllowanceDeductionRepository.Update(tracked);
                                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    }

                                }
                            }

                        }
                        else
                        {
                            if (newIncrementsAllowanceDeduction.IsNew == false)
                            {
                                newIncrementsAllowanceDeduction.IsUpdated = true;

                                await hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.Add(newIncrementsAllowanceDeduction);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                if (Entity.ApplyType == 1)
                                {

                                    var getAllDed = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(deductionsitem.Id);
                                    if (getAllDed != null)
                                    {
                                        getAllDed.IsDeleted = true;
                                        getAllDed.ModifiedBy = session.UserId;

                                        hrRepositoryManager.HrAllowanceDeductionRepository.Update(getAllDed);
                                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    }
                                }
                            }
                        }
                    }
                }

                //  اذا تم احتساب  فارق العلاوة
                if (Entity.ApplyType == 1 && Entity.ChkRetroactiveAmount == false)
                {
                    //if(Entity)
                    if (Entity.DifferenceAmount != null && Entity.DifferenceAmount > 0)
                    {
                        var newAllowanceDeduction = new HrAllowanceDeduction
                        {
                            AdId = Entity.DeductionType,
                            TypeId = 2,
                            Rate = 0,
                            Amount = Entity.DifferenceAmount,
                            IsDeleted = false,
                            FixedOrTemporary = 2,
                            MAdId = 0,
                            Note = Entity.Note + " " + localization.GetMessagesResource("AllowanceDifference") + " " + localization.GetMessagesResource("AddFromIncrement") ?? "",
                            DueDate = Entity.DeductionDate,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            EmpId = checkEmpExist.Id,
                        };
                        var allDed = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(newAllowanceDeduction);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        var newIncrementsAllowanceDeduction = new HrIncrementsAllowanceDeduction
                        {
                            IncrementId = newEntity.Id,
                            AdId = Entity.DeductionType,
                            TypeId = 2,
                            Rate = 0,
                            Amount = 0,
                            IsDeleted = false,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            NewRate = 0,
                            NewAmount = Entity.DifferenceAmount,
                            AllDedId = allDed.Id,
                            IsNew = true
                        };

                        var newHrIncrementAllowance = await hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.AddAndReturn(newIncrementsAllowanceDeduction);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                }

                if (Entity.ApplyType == 1 && Entity.ChkRetroactiveAmount == true)
                {
                    //if(Entity)
                    if (Entity.ChkRetroactiveAmount != null && Entity.TxtRetroactiveAmount > 0)
                    {
                        var newAllowanceDeduction = new HrAllowanceDeduction
                        {
                            AdId = Entity.DDLallowanceRetroactiveAmount,
                            TypeId = 1,
                            Rate = 0,
                            Amount = Entity.TxtRetroactiveAmount,
                            IsDeleted = false,
                            FixedOrTemporary = 2,
                            Note = Entity.Note + " " + localization.GetMessagesResource("CalculateRetroactivePay") + " " + localization.GetMessagesResource("AddFromIncrement") ?? "",
                            DueDate = Entity.DeductionDate,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            EmpId = checkEmpExist.Id,
                        };
                        var allDed = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(newAllowanceDeduction);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        var newIncrementsAllowanceDeduction = new HrIncrementsAllowanceDeduction
                        {
                            IncrementId = newEntity.Id,
                            AdId = Entity.DDLallowanceRetroactiveAmount,
                            TypeId = 1,
                            Rate = 0,
                            Amount = 0,
                            IsDeleted = false,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            NewRate = 0,
                            NewAmount = Entity.TxtRetroactiveAmount,
                            AllDedId = allDed.Id,
                            IsNew = true
                        };

                        var newHrIncrementAllowance = await hrRepositoryManager.HrIncrementsAllowanceDeductionRepository.AddAndReturn(newIncrementsAllowanceDeduction);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                }

                if (Entity.ChkUpdateInsuranceInEmployeeFile == true)
                {
                    var allawanceList = Entity.allowancesList;
                    decimal House_Allowance = 0;

                    if (allawanceList != null && allawanceList.Count() > 0)
                    {
                        int Housing_allowance = 0;
                        var getHrSettings = await hrRepositoryManager.HrSettingRepository.GetAll(x => x.FacilityId == session.FacilityId);
                        Housing_allowance = getHrSettings.Select(x => x.HousingAllowance).FirstOrDefault() ?? 0;
                        var rows = allawanceList.Where(x => x.AdId == Housing_allowance);
                        if (rows.Count() > 0)
                        {
                            House_Allowance = rows.FirstOrDefault().NewAmount ?? 0;
                        }
                    }

                    var emp = await mainRepositoryManager.InvestEmployeeRepository.GetById(checkEmpExist.Id);
                    if(emp != null)
                    {
                        emp.GosiBiscSalary = Entity.NewSalary;
                        emp.GosiHouseAllowance = House_Allowance;
                        emp.ModifiedBy = session.UserId;
                        emp.ModifiedOn = DateTime.Now;
                        mainRepositoryManager.InvestEmployeeRepository.Update(emp);
                        await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);


                return await Result<HrIncrementDto>.SuccessAsync(_mapper.Map<HrIncrementDto>(newEntity), localization.GetResource1("AddSuccess"));

            }
            catch (Exception exp)
            {

                return await Result<HrIncrementDto>.FailAsync($"EXP in Add at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<HrEmployeeIncremenResultDto>>> IncrementsEvaluationsSearch(IncrementsBothFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_Increments_During_Time_SP(filter);
                if (result.Count() > 0)
                    return await Result<IEnumerable<HrEmployeeIncremenResultDto>>.SuccessAsync(result, "");
                return await Result<IEnumerable<HrEmployeeIncremenResultDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<HrEmployeeIncremenResultDto>>.FailAsync($"EXP in IncrementsEvaluationsSearch at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> MakeApprove(List<MakeApproveDto> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await mainRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var getDateForPackageNumber = await hrRepositoryManager.HrIncrementRepository.GetAll();
                var PackageNumber = getDateForPackageNumber.Select(x => (int?)x.PackageNumber ?? 0).DefaultIfEmpty(0).Max() + 1;
                var hrIncrements = await hrRepositoryManager.HrIncrementRepository.GetAll(x => x.IsDeleted == false && x.PackageNumber == PackageNumber);
                var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(x => x.IsDeleted == false);

                foreach (var entity in entities)
                {
                    if (entity.increase_Amount <= 0 || entity.NewSalary <= 0 || entity.new_Grad_ID <= 0) continue;


                    var filteredIncrements = hrIncrements.Where(x => x.IncreaseDate == entity.ToDate);
                    foreach (var inc in filteredIncrements)
                    {
                        var ie = investEmployees.FirstOrDefault(x => x.Id == inc.EmpId);
                        if (ie != null)
                        {
                            ie.Salary = inc.NewSalary;
                            ie.DegreeId = (int?)inc.NewGradId;
                            ie.LastIncrementDate = entity.ToDate;
                            mainRepositoryManager.InvestEmployeeRepository.Update(ie);
                            await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }

                    }

                }
                await mainRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("ActionSuccess"));

            }
            catch (Exception exp)
            {

                return await Result<string>.FailAsync($"EXP in MakeApprove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<List<HrIncrementsVw>>> Search(HrIncrementFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                filter.BranchId ??= 0;
                filter.DeptId ??= 0;
                filter.Location ??= 0;
                filter.TransactionType ??= 0;
                var BranchesList = session.Branches.Split(',');

                var items = await hrRepositoryManager.HrIncrementRepository.GetAllVw(e => e.IsDeleted == false
                && (filter.BranchId != 0 ? e.BranchId == filter.BranchId : BranchesList.Contains(e.BranchId.ToString()))
                && (filter.DeptId == 0 || e.DeptId == filter.DeptId)
                && (filter.Location == 0 || e.Location == filter.Location)
                && (filter.TransactionType == 0 || e.TransTypeId == filter.TransactionType)
                && (string.IsNullOrEmpty(filter.EmpName) || e.EmpName == filter.EmpName)
                && (string.IsNullOrEmpty(filter.EmpCode) || e.EmpCode == filter.EmpCode)
                );
                if (items != null)
                {
                    if (items.Count() > 0)
                    {
                        var res = items.AsQueryable();

                        if (!string.IsNullOrEmpty(filter.From) && !string.IsNullOrEmpty(filter.To))
                        {
                            res = res.Where(c => (DateHelper.StringToDate(filter.From) <= DateHelper.StringToDate(c.StartDate)) &&
                             (DateHelper.StringToDate(filter.To) >= DateHelper.StringToDate(c.StartDate))
                            );
                        }
                        if (res.Count() > 0) return await Result<List<HrIncrementsVw>>.SuccessAsync(res.ToList(), "");

                        return await Result<List<HrIncrementsVw>>.SuccessAsync(res.ToList(), localization.GetResource1("NosearchResult"));
                    }

                    return await Result<List<HrIncrementsVw>>.SuccessAsync(items.ToList(), localization.GetResource1("NosearchResult"));

                }

                return await Result<List<HrIncrementsVw>>.FailAsync("");
            }
            catch (Exception ex)
            {
                return await Result<List<HrIncrementsVw>>.FailAsync(ex.Message);
            }
        }
    }

}
