using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.HR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Linq;
using System.Security;
using IResult = Logix.Application.Wrapper.IResult;

namespace Logix.Application.Services.HR
{
    public class HrProvisionService : GenericQueryService<HrProvision, HrProvisionDto, HrProvisionsVw>, IHrProvisionService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IAccRepositoryManager accRepositoryManager;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;


        public HrProvisionService(IQueryRepository<HrProvision> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IAccRepositoryManager accRepositoryManager, ISysConfigurationAppHelper sysConfigurationAppHelper) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.accRepositoryManager = accRepositoryManager;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
        }


        #region Generic Functions

        public async Task<IResult<HrProvisionDto>> Add(HrProvisionDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrProvisionDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                if (entity.EmpCodes.Count() <= 0)
                    return await Result<HrProvisionDto>.FailAsync($"لم يتم تحديد اي مخصص");

                int countMonth = (entity.YearlyOrMonthly == 1) ? 1 : 12;

                long getMaxForCode = 0;
                string Code = "PRO-";
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                getMaxForCode = hrRepositoryManager.HrProvisionRepository.Entities.Where(x => x.FacilityId == session.FacilityId).Max(x => x.Id);
                getMaxForCode++;
                Code += getMaxForCode.ToString();

                var newHrProvisionEntity = new HrProvision
                {
                    Code = Code,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = session.FacilityId,
                    YearlyOrMonthly = entity.YearlyOrMonthly,
                    Description = entity.Description,
                    MonthId = entity.MonthId,
                    FinYear = entity.FinYear,
                    TypeId = entity.TypeId,
                    PDate = entity.PDate,
                    No = 0

                };

                var AddedProvisionEntity = await hrRepositoryManager.HrProvisionRepository.AddAndReturn(newHrProvisionEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //  Add  Provision Employee

                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);

                var getAllAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
                    e.IsDeleted == false &&
                    e.Status == true &&
                    e.FixedOrTemporary == 1);
                foreach (var empCode in entity.EmpCodes)
                {
                    var empDate = getFromEmployees.Where(x => x.EmpId == empCode).FirstOrDefault();
                    if (empDate == null)
                        return await Result<HrProvisionDto>.FailAsync((session.Language == 1) ? $"لا يوجد موظف بهذا الرقم : {empCode}" : $"There is No Employee With This Code {empCode}");

                    var getApplyPloicyValue = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 4, empDate.Id);
                    var CalculateAmount = (getApplyPloicyValue / countMonth / 30 * empDate.VacationDaysYear) ?? 0;
                    if (CalculateAmount <= 0) continue;

                    decimal salary = empDate.Salary ?? 0;
                    decimal totalAllowance = getAllAllowanceDeduction
                        .Where(a => a.EmpId == empDate.Id && a.TypeId == 1)
                        .Sum(a => a.Amount) ?? 0;

                    decimal totalDeduction = getAllAllowanceDeduction
                        .Where(a => a.EmpId == empDate.Id && a.TypeId == 2)
                        .Sum(a => a.Amount) ?? 0;


                    var newHrProvisionsEmployeeEntity = new HrProvisionsEmployee
                    {
                        EmpId = empDate.Id,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        PId = AddedProvisionEntity.Id,
                        LocationId = empDate.Location ?? 0,
                        DeptId = empDate.DeptId,
                        BranchId = empDate.BranchId ?? 0,
                        FacilityId = session.FacilityId,
                        CcId = empDate.CcId ?? 0,
                        TotalAllowances = totalAllowance,
                        TotalDeductions = totalDeduction,
                        SalaryGroupId = empDate.SalaryGroupId,
                        BasicSalary = salary,
                        TotalSalary = salary + totalAllowance,
                        NetSalary = salary + totalAllowance - totalDeduction,
                        Amount = CalculateAmount,

                    };
                    var AddedHrProvisionsEmployeeEntity = await hrRepositoryManager.HrProvisionsEmployeeRepository.AddAndReturn(newHrProvisionsEmployeeEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrProvisionDto>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<HrProvisionDto>.FailAsync($"EXP  at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrProvisionRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrProvisionDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                // Start of Check Status ID

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(Id), 54);

                if (status == 2)
                {
                    return await Result<HrProvisionDto>.FailAsync($"{localization.GetResource1("AccrualCannotBeDeletedDueToMigration")} ");

                }
                else
                {
                    await accRepositoryManager.AccJournalMasterRepository.DeleteJournalWithDetailsbyReference(Id, 54);
                    await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                // End of Check Status ID


                // start of delelte Provision 

                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrProvisionRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                // end  of delelte Provision 

                // start of delelte Provision Employee

                var getAllProvisionEmployee = await hrRepositoryManager.HrProvisionsEmployeeRepository.GetAll(x => x.IsDeleted == false && x.PId == Id);
                if (getAllProvisionEmployee != null)
                {
                    foreach (var singleProvisionEmployee in getAllProvisionEmployee)
                    {
                        singleProvisionEmployee.IsDeleted = true;
                        singleProvisionEmployee.ModifiedBy = session.UserId;
                        singleProvisionEmployee.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrProvisionsEmployeeRepository.Update(singleProvisionEmployee);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                // End of delelte Provision Employee

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrProvisionDto>.SuccessAsync(_mapper.Map<HrProvisionDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrProvisionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrProvisionRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrProvisionDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                // Start of Check Status ID

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(Id), 54);

                if (status == 2)
                {
                    return await Result<HrProvisionDto>.FailAsync($"{localization.GetResource1("AccrualCannotBeDeletedDueToMigration")} ");

                }
                else
                {
                    await accRepositoryManager.AccJournalMasterRepository.DeleteJournalWithDetailsbyReference(Id, 54);
                    await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                // End of Check Status ID


                // start of delelte Provision 

                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrProvisionRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                // end  of delelte Provision 

                // start of delelte Provision Employee

                var getAllProvisionEmployee = await hrRepositoryManager.HrProvisionsEmployeeRepository.GetAll(x => x.IsDeleted == false && x.PId == Id);
                if (getAllProvisionEmployee != null)
                {
                    foreach (var singleProvisionEmployee in getAllProvisionEmployee)
                    {
                        singleProvisionEmployee.IsDeleted = true;
                        singleProvisionEmployee.ModifiedBy = session.UserId;
                        singleProvisionEmployee.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrProvisionsEmployeeRepository.Update(singleProvisionEmployee);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                // End of delelte Provision Employee

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrProvisionDto>.SuccessAsync(_mapper.Map<HrProvisionDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrProvisionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrProvisionEditDto>> Update(HrProvisionEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrProvisionRepository.GetById(entity.Id);
                if (item == null) return await Result<HrProvisionEditDto>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");


                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(entity.Id), 54);

                if (status == 2)
                    return await Result<HrProvisionEditDto>.FailAsync($"{localization.GetResource1("AccrualCannotBeEditDueToMigration")} ");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.Description = entity.Description;
                item.MonthId = entity.MonthId;
                item.No = entity.No;

                hrRepositoryManager.HrProvisionRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                var getFromProvisionEmployees = await hrRepositoryManager.HrProvisionsEmployeeRepository.GetAll(x => x.IsDeleted == false && x.PId == entity.Id);
                // نمر على جميع العناصر لمعرفه حالة العنصر اذا كان  محذوف
                foreach (var singleItem in entity.ProvisionsEmployee)
                {
                    if (singleItem.IsDeleted)
                    {
                        var CheckIfRecordExist = getFromProvisionEmployees.Where(x => x.IsDeleted == false && x.Id == singleItem.Id).FirstOrDefault();
                        if (CheckIfRecordExist == null) return await Result<HrProvisionEditDto>.FailAsync($"--- لاتوجد مخصص للموظف بهذا الرقم: {entity.Id}---");
                        CheckIfRecordExist.IsDeleted = true;
                        CheckIfRecordExist.ModifiedBy = session.UserId;
                        CheckIfRecordExist.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrProvisionsEmployeeRepository.Update(CheckIfRecordExist);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrProvisionEditDto>.SuccessAsync(localization.GetResource1("SaveSuccess"));

            }
            catch (Exception exc)
            {
                return await Result<HrProvisionEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message} && {localization.GetResource1("UpdateError")}");
            }

        }

        public async Task<IResult<string>> CreateProvisionEntry(HrProvisionEntryAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity.Type <= 0) return await Result<string>.FailAsync($"يجب تحديد نوع العمليه");
                int type = (int)entity.DocTypeId;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                int? JournalStatus = 0;
                var item = await hrRepositoryManager.HrProvisionRepository.GetOne(x => x.Id == entity.Id);
                if (item == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");
                JournalStatus = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(entity.Id), type);
                if (JournalStatus == 2)
                    return await Result<string>.FailAsync($"{localization.GetResource1("AccrualCannotBeEditDueToMigration")} ");

                //var StatusId = await accRepositoryManager.AccFacilityRepository.GetOne(x => x.Posting, x => x.FacilityId == session.FacilityId);

                //  جلب كود الفترة المحاسبية
                var getPeriodId = await accRepositoryManager.AccPeriodsRepository.GetPreiodIDByDate(entity.JournalDate, session.FacilityId);
                var AccjournalMasterItem = new AccJournalMasterDto();
                var Description = $"{localization.GetResource1("JournalOfVacationAccrual")}";
                AccjournalMasterItem.JDateHijri = entity.JournalDate;
                AccjournalMasterItem.JDateGregorian = entity.JournalDate;
                AccjournalMasterItem.Amount = 0;
                AccjournalMasterItem.AmountWrite = "";
                AccjournalMasterItem.JDescription = Description;
                AccjournalMasterItem.JTime = "";
                AccjournalMasterItem.PaymentTypeId = 2;
                AccjournalMasterItem.PeriodId = getPeriodId;
                AccjournalMasterItem.StatusId = 1;
                AccjournalMasterItem.CreatedBy = (int?)session.UserId;
                AccjournalMasterItem.InsertUserId = (int?)session.UserId;
                AccjournalMasterItem.CreatedOn = DateTime.Now;
                AccjournalMasterItem.InsertDate = DateTime.Now;
                AccjournalMasterItem.FinYear = session.FinYear;
                AccjournalMasterItem.FacilityId = session.FacilityId;
                AccjournalMasterItem.DocTypeId = entity.DocTypeId;
                AccjournalMasterItem.ReferenceNo = entity.Id;
                AccjournalMasterItem.JBian = Description;
                AccjournalMasterItem.BankId = 0;
                AccjournalMasterItem.CcId = session.BranchId;
                AccjournalMasterItem.CurrencyId = 1;
                AccjournalMasterItem.ExchangeRate = 1;
                AccjournalMasterItem.ChequNo = "";
                var addJournalMaster = await accRepositoryManager.AccJournalMasterRepository.AddACCJournalMaster(AccjournalMasterItem);
                await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (!addJournalMaster.Succeeded)
                    return await Result<string>.FailAsync(addJournalMaster.Status.message);

                var JID = addJournalMaster.Data.JId;
                //تمت الاضافة للخاصية بواسطة فارس بتاريخ 13-04-2024 الهدف منها استخدام مركز التكلفة بالجانب الدائن
                string UseCCInCredit = "";
                UseCCInCredit = await sysConfigurationAppHelper.GetValue(57, session.FacilityId);


                var getProvisionsAcc = await hrRepositoryManager.HrProvisionsEmployeeAccVwRepository.GetAccDetailsAsync(entity.Id, type);

                if (getProvisionsAcc.Count() > 0)
                {
                    foreach (var SingleRecord in getProvisionsAcc.ToList())
                    {
                        //هل العملة الحالية تختلف عن العملة المستخدمة في النظام والتي تعتبر الرئيسية

                        var AccountCurreny = await accRepositoryManager.AccAccountRepository.GetCuureny(0, session.FacilityId);

                        var newDetail = new AccJournalDetaileDto();
                        newDetail.JId = JID;
                        newDetail.AccAccountId = SingleRecord.AccountID;
                        newDetail.Credit = SingleRecord.Credit;
                        newDetail.Debit = SingleRecord.Debit;
                        newDetail.Description = SingleRecord.Description;
                        if ((SingleRecord.Credit > 0 && UseCCInCredit == "1") || SingleRecord.Debit > 0)
                        {
                            newDetail.CcId = (SingleRecord.CCID);
                            newDetail.Cc2Id = (SingleRecord.CCID2);
                            newDetail.Cc3Id = (SingleRecord.CCID3);
                            newDetail.Cc4Id = (SingleRecord.CCID4);
                            newDetail.Cc5Id = (SingleRecord.CCID5);
                        }
                        else
                        {
                            newDetail.CcId = 0;
                            newDetail.Cc2Id = 0;
                            newDetail.Cc3Id = 0;
                            newDetail.Cc4Id = 0;
                            newDetail.Cc5Id = 0;
                        }

                        newDetail.ReferenceNo = SingleRecord.ReferenceNo;
                        newDetail.ReferenceTypeId = SingleRecord.ReferenceTypeID;
                        newDetail.ExchangeRate = 1;
                        newDetail.CurrencyId = (int?)AccountCurreny;
                        newDetail.JDateGregorian = entity.JournalDate;
                        var AddedItem = await accRepositoryManager.AccJournalDetaileRepository.AddAccJournalDetail(newDetail);
                        await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                // still Use SendToWebHook

                return await Result<string>.SuccessAsync(localization.GetResource1("SaveSuccess"));

            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in CreateExpensesEntry at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");

            }
        }

        #endregion
        
        
        
        #region Vacation Provision
        
        
        public async Task<IResult<HrProvisionDto>> AddVacationProvision(HrProvisionDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrProvisionDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                if (entity.EmpCodes.Count() <= 0)
                    return await Result<HrProvisionDto>.FailAsync($"لم يتم تحديد اي مخصص");

                int countMonth = (entity.YearlyOrMonthly == 1) ? 1 : 12;

                long getMaxForCode = 0;
                string Code = "PRO-";
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                getMaxForCode = hrRepositoryManager.HrProvisionRepository.Entities.Where(x => x.FacilityId == session.FacilityId).Max(x => x.Id);
                getMaxForCode++;
                Code += getMaxForCode.ToString();

                var newHrProvisionEntity = new HrProvision
                {
                    Code = Code,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = session.FacilityId,
                    YearlyOrMonthly = entity.YearlyOrMonthly,
                    Description = entity.Description,
                    MonthId = entity.MonthId,
                    FinYear = entity.FinYear,
                    TypeId = entity.TypeId,
                    PDate = entity.PDate,
                    No = 0

                };

                var AddedProvisionEntity = await hrRepositoryManager.HrProvisionRepository.AddAndReturn(newHrProvisionEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //  Add  Provision Employee

                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);

                var getAllAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
                    e.IsDeleted == false &&
                    e.Status == true &&
                    e.FixedOrTemporary == 1);
                foreach (var empCode in entity.EmpCodes)
                {
                    var empDate = getFromEmployees.Where(x => x.EmpId == empCode).FirstOrDefault();
                    if (empDate == null)
                        return await Result<HrProvisionDto>.FailAsync((session.Language == 1) ? $"لا يوجد موظف بهذا الرقم : {empCode}" : $"There is No Employee With This Code {empCode}");

                    var getApplyPloicyValue = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 4, empDate.Id);
                    var CalculateAmount = (getApplyPloicyValue / countMonth / 30 * empDate.VacationDaysYear) ?? 0;
                    if (CalculateAmount <= 0) continue;

                    decimal salary = empDate.Salary ?? 0;
                    decimal totalAllowance = getAllAllowanceDeduction
                        .Where(a => a.EmpId == empDate.Id && a.TypeId == 1)
                        .Sum(a => a.Amount) ?? 0;

                    decimal totalDeduction = getAllAllowanceDeduction
                        .Where(a => a.EmpId == empDate.Id && a.TypeId == 2)
                        .Sum(a => a.Amount) ?? 0;


                    var newHrProvisionsEmployeeEntity = new HrProvisionsEmployee
                    {
                        EmpId = empDate.Id,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        PId = AddedProvisionEntity.Id,
                        LocationId = empDate.Location ?? 0,
                        DeptId = empDate.DeptId,
                        BranchId = empDate.BranchId ?? 0,
                        FacilityId = session.FacilityId,
                        CcId = empDate.CcId ?? 0,
                        TotalAllowances = totalAllowance,
                        TotalDeductions = totalDeduction,
                        SalaryGroupId = empDate.SalaryGroupId,
                        BasicSalary = salary,
                        TotalSalary = salary + totalAllowance,
                        NetSalary = salary + totalAllowance - totalDeduction,
                        Amount = CalculateAmount,

                    };
                    var AddedHrProvisionsEmployeeEntity = await hrRepositoryManager.HrProvisionsEmployeeRepository.AddAndReturn(newHrProvisionsEmployeeEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrProvisionDto>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<HrProvisionDto>.FailAsync($"EXP  at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<List<HrProvisionEmployeeResultDto>>> GetEmployeeProvisionVacationData(ProvisionSearchOnAddFilter filter)
        {
            try
            {
                List<HrProvisionEmployeeResultDto> resultList = new List<HrProvisionEmployeeResultDto>();
                int countMonth = (filter.YearlyOrMonthly == 1) ? 1 : 12;
                var StatusList = !string.IsNullOrEmpty(filter.StatusList)
                    ? filter.StatusList.Split(',').Select(int.Parse).ToList() : new List<int>();
                var items = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e =>
                    e.IsDeleted == false &&
                    e.Isdel == false &&
                    e.Doappointment != null &&
                    (string.IsNullOrEmpty(filter.StatusList) || (e.StatusId.HasValue && StatusList.Contains(e.StatusId.Value))) &&
                    e.FacilityId == session.FacilityId &&
                    (string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpId) &&
                    (string.IsNullOrEmpty(filter.EmpName) || (e.EmpName != null && e.EmpName.ToLower().Contains(filter.EmpName)) || (e.EmpName2 != null && e.EmpName2.ToLower().Contains(filter.EmpName))) &&
                    (filter.LocationId == 0 || filter.LocationId == e.Location) &&
                    (filter.DepartmentId == 0 || filter.DepartmentId == e.DeptId) &&
                    (filter.NationalityId == 0 || filter.NationalityId == e.NationalityId) &&
                    (filter.JobCategory == 0 || filter.JobCategory == e.JobCatagoriesId) &&
                    (filter.BranchId == 0 || filter.BranchId == e.BranchId) &&
                    (filter.SalaryGroupId == 0 || filter.SalaryGroupId == e.SalaryGroupId)
                );

                if (!items.Any())
                    return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

                var getAllProvisionsEmployeeAcc = await hrRepositoryManager.HrProvisionsEmployeeAccVwRepository.GetAll(x => x.EmpId,
                    x => x.TypeId == filter.TypeId &&
                    x.YearlyOrMonthly == filter.YearlyOrMonthly &&
                    x.FinYear == filter.FinYear &&
                    x.MonthId == filter.MonthId
                    );
                var AllEmpList = getAllProvisionsEmployeeAcc.ToList();
                var res = items.AsQueryable();

                res = res.Where(x => !AllEmpList.Contains(x.Id));

                if (!res.Any())
                    return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));


                var getAllAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
                    e.IsDeleted == false &&
                    e.Status == true &&
                    e.FixedOrTemporary == 1);

                foreach (var item in res)
                {
                    decimal salary = item.Salary ?? 0;
                    decimal totalAllowance = getAllAllowanceDeduction
                        .Where(a => a.EmpId == item.Id && a.TypeId == 1)
                        .Sum(a => a.Amount) ?? 0;

                    decimal totalDeduction = getAllAllowanceDeduction
                        .Where(a => a.EmpId == item.Id && a.TypeId == 2)
                        .Sum(a => a.Amount) ?? 0;
                    var getApplyPloicyValue = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 4, item.Id);

                    var Amount = (getApplyPloicyValue / countMonth / 30 * item.VacationDaysYear) ?? 0;
                    resultList.Add(new HrProvisionEmployeeResultDto
                    {
                        Id = item.Id,
                        EmpCode = item.EmpId,
                        EmpName = (session.Language == 1) ? item.EmpName : item.EmpName2,
                        BasicSalary = Math.Round(salary, 2),
                        TotalSalary = Math.Round(salary + totalAllowance, 2),
                        DepName = (session.Language == 1) ? item.DepName : item.DepName2,
                        LocationName = (session.Language == 1) ? item.LocationName : item.LocationName2,
                        TotalAllowances = Math.Round(totalAllowance, 2),
                        TotalDeductions = Math.Round(totalDeduction, 2),
                        NetSalary = Math.Round((salary + totalAllowance - totalDeduction), 2),
                        DOAppointment = item.Doappointment,
                        Amount = Math.Round(Amount, 2),
                        SalaryGroup = item.SalaryGroupId
                    });
                }

                return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList);
            }
            catch (Exception ex)
            {
                return await Result<List<HrProvisionEmployeeResultDto>>.FailAsync($"Error occurred while processing the OPeration: {ex.Message}");
            }
        }


		#endregion


		#region EndOfService Provision

		//public async Task<IResult<List<HrProvisionEmployeeResultDto>>> GetEmployeeProvisionEndOfServiceData(ProvisionSearchOnAddFilter filter)
		//{
		//    try
		//    {

		//        List<HrProvisionEmployeeResultDto> resultList = new List<HrProvisionEmployeeResultDto>();

		//        if (string.IsNullOrEmpty(filter.ToDate))
		//            return await Result<List<HrProvisionEmployeeResultDto>>.FailAsync($"{localization.GetCommonResource("ToDate")}");

		//        string lastDateStr = "";
		//        if (filter.YearlyOrMonthly == 1)
		//        {
		//            // If yearly, subtract 12 months from the current date
		//            DateTime curDate;
		//            if (!DateTime.TryParseExact(filter.ToDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out curDate))
		//            {
		//                // Handle invalid date format
		//                return await Result<List<HrProvisionEmployeeResultDto>>.FailAsync("Invalid date format.");
		//            }

		//            DateTime lastDate = curDate.AddMonths(-12).Date; // Subtract 12 months (1 year)
		//            lastDateStr = lastDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); // Format the date as yyyy/MM/dd
		//        }

		//        if (filter.YearlyOrMonthly == 2)
		//        {
		//            // If monthly, subtract 1 month from the current date
		//            DateTime curDate;
		//            if (!DateTime.TryParseExact(filter.ToDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out curDate))
		//            {
		//                // Handle invalid date format
		//                return await Result<List<HrProvisionEmployeeResultDto>>.FailAsync("Invalid date format.");
		//            }

		//            DateTime lastDate = curDate.AddMonths(-1).Date; // Subtract 1 month

		//            // Extract the month and year parts
		//            string msMonth = lastDate.ToString("MM", CultureInfo.InvariantCulture); // Get month (MM)
		//            string finYear = lastDate.ToString("yyyy", CultureInfo.InvariantCulture); // Get year (yyyy)

		//            // Set the last day of the month by adding one month and subtracting one day
		//            lastDate = new DateTime(lastDate.Year, lastDate.Month, 1).AddMonths(1).AddDays(-1);

		//            // Format the final date string
		//            lastDateStr = $"{finYear}/{msMonth}/{lastDate.Day.ToString("00", CultureInfo.InvariantCulture)}";
		//        }

		//        var statusList = !string.IsNullOrEmpty(filter.StatusList)
		//            ? filter.StatusList.Split(',').Select(int.Parse).ToList()
		//            : new List<int>();

		//        // Fetch employee data
		//        var employees = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e =>
		//            e.IsDeleted == false &&
		//            e.Isdel == false &&
		//            e.Doappointment != null &&
		//            (string.IsNullOrEmpty(filter.StatusList) || (e.StatusId.HasValue && statusList.Contains(e.StatusId.Value))) &&
		//            e.FacilityId == session.FacilityId &&
		//            (string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpId) &&
		//            (string.IsNullOrEmpty(filter.EmpName) || (e.EmpName != null && e.EmpName.ToLower().Contains(filter.EmpName)) || (e.EmpName2 != null && e.EmpName2.ToLower().Contains(filter.EmpName))) &&
		//            (filter.LocationId == 0 || filter.LocationId == e.Location) &&
		//            (filter.DepartmentId == 0 || filter.DepartmentId == e.DeptId) &&
		//            (filter.NationalityId == 0 || filter.NationalityId == e.NationalityId) &&
		//            (filter.JobCategory == 0 || filter.JobCategory == e.JobCatagoriesId) &&
		//            (filter.BranchId == 0 || filter.BranchId == e.BranchId) &&
		//            (filter.SalaryGroupId == 0 || filter.SalaryGroupId == e.SalaryGroupId)
		//        );

		//        if (!employees.Any())
		//            return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

		//        var provisionsEmployeeAcc = await hrRepositoryManager.HrProvisionsEmployeeAccVwRepository.GetAll(x => x.EmpId,
		//            x => x.TypeId == filter.TypeId &&
		//            x.YearlyOrMonthly == filter.YearlyOrMonthly &&
		//            x.FinYear == filter.FinYear &&
		//            x.MonthId == filter.MonthId
		//        );

		//        var allEmpList = provisionsEmployeeAcc.ToList();
		//        var filteredEmployees = employees.Where(e => !allEmpList.Contains(e.Id));

		//        if (!filteredEmployees.Any())
		//            return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

		//        var allowanceDeductions = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
		//            e.IsDeleted == false &&
		//            e.Status == true &&
		//            e.FixedOrTemporary == 1);

		//        foreach (var employee in filteredEmployees)
		//        {
		//            decimal salary = employee.Salary ?? 0;
		//            decimal totalAllowance = allowanceDeductions
		//                .Where(a => a.EmpId == employee.Id && a.TypeId == 1)
		//                .Sum(a => a.Amount) ?? 0;

		//            decimal totalDeduction = allowanceDeductions
		//                .Where(a => a.EmpId == employee.Id && a.TypeId == 2)
		//                .Sum(a => a.Amount) ?? 0;
		//            // Calculate Cut_Year
		//            int cutYear = !string.IsNullOrEmpty(employee.Doappointment)
		//                ? (DateTime.Now.Year - DateHelper.StringToDate(employee.Doappointment).Year) + 1
		//                : 0;
		//            var newEndOfService = await hrRepositoryManager.HrLeaveRepository.HR_End_Service_Due(filter.ToDate, employee.Id, 2);
		//            var prevEndOfService = await hrRepositoryManager.HrLeaveRepository.HR_End_Service_Due(lastDateStr, employee.Id, 2);

		//            decimal amountEndService = newEndOfService - prevEndOfService;

		//            resultList.Add(new HrProvisionEmployeeResultDto
		//            {
		//                Id = employee.Id,
		//                EmpCode = employee.EmpId,
		//                EmpName = (session.Language == 1) ? employee.EmpName : employee.EmpName2,
		//                BasicSalary = Math.Round(salary, 2),
		//                TotalSalary = Math.Round(salary + totalAllowance, 2),
		//                DepName = (session.Language == 1) ? employee.DepName : employee.DepName2,
		//                LocationName = (session.Language == 1) ? employee.LocationName : employee.LocationName2,
		//                TotalAllowances = Math.Round(totalAllowance, 2),
		//                TotalDeductions = Math.Round(totalDeduction, 2),
		//                NetSalary = Math.Round(salary + totalAllowance - totalDeduction, 2),
		//                DOAppointment = employee.Doappointment,
		//                Amount = Math.Round(amountEndService, 2), // Include End Of Service calculation
		//                SalaryGroup = employee.SalaryGroupId,
		//                CutYear = cutYear

		//            });
		//        }

		//        return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList);
		//    }
		//    catch (Exception ex)
		//    {
		//        return await Result<List<HrProvisionEmployeeResultDto>>.FailAsync($"Error occurred while processing the operation: {ex.Message}");
		//    }
		//}

		public async Task<IResult<List<HrProvisionEmployeeResultDto>>> GetEmployeeProvisionEndOfServiceData(ProvisionSearchOnAddFilter filter)
		{
			try
			{
				List<HrProvisionEmployeeResultDto> resultList = new List<HrProvisionEmployeeResultDto>();

				if (string.IsNullOrEmpty(filter.ToDate))
					return await Result<List<HrProvisionEmployeeResultDto>>.FailAsync($"{localization.GetCommonResource("ToDate")}");

				// حساب lastDateStr
				string lastDateStr = "";
				if (!DateTime.TryParseExact(filter.ToDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var curDate))
					return await Result<List<HrProvisionEmployeeResultDto>>.FailAsync("Invalid date format.");

				if (filter.YearlyOrMonthly == 1) // Yearly
					lastDateStr = curDate.AddMonths(-12).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

				if (filter.YearlyOrMonthly == 2) // Monthly
				{
					var lastDate = curDate.AddMonths(-1);
					lastDate = new DateTime(lastDate.Year, lastDate.Month, 1).AddMonths(1).AddDays(-1);
					lastDateStr = $"{lastDate:yyyy/MM/dd}";
				}

				var statusList = !string.IsNullOrEmpty(filter.StatusList)
					? filter.StatusList.Split(',').Select(int.Parse).ToList()
					: new List<int>();

				// جلب الموظفين
				var employees = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e =>
					e.IsDeleted == false &&
					e.Isdel == false &&
					e.Doappointment != null &&
					(string.IsNullOrEmpty(filter.StatusList) || (e.StatusId.HasValue && statusList.Contains(e.StatusId.Value))) &&
					e.FacilityId == session.FacilityId &&
					(string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpId) &&
					(string.IsNullOrEmpty(filter.EmpName) ||
						(e.EmpName != null && e.EmpName.ToLower().Contains(filter.EmpName)) ||
						(e.EmpName2 != null && e.EmpName2.ToLower().Contains(filter.EmpName))) &&
					(filter.LocationId == 0 || filter.LocationId == e.Location) &&
					(filter.DepartmentId == 0 || filter.DepartmentId == e.DeptId) &&
					(filter.NationalityId == 0 || filter.NationalityId == e.NationalityId) &&
					(filter.JobCategory == 0 || filter.JobCategory == e.JobCatagoriesId) &&
					(filter.BranchId == 0 || filter.BranchId == e.BranchId) &&
					(filter.SalaryGroupId == 0 || filter.SalaryGroupId == e.SalaryGroupId)
				);

				if (!employees.Any())
					return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

				// جلب بيانات Provisions دفعة واحدة
				var provisionsEmployeeAcc = await hrRepositoryManager.HrProvisionsEmployeeAccVwRepository.GetAll(
					x => x.EmpId,
					x => x.TypeId == filter.TypeId &&
						 x.YearlyOrMonthly == filter.YearlyOrMonthly &&
						 x.FinYear == filter.FinYear &&
						 x.MonthId == filter.MonthId
				);
				var allEmpList = provisionsEmployeeAcc.ToList();
				var filteredEmployees = employees.Where(e => !allEmpList.Contains(e.Id));


				if (!filteredEmployees.Any())
					return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

				// جلب Allowances و Deductions دفعة واحدة
				var allowanceDeductions = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
					e.IsDeleted == false &&
					e.Status == true &&
					e.FixedOrTemporary == 1);

				// جلب بيانات HR_End_Service_Due لكل الموظفين مرة واحدة
				var empIds = filteredEmployees.Select(e => (long)e.Id).ToList();

				foreach (var employee in filteredEmployees)
				{
					decimal newEndOfService = await hrRepositoryManager.HrLeaveRepository
						.HR_End_Service_Due(filter.ToDate, (long)employee.Id, 2);

					decimal prevEndOfService = await hrRepositoryManager.HrLeaveRepository
						.HR_End_Service_Due(lastDateStr, (long)employee.Id, 2);

					decimal amountEndService = newEndOfService - prevEndOfService;

					resultList.Add(new HrProvisionEmployeeResultDto
					{
						Id = employee.Id,
						EmpCode = employee.EmpId,
						EmpName = (session.Language == 1) ? employee.EmpName : employee.EmpName2,
						BasicSalary = Math.Round(employee.Salary ?? 0, 2),
						TotalSalary = Math.Round((employee.Salary ?? 0) +
												 allowanceDeductions.Where(a => a.EmpId == employee.Id && a.TypeId == 1).Sum(a => a.Amount) ?? 0, 2),
						DepName = (session.Language == 1) ? employee.DepName : employee.DepName2,
						LocationName = (session.Language == 1) ? employee.LocationName : employee.LocationName2,
						TotalAllowances = Math.Round(allowanceDeductions.Where(a => a.EmpId == employee.Id && a.TypeId == 1).Sum(a => a.Amount) ?? 0, 2),
						TotalDeductions = Math.Round(allowanceDeductions.Where(a => a.EmpId == employee.Id && a.TypeId == 2).Sum(a => a.Amount) ?? 0, 2),
						NetSalary = Math.Round((employee.Salary ?? 0) +
											   allowanceDeductions.Where(a => a.EmpId == employee.Id && a.TypeId == 1).Sum(a => a.Amount) ?? 0 -
											   allowanceDeductions.Where(a => a.EmpId == employee.Id && a.TypeId == 2).Sum(a => a.Amount) ?? 0, 2),
						DOAppointment = employee.Doappointment,
						Amount = Math.Round(amountEndService, 2),
						SalaryGroup = employee.SalaryGroupId,
						CutYear = !string.IsNullOrEmpty(employee.Doappointment)
							? (DateTime.Now.Year - DateHelper.StringToDate(employee.Doappointment).Year) + 1
							: 0
					});
				}


				return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList);
			}
			catch (Exception ex)
			{
				return await Result<List<HrProvisionEmployeeResultDto>>.FailAsync($"Error occurred while processing the operation: {ex.Message}");
			}
		}



		public async Task<IResult<HrProvisionDto>> AddEndOfServiceProvision(HrProvisionDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrProvisionDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                if (entity.EmpCodes.Count() <= 0)
                    return await Result<HrProvisionDto>.FailAsync($"لم يتم تحديد اي مخصص");

                int countMonth = (entity.YearlyOrMonthly == 1) ? 1 : 12;

                long getMaxForCode = 0;
                string Code = "PRO-";
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                getMaxForCode = hrRepositoryManager.HrProvisionRepository.Entities.Where(x => x.FacilityId == session.FacilityId).Max(x => x.Id);
                getMaxForCode++;
                Code += getMaxForCode.ToString();
                string lastDateStr = "";
                if (entity.YearlyOrMonthly == 1)
                {
                    // If yearly, subtract 12 months from the current date
                    DateTime curDate;
                    if (!DateTime.TryParseExact(entity.ToDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out curDate))
                    {
                        // Handle invalid date format
                        return await Result<HrProvisionDto>.SuccessAsync("Invalid date format.");
                    }

                    DateTime lastDate = curDate.AddMonths(-12).Date; // Subtract 12 months (1 year)
                    lastDateStr = lastDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); // Format the date as yyyy/MM/dd
                }

                if (entity.YearlyOrMonthly == 2)
                {
                    // If monthly, subtract 1 month from the current date
                    DateTime curDate;
                    if (!DateTime.TryParseExact(entity.ToDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out curDate))
                    {
                        // Handle invalid date format
                        return await Result<HrProvisionDto>.SuccessAsync("Invalid date format.");

                    }

                    DateTime lastDate = curDate.AddMonths(-1).Date; // Subtract 1 month

                    // Extract the month and year parts
                    string msMonth = lastDate.ToString("MM", CultureInfo.InvariantCulture); // Get month (MM)
                    string finYear = lastDate.ToString("yyyy", CultureInfo.InvariantCulture); // Get year (yyyy)

                    // Set the last day of the month by adding one month and subtracting one day
                    lastDate = new DateTime(lastDate.Year, lastDate.Month, 1).AddMonths(1).AddDays(-1);

                    // Format the final date string
                    lastDateStr = $"{finYear}/{msMonth}/{lastDate.Day.ToString("00", CultureInfo.InvariantCulture)}";
                }

                var newHrProvisionEntity = new HrProvision
                {
                    Code = Code,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = session.FacilityId,
                    YearlyOrMonthly = entity.YearlyOrMonthly,
                    Description = entity.Description,
                    MonthId = entity.MonthId,
                    FinYear = entity.FinYear,
                    TypeId = entity.TypeId,
                    PDate = entity.PDate,
                    No = 0

                };

                var AddedProvisionEntity = await hrRepositoryManager.HrProvisionRepository.AddAndReturn(newHrProvisionEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //  Add  Provision Employee

                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);

                var getAllAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
                    e.IsDeleted == false &&
                    e.Status == true &&
                    e.FixedOrTemporary == 1);
                foreach (var empCode in entity.EmpCodes)
                {
                    var empDate = getFromEmployees.Where(x => x.EmpId == empCode).FirstOrDefault();
                    if (empDate == null)
                        return await Result<HrProvisionDto>.FailAsync((session.Language == 1) ? $"لا يوجد موظف بهذا الرقم : {empCode}" : $"There is No Employee With This Code {empCode}");

                    var newEndOfService = await hrRepositoryManager.HrLeaveRepository.HR_End_Service_Due(entity.ToDate, empDate.Id, 2);
                    var prevEndOfService = await hrRepositoryManager.HrLeaveRepository.HR_End_Service_Due(lastDateStr, empDate.Id, 2);

                    decimal amountEndService = newEndOfService - prevEndOfService;
                    if (amountEndService <= 0) continue;

                    decimal salary = empDate.Salary ?? 0;
                    decimal totalAllowance = getAllAllowanceDeduction
                        .Where(a => a.EmpId == empDate.Id && a.TypeId == 1)
                        .Sum(a => a.Amount) ?? 0;

                    decimal totalDeduction = getAllAllowanceDeduction
                        .Where(a => a.EmpId == empDate.Id && a.TypeId == 2)
                        .Sum(a => a.Amount) ?? 0;


                    var newHrProvisionsEmployeeEntity = new HrProvisionsEmployee
                    {
                        EmpId = empDate.Id,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        PId = AddedProvisionEntity.Id,
                        LocationId = empDate.Location ?? 0,
                        DeptId = empDate.DeptId,
                        BranchId = empDate.BranchId ?? 0,
                        FacilityId = session.FacilityId,
                        CcId = empDate.CcId ?? 0,
                        TotalAllowances = totalAllowance,
                        TotalDeductions = totalDeduction,
                        SalaryGroupId = empDate.SalaryGroupId,
                        BasicSalary = salary,
                        TotalSalary = salary + totalAllowance,
                        NetSalary = salary + totalAllowance - totalDeduction,
                        Amount = amountEndService,

                    };
                    var AddedHrProvisionsEmployeeEntity = await hrRepositoryManager.HrProvisionsEmployeeRepository.AddAndReturn(newHrProvisionsEmployeeEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrProvisionDto>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<HrProvisionDto>.FailAsync($"EXP  at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }



        #endregion


        #region Ticket Prorvision
        public async Task<IResult<HrProvisionDto>> AddTicketProvision(HrProvisionDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrProvisionDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                if (entity.EmpCodes.Count() <= 0)
                    return await Result<HrProvisionDto>.FailAsync($"لم يتم تحديد اي مخصص");

                int countMonth = (entity.YearlyOrMonthly == 1) ? 1 : 12;

                long getMaxForCode = 0;
                string Code = "PRO-";
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                getMaxForCode = hrRepositoryManager.HrProvisionRepository.Entities.Where(x => x.FacilityId == session.FacilityId).Max(x => x.Id);
                getMaxForCode++;
                Code += getMaxForCode.ToString();

                var newHrProvisionEntity = new HrProvision
                {
                    Code = Code,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = session.FacilityId,
                    YearlyOrMonthly = entity.YearlyOrMonthly,
                    Description = entity.Description,
                    MonthId = entity.MonthId,
                    FinYear = entity.FinYear,
                    TypeId = entity.TypeId,
                    PDate = entity.PDate,
                    No = 0

                };

                var AddedProvisionEntity = await hrRepositoryManager.HrProvisionRepository.AddAndReturn(newHrProvisionEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //  Add  Provision Employee

                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);

                var getAllAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
                    e.IsDeleted == false &&
                    e.Status == true &&
                    e.FixedOrTemporary == 1);
                foreach (var empCode in entity.EmpCodes)
                {
                    var empDate = getFromEmployees.Where(x => x.EmpId == empCode).FirstOrDefault();
                    if (empDate == null)
                        return await Result<HrProvisionDto>.FailAsync((session.Language == 1) ? $"لا يوجد موظف بهذا الرقم : {empCode}" : $"There is No Employee With This Code {empCode}");

                    decimal? CalculateAmount = 0;
                    var TicketNoDependent = string.IsNullOrEmpty(empDate.TicketNoDependent) ? 0 : Convert.ToInt32(empDate.TicketNoDependent);

                    if (empDate.TicketEntitlement == 1)
                    {
                        CalculateAmount = (((empDate.ValueTicket * TicketNoDependent) * 1) / countMonth);
                    }
                    else if (empDate.TicketEntitlement == 2)
                    {
                        CalculateAmount = (((empDate.ValueTicket * TicketNoDependent) / 2) / countMonth);
                    }
                    else if (empDate.TicketEntitlement == 3)
                    {
                        CalculateAmount = (((empDate.ValueTicket * TicketNoDependent) / 3) / countMonth);
                    }
                    else
                    {
                        CalculateAmount = 0;
                    }
                    if (CalculateAmount <= 0) continue;

                    decimal salary = empDate.Salary ?? 0;
                    decimal totalAllowance = getAllAllowanceDeduction
                        .Where(a => a.EmpId == empDate.Id && a.TypeId == 1)
                        .Sum(a => a.Amount) ?? 0;

                    decimal totalDeduction = getAllAllowanceDeduction
                        .Where(a => a.EmpId == empDate.Id && a.TypeId == 2)
                        .Sum(a => a.Amount) ?? 0;


                    var newHrProvisionsEmployeeEntity = new HrProvisionsEmployee
                    {
                        EmpId = empDate.Id,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        PId = AddedProvisionEntity.Id,
                        LocationId = empDate.Location ?? 0,
                        DeptId = empDate.DeptId,
                        BranchId = empDate.BranchId ?? 0,
                        FacilityId = session.FacilityId,
                        CcId = empDate.CcId ?? 0,
                        TotalAllowances = totalAllowance,
                        TotalDeductions = totalDeduction,
                        SalaryGroupId = empDate.SalaryGroupId,
                        BasicSalary = salary,
                        TotalSalary = salary + totalAllowance,
                        NetSalary = salary + totalAllowance - totalDeduction,
                        Amount = CalculateAmount,

                    };
                    var AddedHrProvisionsEmployeeEntity = await hrRepositoryManager.HrProvisionsEmployeeRepository.AddAndReturn(newHrProvisionsEmployeeEntity);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrProvisionDto>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<HrProvisionDto>.FailAsync($"EXP  at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<List<HrProvisionEmployeeResultDto>>> GetEmployeeProvisionTicketData(ProvisionSearchOnAddFilter filter)
        {
            try
            {
                List<HrProvisionEmployeeResultDto> resultList = new List<HrProvisionEmployeeResultDto>();
                int countMonth = (filter.YearlyOrMonthly == 1) ? 1 : 12;
                var StatusList = !string.IsNullOrEmpty(filter.StatusList)
                    ? filter.StatusList.Split(',').Select(int.Parse).ToList() : new List<int>();
                var items = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(e =>
                    e.IsDeleted == false &&
                    e.Isdel == false &&
                    e.Doappointment != null &&
                    (string.IsNullOrEmpty(filter.StatusList) || (e.StatusId.HasValue && StatusList.Contains(e.StatusId.Value))) &&
                    e.FacilityId == session.FacilityId &&
                    (string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpId) &&
                    (string.IsNullOrEmpty(filter.EmpName) || (e.EmpName != null && e.EmpName.ToLower().Contains(filter.EmpName)) || (e.EmpName2 != null && e.EmpName2.ToLower().Contains(filter.EmpName))) &&
                    (filter.LocationId == 0 || filter.LocationId == e.Location) &&
                    (filter.DepartmentId == 0 || filter.DepartmentId == e.DeptId) &&
                    (filter.NationalityId == 0 || filter.NationalityId == e.NationalityId) &&
                    (filter.JobCategory == 0 || filter.JobCategory == e.JobCatagoriesId) &&
                    (filter.BranchId == 0 || filter.BranchId == e.BranchId) &&
                    (filter.SalaryGroupId == 0 || filter.SalaryGroupId == e.SalaryGroupId)
                );

                if (!items.Any())
                    return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

                var getAllProvisionsEmployeeAcc = await hrRepositoryManager.HrProvisionsEmployeeAccVwRepository.GetAll(x => x.EmpId,
                    x => x.TypeId == filter.TypeId &&
                    x.YearlyOrMonthly == filter.YearlyOrMonthly &&
                    x.FinYear == filter.FinYear &&
                    x.MonthId == filter.MonthId
                    );
                var AllEmpList = getAllProvisionsEmployeeAcc.ToList();
                var res = items.AsQueryable();

                res = res.Where(x => !AllEmpList.Contains(x.Id));

                if (!res.Any())
                    return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));


                var getAllAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
                    e.IsDeleted == false &&
                    e.Status == true &&
                    e.FixedOrTemporary == 1);

                foreach (var item in res)
                {
                    decimal salary = item.Salary ?? 0;
                    decimal totalAllowance = getAllAllowanceDeduction
                        .Where(a => a.EmpId == item.Id && a.TypeId == 1)
                        .Sum(a => a.Amount) ?? 0;

                    decimal totalDeduction = getAllAllowanceDeduction
                        .Where(a => a.EmpId == item.Id && a.TypeId == 2)
                        .Sum(a => a.Amount) ?? 0;
                    var getApplyPloicyValue = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 4, item.Id);
                    decimal? Amount = 0m;
                    var TicketNoDependent = string.IsNullOrEmpty(item.TicketNoDependent) ? 0 : Convert.ToInt32(item.TicketNoDependent);

                    if (item.TicketEntitlement == 1)
                    {
                        Amount =((( item.ValueTicket* TicketNoDependent) *1)/countMonth);
                    }
                    else if (item.TicketEntitlement == 2)
                    {
                        Amount = (((item.ValueTicket * TicketNoDependent) /2) / countMonth);
                    }
                    else if (item.TicketEntitlement == 3)
                    {
                        Amount = (((item.ValueTicket * TicketNoDependent) / 3) / countMonth);
                    }
                    else
                    {
                        Amount = 0;
                    }
                    resultList.Add(new HrProvisionEmployeeResultDto
                    {
                        Id = item.Id,
                        EmpCode = item.EmpId,
                        EmpName = (session.Language == 1) ? item.EmpName : item.EmpName2,
                        BasicSalary = Math.Round(salary, 2),
                        TotalSalary = Math.Round(salary + totalAllowance, 2),
                        DepName = (session.Language == 1) ? item.DepName : item.DepName2,
                        LocationName = (session.Language == 1) ? item.LocationName : item.LocationName2,
                        TotalAllowances = Math.Round(totalAllowance, 2),
                        TotalDeductions = Math.Round(totalDeduction, 2),
                        NetSalary = Math.Round((salary + totalAllowance - totalDeduction), 2),
                        DOAppointment = item.Doappointment,
                        Amount = Math.Round((decimal)Amount, 2),
                        SalaryGroup = item.SalaryGroupId
                    });
                }

                return await Result<List<HrProvisionEmployeeResultDto>>.SuccessAsync(resultList);
            }
            catch (Exception ex)
            {
                return await Result<List<HrProvisionEmployeeResultDto>>.FailAsync($"Error occurred while processing the OPeration: {ex.Message}");
            }
        }

        #endregion

    }

}