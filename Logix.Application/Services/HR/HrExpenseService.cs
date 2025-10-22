using AutoMapper;
using HtmlAgilityPack;
using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.FXA;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.Hr;
using Logix.Domain.HR;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static QuestPDF.Helpers.Colors;
using IResult = Logix.Application.Wrapper.IResult;

namespace Logix.Application.Services.HR
{
    public class HrExpenseService : GenericQueryService<HrExpense, HrExpenseDto, HrExpensesVw>, IHrExpenseService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IAccRepositoryManager accRepositoryManager;
        private readonly IWorkflowHelper workflowHelper;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;
        private readonly IHttpContextAccessor httpContextAccessor;


        public HrExpenseService(IQueryRepository<HrExpense> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IAccRepositoryManager accRepositoryManager, IWorkflowHelper workflowHelper, ISysConfigurationAppHelper sysConfigurationAppHelper, IHttpContextAccessor httpContextAccessor) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.accRepositoryManager = accRepositoryManager;
            this.workflowHelper = workflowHelper;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<IResult<HrExpenseDto>> Add(HrExpenseDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> Add(HrExpenseAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync("حدث خطأ ");

            try
            {
                int StatusId = 2;
                string Code = "EXP-";
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var checkIfApplicantExist = await hrRepositoryManager.HrEmployeeRepository.GetOneVw(x => x.Isdel == false && x.IsDeleted == false && x.EmpId == entity.ApplicantCode);
                if (checkIfApplicantExist == null)
                {
                    return await Result<string>.FailAsync($"There is No Applicant With This Code {entity.ApplicantCode}");
                }

                entity.AppTypeID ??= 0;
                long? AppID = 0;
                var empID = Convert.ToInt64(session.EmpId);
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 2107, entity.AppTypeID);
                AppID = GetApp_ID;
                // اذا كان مرتبط بسير عمل مايتم الاعتماد الا من خلال سير العمل اما اذا كان من المالية فقط فهو معتمد مباشرة

                if (AppID == 0)
                {
                    StatusId = 2;
                }
                else
                {
                    StatusId = 1;
                }
                entity.AppId = AppID;
                var getMaxForCode = hrRepositoryManager.HrExpenseRepository.Entities.Where(x => x.FacilityId == session.FacilityId).Max(x => x.Id);
                getMaxForCode++;
                Code += getMaxForCode.ToString();
                var getMaxForNo = hrRepositoryManager.HrExpenseRepository.Entities.Where(x => x.FacilityId == session.FacilityId && x.ExDate == entity.RequestDate).Max(x => x.No);
                getMaxForNo++;
                var newHrExpenseEntity = new HrExpense
                {
                    Code = Code,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    FacilityId = (int?)session.FacilityId,
                    AppId = AppID,
                    IsDeleted = false,
                    Title = entity.Title ?? "",
                    Note = entity.Note ?? "",
                    StatusId = StatusId,
                    EmpId = checkIfApplicantExist.Id,
                    No = getMaxForNo,
                    ExDate = entity.RequestDate,
                    Total = entity.Total,
                    SubTotal = entity.SubTotal,
                    VatAmount = entity.VatAmount
                };

                var AddedExpenseEntity = await hrRepositoryManager.HrExpenseRepository.AddAndReturn(newHrExpenseEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);



                //  Add  Expenses Employee
                if (entity.employeeDetails != null && entity.employeeDetails.Count() > 0)
                {
                    var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);

                    foreach (var item in entity.employeeDetails)
                    {
                        var empDate = getFromEmployees.Where(x => x.EmpId == item.EmpCode).FirstOrDefault();
                        if (empDate == null)
                        {
                            return await Result<string>.FailAsync($"There is No Employee With This Code {item.EmpCode}");

                        }
                        var newExpensesEmployeeEntity = new HrExpensesEmployee
                        {
                            EmpId = empDate.Id,
                            CreatedBy = session.UserId,
                            ExpenseId = AddedExpenseEntity.Id,
                            ExpenseTypeId = item.ExpenseTypeId,
                            PaidBy = item.PaidBy,
                            Total = item.Amount,
                            SubTotal = item.Total,
                            VatAmount = item.VatAmount,
                            VatRate = item.VatRate,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            Note = item.Note,
                            PaymentDate = item.PaymentDate,
                            InvCode = item.InvCode,
                        };
                        var AddedExpenseEmployeeEntity = await hrRepositoryManager.HrExpensesEmployeeRepository.AddAndReturn(newExpensesEmployeeEntity);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                }


                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, AddedExpenseEntity.Id, 137);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP  at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrExpenseRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrExpenseDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(Id), 106);

                if (status == 2)
                {
                    return await Result<HrExpenseDto>.FailAsync($"لايمكن حذف المصروف وذلك لترحيله  ");

                }
                else
                {
                    await accRepositoryManager.AccJournalMasterRepository.DeleteJournalWithDetailsbyReference(Id, 106);
                    await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrExpenseRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrExpenseDto>.SuccessAsync(_mapper.Map<HrExpenseDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrExpenseDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrExpenseRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrExpenseDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(Id), 106);

                if (status == 2)
                {
                    return await Result<HrExpenseDto>.FailAsync($"لايمكن حذف المصروف وذلك لترحيله  ");

                }
                else
                {
                    await accRepositoryManager.AccJournalMasterRepository.DeleteJournalWithDetailsbyReference(Id, 106);
                    await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrExpenseRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrExpenseDto>.SuccessAsync(_mapper.Map<HrExpenseDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrExpenseDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }



        public async Task<IResult<HrExpenseEditDto>> Update(HrExpenseEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrExpenseRepository.GetById(entity.Id);
                if (item == null) return await Result<HrExpenseEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.ExDate = entity.RequestDate;
                item.Total = entity.Total;
                item.SubTotal = entity.SubTotal;
                item.VatAmount = entity.VatAmount;
                hrRepositoryManager.HrExpenseRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);
                // نمر على جميع العناصر لمعرفه حالاتاها اما محذوفه او مضافة جديدة او مضافة مسبقا
                foreach (var singleItem in entity.employeeDetails)
                {
                    if (singleItem.IsDeleted)
                    {
                        var CheckIfRecordExist = await hrRepositoryManager.HrExpensesEmployeeRepository.GetOne(x => x.IsDeleted == false && x.Id == singleItem.Id);
                        if (CheckIfRecordExist == null) return await Result<HrExpenseEditDto>.FailAsync($"--- لاتوجد مصروف للموظف بهذا الرقم: {entity.Id}---");

                        CheckIfRecordExist.IsDeleted = true;
                        CheckIfRecordExist.ModifiedBy = session.UserId;
                        CheckIfRecordExist.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrExpensesEmployeeRepository.Update(CheckIfRecordExist);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                    else
                    {
                        var empDate = getFromEmployees.Where(x => x.EmpId == singleItem.EmpCode).FirstOrDefault();
                        if (empDate == null)
                        {
                            return await Result<HrExpenseEditDto>.FailAsync($"There is No Employee With This Code {singleItem.EmpCode}");

                        }
                        // نقوم باضافة العناصر الجديدة فقط
                        if (singleItem.Id == 0)
                        {
                            var newExpensesEmployeeEntity = new HrExpensesEmployee
                            {
                                EmpId = empDate.Id,
                                CreatedBy = session.UserId,
                                ExpenseId = entity.Id,
                                ExpenseTypeId = singleItem.ExpenseTypeId,
                                PaidBy = singleItem.PaidBy,
                                Total = singleItem.Amount,
                                SubTotal = item.Total,
                                VatAmount = item.VatAmount,
                                VatRate = singleItem.VatRate,
                                CreatedOn = DateTime.Now,
                                IsDeleted = false,
                                Note = item.Note,
                                PaymentDate = singleItem.PaymentDate,
                                InvCode = singleItem.InvCode,
                            };
                            var AddedExpenseEmployeeEntity = await hrRepositoryManager.HrExpensesEmployeeRepository.AddAndReturn(newExpensesEmployeeEntity);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }

                    }

                }

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, entity.Id, 137);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrExpenseEditDto>.SuccessAsync(localization.GetResource1("UpdateSuccess"));

            }
            catch (Exception exc)
            {
                return await Result<HrExpenseEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message} && {localization.GetResource1("UpdateError")}");
            }

        }

        public async Task<IResult<string>> CreateExpensesEntry(HrCreateExpensesEntryDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                int? JournalStatus = 0;
                var item = await hrRepositoryManager.HrExpenseRepository.GetOne(x => x.Id == entity.Id);
                if (item == null) return await Result<string>.FailAsync($"يجب التأكد من رقم المصروف");
                JournalStatus = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(entity.Id), 106);
                if (JournalStatus == 2)
                    return await Result<string>.FailAsync($"لايمكن تعديل الاستحقاق وذلك لترحيله  ");

                var StatusId = await accRepositoryManager.AccFacilityRepository.GetOne(x => x.Posting, x => x.FacilityId == session.FacilityId);

                //  جلب كود الفترة المحاسبية
                var getPeriodId = await accRepositoryManager.AccPeriodsRepository.GetPreiodIDByDate(entity.RequestDate, session.FacilityId);
                var AccjournalMasterItem = new AccJournalMasterDto();
                var Description = "  قيد للمصروفات للموظفين ";
                AccjournalMasterItem.JDateHijri = entity.JournalDate;
                AccjournalMasterItem.JDateGregorian = entity.JournalDate;
                AccjournalMasterItem.Amount = 0;
                AccjournalMasterItem.AmountWrite = "";
                AccjournalMasterItem.JDescription = Description;
                AccjournalMasterItem.JTime = "";
                AccjournalMasterItem.PaymentTypeId = 2;
                AccjournalMasterItem.PeriodId = getPeriodId;
                AccjournalMasterItem.StatusId = StatusId;
                AccjournalMasterItem.CreatedBy = (int?)session.UserId;
                AccjournalMasterItem.CreatedOn = DateTime.Now;
                AccjournalMasterItem.FinYear = session.FinYear;
                AccjournalMasterItem.FacilityId = session.FacilityId;
                AccjournalMasterItem.DocTypeId = 106;//نوع القيد قيد مصروفات الموظفين
                AccjournalMasterItem.ReferenceNo = entity.Id;
                AccjournalMasterItem.JBian = Description;
                AccjournalMasterItem.BankId = 0;
                AccjournalMasterItem.CcId = session.BranchId;
                AccjournalMasterItem.CurrencyId = 1;
                AccjournalMasterItem.ExchangeRate = 1;
                var addJournalMaster = await accRepositoryManager.AccJournalMasterRepository.AddACCJournalMaster(AccjournalMasterItem);
                await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (!addJournalMaster.Succeeded)
                    return await Result<string>.FailAsync(addJournalMaster.Status.message);

                var JID = addJournalMaster.Data.JId;

                IEnumerable<HrExpensesEmployeesVw>? allExpensesEmployee;


                allExpensesEmployee = await hrRepositoryManager.HrExpensesEmployeeRepository.GetAllFromView(x => x.IsDeleted == false && x.ExpenseId == entity.Id);

                if (allExpensesEmployee.Count() > 0)
                {
                    foreach (var SingleDetails in allExpensesEmployee.ToList())
                    {
                        //هل العملة الحالية تختلف عن العملة المستخدمة في النظام والتي تعتبر الرئيسية

                        var AccountCurreny = await accRepositoryManager.AccAccountRepository.GetCuureny(SingleDetails.AccountPaidAdvanceId, session.FacilityId);

                        var newDetail = new AccJournalDetaileDto();
                        newDetail.JId = JID;
                        newDetail.AccAccountId = SingleDetails.AccountPaidAdvanceId;
                        newDetail.Credit = 0;
                        newDetail.Debit = SingleDetails.Total;
                        newDetail.Description = (SingleDetails.Note + " " + SingleDetails.EmpName);
                        newDetail.CcId = (SingleDetails.CcId ?? 0);
                        newDetail.Cc2Id = (SingleDetails.CcId2 ?? 0);
                        newDetail.Cc3Id = (SingleDetails.CcId3 ?? 0);
                        newDetail.Cc4Id = (SingleDetails.CcId4 ?? 0);
                        newDetail.Cc5Id = (SingleDetails.CcId5 ?? 0);
                        newDetail.ReferenceNo = 0;
                        newDetail.ReferenceTypeId = 1;
                        newDetail.ExchangeRate = 1;
                        newDetail.CurrencyId = (int?)AccountCurreny;
                        newDetail.JDateGregorian = entity.JournalDate;
                        newDetail.EmpId = SingleDetails.EmpId;

                        var AddedItem = await accRepositoryManager.AccJournalDetaileRepository.AddAccJournalDetail(newDetail);
                        await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                }



                var IsCopyfileForACC = await sysConfigurationAppHelper.GetValue(253, session.FacilityId);

                if (IsCopyfileForACC == "1")
                {
                    var getSysFiles = await mainRepositoryManager.SysFileRepository.GetAll(f => f.TableId == 137 && f.PrimaryKey == entity.Id);

                    if (getSysFiles.Count() > 0)
                    {
                        var fileDate = Bahsas.HDateNow3(session);

                        foreach (var file in getSysFiles)
                        {
                            var newAccFile = new AccJournalMasterFile
                            {
                                JId = JID,
                                FileName = file.FileName,
                                FileType = 0,
                                FileUrl = file.FileUrl,
                                FileExt = file.FileExt,
                                FileDescription = "",
                                FileDate = fileDate ?? "",
                                SourceFile = "",
                                CreatedBy = session.UserId,
                            };
                            await accRepositoryManager.AccJournalMasterFileRepository.Add(newAccFile);
                            await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }

                    }


                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("تم انشاء قيد المصروف في المالية"));

            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in CreateExpensesEntry at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");

            }
        }


    }
}

