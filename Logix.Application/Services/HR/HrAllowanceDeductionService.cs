using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Globalization;
using IResult = Logix.Application.Wrapper.IResult;

namespace Logix.Application.Services.HR
{
    public class HrAllowanceDeductionService : GenericQueryService<HrAllowanceDeduction, HrAllowanceDeductionDto, HrAllowanceDeductionVw>, IHrAllowanceDeductionService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData currentData;
        private readonly ILocalizationService localization;


        public HrAllowanceDeductionService(IQueryRepository<HrAllowanceDeduction> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData currentData, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.currentData = currentData;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrAllowanceDeductionDto>> Add(HrAllowanceDeductionDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrAllowanceDeductionDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                var item = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (item == null) return await Result<HrAllowanceDeductionDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                entity.CreatedBy = currentData.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.EmpId = item.Id;

                var newEntity = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(_mapper.Map<HrAllowanceDeduction>(entity));

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrAllowanceDeductionDto>(newEntity);

                return await Result<HrAllowanceDeductionDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrAllowanceDeductionDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<HrAllowanceDeductionDto>> AddOneEdit(HrAllowanceDeductionExtraVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAllowanceDeductionDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                var checkIFExist = await hrRepositoryManager.HrAllowanceDeductionRepository.GetOne(x => x.EmpId == entity.EmpId && x.TypeId == entity.TypeId && x.AdId == entity.TypeId);
                if (checkIFExist != null) return await Result<HrAllowanceDeductionDto>.FailAsync("العنصر موجود مسبقا");


                var newEntity = new HrAllowanceDeduction
                {
                    CreatedBy = currentData.UserId,
                    CreatedOn = DateTime.Now,
                    Rate = entity.Rate,
                    Amount = entity.Amount,
                    TypeId = entity.TypeId,
                    FixedOrTemporary = 1,
                    EmpId = entity.EmpId

                };
                var naddEntity = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(newEntity);

                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrAllowanceDeductionDto>(newEntity);

                return await Result<HrAllowanceDeductionDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {
                return await Result<HrAllowanceDeductionDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<HrAllowanceDeductionDto>> AddYearlyAllowanceDeduction(HrAllowanceDeductionDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAllowanceDeductionDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {

                var item = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (item == null) return await Result<HrAllowanceDeductionDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (string.IsNullOrEmpty(entity.ContractDate)) await Result<HrAllowanceDeductionDto>.FailAsync("تاريخ العقد مطلوب");
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.EmpId = item.Id;
                entity.Rate = 0;
                var DateGregorian = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                entity.StartDate = DateGregorian.Substring(0, 4) + "/" +
                                    DateHelper.StringToDate(entity.ContractDate).Month.ToString("00") + "/" +
                                    DateHelper.StringToDate(entity.ContractDate).Day.ToString("00");
                entity.EndDate = DateHelper.StringToDate(entity.StartDate).AddDays(364).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                // Chk if Allowance Deduction Exists
                var ChkAllowanceDeductionExists = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(x => x.IsDeleted == false && x.EmpId == item.Id && x.TypeId == entity.TypeId && x.AdId == entity.AdId && x.FixedOrTemporary == entity.FixedOrTemporary && x.DueDate != null);
                if (ChkAllowanceDeductionExists.Count() > 0)
                {
                    var filterdata = ChkAllowanceDeductionExists.Where(x => DateHelper.StringToDate(x.DueDate) >= DateHelper.StringToDate(entity.StartDate) && DateHelper.StringToDate(x.DueDate) <= DateHelper.StringToDate(entity.EndDate));
                    if (filterdata.Count() > 0)
                    {
                        return await Result<HrAllowanceDeductionDto>.FailAsync(localization.GetResource1("RecodeExists"));

                    }
                }
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync();
                //  HR_Allowance_Deduction_M   هنا سنقوم بالادخال الى جدول
                var newAllowanceDeductionM = new HrAllowanceDeductionM()
                {
                    EmpId = entity.EmpId,
                    TypeId = entity.TypeId,
                    AdId = entity.AdId,
                    Rate = entity.Rate,
                    Amount = entity.Amount,
                    FixedOrTemporary = entity.FixedOrTemporary,
                    Note = entity.Note,
                    CreatedBy = currentData.UserId,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,

                };
                var addNewAllowanceDeductionM = await hrRepositoryManager.HrAllowanceDeductionMRepository.AddAndReturn(newAllowanceDeductionM);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //Insert Into Allowance_Deduction Table
                for (int i = 0; i <= (12 / entity.FixedOrTemporary) - 1; i++)
                {
                    var duedate = DateHelper.StringToDate(entity.StartDate);
                    var dueDataAsString = duedate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    if (i != 0)
                    {
                        dueDataAsString = duedate.AddMonths((int)(entity.FixedOrTemporary * i)).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    }

                    var newAllowanceDeductionTranc = new HrAllowanceDeduction
                    {
                        EmpId = entity.EmpId,
                        AdId = entity.AdId,
                        MAdId = addNewAllowanceDeductionM.Id,
                        TypeId = entity.TypeId,
                        Rate = 0,
                        Amount = entity.Amount / (12 / entity.FixedOrTemporary),
                        FixedOrTemporary = 2,
                        Note = entity.Note,
                        DueDate = dueDataAsString,
                        CreatedBy = currentData.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false
                    };
                    await hrRepositoryManager.HrAllowanceDeductionRepository.Add(newAllowanceDeductionTranc);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync();
                return await Result<HrAllowanceDeductionDto>.SuccessAsync(localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrAllowanceDeductionDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(Id);
                if (item == null) return Result<HrAllowanceDeductionDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                if (item.IsDeleted == true) return Result<HrAllowanceDeductionDto>.Fail($"البدل محذوف مسبقا");

                item.ModifiedBy = currentData.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrAllowanceDeductionRepository.Update(item);

                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAllowanceDeductionDto>.SuccessAsync(_mapper.Map<HrAllowanceDeductionDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrAllowanceDeductionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(Id);
                if (item == null) return Result<HrAllowanceDeductionDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                if (item.IsDeleted == true) return Result<HrAllowanceDeductionDto>.Fail($"البدل محذوف مسبقا");
                item.ModifiedBy = currentData.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrAllowanceDeductionRepository.Update(item);

                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAllowanceDeductionDto>.SuccessAsync(_mapper.Map<HrAllowanceDeductionDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrAllowanceDeductionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrAllowanceDeductionDto>> RemoveOtherDeductionAllowance(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrAllowanceDeductionRepository.GetOne(x => x.Id == Id && x.IsDeleted == false && x.Status == true && x.FixedOrTemporary == 2);
                if (item == null) return Result<HrAllowanceDeductionDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}{Id}");
                // Check_Emp_Exists_In_Payroll
                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.Check_Emp_Exists_In_Payroll(item.DueDate, 1, item.EmpId ?? 0);
                if (IfEmpExistsInPayroll > 0)
                {


                    return await Result<HrAllowanceDeductionDto>.FailAsync($"{localization.GetMessagesResource("deletAllowancesanddeductions")}"); ;
                }

                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)currentData.UserId;
                hrRepositoryManager.HrAllowanceDeductionRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAllowanceDeductionDto>.SuccessAsync(_mapper.Map<HrAllowanceDeductionDto>(item), localization.GetResource1("DeleteSuccess"));

            }
            catch (Exception)
            {
                return await Result<HrAllowanceDeductionDto>.FailAsync(localization.GetResource1("DeleteFail"));
            }
        }

        public async Task<IResult<string>> RemoveOtherDeductionAllowance(List<long> Ids, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Ids.Count <= 0) return await Result<string>.FailAsync($"{localization.GetMessagesResource("ChooseDelete")}");
                var Allitem = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(x => x.IsDeleted == false);
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                foreach (var SingleRecorditem in Ids)
                {
                    var item = Allitem.Where(x => x.Id == SingleRecorditem).FirstOrDefault();
                    if (item == null) return Result<string>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}{SingleRecorditem}");

                    // Check_Emp_Exists_In_Payroll

                    var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.Check_Emp_Exists_In_Payroll(item.DueDate, 1, item.EmpId ?? 0);


                    if (IfEmpExistsInPayroll > 0)
                    {

                        return await Result<string>.FailAsync($"{localization.GetMessagesResource("deletAllowancesanddeductions")}"); ;


                    }

                    item.IsDeleted = true;
                    item.ModifiedOn = DateTime.Now;
                    item.ModifiedBy = (int)currentData.UserId;
                    hrRepositoryManager.HrAllowanceDeductionRepository.Update(item);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync($"{localization.GetResource1("Deletedforanumber")} {Ids.Count} {localization.GetResource1("Row")}", localization.GetResource1("DeleteSuccess")
                );

            }
            catch (Exception)
            {
                return await Result<string>.FailAsync(localization.GetResource1("DeleteFail"));
            }
        }

        public Task<IResult<HrAllowanceDeductionEditDto>> Update(HrAllowanceDeductionEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<IResult<HrAllowanceDeductionDto>> AddOtherAllowanceDeduction(HrOtherAllowanceDeductionAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAllowanceDeductionDto>.FailAsync(localization.GetMessagesResource("AddNullEntity"));
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // employee check Exist  and status
                var employee = await hrRepositoryManager.HrEmployeeRepository.GetEmpByCode(entity.EmpCode, currentData.FacilityId);
                // employee check Exist  
                if (employee == null)
                    return await Result<HrAllowanceDeductionDto>.WarningAsync(localization.GetResource1("EmployeeNotFound"));
                // employee status
                if (employee.StatusId == 2)
                    return await Result<HrAllowanceDeductionDto>.WarningAsync(localization.GetResource1("EmpNotActive"));
                // Check_Emp_Exists_In_Payroll

                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.Check_Emp_Exists_In_Payroll(entity.DueDate, 1, employee.Id);


                if (IfEmpExistsInPayroll > 0)
                {

                    return await Result<HrAllowanceDeductionDto>.WarningAsync(localization.GetMessagesResource("cannot_add_allowance_or_deduction"));

                }

                // Convert TypeId logic (like VB: 20 => 1, else 2)

                entity.TypeId = entity.TypeId == 20 ? 1 : 2;

                // Check if an allowance or deduction record already exists

                var allAllowanceDeductionExit = await hrRepositoryManager.HrAllowanceDeductionRepository.Chk_Allowance_Deduction_Exists_2(employee.Id, entity.TypeId ?? 0, entity.AdId ?? 0, 2, entity.DueDate);

                if (allAllowanceDeductionExit > 0)
                {
                    return await Result<HrAllowanceDeductionDto>.WarningAsync(localization.GetResource1("RecodeExists"));
                }
                entity.CreatedBy = currentData.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                var newEntity = _mapper.Map<HrAllowanceDeduction>(entity);
                newEntity.EmpId = employee.Id;
                newEntity.Rate = 0;
                newEntity.FixedOrTemporary = 2;
                newEntity.MAdId = 0;
                var addedEntity = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(newEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, addedEntity.Id, 49);

                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var dto = _mapper.Map<HrAllowanceDeductionDto>(addedEntity);
                return await Result<HrAllowanceDeductionDto>.SuccessAsync(dto, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception ex)
            {
                await hrRepositoryManager.UnitOfWork.RollbackTransactionAsync(cancellationToken);
                return await Result<HrAllowanceDeductionDto>.FailAsync($"EXP in {this.GetType()}, Message: {ex.Message}");
            }
        }

        public async Task<IResult<HrAllowanceDeductionEditDto>> EditOtherAllowanceDeduction(HrOtherAllowanceDeductionEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAllowanceDeductionEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrAllowanceDeductionEditDto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));

            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrAllowanceDeductionEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));


                // employee check Exist  and status
                var employee = await hrRepositoryManager.HrEmployeeRepository.GetEmpByCode(entity.EmpCode, currentData.FacilityId);
                // employee check Exist  
                if (employee == null)
                    return await Result<HrAllowanceDeductionEditDto>.WarningAsync(localization.GetResource1("EmployeeNotFound"));


                var item = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(entity.Id);

                if (item == null) return await Result<HrAllowanceDeductionEditDto>.WarningAsync(localization.GetResource1("UpdateError"));
                // Chk_Allowance_Deduction_Exists_2
                var ChkAllowanceDeductionExists = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(e => e.EmpId == checkEmpExist.Id && e.IsDeleted == false && e.TypeId == entity.TypeId && e.AdId == entity.AdId && e.FixedOrTemporary == 2 && e.DueDate == entity.DueDate);
                if (ChkAllowanceDeductionExists.Count() > 1)
                {
                    return await Result<HrAllowanceDeductionEditDto>.WarningAsync(localization.GetResource1("RecodeExists"));
                }
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                _mapper.Map(entity, item);
                int Type_ID;
                if (entity.TypeId == 20)
                    Type_ID = 1;
                else
                    Type_ID = 2;
                item.TypeId = Type_ID;
                item.AdId = entity.AdId;
                item.Rate = 0;
                item.Amount = entity.Amount;
                item.CreatedBy = currentData.UserId;
                item.EmpId = checkEmpExist.Id;
                item.FixedOrTemporary = 2;
                item.Note = entity.Note;
                item.DueDate = entity.DueDate;
                item.ModifiedBy = currentData.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrAllowanceDeductionRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, entity.Id, 49);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                // End ChangeStatus_Payroll_Trans
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrAllowanceDeductionEditDto>.SuccessAsync(_mapper.Map<HrAllowanceDeductionEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrAllowanceDeductionEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        // اضافة - حسميات أو بدلات متعددة
        public async Task<IResult<string>> MultiAddOtherAllowanceDeduction(HrOtherAllowanceDeductionMultiAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                return await Result<string>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                if (entity.dataDto == null || !entity.dataDto.Any())
                    return await Result<string>.FailAsync(localization.GetResource1("NoDataProvided"));
                if (entity.TypeId == 20)
                {
                    entity.TypeId = 1;
                }
                else
                {
                    entity.TypeId = 2;
                }
                var isAmountZero = entity.dataDto.Any(x => x.Amount == 0 || x.Amount == null);
                if (isAmountZero)
                    return await Result<string>.WarningAsync(localization.GetMessagesResource("ValueMsustbeenteredforallfields"));





                foreach (var item in entity.dataDto)
                {
                    if (item.Amount == null || item.Amount <= 0)
                        continue;

                    // employee check Exist  and status
                    var employee = await hrRepositoryManager.HrEmployeeRepository.GetEmpByCode(item.EmpCode, currentData.FacilityId);
                    // employee check Exist  
                    if (employee == null)
                        return await Result<string>.WarningAsync(localization.GetResource1("EmployeeNotFound"));
                    // employee status
                    if (employee.StatusId == 2)
                        return await Result<string>.WarningAsync(localization.GetResource1("EmpNotActive"));


                    var deduction = new HrAllowanceDeduction
                    {
                        TypeId = entity.TypeId,
                        AdId = entity.AdId,
                        DueDate = entity.DueDate,
                        Amount = item.Amount.Value,
                        EmpId = employee.Id,
                        Rate = 0,
                        FixedOrTemporary = 2,
                        CreatedBy = currentData.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        MAdId = 0
                    };
                    var allAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(e => !e.IsDeleted && e.TypeId == entity.TypeId && e.AdId == entity.AdId && e.FixedOrTemporary == 2 && e.DueDate == entity.DueDate && e.EmpId == employee.Id);
                    var exists = allAllowanceDeduction.Any();

                    if (exists)
                    {
                        var existing = allAllowanceDeduction.FirstOrDefault(e => e.EmpId == employee.Id);
                        if (existing != null)
                        {
                            existing.TypeId = entity.TypeId;
                            existing.AdId = entity.AdId;
                            existing.Rate = 0;
                            existing.Amount = item.Amount.Value;
                            existing.DueDate = entity.DueDate;
                            existing.ModifiedBy = currentData.UserId;
                            existing.ModifiedOn = DateTime.Now;

                            hrRepositoryManager.HrAllowanceDeductionRepository.Update(existing);
                        }
                    }
                    else
                    {
                        await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(deduction);
                    }

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("CreateSuccess"));
            }
            catch (Exception ex)
            {
                await hrRepositoryManager.UnitOfWork.RollbackTransactionAsync(cancellationToken);
                return await Result<string>.FailAsync($"EXP in {GetType()}, Message: {ex.Message}");
            }
        }

        public async Task<IResult<string>> IntervalAddOtherAllowanceDeduction(HrOtherAllowanceDeductionIntervalAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                return await Result<string>.FailAsync(localization.GetMessagesResource("AddNullEntity"));

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var fromDate = DateHelper.StringToDate(entity.FromDate);
                var toDate = DateHelper.StringToDate(entity.ToDate);
                var countMonth = ((toDate.Year - fromDate.Year) * 12) + (toDate.Month - fromDate.Month);

                var emp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x =>
                    x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);

                if (emp == null)
                    return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                if (emp.StatusId == 2)
                    return await Result<string>.FailAsync(localization.GetResource1("EmpNotActive"));

                var existingRecords = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(e =>
                    e.EmpId == emp.Id &&
                    e.IsDeleted == false &&
                    e.TypeId == entity.TypeId &&
                    e.AdId == entity.AdId &&
                    e.FixedOrTemporary == 2
                );

                var filterResult = existingRecords
                                    .Where(e =>
                                    {
                                        bool isDateValid = DateTime.TryParseExact(e.DueDate, currentData.DateFomet, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate);
                                        bool isStartDateValid = DateTime.TryParseExact(e.StartDate, currentData.DateFomet, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                                        bool isEndDateValid = DateTime.TryParseExact(e.EndDate, currentData.DateFomet, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                                        return isDateValid && isStartDateValid && isEndDateValid &&
                                               dueDate >= startDate && dueDate <= endDate;
                                    })
                                    .ToList();
                if (filterResult.Any())
                {
                    return await Result<string>.FailAsync(localization.GetResource1("RecodeExists"));
                }

                for (int i = 0; i <= countMonth; i++)
                {
                    var dueDate = fromDate.AddMonths(i).ToString(currentData.DateFomet, CultureInfo.InvariantCulture);

                    var newEntity = new HrAllowanceDeduction
                    {
                        EmpId = emp.Id,
                        CreatedBy = currentData.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        Note = entity.Note,
                        Rate = 0,
                        Amount = entity.Amount,
                        DueDate = dueDate,
                        TypeId = entity.TypeId == 20 ? 1 : 2,
                        AdId = entity.AdId,
                        FixedOrTemporary = 2,
                        MAdId = 0,
                        StartDate = entity.FromDate,
                        EndDate = entity.ToDate
                    };

                    await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(newEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("CreateSuccess"), 200);
            }
            catch (Exception ex)
            {
                await hrRepositoryManager.UnitOfWork.RollbackTransactionAsync(cancellationToken);
                return await Result<string>.FailAsync($"EXP in {GetType()}, Message: {ex.Message}");
            }
        }

        //  سحب البدلات والحسميات من الإكسل
        public async Task<IResult<AddFromExcelResultDto>> AddOtherAllowanceDeductionFromExcel(List<HrOtherAllowanceDeductionAddFromExcelDto> entities, CancellationToken cancellationToken = default)
        {
            if (entities.Count <= 0) return await Result<AddFromExcelResultDto>.FailAsync($"يجب تعبئة الملف ");
            try
            {
                AddFromExcelResultDto result = new AddFromExcelResultDto();
                result.EmpWithProblems = new List<string>();
                result.SavedRecord = 0;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var AllEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                var AllPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.PayrollTypeId == 1 && e.IsDeleted == false && e.StartDate != null && e.EndDate != null);

                foreach (var item in entities)
                {
                    var checkEmpExist = AllEmployees.Where(x => x.EmpId == item.EmpCode).FirstOrDefault();
                    if (checkEmpExist == null)
                    {
                        result.EmpWithProblems.Add($"الموظف رقم  {item.EmpCode}  غير موجود");
                        continue;
                    }

                    if (checkEmpExist.StatusId == 2)
                    {
                        result.EmpWithProblems.Add($" تم عمل انهاء خدمة للموظف رقم   {item.EmpCode}");
                        continue;

                    }


                    // Check_Emp_Exists_In_Payroll
                    var IfEmpExistsInPayroll = AllPayroll.Where(e => e.EmpId == checkEmpExist.Id);

                    if (IfEmpExistsInPayroll.Count() > 0)
                    {
                        var filterResult = IfEmpExistsInPayroll.Where(e => DateHelper.StringToDate(item.DueDate) >= DateHelper.StringToDate(e.StartDate) && DateHelper.StringToDate(item.DueDate) <= DateHelper.StringToDate(e.EndDate));
                        if (filterResult.Any())
                        {
                            result.EmpWithProblems.Add($"الموظف رقم  {item.EmpCode}  لديه مسير في نفس الشهر");
                            continue;
                        }
                    }


                    // Chk_Allowance_Deduction_Exists_2
                    var ChkAllowanceDeductionExists = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(e => e.EmpId == checkEmpExist.Id && e.IsDeleted == false && e.TypeId == item.TypeId && e.AdId == item.AdId && e.FixedOrTemporary == 2 && e.DueDate == item.DueDate);

                    if (ChkAllowanceDeductionExists.Count() >= 1)
                    {
                        result.EmpWithProblems.Add(localization.GetResource1("RecodeExists") + $"   للموظف رقم  : {item.EmpCode}");
                        continue;
                    }

                    var newEntity = new HrAllowanceDeduction();
                    newEntity.CreatedBy = currentData.UserId;
                    newEntity.CreatedOn = DateTime.Now;
                    newEntity.IsDeleted = false;
                    newEntity.EmpId = checkEmpExist.Id;
                    newEntity.Rate = 0;
                    newEntity.FixedOrTemporary = 2;
                    newEntity.Amount = item.Amount;
                    newEntity.AdId = item.AdId;
                    newEntity.TypeId = item.TypeId;
                    newEntity.DueDate = item.DueDate;
                    newEntity.MAdId = 0;

                    var newAddedEntity = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(newEntity);

                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    result.SavedRecord += 1;

                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                if (result.SavedRecord > 0)
                {
                    result.SavedRecordMessage = "تم استيراد " + $"{result.SavedRecord}" + " سجل ";
                }
                else
                {
                    result.SavedRecordMessage = "  لم يتم استيراد أي  سجل   ";

                }
                return await Result<AddFromExcelResultDto>.SuccessAsync(result, "", 200);
            }
            catch (Exception exc)
            {
                return await Result<AddFromExcelResultDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<decimal>> GetTotalAllowances(long EmpId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(EmpId);
                return await Result<decimal>.SuccessAsync(result, "", 200);
            }
            catch (Exception exc)
            {

                return await Result<decimal>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<decimal>> GetTotalDeduction(long EmpId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalDeduction(EmpId);
                return await Result<decimal>.SuccessAsync(result, "", 200);
            }
            catch (Exception exc)
            {

                return await Result<decimal>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<List<HrAllowanceDeductionVw>>> Search(HrAllowanceDeductionOtherFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = currentData.Branches.Split(',');
                filter.DeptId ??= 0;
                filter.Location ??= 0;
                filter.Amount ??= 0;
                filter.ADID ??= 0;
                filter.Type ??= 0;
                filter.BranchId ??= 0;

                var items = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllVw(e => e.IsDeleted == false && e.FixedOrTemporary == 2
                && BranchesList.Contains(e.BranchId.ToString())
                && (filter.DeptId == 0 || filter.DeptId == e.DeptId)
                && (filter.Location == 0 || filter.Location == e.Location)
                && (filter.Amount == 0 || filter.Amount == e.Amount)
                && (filter.ADID == 0 || filter.ADID == e.AdId)
                && (filter.Type == 0 || filter.Type == e.TypeId)
                && (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower()) || e.EmpName2.ToLower().Contains(filter.EmpName.ToLower()))
               && (string.IsNullOrEmpty(filter.EmpCode) || e.EmpCode == filter.EmpCode)
               && (filter.BranchId == 0 || BranchesList.Contains(e.BranchId.ToString()))
                );
                if (items != null)
                {
                    if (items.Count() > 0)
                    {
                        var res = items.AsQueryable();

                        if (!string.IsNullOrEmpty(filter.From) && !string.IsNullOrEmpty(filter.To))
                        {
                            res = res.Where(r => r.DueDate != null && DateHelper.StringToDate(r.DueDate) >= DateHelper.StringToDate(filter.From) && DateHelper.StringToDate(r.DueDate) <= DateHelper.StringToDate(filter.To));
                        }
                        return await Result<List<HrAllowanceDeductionVw>>.SuccessAsync(res.ToList(), "");
                    }

                    return await Result<List<HrAllowanceDeductionVw>>.SuccessAsync(items.ToList(), localization.GetResource1("NosearchResult"));

                }

                return await Result<List<HrAllowanceDeductionVw>>.FailAsync("");
            }
            catch (Exception ex)
            {
                return await Result<List<HrAllowanceDeductionVw>>.FailAsync(ex.Message);
            }
        }
    }


}