using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static Castle.MicroKernel.ModelBuilder.Descriptors.InterceptorDescriptor;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Logix.Application.Services.HR
{


    public class HrContractService : GenericQueryService<HrContracte, HrContracteDto, HrContractesVw>, IHrContracteService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrContractService(IQueryRepository<HrContracte> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }
        public async Task<IResult<HrContracteDto>> Add(HrContracteDto entity, CancellationToken cancellationToken = default)
        {
            //هذه الدالة تمثل دالة الإضافة في شاشة اضافة سجل متعدد في النظام القديم(Add)
            if (entity == null)
                return await Result<HrContracteDto>.FailAsync(localization.GetMessagesResource("AddNullEntity"));

            if (entity.EmpCodes == null || !entity.EmpCodes.Any())
                return await Result<HrContracteDto>.FailAsync(localization.GetMessagesResource("ChooseEmployee"));

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                foreach (var item in entity.EmpCodes)
                {
                    // check if Emp Is Exist
                    var checkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == item && x.IsDeleted == false && x.Isdel == false);
                    if (checkEmp == null) return await Result<HrContracteDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                    if (checkEmp.StatusId == 2) return await Result<HrContracteDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                    string NewContract_expiry_Date = "";
                    if (string.IsNullOrEmpty(checkEmp.ContractExpiryDate) == false && entity.ContractDurationNo > 0 && entity.contractDurationType > 0)
                    {
                        if (entity.contractDurationType == 1)
                        {
                            var XYear = Convert.ToInt32(entity.ContractDurationNo);

                            var gregorianCulture = new CultureInfo("en-US");
                            gregorianCulture.DateTimeFormat.Calendar = new GregorianCalendar();

                            DateTime expiryDate = DateTime.ParseExact(
                                checkEmp.ContractExpiryDate,
                                "yyyy/MM/dd",
                                gregorianCulture
                            );

                            NewContract_expiry_Date = expiryDate
                            .AddYears(XYear)
                            .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                        }
                        else if (entity.contractDurationType == 2)
                        {
                            int Xmonth = Convert.ToInt32(entity.ContractDurationNo);

                            var gregorianCulture = new CultureInfo("en-US");
                            gregorianCulture.DateTimeFormat.Calendar = new GregorianCalendar();

                            DateTime expiryDate = DateTime.ParseExact(
                                checkEmp.ContractExpiryDate,
                                "yyyy/MM/dd",
                                gregorianCulture
                            );

                            NewContract_expiry_Date = expiryDate
                            .AddMonths(Xmonth)
                            .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                        }
                        else if (entity.contractDurationType == 3)
                        {
                            int Xweek = Convert.ToInt32(entity.ContractDurationNo);

                            var gregorianCulture = new CultureInfo("en-US");
                            gregorianCulture.DateTimeFormat.Calendar = new GregorianCalendar();

                            DateTime expiryDate = DateTime.ParseExact(
                                checkEmp.ContractExpiryDate,
                                "yyyy/MM/dd",
                                gregorianCulture
                            );

                            NewContract_expiry_Date = expiryDate
                            .AddDays((Xweek * 7))
                            .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        }
                        else if (entity.contractDurationType == 4)
                        {
                            int Xday = Convert.ToInt32(entity.ContractDurationNo);

                            var gregorianCulture = new CultureInfo("en-US");
                            gregorianCulture.DateTimeFormat.Calendar = new GregorianCalendar();

                            DateTime expiryDate = DateTime.ParseExact(
                                checkEmp.ContractExpiryDate,
                                "yyyy/MM/dd",
                                gregorianCulture
                            );

                            NewContract_expiry_Date = expiryDate
                            .AddDays(Xday)
                            .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        }
                    }

                    var newContract = new HrContracte
                    {
                        EmpId = checkEmp.Id,
                        BranchId = checkEmp.BranchId,
                        LocationId = checkEmp.Location ?? 0,
                        DepartmentId = checkEmp.DeptId ?? 0,
                        TypeId = 1,
                        FacilityId = (int?)session.FacilityId,
                        ContractExpiryDate = checkEmp.ContractExpiryDate,
                        NewContractExpiryDate = NewContract_expiry_Date,
                        ContractDurationNo = entity.ContractDurationNo,
                        ContractDurationType = entity.contractDurationType,
                        StartContractDate = DateHelper.DateToString(DateHelper.StringToDate(checkEmp.ContractExpiryDate).AddDays(1), CultureInfo.InvariantCulture),
                        NewStartContractDate = DateHelper.DateToString(DateHelper.StringToDate(checkEmp.ContractExpiryDate).AddDays(1), CultureInfo.InvariantCulture),
                        Note = entity.Note,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        TDate = entity.TDate,
                        IsDeleted = false,
                        WithSalaryInc = false
                    };
                    var newEntity = await hrRepositoryManager.HrContracteRepository.AddAndReturn(newContract);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    //---------------UpdateContract_expiry_Date-----------------------//                        
                    checkEmp.ModifiedBy = session.UserId;
                    checkEmp.ModifiedOn = DateTime.Now;
                    checkEmp.ContractData = DateHelper.DateToString(DateHelper.StringToDate(checkEmp.ContractExpiryDate).AddDays(1), CultureInfo.InvariantCulture);
                    checkEmp.ContractExpiryDate = NewContract_expiry_Date;
                    mainRepositoryManager.InvestEmployeeRepository.Update(checkEmp);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    //----------------------  من هنا تبدأ اضافة الاشعارات -----------------------------------------------------//
                    var getAllNotificationsType = await hrRepositoryManager.HrNotificationsTypeRepository.GetAllVW(x => x.IsActive == true && x.SubjectType == 1 && x.FacilityId == session.FacilityId && x.IsDeleted == false);
                    if (getAllNotificationsType.Count() > 0)
                    {
                        foreach (var newType in getAllNotificationsType)
                        {
                            var newNotification = new HrNotification
                            {
                                TypeId = 1,
                                NotificationDate = NewContract_expiry_Date,
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,
                                EmpId = checkEmp.Id,
                                Subject = newType.MsgSubject,
                                Detailes = newType.Detailes,
                                FacilityId = session.FacilityId,
                                IsDeleted = false,
                                IsRead = false
                            };

                            await hrRepositoryManager.HrNotificationRepository.Add(newNotification);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }
                    //-------------------------------- -------------------------------------------//
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrContracteDto>.SuccessAsync(localization.GetMessagesResource("RenewalSuccessFor") + " " + $"{entity.EmpCodes.Count()}" + " " + localization.GetMessagesResource("RenewalSuccessFor2"));
            }
            catch (Exception)
            {
                return await Result<HrContracteDto>.FailAsync(localization.GetResource1("SaveError"));
            }
        }

        public async Task<IResult<HrContracteAdd2Dto>> Add2(HrContracteAdd2Dto entity, CancellationToken cancellationToken = default)
        {
            //هذه الدالة تمثل دالة الإضافة في شاشة اضافة سجل جديد في النظام القديم(Add2)
            if (entity == null) return await Result<HrContracteAdd2Dto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            if (string.IsNullOrEmpty(entity.EmpCode))
                return await Result<HrContracteAdd2Dto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // check if Emp Is Exist
                var checkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrContracteAdd2Dto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmp.StatusId == 2) return await Result<HrContracteAdd2Dto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                if (entity.ApplyType == 1)
                {
                    if (entity.DifferenceAmount != null && entity.DifferenceAmount > 0)
                    {
                        var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId == checkEmp.Id && e.PayrollTypeId == 1 && e.IsDeleted == false);

                        if (IfEmpExistsInPayroll.Count() > 0)
                        {
                            var msDate = DateHelper.StringToDate(entity.DeductionDate);
                            var filterResult = IfEmpExistsInPayroll
                            .Where(e => !string.IsNullOrWhiteSpace(e.StartDate) && !string.IsNullOrWhiteSpace(e.EndDate))
                            .AsEnumerable()
                            .Where(e =>
                                msDate >= DateHelper.StringToDate(e.StartDate) &&
                                msDate <= DateHelper.StringToDate(e.EndDate)
                            )
                            .ToList();
                            if (filterResult.Count() > 0)
                            {
                                return await Result<HrContracteAdd2Dto>.FailAsync(localization.GetMessagesResource("ContractRenewalPayrollError"));

                            }
                        }
                    }

                }

                entity.deptId ??= 0;
                entity.LocationId ??= 0;
                entity.BranchId ??= 0;
                entity.OldSalary ??= 0;
                entity.NewSalary ??= 0;
                entity.IncAmount ??= 0;
                entity.IncRate ??= 0;
                var newItem = _mapper.Map<HrContracte>(entity);
                newItem.EmpId = checkEmp.Id;
                newItem.DepartmentId = entity.deptId;
                newItem.TypeId = 1;
                newItem.FacilityId = (int)session.FacilityId;
                newItem.CreatedBy = session.UserId;
                newItem.CreatedOn = DateTime.Now;
                newItem.IsDeleted = false;
                newItem.DecisionNo = entity.DecisionNo?.ToString();

                if (!string.IsNullOrEmpty(entity.ContractExpiryDate))
                {
                    // Force parse as Gregorian
                    var gregorianCulture = new CultureInfo("en-US");
                    gregorianCulture.DateTimeFormat.Calendar = new GregorianCalendar();

                    DateTime expiryDate = DateTime.ParseExact(
                        entity.ContractExpiryDate,
                        "yyyy/MM/dd",
                        gregorianCulture
                    );

                    // Force format back as Gregorian string
                    newItem.NewStartContractDate = expiryDate
                        .AddDays(1)
                        .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }


                if (entity.IncRate != 0 || entity.IncAmount != 0)
                {
                    newItem.WithSalaryInc = true;
                }
                else
                {
                    newItem.WithSalaryInc = false;
                    newItem.OldSalary = entity.OldSalary;
                    newItem.NewSalary = entity.OldSalary;
                    newItem.IncAmount = 0;
                    newItem.IncRate = 0;
                }

                var newEntity = await hrRepositoryManager.HrContracteRepository.AddAndReturn(newItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                bool All_Ded_Count = false;
                //  معالجة البدلات
                if (entity.allowances.Count() > 0)
                {
                    foreach (var allowanceitem in entity.allowances)
                    {
                        var newContractAllowanceDeduction = new HrContractsAllowanceDeduction
                        {
                            ContractId = newEntity.Id,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            TypeId = 1,
                            Rate = allowanceitem.Rate,
                            Amount = allowanceitem.Amount,
                            NewAmount = allowanceitem.NewAmount,
                            NewRate = 0,
                            AdId = allowanceitem.AdId,
                            AllDedId = allowanceitem.Id,
                            IsDeleted = allowanceitem.IsDeleted,
                            IsNew = allowanceitem.IsNew
                        };
                        //Next
                        if (newContractAllowanceDeduction.IsDeleted == false)
                        {
                            if (newContractAllowanceDeduction.Amount == newContractAllowanceDeduction.NewAmount)
                            {
                                newContractAllowanceDeduction.IsUpdated = false;
                            }
                            else
                            {
                                newContractAllowanceDeduction.IsUpdated = true;
                            }
                            newContractAllowanceDeduction.Status = false;
                            var newHrIncrementAllowance = await hrRepositoryManager.HrContractsAllowanceDeductionRepository.AddAndReturn(newContractAllowanceDeduction);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            All_Ded_Count = true;
                            //'تحديث البدلات والحسميات
                            if (entity.ApplyType == 1)
                            {

                                var updateAllowanceDeduction = new HrAllowanceDeduction
                                {
                                    TypeId = 1,
                                    AdId = allowanceitem.AdId,
                                    Rate = allowanceitem.Rate,
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
                                    updateAllowanceDeduction.Note = localization.GetMessagesResource("AddedFromContract") + " " + newEntity.Id;

                                    var ad = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(updateAllowanceDeduction);
                                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    newContractAllowanceDeduction.AllDedId = ad.Id;

                                    //newIncrementsAllowanceDeduction.Id = newHrIncrementAllowance.Id;
                                    hrRepositoryManager.HrContractsAllowanceDeductionRepository.Update(newContractAllowanceDeduction);

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
                                        tracked.Note = localization.GetMessagesResource("UpdatedFromContract") + " " + newEntity.Id;

                                        hrRepositoryManager.HrAllowanceDeductionRepository.Update(tracked);
                                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (newContractAllowanceDeduction.IsNew == false)
                            {
                                newContractAllowanceDeduction.IsUpdated = true;

                                newContractAllowanceDeduction.Status = false;
                                await hrRepositoryManager.HrContractsAllowanceDeductionRepository.Add(newContractAllowanceDeduction);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                All_Ded_Count = true;

                                if (entity.ApplyType == 1)
                                {

                                    var getAllDed = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(allowanceitem.Id);
                                    if (getAllDed != null)
                                    {
                                        getAllDed.IsDeleted = true;
                                        getAllDed.ModifiedBy = session.UserId;
                                        getAllDed.Note = localization.GetMessagesResource("DeletedFromContract") + " " + newEntity.Id;

                                        hrRepositoryManager.HrAllowanceDeductionRepository.Update(getAllDed);
                                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    }
                                }
                            }
                        }
                    }
                }

                // معالجة الحسميات
                if (entity.deductions.Count() > 0)
                {
                    foreach (var deductionitem in entity.deductions)
                    {
                        var newContractAllowanceDeduction = new HrContractsAllowanceDeduction
                        {
                            ContractId = newEntity.Id,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            TypeId = 2,
                            Rate = deductionitem.Rate,
                            Amount = deductionitem.Amount,
                            NewAmount = deductionitem.NewAmount,
                            NewRate = 0,
                            AdId = deductionitem.AdId,
                            AllDedId = deductionitem.Id,
                            IsDeleted = deductionitem.IsDeleted,
                            IsNew = deductionitem.IsNew
                        };
                        //Next
                        if (newContractAllowanceDeduction.IsDeleted == false)
                        {
                            if (deductionitem.Amount == deductionitem.NewAmount)
                            {
                                deductionitem.IsUpdated = false;
                            }
                            else
                            {
                                deductionitem.IsUpdated = true;
                            }
                            newContractAllowanceDeduction.Status = false;
                            var newHrIncrementAllowance = await hrRepositoryManager.HrContractsAllowanceDeductionRepository.AddAndReturn(newContractAllowanceDeduction);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            All_Ded_Count = true;
                            //'تحديث البدلات والحسميات
                            if (entity.ApplyType == 1)
                            {

                                var updateAllowanceDeduction = new HrAllowanceDeduction
                                {
                                    TypeId = 2,
                                    AdId = deductionitem.AdId,
                                    Rate = deductionitem.Rate,
                                    Amount = deductionitem.NewAmount,
                                    CreatedBy = session.UserId,
                                    CreatedOn = DateTime.Now,
                                    EmpId = checkEmp.Id,
                                    FixedOrTemporary = 1,
                                    MAdId = 0,
                                    Id = deductionitem.Id,
                                };
                                if (updateAllowanceDeduction.Id == 0)
                                {
                                    updateAllowanceDeduction.Note = localization.GetMessagesResource("AddedFromContract") + " " + newEntity.Id;
                                    
                                    var ad = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(updateAllowanceDeduction);
                                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    newContractAllowanceDeduction.AllDedId = ad.Id;

                                    hrRepositoryManager.HrContractsAllowanceDeductionRepository.Update(newContractAllowanceDeduction);

                                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                }
                                else
                                {
                                    var tracked = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(deductionitem.Id);

                                    if (tracked != null)
                                    {
                                        tracked.Amount = deductionitem.NewAmount;
                                        tracked.Rate = 0;
                                        tracked.ModifiedBy = session.UserId;
                                        tracked.ModifiedOn = DateTime.Now;
                                        tracked.TypeId = 2;
                                        tracked.FixedOrTemporary = 1;
                                        tracked.MAdId = 0;
                                        tracked.Note = localization.GetMessagesResource("UpdatedFromContract") + " " + newEntity.Id;

                                        hrRepositoryManager.HrAllowanceDeductionRepository.Update(tracked);
                                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (newContractAllowanceDeduction.IsNew == false)
                            {
                                newContractAllowanceDeduction.IsUpdated = true;

                                newContractAllowanceDeduction.Status = false;
                                await hrRepositoryManager.HrContractsAllowanceDeductionRepository.Add(newContractAllowanceDeduction);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                All_Ded_Count = true;

                                if (entity.ApplyType == 1)
                                {

                                    var getAllDed = await hrRepositoryManager.HrAllowanceDeductionRepository.GetById(deductionitem.Id);
                                    if (getAllDed != null)
                                    {
                                        getAllDed.IsDeleted = true;
                                        getAllDed.ModifiedBy = session.UserId;
                                        getAllDed.Note = localization.GetMessagesResource("DeletedFromContract") + " " + newEntity.Id;

                                        hrRepositoryManager.HrAllowanceDeductionRepository.Update(getAllDed);
                                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                                    }
                                }
                            }
                        }
                    }
                }

                //  اذا تم احتساب  فارق العلاوة
                if (entity.ApplyType == 1 && (All_Ded_Count || newItem.WithSalaryInc == true))
                {
                    //if(Entity)
                    if (entity.DifferenceAmount != null && entity.DifferenceAmount > 0)
                    {
                        var newAllowanceDeduction = new HrAllowanceDeduction
                        {
                            TypeId = 2,
                            AdId = entity.DeductionType,
                            Rate = 0,
                            Amount = entity.DifferenceAmount,
                            IsDeleted = false,
                            FixedOrTemporary = 2,
                            MAdId = 0,
                            Status = true,
                            Note = localization.GetMessagesResource("AddedFromContract") + " " + newEntity.Id,
                            DueDate = entity.DeductionDate,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            EmpId = checkEmp.Id,
                        };
                        var allDed = await hrRepositoryManager.HrAllowanceDeductionRepository.AddAndReturn(newAllowanceDeduction);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        var newContractsAllowanceDeduction = new HrContractsAllowanceDeduction
                        {
                            ContractId = newEntity.Id,
                            AdId = entity.DeductionType,
                            TypeId = 2,
                            Rate = 0,
                            Amount = 0,
                            IsDeleted = false,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            NewRate = 0,
                            NewAmount = entity.DifferenceAmount,
                            AllDedId = allDed.Id,
                            IsNew = true
                        };
                        newContractsAllowanceDeduction.Status = false;
                        var newHrIncrementAllowance = await hrRepositoryManager.HrContractsAllowanceDeductionRepository.AddAndReturn(newContractsAllowanceDeduction);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                }

                //---------------UpdateContract_expiry_Date-----------------------//
                checkEmp.ModifiedBy = session.UserId;
                checkEmp.ModifiedOn = DateTime.Now;
                checkEmp.ContractData = entity.ContractExpiryDate;
                checkEmp.ContractExpiryDate = newItem.NewStartContractDate;
                mainRepositoryManager.InvestEmployeeRepository.Update(checkEmp);
                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                //---------------------------------------------------------------------------//
                // اذا كان هناك زيادة في الراتب يتم تحديث راتب الموظف
                if (entity.ApplyType == 1 && newItem.WithSalaryInc == true)
                {
                    checkEmp.Salary = entity.NewSalary;
                    mainRepositoryManager.InvestEmployeeRepository.Update(checkEmp);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                // اذا لم يكن هناك زيادة في الراتب ولا بدلات او حسميات جديدة يتم الغاء التفعيل
                if (newItem.WithSalaryInc == false && All_Ded_Count == false)
                {
                    var getContract = await hrRepositoryManager.HrContracteRepository.GetById(newEntity.Id);
                    if (getContract != null)
                    {
                        getContract.ApplyType = 0;
                        hrRepositoryManager.HrContracteRepository.Update(getContract);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 106);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<HrContracteAdd2Dto>(newEntity);

                return await Result<HrContracteAdd2Dto>.SuccessAsync(entityMap, localization.GetResource1("SaveSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrContracteAdd2Dto>.FailAsync(localization.GetResource1("SaveError"));
            }
        }

        public async Task<IResult<HrContracteAdd3Dto>> Add3(HrContracteAdd3Dto entity, CancellationToken cancellationToken = default)
        {
            //هذه الدالة تمثل دالة الإضافة في شاشة اضافة سجل يدوي في النظام القديم(Add3)
            if (entity == null)
                return await Result<HrContracteAdd3Dto>.FailAsync(localization.GetMessagesResource("AddNullEntity"));

            if (entity.EmpCode == null)
                return await Result<HrContracteAdd3Dto>.FailAsync(localization.GetMessagesResource("EmployeeNumberIsRequired"));

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                // check if Emp Is Exist
                var checkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrContracteAdd3Dto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmp.StatusId == 2) return await Result<HrContracteAdd3Dto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                var newContract = new HrContracte
                {
                    EmpId = checkEmp.Id,
                    BranchId = checkEmp.BranchId,
                    LocationId = checkEmp.Location ?? 0,
                    DepartmentId = checkEmp.DeptId ?? 0,
                    TypeId = 1,
                    TDate = entity.TDate,
                    FacilityId = (int?)session.FacilityId,
                    StartContractDate = checkEmp.ContractData,
                    ContractExpiryDate = checkEmp.ContractExpiryDate,
                    NewStartContractDate = entity.NewStartContractDate,
                    NewContractExpiryDate = entity.NewContractExpiryDate,
                    ContractDurationNo = entity.ContractDurationNo,
                    ContractDurationType = entity.ContractDurationType,
                    Note = entity.Note,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    WithSalaryInc = false
                };
                var newEntity = await hrRepositoryManager.HrContracteRepository.AddAndReturn(newContract);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                checkEmp.ModifiedBy = session.UserId;
                checkEmp.ModifiedOn = DateTime.Now;
                checkEmp.ContractData = entity.NewStartContractDate;
                checkEmp.ContractExpiryDate = entity.NewContractExpiryDate;
                mainRepositoryManager.InvestEmployeeRepository.Update(checkEmp);
                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 106);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<HrContracteAdd3Dto>(newEntity);

                return await Result<HrContracteAdd3Dto>.SuccessAsync(entityMap, localization.GetResource1("SaveSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrContracteAdd3Dto>.FailAsync(localization.GetResource1("SaveError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrContracteRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrContracteDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrContracteRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrContracteDto>.SuccessAsync(_mapper.Map<HrContracteDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrContracteDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrContracteRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrContracteDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrContracteRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrContracteDto>.SuccessAsync(_mapper.Map<HrContracteDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrContracteDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrContracteEditDto>> Update(HrContracteEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrContracteEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.empCode)) return await Result<HrContracteEditDto>.FailAsync($"{localization.GetMessagesResource("NoIdInUpdate")}");

            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.empCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrContracteEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmpExist.StatusId == 2) return await Result<HrContracteEditDto>.FailAsync(localization.GetHrResource("EmpNotActive"));
                var item = await hrRepositoryManager.HrContracteRepository.GetById(entity.Id);

                if (item == null) return await Result<HrContracteEditDto>.FailAsync("the Record Is Not Found");
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                _mapper.Map(entity, item);
                item.EmpId = checkEmpExist.Id;
                hrRepositoryManager.HrContracteRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //---------------UpdateContract_expiry_Date-----------------------//
                checkEmpExist.ModifiedBy = session.UserId;
                checkEmpExist.ModifiedOn = DateTime.Now;
                checkEmpExist.ContractData = entity.ContractExpiryDate;
                checkEmpExist.ContractExpiryDate = entity.NewContractExpiryDate;
                mainRepositoryManager.InvestEmployeeRepository.Update(checkEmpExist);
                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, entity.Id, 106);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrContracteEditDto>.SuccessAsync(_mapper.Map<HrContracteEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrContracteEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}
