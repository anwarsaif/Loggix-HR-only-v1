using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Data;
using System.Globalization;

namespace Logix.Application.Services.HR
{
    public class HrLoanService : GenericQueryService<HrLoan, HrLoanDto, HrLoanVw>, IHrLoanService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;



        public HrLoanService(IQueryRepository<HrLoan> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrLoanDto>> Add(HrLoanDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrLoanDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            // 1. Check for required attachments if property 349 is set
            var CheckAttachments = await _mainRepositoryManager.SysPropertyValueRepository.GetByProperty(349, session.FacilityId);

            if (CheckAttachments != null && CheckAttachments.PropertyValue != null && CheckAttachments.PropertyValue == "1")
            {
                if (entity.files == null || entity.files.Count() <= 0)
                {
                    return await Result<HrLoanDto>.WarningAsync(localization.GetMessagesResource("AttachmentsMustBeAdded"));
                }
            }

            // 2. Check for required attachments if property 349 is set
            decimal? SumPayed = 0m;
            foreach (var item in entity.Details)
            {
                SumPayed += item.Amount;
            }
            if (SumPayed != entity.LoanValue)
                return await Result<HrLoanDto>.WarningAsync(localization.GetMessagesResource("TotalInstalNotEquVaLoan"));

            decimal Installment_V = 0;

            var Checkproperty = await _mainRepositoryManager.SysPropertyValueRepository.GetByProperty(55, session.FacilityId);

            if (!string.IsNullOrEmpty(Checkproperty.PropertyValue))
            {
                Installment_V = Convert.ToDecimal(Checkproperty.PropertyValue);
                if (Installment_V != 0)
                {
                    foreach(var i in entity.Details)
                    {
                        if(i.Rate >= Installment_V)
                        {
                            return await Result<HrLoanDto>.WarningAsync(localization.GetMessagesResource("RecorBelExcAllowPremPercentage"));
                        }
                    }
                }
            }
            // 3. Validate employee existence and status
            var employee = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x=> x.EmpId == entity.EmpId && x.Isdel == false);
            if (employee == null)
                return await Result<HrLoanDto>.WarningAsync(localization.GetResource1("EmployeeNotFound"));
            if (employee.StatusId == 2) // Terminated
                return await Result<HrLoanDto>.WarningAsync(localization.GetMessagesResource("NotPossGraLoanEmpTerminated"));

            // 4. Create loan and installments in a transaction
            await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var loan = new HrLoan
                {
                    EmpId = employee.Id.ToString(),
                    InstallmentValue = entity.InstallmentValue,
                    LoanDate = entity.LoanDate,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    InstallmentCount = entity.InstallmentCount,
                    InstallmentLastValue = entity.InstallmentValue,
                    //InstallmentLastValue =  entity.InstallmentLastValue,
                    LoanValue = entity.LoanValue,
                    StartDatePayment = entity.StartDatePayment,
                    EndDatePayment = entity.EndDatePayment,
                    Note = entity.Note,
                    Type = entity.Type
                };

                var loanId = await hrRepositoryManager.HrLoanRepository.AddAndReturn(loan);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                int IntallmentNo = 1;
                foreach (var inst in entity.Details)
                {
                    var installment = new HrLoanInstallment
                    {
                        LoanId = loanId.Id,
                        IntallmentNo = IntallmentNo,
                        Amount = inst.Amount,
                        DueDate = inst.DueDate,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        IsPaid = false
                    };
                    IntallmentNo++;
                    await hrRepositoryManager.HrLoanInstallmentRepository.Add(installment);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                if (entity.files != null && entity.files.Count() > 0)
                {
                    var addFiles = await _mainRepositoryManager.SysFileRepository.SaveFiles(entity.files, loanId.Id, 138);
                    await _mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                var entityMap = _mapper.Map<HrLoanDto>(loanId);
                return await Result<HrLoanDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception ex)
            {
                await hrRepositoryManager.UnitOfWork.RollbackTransactionAsync(cancellationToken);
                return await Result<HrLoanDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {ex.Message}");
            }
        }

        public async Task<IResult<HrLoanDto>> Add2(HrLoanDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrLoanDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var CheckAttachments = await _mainRepositoryManager.SysPropertyValueRepository.GetByProperty(349, session.FacilityId);

                if (CheckAttachments != null && CheckAttachments.PropertyValue != null && CheckAttachments.PropertyValue == "1")
                {
                    if (entity.files == null || entity.files.Count() <= 0)
                    {
                        return await Result<HrLoanDto>.WarningAsync(localization.GetMessagesResource("AttachmentsMustBeAdded"));
                    }
                }
                // check if Emp Is Exist
                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpId && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrLoanDto>.WarningAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmp.StatusId == 2) return await Result<HrLoanDto>.WarningAsync(localization.GetMessagesResource("NotPossGraLoanEmpTerminated"));
                //
                //if (DateHelper.StringToDate(entity.LoanDate) > DateTime.Now) return await Result<HrLoanDto>.FailAsync("تاريخ السلفة غير صحيح");
                //if (DateHelper.StringToDate(entity.StartDatePayment) < DateTime.Now) return await Result<HrLoanDto>.FailAsync(" تاريخ أول قسط غير صحيح");

                //  التاكد من تواريخ الاقساط
                //foreach (var item in entity.Details)
                //{
                //    if (DateHelper.StringToDate(item.DueDate) < DateTime.Now) return await Result<HrLoanDto>.FailAsync(localization.GetMessagesResource("InstallmentDueDateInvalid"));

                //}

                var loan = new HrLoan
                {
                    EmpId = checkEmp.Id.ToString(),
                    InstallmentValue = entity.InstallmentValue,
                    LoanDate = entity.LoanDate,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    InstallmentCount = entity.InstallmentCount,
                    InstallmentLastValue = entity.InstallmentValue,
                    //InstallmentLastValue =  entity.InstallmentLastValue,
                    LoanValue = entity.LoanValue,
                    StartDatePayment = entity.StartDatePayment,
                    EndDatePayment = entity.EndDatePayment,
                    Note = entity.Note,
                    Type = entity.Type
                };

                var newEntity = await hrRepositoryManager.HrLoanRepository.AddAndReturn(loan);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                ///////////////////////////////////////
                int IntallmentNo = 1;
                foreach (var inst in entity.Details)
                {
                    var installment = new HrLoanInstallment
                    {
                        LoanId = newEntity.Id,
                        IntallmentNo = IntallmentNo,
                        Amount = inst.Amount,
                        DueDate = inst.DueDate,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        IsPaid = false
                    };
                    IntallmentNo++;
                    await hrRepositoryManager.HrLoanInstallmentRepository.Add(installment);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                if (entity.files != null && entity.files.Count() > 0)
                {
                    var addFiles = await _mainRepositoryManager.SysFileRepository.SaveFiles(entity.files, newEntity.Id, 138);
                    await _mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                var entityMap = _mapper.Map<HrLoanDto>(newEntity);
                return await Result<HrLoanDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));

            }
            catch (Exception exc)
            {
                return await Result<HrLoanDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<HrLoanDto>> Add3(HrLoanDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrLoanDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var CheckAttachments = await _mainRepositoryManager.SysPropertyValueRepository.GetByProperty(349, session.FacilityId);

                if (CheckAttachments != null && CheckAttachments.PropertyValue != null && CheckAttachments.PropertyValue == "1")
                {
                    if (entity.files == null || entity.files.Count() <= 0)
                    {
                        return await Result<HrLoanDto>.FailAsync(localization.GetMessagesResource("AttachmentsMustBeAdded"));
                    }
                }
                // check if Emp Is Exist
                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpId && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrLoanDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmp.StatusId == 2) return await Result<HrLoanDto>.FailAsync(localization.GetMessagesResource("NotPossGraLoanEmpTerminated"));
                //
                //if (DateHelper.StringToDate(entity.LoanDate) > DateTime.Now) return await Result<HrLoanDto>.FailAsync("تاريخ السلفة غير صحيح");
                //if (DateHelper.StringToDate(entity.StartDatePayment) < DateTime.Now) return await Result<HrLoanDto>.FailAsync(" تاريخ أول قسط غير صحيح");

                //  التاكد من تواريخ الاقساط
                //foreach (var item in entity.Details)
                //{
                //    if (DateHelper.StringToDate(item.DueDate) < DateTime.Now) return await Result<HrLoanDto>.FailAsync("تاريخ الاستحقاق للقسط غير صحيح");

                //}

                var loan = new HrLoan
                {
                    EmpId = checkEmp.Id.ToString(),
                    InstallmentValue = entity.InstallmentValue,
                    LoanDate = entity.LoanDate,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    InstallmentCount = entity.InstallmentCount,
                    InstallmentLastValue = entity.InstallmentValue,
                    //InstallmentLastValue =  entity.InstallmentLastValue,
                    LoanValue = entity.LoanValue,
                    StartDatePayment = entity.StartDatePayment,
                    EndDatePayment = entity.EndDatePayment,
                    Note = entity.Note,
                    Type = entity.Type
                };

                var newEntity = await hrRepositoryManager.HrLoanRepository.AddAndReturn(loan);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                ///////////////////////////////////////
                int IntallmentNo = 1;
                foreach (var inst in entity.Details)
                {
                    var installment = new HrLoanInstallment
                    {
                        LoanId = newEntity.Id,
                        IntallmentNo = IntallmentNo,
                        Amount = inst.Amount,
                        DueDate = inst.DueDate,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        IsPaid = false
                    };
                    IntallmentNo++;
                    await hrRepositoryManager.HrLoanInstallmentRepository.Add(installment);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                if (entity.files != null && entity.files.Count() > 0)
                {
                    var addFiles = await _mainRepositoryManager.SysFileRepository.SaveFiles(entity.files, newEntity.Id, 138);
                    await _mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                var entityMap = _mapper.Map<HrLoanDto>(newEntity);
                return await Result<HrLoanDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));

            }
            catch (Exception exc)
            {
                return await Result<HrLoanDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<HrLoan4Dto>> Add4(HrLoan4Dto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrLoan4Dto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var CheckAttachments = await _mainRepositoryManager.SysPropertyValueRepository.GetByProperty(349, session.FacilityId);

                //if (CheckAttachments.PropertyValue != null && CheckAttachments.PropertyValue == "1")
                //{
                //    if (string.IsNullOrEmpty(entity.))
                //    {
                //        return await Result<HrLoan4Dto>.FailAsync(localization.GetResource1("AttachmentsMustBeAdded"));
                //    }
                //}

                // check if Emp Is Exist
                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpId && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrLoan4Dto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmp.StatusId == 2) return await Result<HrLoan4Dto>.FailAsync(localization.GetResource1("NotPossGraLoanEmpTerminated"));
                //
                //if (DateHelper.StringToDate(entity.LoanDate) > DateTime.Now) return await Result<HrLoan4Dto>.FailAsync("تاريخ السلفة غير صحيح");

                entity.CreatedOn = DateTime.Now;
                entity.CreatedBy = session.UserId;
                entity.EmpId = checkEmp.Id.ToString();

                var newEntity = await hrRepositoryManager.HrLoanRepository.AddAndReturn(_mapper.Map<HrLoan>(entity));
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.files != null && entity.files.Count() > 0)
                {
                    var addFiles = await _mainRepositoryManager.SysFileRepository.SaveFiles(entity.files, newEntity.Id, 138);
                    await _mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                var entityMap = _mapper.Map<HrLoan4Dto>(newEntity);
                return await Result<HrLoan4Dto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));

            }
            catch (Exception exc)
            {
                return await Result<HrLoan4Dto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<HrLoanEditDto>> Update(HrEditLoanDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrLoanEditDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                // check if Emp Is Exist
                var checkEmp = await hrRepositoryManager.HrEmployeeRepository.GetEmpByCode(entity.EmpId, session.FacilityId);
                if (checkEmp == null) return await Result<HrLoanEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrLoanRepository.GetById(entity.Id);
                if (item == null) return await Result<HrLoanEditDto>.FailAsync($"{localization.GetMessagesResource("NoDataWithId")} {entity.Id}");

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // نفحص هل مبلغ السلفه مساوي لاجمالي الأقساط 
                decimal? sumPayed = 0m;
                if (entity.Details != null)
                    sumPayed = entity.Details.Sum(d => d.Amount ?? 0);

                if (sumPayed == null || entity.LoanValue == null || sumPayed != entity.LoanValue)
                    return await Result<HrLoanEditDto>.FailAsync(localization.GetMessagesResource("TotalInstalNotEquVaLoan"));

                bool hasChanges = false;

                if (entity.Details != null)
                {
                    foreach (var singleitem in entity.Details)
                    {
                        if (singleitem.IsDeleted == true)
                        {
                            var installment = await hrRepositoryManager.HrLoanInstallmentRepository.GetById(singleitem.Id);
                            if (installment != null)
                            {
                                installment.IsDeleted = false;
                                installment.ModifiedBy = session.UserId;
                                installment.ModifiedOn = DateTime.Now;
                                hrRepositoryManager.HrLoanInstallmentRepository.Update(installment);
                                hasChanges = true;
                            }
                        }
                    }
                }

                if (hasChanges)
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                //  بداية تحديث السلفة بدون الاقساط
                _mapper.Map(entity, item);
                item.EmpId = checkEmp.Id.ToString();
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrLoanRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.files != null && entity.files.Count() > 0)
                {
                    var addFiles = await _mainRepositoryManager.SysFileRepository.SaveFiles(entity.files, entity.Id, 138);
                    await _mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                ///////////////////////////////////////
                return await Result<HrLoanEditDto>.SuccessAsync(_mapper.Map<HrLoanEditDto>(item), localization.GetResource1("UpdateSuccess"));

            }
            catch (Exception exc)
            {
                return await Result<HrLoanEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public Task<IResult<HrLoanEditDto>> Update(HrLoanEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> DeleteHrLoan(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var getLoanInstallment = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(e => e.IsDeleted == false && e.IsPaid == true && e.LoanId == Id);
                if (getLoanInstallment.Count() > 0) return await Result<string>.FailAsync(localization.GetMessagesResource("LoanDeleteHasPaidInstallments"));

                var LoanItem = await hrRepositoryManager.HrLoanRepository.GetById(Id);
                if (LoanItem == null) return Result<string>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                LoanItem.ModifiedBy = session.UserId;
                LoanItem.ModifiedOn = DateTime.Now;
                LoanItem.IsDeleted = true;
                hrRepositoryManager.HrLoanRepository.Update(LoanItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var LoanInstallmentItem = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(x => x.LoanId == Id);
                if (LoanInstallmentItem.Count() > 0)
                {
                    foreach (var item in LoanInstallmentItem)
                    {
                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        item.IsDeleted = true;
                        hrRepositoryManager.HrLoanInstallmentRepository.Update(item);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }

                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync("", localization.GetResource1("DeleteSuccess"));

            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
        public async Task<Result<HrLoanFilterDto>> GetLoanWithRemainingAmount(long loanId)
        {
            try
            {
                var loanResult = await hrRepositoryManager.HrLoanRepository.GetOneVw(x => x.Id == loanId);
                if (loanResult == null) return Result<HrLoanFilterDto>.Fail($"{localization.GetMessagesResource("NoIdInEdit")}");

                var installmentsResult = await hrRepositoryManager.HrLoanInstallmentRepository.GetAllVw(e => e.LoanId == loanId && e.IsDeleted == false && e.IsPaid == true);

                var paidInstallments = installmentsResult.Sum(x => x.Amount);
                var remainingAmount = (loanResult.LoanValue ?? 0) - paidInstallments;

                var loanDto = new HrLoanFilterDto
                {
                    Id = loanResult.Id,
                    LoanDate = loanResult.LoanDate,
                    LoanValue = loanResult.LoanValue,
                    InstallmentValue = loanResult.InstallmentValue,
                    InstallmentCount = loanResult.InstallmentCount,
                    BraName = loanResult.BraName,
                    DepName = loanResult.DepName,
                    LocationName = loanResult.LocationName,
                    Note = loanResult.Note,
                    EmpName = loanResult.EmpName,
                    EndDatePayment = loanResult.EndDatePayment,
                    StartDatePayment = loanResult.StartDatePayment,
                    EmpCode = loanResult.EmpCode,
                    RemainingAmount = remainingAmount,
                    Type = loanResult.Type
                };

                return await Result<HrLoanFilterDto>.SuccessAsync(loanDto, "", 200);
            }
            catch (Exception exp)
            {
                return await Result<HrLoanFilterDto>.FailAsync($"EXP in GetLoanWithRemainingAmountAsync at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> ReScheduleLoan(InstallmentScheduleDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("dataRequire")}");
            try
            {
                decimal? paidAmount = 0;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                // التأكد من وجود الموظف
                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.empCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                //  التأكد من وجود السلفة
                var item = await hrRepositoryManager.HrLoanRepository.GetById(entity.LoanId);
                if (item == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("NoDataWithId")} {entity.LoanId}");
                //  التأكد من تاريخ اول قسط
                //if (DateHelper.StringToDate(entity.SDatePyment) < DateTime.Now) await Result<string>.FailAsync("تاريخ اول قسط غير صحيح");


                //  جلب بيانات  الر اتب
                decimal TotalAllowance = 0;
                decimal? totalSalary = 0;
                var GetHREmpAllData = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.EmpId == entity.empCode);
                if (GetHREmpAllData == null)
                {
                    return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                }
                TotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(GetHREmpAllData.Id);

                totalSalary = GetHREmpAllData.Salary + TotalAllowance;

                if (totalSalary < 0) return await Result<string>.FailAsync(localization.GetMessagesResource("DetermineEmpSalary"));

                //  لمعرفة نسبه السداد والمتبقي
                var getLoanInstallment = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(e => e.IsDeleted == false && e.LoanId == entity.LoanId);
                if (getLoanInstallment.Count() > 0)
                {
                    // المبلغ المسدد
                    paidAmount = getLoanInstallment.Where(x => x.IsPaid == true).Sum(x => x.Amount);
                }
                decimal? RemainingAmount = item.LoanValue - paidAmount;
                //  اذا كانت السلفة منتهية 
                if (paidAmount == item.LoanValue || RemainingAmount <= 0)
                {
                    return await Result<string>.FailAsync(localization.GetMessagesResource("LoanFullyPaidCannotReschedule"));

                }


                //  بداية تقسيم الفترات
                DateTime dateInss = DateTime.Parse(entity.SDatePyment, CultureInfo.InvariantCulture);
                decimal installCount = entity.Installmentcount;
                int y = (int)Math.Floor(installCount);
                int InstallmentOrder = 1;
                decimal s = installCount - y;
                List<HrLoanInstallment> installmentList = new List<HrLoanInstallment>();
                decimal? installmentValue = RemainingAmount / entity.Installmentcount;

                for (int i = 0; i < y; i++)
                {
                    var newInstalment = new HrLoanInstallment
                    {
                        IntallmentNo = InstallmentOrder,
                        DueDate = dateInss.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                        Amount = installmentValue,
                        IsDeleted = false,
                        IsPaid = false,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        LoanId = entity.LoanId,
                        RecepitDate = null,
                        RecepitNo = null
                    };
                    installmentList.Add(newInstalment);
                    InstallmentOrder++;
                    dateInss = dateInss.AddMonths(1);
                }


                /////    حذف جميع الأقساط المتبقية واللتي لم تسدد
                var AllNonPaidInstalment = getLoanInstallment.Where(x => x.IsPaid == false);
                //  نحذف الاقساط السابقة الغير مدفوعة
                if (AllNonPaidInstalment != null)
                {
                    foreach (var record in AllNonPaidInstalment)
                    {

                        record.ModifiedBy = session.UserId;
                        record.ModifiedOn = DateTime.Now;
                        record.IsDeleted = true;
                        hrRepositoryManager.HrLoanInstallmentRepository.Update(record);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }

                //  هنا نقوم باضافة الجدولة الجديدة

                foreach (var newItem in installmentList)
                {

                    await hrRepositoryManager.HrLoanInstallmentRepository.Add(newItem);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in GetLoanWithRemainingAmountAsync at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        //public async Task<IResult<List<HrLoanFilterDto>>> Search(HrLoanFilterDto filter, CancellationToken cancellationToken = default)
        //{
        //    List<HrLoanFilterDto> results = new List<HrLoanFilterDto>();
        //    try
        //    {
        //        var BranchesList = session.Branches.Split(',');
        //        var getLoanInstallment = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(e => e.IsDeleted == false && e.IsPaid == true);
        //        var items = await hrRepositoryManager.HrLoanRepository.GetAllVw(e => e.IsDeleted == false &&
        //        (filter.DeptId == null || filter.DeptId == 0 || filter.DeptId == e.DeptId) &&
        //        (BranchesList.Contains(e.BranchId.ToString())) &&
        //        (filter.Location == null || filter.Location == 0 || filter.Location == e.Location) &&
        //        (filter.Type == null || filter.Type == 0 || filter.Type == e.Type) &&
        //        (string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpCode) &&
        //        (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower()))
        //        );

        //        if (items != null)
        //        {
        //            if (items.Count() > 0)
        //            {
        //                var res = items.AsQueryable();
        //                if (filter.BranchId > 0)
        //                {
        //                    res = res.Where(x => x.BranchId == filter.BranchId).AsQueryable();
        //                }
        //                if (!string.IsNullOrEmpty(filter.From) && !string.IsNullOrEmpty(filter.To))
        //                {
        //                    res = res.Where(x => x.LoanDate != null &&
        //                    DateHelper.StringToDate(x.LoanDate) >= DateHelper.StringToDate(filter.From) &&
        //                    DateHelper.StringToDate(x.LoanDate) <= DateHelper.StringToDate(filter.To)
        //                    ).AsQueryable();
        //                }

        //                if (res.Any())
        //                {
        //                    foreach (var item in res)
        //                    {
        //                        var paidInstallments = getLoanInstallment
        //                            .Where(x => x.LoanId == item.Id && x.IsPaid == true && x.IsDeleted == false)
        //                            .Sum(x => x.Amount);

        //                        var remainingAmount = (item.LoanValue ?? 0) - paidInstallments;

        //                        var singleItem = new HrLoanFilterDto
        //                        {
        //                            Id = item.Id,
        //                            LoanDate = item.LoanDate,
        //                            LoanValue = item.LoanValue,
        //                            InstallmentValue = item.InstallmentValue,
        //                            InstallmentCount = item.InstallmentCount,
        //                            BraName = item.BraName,
        //                            DepName = item.DepName,
        //                            LocationName = item.LocationName,
        //                            Note = item.Note,
        //                            EmpName = item.EmpName,
        //                            EndDatePayment = item.EndDatePayment,
        //                            StartDatePayment = item.StartDatePayment,
        //                            EmpCode = item.EmpCode,
        //                            RemainingAmount = remainingAmount
        //                        };

        //                        results.Add(singleItem);
        //                    }



        //                    return await Result<List<HrLoanFilterDto>>.SuccessAsync(results, "");
        //                }

        //                return await Result<List<HrLoanFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));
        //            }

        //            return await Result<List<HrLoanFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));
        //        }

        //        return await Result<List<HrLoanFilterDto>>.FailAsync("");
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Result<List<HrLoanFilterDto>>.FailAsync(ex.Message);
        //    }
        //}

        public async Task<IResult<List<HrLoanFilterDto>>> Search(HrLoanFilterDto filter, CancellationToken cancellationToken = default)
        {
            List<HrLoanFilterDto> results = new List<HrLoanFilterDto>();

            try
            {
                var branchesList = session.Branches.Split(',');
                filter.DeptId ??= 0;
                filter.BranchId ??= 0;
                filter.Type ??= 0;
                filter.Location ??= 0;
                // استرجاع القروض
                var items = await hrRepositoryManager.HrLoanRepository.GetAllVw(e =>
                    e.IsDeleted == false &&
                    (filter.DeptId == 0 || filter.DeptId == e.DeptId) &&
                      (filter.BranchId != 0
                            ? e.BranchId == filter.BranchId
                            : branchesList.Contains(e.BranchId.ToString())) &&
                    (filter.Location == 0 || filter.Location == e.Location) &&
                    (filter.Type == 0 || filter.Type == e.Type) &&
                    (string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpCode) &&
                    (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower()))
                );

                if (items == null || !items.Any())
                {
                    return await Result<List<HrLoanFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));
                }

                // تجهيز الـ query
                var res = items.AsQueryable();


                if (!string.IsNullOrEmpty(filter.From) && !string.IsNullOrEmpty(filter.To))
                {
                    res = res.Where(x =>
                        x.LoanDate != null &&
                        DateHelper.StringToDate(x.LoanDate) >= DateHelper.StringToDate(filter.From) &&
                        DateHelper.StringToDate(x.LoanDate) <= DateHelper.StringToDate(filter.To));
                }

                // الأقساط المدفوعة
                var installments = await hrRepositoryManager.HrLoanInstallmentRepository
                    .GetAll(x => x.IsDeleted == false && x.IsPaid == true);

                var paidInstallments = installments
                    .GroupBy(x => x.LoanId)
                    .Select(g => new { LoanId = g.Key, PaidAmount = g.Sum(x => x.Amount) })
                    .ToDictionary(x => x.LoanId, x => x.PaidAmount);

                // تجهيز النتيجة
                var resList = res.ToList();
                if (resList.Any())
                {
                    results = resList.Select(item =>
                    {
                        var paidAmount = paidInstallments.TryGetValue(item.Id, out var val) ? val : 0;
                        var remainingAmount = (item.LoanValue ?? 0) - paidAmount;

                        return new HrLoanFilterDto
                        {
                            Id = item.Id,
                            LoanDate = item.LoanDate,
                            LoanValue = item.LoanValue,
                            InstallmentValue = item.InstallmentValue,
                            InstallmentCount = item.InstallmentCount,
                            BraName = item.BraName,
                            DepName = item.DepName,
                            EmpName = item.EmpName,
                            LocationName = item.LocationName,
                            BraName2 = item.BraName2,
                            DepName2 = item.DepName2,
                            EmpName2 = item.EmpName2,
                            LocationName2 = item.LocationName2,
                            Note = item.Note,
                            EndDatePayment = item.EndDatePayment,
                            StartDatePayment = item.StartDatePayment,
                            EmpCode = item.EmpCode,
                            RemainingAmount = remainingAmount
                        };
                    }).ToList();

                    return await Result<List<HrLoanFilterDto>>.SuccessAsync(results, "");
                }

                return await Result<List<HrLoanFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));
            }
            catch (Exception ex)
            {
                return await Result<List<HrLoanFilterDto>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<object>> GetSumInstallmentLoanLoanId(HrLoanInstallmentInputDto obj, CancellationToken cancellationToken = default)
        {

            if (!DateTime.TryParseExact(obj.fromDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate))
            {
                return await Result<string>.FailAsync("Invalid fromDate or toDate format. Expected format: yyyy/MM/dd");
            }
            var empId = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == obj.empCode && x.IsDeleted == false);

            var result = await hrRepositoryManager.HrLoanInstallmentRepository
                .GetAllVw(x => x.EmpId == empId.Id.ToString()
                            && x.IsDeleted == false
                            && x.IsDeletedM == false
                            && x.IsPaid == false);
            if (!result.Any())
                return await Result<HrLoanDto>.FailAsync();
            string monthYear = startDate.ToString("MM/yyyy", CultureInfo.InvariantCulture);

            decimal x = obj.installmentcount ?? 0;
            int y = (int)Math.Floor(x);
            int NO = 1;
            decimal s = Convert.ToDecimal(obj.installmentcount) - y;
            var totalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(empId.Id);
            var totalSalary = (empId.Salary + totalAllowance) ?? 0;
            var dueDate = startDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            List<HrLoanInstallmentSummaryDto> installmentList = new List<HrLoanInstallmentSummaryDto>();
            for (int i = 0; i < y; i++)
            {
                var currentDueDate = startDate.AddMonths(i);
                string dueDateStr = currentDueDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string targetMonth = currentDueDate.ToString("MM/yyyy", CultureInfo.InvariantCulture);

                var getHrLoanInstallmentDto = new HrLoanInstallmentSummaryDto
                {
                    IntallmentNo = NO++,
                    DueDate = dueDateStr,
                    Amount = obj.installmentValue
                };

                getHrLoanInstallmentDto.SameMonthInstallments = result
                    .Where(c =>
                        !string.IsNullOrWhiteSpace(c.DueDate) &&
                        DateTime.TryParseExact(c.DueDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDueDate) &&
                        parsedDueDate.ToString("MM/yyyy", CultureInfo.InvariantCulture) == targetMonth)
                    .Sum(c => c.Amount);

                var installmentTotal = (getHrLoanInstallmentDto.SameMonthInstallments + obj.installmentValue) ?? 0;

                if (totalSalary > 0)
                {
                    getHrLoanInstallmentDto.Rate = Math.Round(installmentTotal / totalSalary * 100, 2);
                }
                else
                {
                    return await Result<string>.FailAsync(localization.GetMessagesResource("DetermineEmpSalary"));
                }

                installmentList.Add(getHrLoanInstallmentDto);
            }

            if (s > 0)
            {
                var currentDueDate = startDate.AddMonths(y);
                string dueDateStr = currentDueDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string targetMonth = currentDueDate.ToString("MM/yyyy", CultureInfo.InvariantCulture);
                var getHrLoanInstallmentDto = new HrLoanInstallmentSummaryDto();
                getHrLoanInstallmentDto.Amount = Math.Round((Convert.ToDecimal(obj.installmentValue) * s), 2);
                getHrLoanInstallmentDto.IntallmentNo = NO;
                getHrLoanInstallmentDto.DueDate = dueDateStr;

                getHrLoanInstallmentDto.SameMonthInstallments = result
                    .Where(c =>
                        !string.IsNullOrWhiteSpace(c.DueDate) &&
                        DateTime.TryParseExact(c.DueDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDueDate) &&
                        parsedDueDate.ToString("MM/yyyy", CultureInfo.InvariantCulture) == targetMonth)
                    .Sum(c => c.Amount);
                var installmentTotal = (getHrLoanInstallmentDto.SameMonthInstallments + getHrLoanInstallmentDto.Amount) ?? 0;
                if (totalSalary > 0)
                {
                    getHrLoanInstallmentDto.Rate = Math.Round(installmentTotal / totalSalary * 100, 2);
                }
                else
                {
                    getHrLoanInstallmentDto.Rate = 0;

                    return await Result<string>.FailAsync(localization.GetMessagesResource("DetermineEmpSalary"));

                }

                installmentList.Add(getHrLoanInstallmentDto);

            }

            return await Result<object>.SuccessAsync(installmentList);
        }
        public async Task<decimal> GetSumInstallmentLoanNotLoanId(HrLoanInstallmentNotLoanIdDto obj, CancellationToken cancellationToken = default)
        {

            if (!DateTime.TryParseExact(obj.fromDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate))
            {
                return 0;
            }
            //var empId = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == obj.empCode && x.IsDeleted == false);

            var result = await hrRepositoryManager.HrLoanInstallmentRepository
                .GetAllVw(x => x.EmpId == obj.empId.ToString()
                            && x.IsDeleted == false
                            && x.IsDeletedM == false
                            && x.IsPaid == false
                            && x.LoanId != obj.LoanId);
            if (!result.Any())
                return 0;
            string monthYear = startDate.ToString("MM/yyyy", CultureInfo.InvariantCulture);

            var sameMonth = result
                .Where(c =>
                    !string.IsNullOrWhiteSpace(c.DueDate) &&
                    DateTime.TryParseExact(c.DueDate, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDueDate) &&
                    parsedDueDate.ToString("MM/yyyy", CultureInfo.InvariantCulture) == monthYear)
                .Sum(c => c.Amount);

            return sameMonth ?? 0;
        }
    }
}
