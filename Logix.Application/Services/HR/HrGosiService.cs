using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.ACC;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.HR;
using Logix.Domain.Main;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using IResult = Logix.Application.Wrapper.IResult;

namespace Logix.Application.Services.HR
{
    public class HrGosiService : GenericQueryService<HrGosi, HrGosiDto, HrGosiVw>, IHrGosiService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IAccRepositoryManager accRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HrGosiService(IQueryRepository<HrGosi> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IAccRepositoryManager accRepositoryManager, ISysConfigurationAppHelper sysConfigurationAppHelper, IHttpContextAccessor _httpContextAccessor) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.accRepositoryManager = accRepositoryManager;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
            this.httpContextAccessor = _httpContextAccessor;
        }

        public Task<IResult<HrGosiDto>> Add(HrGosiDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> Add(HrGosiAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity.BranchId == 0 || entity.BranchId == null)
                {
                    entity.BranchId = session.BranchId;
                }

                var MonthCode = string.Empty;
                if (entity.TMonth < 10)
                {
                    MonthCode = "0" + entity.TMonth.ToString();

                }
                else
                {
                    MonthCode = entity.TMonth.ToString();

                }
                // نتأكد من وجود استحقاق مسبقا
                //var IsGosiExists = await hrRepositoryManager.HrGosiRepository.GetAll(g => g.IsDeleted == false && g.TMonth == MonthCode && g.FinancelYear == entity.FinancelYear && g.FacilityId == session.FacilityId);
                //if (IsGosiExists.Count() > 0)
                //{
                //    return await Result<string>.FailAsync($"تم انشاء اثبات استحقاق تأمينات مسبقاً");
                //}
                var AllEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);

                var startDate = entity.FinancelYear.ToString() + "/" + MonthCode + "/01";
                int year = int.Parse(startDate.Split('/')[0]);
                int month = int.Parse(startDate.Split('/')[1]);
                int lastDay = DateTime.DaysInMonth(year, month);
                var endtDate = entity.FinancelYear.ToString() + "/" + MonthCode + "/" + lastDay.ToString();
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var newGosi = new HrGosi
                {
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    FinancelYear = entity.FinancelYear,
                    BranchId = entity.BranchId,
                    TMonth = MonthCode,
                    TDate = entity.TDate,
                    StartDate = startDate,
                    EndDate = endtDate,
                    FacilityId = session.FacilityId
                };
                var AddedGosiItem = await hrRepositoryManager.HrGosiRepository.AddAndReturn(newGosi);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.GosiEmp.Count > 0 && entity.GosiEmp != null)
                {
                    foreach (var item in entity.GosiEmp)
                    {
                        var checkEmpExist = AllEmployees.Where(x => x.EmpId == item.emp_ID).FirstOrDefault();
                        if (checkEmpExist == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                        var newGosiEmployee = new HrGosiEmployee
                        {
                            GosiId = AddedGosiItem.Id,
                            EmpId = checkEmpExist.Id,
                            GosiCompany = item.gosi_Salary_Facility,
                            GosiCompanyRate = 0,
                            GosiEmp = item.gosi_Salary_Emp,
                            GosiEmpRate = item.gosi_Rate_Facility,
                            HousingAllowance = item.gosi_House_Allowance,
                            BasicSalary = item.gosi_Bisc_Salary,
                            TotalSalary = item.gosi_Salary,
                            OtherAllowance = item.gosi_Allowance_Commission,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            CcId = item.cC_ID ?? 0,
                        };
                        await hrRepositoryManager.HrGosiEmployeeRepository.Add(newGosiEmployee);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, AddedGosiItem.Id, 84);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), "", 200);

            }
            catch (Exception exp)
            {

                return await Result<string>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrGosiRepository.GetOne(x => x.Id == Id && x.IsDeleted == false);
                if (item == null) return Result<HrPayrollDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(Id), 37);
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                if (status == 2)
                {
                    return await Result<HrPayrollDto>.FailAsync($"لايمكن حذف الاستحقاق وذلك لترحيله  ");

                }
                else
                {
                    await accRepositoryManager.AccJournalMasterRepository.DeleteJournalWithDetailsbyReference(Convert.ToInt32(Id), 37);
                    await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }


                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrGosiRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                var GosiEmployeeRItem = await hrRepositoryManager.HrGosiEmployeeRepository.GetAll(x => x.GosiId == Id && x.IsDeleted == false);
                if (GosiEmployeeRItem.Count() > 0)
                {
                    foreach (var singleGosiEmployeeItem in GosiEmployeeRItem)
                    {
                        singleGosiEmployeeItem.IsDeleted = true;
                        singleGosiEmployeeItem.ModifiedBy = session.UserId;
                        singleGosiEmployeeItem.ModifiedOn = DateTime.Now;
                    }
                    hrRepositoryManager.HrGosiEmployeeRepository.UpdateAll(GosiEmployeeRItem);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrGosiDto>.SuccessAsync(_mapper.Map<HrGosiDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrGosiDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrGosiRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrPayrollDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(Id), 37);
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                if (status == 2)
                {
                    return await Result<HrPayrollDto>.FailAsync($"لايمكن حذف الاستحقاق وذلك لترحيله  ");

                }
                else
                {
                    await accRepositoryManager.AccJournalMasterRepository.DeleteJournalWithDetailsbyReference(Id, 37);
                    await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }


                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrGosiRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                var GosiEmployeeRItem = await hrRepositoryManager.HrGosiEmployeeRepository.GetAll(x => x.GosiId == Id);
                if (GosiEmployeeRItem.Count() > 0)
                {
                    foreach (var singleGosiEmployeeItem in GosiEmployeeRItem)
                    {
                        singleGosiEmployeeItem.IsDeleted = true;
                        singleGosiEmployeeItem.ModifiedBy = session.UserId;
                        singleGosiEmployeeItem.ModifiedOn = DateTime.Now;
                    }
                    hrRepositoryManager.HrGosiEmployeeRepository.UpdateAll(GosiEmployeeRItem);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                return await Result<HrPayrollDto>.SuccessAsync(_mapper.Map<HrPayrollDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPayrollDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(long Id, long GosiId, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrGosiRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrPayrollDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                var GosiEmployee = await hrRepositoryManager.HrGosiEmployeeRepository.GetOne(x => x.Id == Id);
                if (GosiEmployee == null) return Result<HrPayrollDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(GosiId), 37);
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                if (status == 2)
                {
                    return await Result<HrPayrollDto>.FailAsync($"لايمكن حذف الاستحقاق وذلك لترحيله  ");

                }
                else
                {
                    await accRepositoryManager.AccJournalMasterRepository.DeleteJournalWithDetailsbyReference(Id, 37);
                    await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                GosiEmployee.IsDeleted = true;
                GosiEmployee.ModifiedBy = session.UserId;
                GosiEmployee.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrGosiEmployeeRepository.Update(GosiEmployee);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPayrollDto>.SuccessAsync(_mapper.Map<HrPayrollDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPayrollDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult<HrGosiEditDto>> Update(HrGosiEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> UpdateGosiEmployee(HrGosiEditDto entity, CancellationToken cancellationToken)
        {
            if (entity == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrGosiRepository.GetById(entity.Id);

                if (item == null) return await Result<string>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(entity.Id, 37);

                if (status == 2)
                {
                    return await Result<string>.FailAsync(localization.GetMessagesResource("EntitlementCannotBeModifiedDueToItsTransfer"));
                }

                if (entity.GosiEmp.Count > 0 && entity.GosiEmp != null)
                {
                    foreach (var SingleRecord in entity.GosiEmp)
                    {
                        var ccId = SingleRecord.CcId;
                        var GosiEmpItem = await hrRepositoryManager.HrGosiEmployeeRepository.GetOne(x => x.IsDeleted == false && x.Id == SingleRecord.Id);
                        if (GosiEmpItem == null) return await Result<string>.FailAsync(localization.GetMessagesResource("NoEmployeesWithThisGosi"));
                        var costCenter = await accRepositoryManager.AccCostCenterRepository.GetOne(x => x.CostCenterCode == SingleRecord.CostCenterCode && x.IsDeleted == false);
                        if (costCenter != null)
                        {
                            ccId = costCenter.CcId;
                        }
                        if (SingleRecord.IsDeleted == true)
                        {
                            GosiEmpItem.IsDeleted = true;
                        }
                        else
                        {
                            GosiEmpItem.BasicSalary = SingleRecord.BasicSalary;
                            GosiEmpItem.TotalSalary = SingleRecord.TotalSalary;
                            GosiEmpItem.GosiCompany = SingleRecord.GosiCompany;
                            GosiEmpItem.GosiEmp = SingleRecord.GosiEmp;
                            GosiEmpItem.HousingAllowance = SingleRecord.HousingAllowance;
                            GosiEmpItem.CcId = ccId;
                            GosiEmpItem.OtherAllowance = SingleRecord.OtherAllowance;
                        }
                        GosiEmpItem.ModifiedBy = session.UserId;
                        GosiEmpItem.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrGosiEmployeeRepository.Update(GosiEmpItem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, entity.Id, 84);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("UpdateSuccess"), "", 200);

            }
            catch (Exception exp)
            {

                return await Result<string>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<AccJournalMaster>> CreateDue(HrCreateDueDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrGosiRepository.GetOne(x => x.Id == entity.Id);
                if (item == null) return await Result<AccJournalMaster>.FailAsync($"{localization.GetMessagesResource("NoIdInUpdate")}");
                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(entity.Id), 37);

                if (status == 2)
                {
                    return await Result<AccJournalMaster>.FailAsync(localization.GetMessagesResource("EntitlementCannotBeModifiedDueToItsTransfer"));
                }

                /////////
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                if (entity.GosiEmp.Count > 0 && entity.GosiEmp != null)
                {
                    foreach (var SingleRecord in entity.GosiEmp)
                    {
                        var ccId = SingleRecord.CcId;
                        var GosiEmpItem = await hrRepositoryManager.HrGosiEmployeeRepository.GetOne(x => x.IsDeleted == false && x.Id == SingleRecord.Id);
                        if (GosiEmpItem == null) return await Result<AccJournalMaster>.FailAsync(localization.GetMessagesResource("NoEmployeesWithThisGosi"));
                        var costCenter = await accRepositoryManager.AccCostCenterRepository.GetOne(x => x.CostCenterCode == SingleRecord.CostCenterCode && x.IsDeleted == false);
                        if (costCenter != null)
                        {
                            ccId = costCenter.CcId;
                        }
                        if (SingleRecord.IsDeleted == true)
                        {
                            GosiEmpItem.IsDeleted = true;
                        }
                        else
                        {
                            GosiEmpItem.BasicSalary = SingleRecord.BasicSalary;
                            GosiEmpItem.TotalSalary = SingleRecord.TotalSalary;
                            GosiEmpItem.GosiCompany = SingleRecord.GosiCompany;
                            GosiEmpItem.GosiEmp = SingleRecord.GosiEmp;
                            GosiEmpItem.HousingAllowance = SingleRecord.HousingAllowance;
                            GosiEmpItem.CcId = ccId;
                            GosiEmpItem.OtherAllowance = SingleRecord.OtherAllowance;
                        }
                        GosiEmpItem.ModifiedBy = session.UserId;
                        GosiEmpItem.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrGosiEmployeeRepository.Update(GosiEmpItem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                //  جلب كود الفترة المحاسبية
                var getPeriodId = await accRepositoryManager.AccPeriodsRepository.GetPreiodIDByDate(entity.tDate, session.FacilityId);
                var AccjournalMasterItem = new AccJournalMasterDto();
                var Description = localization.GetMessagesResource("InsuranceForTheMonth") + DateHelper.GetArMonthName(entity.tMonth) + " " + localization.GetMessagesResource("Foryear") + " " + entity.FinancelYear.ToString();

                AccjournalMasterItem.JCode = "0";
                AccjournalMasterItem.JDateHijri = entity.tDate;
                AccjournalMasterItem.JDateGregorian = entity.tDate;
                AccjournalMasterItem.Amount = 0;
                AccjournalMasterItem.AmountWrite = "";
                AccjournalMasterItem.JDescription = Description;
                AccjournalMasterItem.JTime = "";
                AccjournalMasterItem.PaymentTypeId = 2;
                AccjournalMasterItem.PeriodId = getPeriodId;
                AccjournalMasterItem.StatusId = 1;
                AccjournalMasterItem.CreatedBy = (int?)session.UserId;
                AccjournalMasterItem.CreatedOn = DateTime.Now;
                AccjournalMasterItem.FinYear = session.FinYear;
                AccjournalMasterItem.FacilityId = session.FacilityId;
                AccjournalMasterItem.DocTypeId = 37;//نوع القيد قيد استحقاق التأمينات
                AccjournalMasterItem.ReferenceNo = entity.Id;
                AccjournalMasterItem.JBian = Description;
                AccjournalMasterItem.ChequNo = null;
                AccjournalMasterItem.BankId = 0;
                AccjournalMasterItem.ChequDateHijri = null;
                AccjournalMasterItem.CcId = session.BranchId;
                AccjournalMasterItem.CurrencyId = 1;
                AccjournalMasterItem.ExchangeRate = 1;
                var GetJID = await accRepositoryManager.AccJournalMasterRepository.AddACCJournalMaster(AccjournalMasterItem);
                await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (GetJID.Succeeded == false)
                {
                    return await Result<AccJournalMaster>.FailAsync(GetJID.Status.message);

                }
                var JID = GetJID.Data.JId;

                //هل العملة الحالية تختلف عن العملة المستخدمة في النظام والتي تعتبر الرئيسية

                // جلب العملة الإفتراضية
                var DefaultCurrency = await mainRepositoryManager.SysExchangeRateRepository.GetOne(c => c.CurrencyToID, c => c.Id == 1);
                var IsCreateDueByCC_ID = await sysConfigurationAppHelper.GetValue(296, session.FacilityId);
                List<HRGOSIAccEntryDto> HRGOSIAccEntry = new List<HRGOSIAccEntryDto>();
                if (IsCreateDueByCC_ID != "2" && IsCreateDueByCC_ID != "3")
                {
                    HRGOSIAccEntry = (List<HRGOSIAccEntryDto>)await hrRepositoryManager.HrGosiRepository.GetGosiEmployeeAcc(entity.Id, session.FacilityId);
                }
                else if (IsCreateDueByCC_ID == "2")
                {
                    HRGOSIAccEntry = (List<HRGOSIAccEntryDto>)await hrRepositoryManager.HrGosiRepository.GetHRGOSIAccbyCCID(entity.Id, session.FacilityId);
                }
                else if (IsCreateDueByCC_ID == "3")
                {
                    HRGOSIAccEntry = (List<HRGOSIAccEntryDto>)await hrRepositoryManager.HrGosiRepository.GetHRGOSIAccbyReferenceTypeID(entity.Id, session.FacilityId);
                }
                if (HRGOSIAccEntry.Count > 0)
                {
                    foreach (var Gosiitem in HRGOSIAccEntry)
                    {
                        var newDetail = new AccJournalDetaileDto();

                        newDetail.JId = JID;
                        newDetail.AccAccountId = Gosiitem.AccountID;
                        newDetail.Credit = Gosiitem.Credit;
                        newDetail.Debit = Gosiitem.Debit;
                        newDetail.Description = Gosiitem.Description;
                        newDetail.CreatedBy = (int)session.UserId;
                        newDetail.CreatedOn = DateTime.Now;

                        newDetail.CcId = Gosiitem.CC_ID;
                        newDetail.ReferenceNo = Gosiitem.ReferenceNo;
                        newDetail.ReferenceTypeId = Gosiitem.ReferenceType_ID;
                        newDetail.ExchangeRate = 1;
                        newDetail.CurrencyId = 1;
                        newDetail.JDateGregorian = entity.tDate;

                        if (Gosiitem.Cc2Id > 0)
                        {
                            newDetail.Cc2Id = Gosiitem.Cc2Id;
                        }
                        if (Gosiitem.Cc3Id > 0)
                        {
                            newDetail.Cc3Id = Gosiitem.Cc3Id;
                        }
                        if (Gosiitem.Cc4Id > 0)
                        {
                            newDetail.Cc4Id = Gosiitem.Cc4Id;
                        }
                        if (Gosiitem.Cc5Id > 0)
                        {
                            newDetail.Cc5Id = Gosiitem.Cc5Id;
                        }
                        if (Gosiitem.EmpId > 0)
                        {
                            newDetail.EmpId = Gosiitem.EmpId;
                        }

                        var AddedItem = await accRepositoryManager.AccJournalDetaileRepository.AddAccJournalDetail(newDetail);
                        await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                var IsCopyfileForACC = await sysConfigurationAppHelper.GetValue(253, session.FacilityId);

                if (IsCopyfileForACC == "1")
                {
                    var getSysFiles = await mainRepositoryManager.SysFileRepository.GetAll(f => f.TableId == 84 && f.PrimaryKey == entity.Id);

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

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, entity.Id, 84);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<AccJournalMaster>.SuccessAsync(GetJID.Data, localization.GetResource1("UpdateSuccess"));

            }
            catch (Exception exp)
            {
                return await Result<AccJournalMaster>.FailAsync($"EXP in CreateDue at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");

            }
        }

        public async Task<IResult<IEnumerable<EmployeeGosiDto>>> getEmployeeData(EmployeeGosiSearchtDto filter)
        {
            try
            {
                var result = await mainRepositoryManager.StoredProceduresRepository.HR_RPEmployee_Sp(filter);
                if (result.Count() > 0)
                    return await Result<IEnumerable<EmployeeGosiDto>>.SuccessAsync(result, "");
                return await Result<IEnumerable<EmployeeGosiDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<EmployeeGosiDto>>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<HrEmployeeGosiReportDto>>> GetEmployeeGosiReportInf(HrEmployeeGosiReportFilterDto filter)
        {
            var result = new List<HrEmployeeGosiReportDto>();

            var BranchesList = filter.BranchIds?.Split(',').Select(int.Parse).ToList();

            // Default Month Code and Financial Year
            var monthCode = string.IsNullOrEmpty(filter.MonthCode) ? DateTime.Now.Month.ToString("D2") : filter.MonthCode.PadLeft(2, '0');
            var financialYear = DateTime.Now.Year.ToString();

            // Fetch HR_Setting details
            var hrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == filter.FacilityId);
            var attStartDay = hrSetting?.MonthStartDay ?? "01";
            var attEndDay = hrSetting?.MonthEndDay ?? "01";

            // Calculate start and end dates for the month
            var startDate = new DateTime(int.Parse(financialYear), int.Parse(monthCode), 1);
            var endDate = startDate.AddMonths(1).AddDays(-1); // Last day of the month

            // Calculate attendance start and end dates
            DateTime attStartDate = new DateTime(int.Parse(financialYear), int.Parse(monthCode), int.Parse(attStartDay));
            DateTime attEndDate = new DateTime(int.Parse(financialYear), int.Parse(monthCode), int.Parse(attEndDay));

            // Ensure attEndDate does not exceed the actual end of the month
            if (attEndDate > endDate) attEndDate = endDate;

            // Use the system configuration to determine whether to override startDate and endDate
            var useDateFromSetting = await sysConfigurationAppHelper.GetValue(127, filter.FacilityId);
            if (string.IsNullOrEmpty(useDateFromSetting)) useDateFromSetting = "1";

            if (useDateFromSetting == "1")
            {
                startDate = attStartDate;
                endDate = attEndDate;
            }



            // Fetch employee data
            var GetemployeeData = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(
                x => x.IsDeleted == false &&
                    (filter.FacilityId == 0 || x.FacilityId == filter.FacilityId) &&
                    (string.IsNullOrEmpty(filter.BranchIds) || BranchesList.Contains((int)x.BranchId)) &&
                    (filter.Location == 0 || x.Location == filter.Location) &&
                    (string.IsNullOrEmpty(filter.EmpCode) || x.EmpId == filter.EmpCode) &&
                    (string.IsNullOrEmpty(filter.EmpName) || x.EmpName.Contains(filter.EmpName)) &&
                    (filter.SalaryGroupId == 0 || x.SalaryGroupId == filter.SalaryGroupId) &&
                    (x.GosiSalary > 0) &&
                    (filter.BranchId == 0 || x.BranchId == filter.BranchId) &&
                    (filter.SalaryGroupId == 0 || x.SalaryGroupId == filter.SalaryGroupId)
            );
            var employeeData = GetemployeeData.AsQueryable();


            if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
            {
                var fromDate = DateHelper.StringToDate(filter.FromDate);
                var toDate = DateHelper.StringToDate(filter.ToDate);

                employeeData = employeeData.Where(x => !string.IsNullOrEmpty(x.StopDateSalary) &&
                                                        DateHelper.StringToDate(x.StopDateSalary) >= fromDate &&
                                                        DateHelper.StringToDate(x.StopDateSalary) <= toDate);
            }

            var filteredEmployees = employeeData.Where(x => !string.IsNullOrEmpty(x.Doappointment) &&
                                                             !string.IsNullOrEmpty(x.ContractExpiryDate) &&
                                                             DateHelper.StringToDate(x.ContractExpiryDate) >= startDate &&
                                                             DateHelper.StringToDate(x.Doappointment) <= endDate).ToList();


            if (filteredEmployees.Count() <= 0)
            {
                return await Result<IEnumerable<HrEmployeeGosiReportDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));

            }


            var getAllGosiEmployee = await hrRepositoryManager.HrGosiEmployeeRepository.GetAllFromView(ad => ad.IsDeleted == false && ad.TMonth == monthCode && ad.FinancelYear.ToString() == financialYear);
            var AllGosiEmployeeList = getAllGosiEmployee.Select(ad => ad.EmpId).ToList();

            var getAllGosiSalaryEmp = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(ad => ad.TypeId == 2 && ad.AdId == 1 && ad.IsDeleted == false && ad.FixedOrTemporary == 1);
            var AllGosiSalaryEmpIDsList = getAllGosiSalaryEmp.Select(ad => ad.EmpId);
            var getAllLeaves = await hrRepositoryManager.HrLeaveRepository.GetAll(ad => ad.IsDeleted == false);
            var filteredLeaveData = getAllLeaves.Where(x => x.LeaveDate != null &&
                                                            DateHelper.StringToDate(x.LeaveDate) >= startDate &&
                                                            DateHelper.StringToDate(x.LeaveDate) <= endDate);
            var getAllLeavesEmpIDsList = filteredLeaveData.Select(ad => ad.EmpId).ToList();

            filteredEmployees = filteredEmployees.Where(x => ((x.StatusId == 1) || (x.StatusId == 2 && getAllLeavesEmpIDsList.Contains(Convert.ToInt64(x.EmpId))))).ToList();
            filteredEmployees = filteredEmployees.Where(x => !AllGosiEmployeeList.Contains(Convert.ToInt64(x.EmpId))).ToList();

            if (filteredEmployees.Count() <= 0)
            {
                return await Result<IEnumerable<HrEmployeeGosiReportDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));

            }
            foreach (var employee in filteredEmployees)
            {
                decimal gosiSalaryEmpAmount = 0;
                var gosiSalaryEmp = getAllGosiSalaryEmp.Where(X => X.EmpId == employee.Id);
                gosiSalaryEmpAmount = gosiSalaryEmp.Sum(x => x.Amount) ?? 0;

                // Calculate Gosi Salary Facility
                var gosiSalaryFacility = employee.GosiSalary * employee.GosiRateFacility / 100;

                var totalDaysInPeriod = (endDate - startDate).Days + 1;
                var daysWorked = (DateHelper.StringToDate(employee.ContractExpiryDate) < endDate ? (DateHelper.StringToDate(employee.ContractExpiryDate) - startDate).Days + 1 : totalDaysInPeriod);

                gosiSalaryFacility *= daysWorked / (decimal)totalDaysInPeriod;

                // Calculate Gosi Salary Employee
                var gosiSalaryEmployee = gosiSalaryEmpAmount;

                result.Add(new HrEmployeeGosiReportDto
                {
                    IdNo = employee.IdNo,
                    GosiSalary = employee.GosiSalary,
                    GosiDate = employee.GosiDate,
                    GosiNo = employee.GosiNo,
                    GoisSubscriptionExpiryDate = employee.GoisSubscriptionExpiryDate,
                    GosiRateFacility = employee.GosiRateFacility,
                    EmpCode = employee.EmpId,
                    EmpName = session.Language == 1 ? employee.EmpName : employee.EmpName2,
                    LocationName = session.Language == 1 ? employee.LocationName : employee.LocationName2,
                    CostCenterCode = employee.CostCenterCode,
                    CostCenterName = employee.CostCenterName,
                    CcId = employee.CcId,
                    GosiBiscSalary = employee.GosiBiscSalary,
                    GosiHouseAllowance = employee.GosiHouseAllowance,
                    GosiAllowanceCommission = employee.GosiAllowanceCommission,
                    GosiOtherAllowances = employee.GosiOtherAllowances,
                    GosiName = session.Language == 1 ? employee.GosiTypeName : employee.GosiTypeName2,
                    GosiSalaryFacility = gosiSalaryFacility,
                    GosiSalaryEmp = gosiSalaryEmployee,
                    GosiTotalSalary = (gosiSalaryEmployee) + (gosiSalaryFacility ?? 0)
                });
            }

            if (result.Any())
                return await Result<IEnumerable<HrEmployeeGosiReportDto>>.SuccessAsync(result, "");
            return await Result<IEnumerable<HrEmployeeGosiReportDto>>.SuccessAsync(result, localization.GetResource1("NosearchResult"));
        }

    }
}