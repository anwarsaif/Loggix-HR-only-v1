using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading;

namespace Logix.Application.Services.HR
{
    public class HrDisciplinaryCaseActionService : GenericQueryService<HrDisciplinaryCaseAction, HrDisciplinaryCaseActionDto, HrDisciplinaryCaseActionVw>, IHrDisciplinaryCaseActionService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrDisciplinaryCaseActionService(IQueryRepository<HrDisciplinaryCaseAction> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrDisciplinaryCaseActionDto>> Add(HrDisciplinaryCaseActionDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDisciplinaryCaseActionDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                //check Empid
                var EmpItem = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCode && e.IsDeleted == false);

                if (EmpItem == null) return await Result<HrDisciplinaryCaseActionDto>.FailAsync($"{localization.GetResource1("EmployeeNotFound")}");
                if (EmpItem.StatusId == 2) return await Result<HrDisciplinaryCaseActionDto>.FailAsync($"{localization.GetHrResource("EmpNotActive")}");
                // Check_Emp_Exists_In_Payroll
                var IfEmpExistsInPayroll = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(e => e.EmpId.ToString() == EmpItem.EmpId && e.PayrollTypeId == 1 &&
                    e.IsDeleted == false &&
                    !string.IsNullOrWhiteSpace(e.StartDate) &&
                    !string.IsNullOrWhiteSpace(e.EndDate));

                    if (IfEmpExistsInPayroll.Any())
                {

                    var msDateStr = entity.DueDate;

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
                            return await Result<HrDisciplinaryCaseActionDto>.FailAsync("لن تتمكن من اضافة جزاء بسبب استخراج مسير للموظف في نفس الشهر");

                        }
                    }
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                entity.StatusId = 1;
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                var item = _mapper.Map<HrDisciplinaryCaseAction>(entity);
                item.EmpId = EmpItem.Id;
                var newEntity = await hrRepositoryManager.HrDisciplinaryCaseActionRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDisciplinaryCaseActionDto>(newEntity);

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await _mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 52);
                    await _mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                // End ChangeStatus_Payroll_Trans
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrDisciplinaryCaseActionDto>.SuccessAsync(entityMap, $"{localization.GetResource1("AddSuccess")}");

            }
            catch (Exception exc)
            {

                return await Result<HrDisciplinaryCaseActionDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrDisciplinaryCaseActionRepository.GetById(Id);
            if (item == null) return Result<HrDisciplinaryCaseActionDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrDisciplinaryCaseActionRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDisciplinaryCaseActionDto>.SuccessAsync(_mapper.Map<HrDisciplinaryCaseActionDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrDisciplinaryCaseActionDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrDisciplinaryCaseActionRepository.GetById(Id);
            if (item == null) return Result<HrDisciplinaryCaseActionDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrDisciplinaryCaseActionRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDisciplinaryCaseActionDto>.SuccessAsync(_mapper.Map<HrDisciplinaryCaseActionDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrDisciplinaryCaseActionDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrDisciplinaryCaseActionEditDto>> Update(HrDisciplinaryCaseActionEditDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrDisciplinaryCaseActionEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            try
            {            //check Empid
                var EmpItem = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCode && e.IsDeleted == false);

                if (EmpItem == null) return await Result<HrDisciplinaryCaseActionEditDto>.FailAsync($"{localization.GetResource1("EmployeeNotFound")}");

                var item = await hrRepositoryManager.HrDisciplinaryCaseActionRepository.GetById(entity.Id);

                if (item == null) return await Result<HrDisciplinaryCaseActionEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(entity, item);
                item.EmpId = EmpItem.Id;
                hrRepositoryManager.HrDisciplinaryCaseActionRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await _mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, entity.Id, 52);
                    await _mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                // End ChangeStatus_Payroll_Trans
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrDisciplinaryCaseActionEditDto>.SuccessAsync(_mapper.Map<HrDisciplinaryCaseActionEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrDisciplinaryCaseActionEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
        public async Task<IResult<decimal>> Apply_Policies(long Facility_ID, long Policie_ID, long Emp_ID)
        {
            try
            {
                var Amount = await _mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(Facility_ID, Policie_ID, Emp_ID);

                return await Result<decimal>.SuccessAsync(Amount);
            }
            catch (Exception exp)
            {
                return await Result<decimal>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
    }

}