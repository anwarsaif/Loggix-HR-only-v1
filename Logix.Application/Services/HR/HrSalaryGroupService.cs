using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrSalaryGroupService : GenericQueryService<HrSalaryGroup, HrSalaryGroupDto, HrSalaryGroupVw>, IHrSalaryGroupService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData currentData;
        private readonly IAccRepositoryManager accRepositoryManager;
        private readonly ILocalizationService localization;


        public HrSalaryGroupService(IQueryRepository<HrSalaryGroup> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData currentData, IHrRepositoryManager hrRepositoryManager, IAccRepositoryManager accRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.currentData = currentData;
            this.hrRepositoryManager = hrRepositoryManager;
            this.accRepositoryManager = accRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrSalaryGroupDto>> Add(HrSalaryGroupDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrSalaryGroupDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var item = new HrSalaryGroup();
                item.CreatedBy = currentData.UserId;
                item.CreatedOn = DateTime.Now;
                item.Name = entity.Name;
                item.FacilityId = entity.FacilityId;
                // AccountSalaryCode
                item.AccountSalaryId = 0;
                if (!string.IsNullOrEmpty(entity.AccountSalaryCode))
                {
                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountSalaryCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("SalaryAccount")} لا يوجد");

                    item.AccountSalaryId = getAccountByCode;
                }


                // AccountAllowancesCode   
                item.AccountAllowancesId = 0;
                if (!string.IsNullOrEmpty(entity.AccountAllowancesCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountAllowancesCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("AllowancesAccount")} لا يوجد");

                    item.AccountAllowancesId = getAccountByCode;
                }


                // AccountOverTimeCode
                item.AccountOverTimeId = 0;
                if (!string.IsNullOrEmpty(entity.AccountOverTimeCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountOverTimeCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("OverTimeAccount")} لا يوجد");

                    item.AccountOverTimeId = getAccountByCode;
                }


                // AccountDeductionCode
                item.AccountDeductionId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDeductionCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDeductionCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("DeductionAccount")} لا يوجد");

                    item.AccountDeductionId = getAccountByCode;
                }


                // AccountDueSalaryCode
                item.AccountDueSalaryId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueSalaryCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueSalaryCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("SalaryDueAccount")} لا يوجد");

                    item.AccountDueSalaryId = getAccountByCode;
                }


                // AccountLoanCode
                item.AccountLoanId = 0;
                if (!string.IsNullOrEmpty(entity.AccountLoanCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountLoanCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("LoanAccount")} لا يوجد");

                    item.AccountLoanId = getAccountByCode;
                }


                // AccountOhadCode
                item.AccountOhadId = 0;
                if (!string.IsNullOrEmpty(entity.AccountOhadCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountOhadCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("OhadAccount")} لا يوجد");

                    item.AccountOhadId = getAccountByCode;
                }


                // AccountTicketsCode
                item.AccountTicketsId = 0;
                if (!string.IsNullOrEmpty(entity.AccountTicketsCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountTicketsCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("TicketsAccount")} لا يوجد");

                    item.AccountTicketsId = getAccountByCode;
                }


                // AccountVacationSalaryCode
                item.AccountVacationSalaryId = 0;
                if (!string.IsNullOrEmpty(entity.AccountVacationSalaryCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountVacationSalaryCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("VacationSalaryAccount")} لا يوجد");

                    item.AccountVacationSalaryId = getAccountByCode;
                }


                // AccountEndServiceCode
                item.AccountEndServiceId = 0;
                if (!string.IsNullOrEmpty(entity.AccountEndServiceCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountEndServiceCode, entity.FacilityId ?? 0);


                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("EndServiceAccount")} لا يوجد");

                    item.AccountEndServiceId = getAccountByCode;
                }


                // AccountDueTicketsCode
                item.AccountDueTicketsId = 0;

                if (!string.IsNullOrEmpty(entity.AccountDueTicketsCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueTicketsCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("DueTicketsAccount")} لا يوجد");

                    item.AccountDueTicketsId = getAccountByCode;
                }


                // AccountDueEndServiceCode
                item.AccountDueEndServiceId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueEndServiceCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueEndServiceCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("DueEndServiceAccount")} لا يوجد");

                    item.AccountDueEndServiceId = getAccountByCode;
                }


                // AccountDueVacationCode
                item.AccountDueVacationId = 0;

                if (!string.IsNullOrEmpty(entity.AccountDueVacationCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueVacationCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("DueVacationAccount")} لا يوجد");

                    item.AccountDueVacationId = getAccountByCode;
                }


                // AccountGosiCode
                item.AccountGosiId = 0;
                if (!string.IsNullOrEmpty(entity.AccountGosiCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountGosiCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("GosiAccount")} لا يوجد");

                    item.AccountGosiId = getAccountByCode;
                }


                // AccountDueGosiCode
                item.AccountDueGosiId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueGosiCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueGosiCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("DueGosiAccount")} لا يوجد");

                    item.AccountDueGosiId = getAccountByCode;
                }


                // AccountMandateCode
                item.AccountMandateId = 0;
                if (!string.IsNullOrEmpty(entity.AccountMandateCode))
                {


                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountMandateCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("MandateAccount")} لا يوجد");

                    item.AccountMandateId = getAccountByCode;
                }


                // AccountDueMandateCode
                item.AccountDueMandateId = 0;

                if (!string.IsNullOrEmpty(entity.AccountDueMandateCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueMandateCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("DueMandateAccount")} لا يوجد");

                    item.AccountDueMandateId = getAccountByCode;
                }


                // AccountCommissionCode
                item.AccountCommissionId = 0;
                if (!string.IsNullOrEmpty(entity.AccountCommissionCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountCommissionCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("CommissionAccount")} لا يوجد");

                    item.AccountCommissionId = getAccountByCode;
                }

                // AccountDueCommissionCode
                item.AccountDueCommissionId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueCommissionCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueCommissionCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("DueCommissionAccount")} لا يوجد");

                    item.AccountDueCommissionId = getAccountByCode;
                }


                // AccountMedicalInsuranceCode
                item.AccountMedicalInsuranceId = 0;
                if (!string.IsNullOrEmpty(entity.AccountMedicalInsuranceCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountMedicalInsuranceCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("AccountMedicalInsurance")} لا يوجد");

                    item.AccountMedicalInsuranceId = getAccountByCode;
                }

                // AccountPrepaidExpensesCode
                item.AccountPrepaidExpensesId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueCommissionCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountPrepaidExpensesCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupDto>.FailAsync($"{localization.GetHrResource("AccountPrepaidExpenses")} لا يوجد");

                    item.AccountPrepaidExpensesId = getAccountByCode;
                }

                var newEntity = await hrRepositoryManager.HrSalaryGroupRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrSalaryGroupDto>(newEntity);


                return await Result<HrSalaryGroupDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {

                return await Result<HrSalaryGroupDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrSalaryGroupRepository.GetById(Id);
                if (item == null) return Result<HrSalaryGroupDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)currentData.UserId;
                hrRepositoryManager.HrSalaryGroupRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrSalaryGroupDto>.SuccessAsync(_mapper.Map<HrSalaryGroupDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrSalaryGroupDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrSalaryGroupRepository.GetById(Id);
                if (item == null) return Result<HrSalaryGroupDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)currentData.UserId;
                hrRepositoryManager.HrSalaryGroupRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrSalaryGroupDto>.SuccessAsync(_mapper.Map<HrSalaryGroupDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrSalaryGroupDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrSalaryGroupEditDto>> Update(HrSalaryGroupEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                var item = await hrRepositoryManager.HrSalaryGroupRepository.GetById(entity.Id);
                if (item == null) return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");

                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)currentData.UserId;
                item.Name = entity.Name;
                item.FacilityId = entity.FacilityId;
                // AccountSalaryCode
                item.AccountSalaryId = 0;
                if (!string.IsNullOrEmpty(entity.AccountSalaryCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountSalaryCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("SalaryAccount")} لا يوجد");

                    item.AccountSalaryId = getAccountByCode;
                }


                // AccountAllowancesCode
                item.AccountAllowancesId = 0;
                if (!string.IsNullOrEmpty(entity.AccountAllowancesCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountAllowancesCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("AllowancesAccount")} لا يوجد");

                    item.AccountAllowancesId = getAccountByCode;
                }


                // AccountOverTimeCode
                item.AccountOverTimeId = 0;
                if (!string.IsNullOrEmpty(entity.AccountOverTimeCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountOverTimeCode, entity.FacilityId ?? 0);


                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("OverTimeAccount")} لا يوجد");

                    item.AccountOverTimeId = getAccountByCode;
                }


                // AccountDeductionCode
                item.AccountDeductionId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDeductionCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDeductionCode, entity.FacilityId ?? 0);


                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("DeductionAccount")} لا يوجد");

                    item.AccountDeductionId = getAccountByCode;
                }


                // AccountDueSalaryCode
                item.AccountDueSalaryId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueSalaryCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueSalaryCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("SalaryDueAccount")} لا يوجد");

                    item.AccountDueSalaryId = getAccountByCode;
                }


                // AccountLoanCode
                item.AccountLoanId = 0;
                if (!string.IsNullOrEmpty(entity.AccountLoanCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountLoanCode, entity.FacilityId ?? 0);


                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("LoanAccount")} لا يوجد");

                    item.AccountLoanId = getAccountByCode;
                }


                // AccountOhadCode
                item.AccountOhadId = 0;
                if (!string.IsNullOrEmpty(entity.AccountOhadCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountOhadCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("OhadAccount")} لا يوجد");

                    item.AccountOhadId = getAccountByCode;
                }


                // AccountTicketsCode
                item.AccountTicketsId = 0;
                if (!string.IsNullOrEmpty(entity.AccountTicketsCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountTicketsCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("TicketsAccount")} لا يوجد");

                    item.AccountTicketsId = getAccountByCode;
                }

                // AccountVacationSalaryCode
                item.AccountVacationSalaryId = 0;
                if (!string.IsNullOrEmpty(entity.AccountVacationSalaryCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountVacationSalaryCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("VacationSalaryAccount")} لا يوجد");

                    item.AccountVacationSalaryId = getAccountByCode;
                }


                // AccountEndServiceCode
                item.AccountEndServiceId = 0;
                if (!string.IsNullOrEmpty(entity.AccountEndServiceCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountEndServiceCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("EndServiceAccount")} لا يوجد");

                    item.AccountEndServiceId = getAccountByCode;
                }


                // AccountDueTicketsCode
                item.AccountDueTicketsId = 0;

                if (!string.IsNullOrEmpty(entity.AccountDueTicketsCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueTicketsCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("DueTicketsAccount")} لا يوجد");

                    item.AccountDueTicketsId = getAccountByCode;
                }


                // AccountDueEndServiceCode
                item.AccountDueEndServiceId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueEndServiceCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueEndServiceCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("DueEndServiceAccount")} لا يوجد");

                    item.AccountDueEndServiceId = getAccountByCode;
                }


                // AccountDueVacationCode
                item.AccountDueVacationId = 0;

                if (!string.IsNullOrEmpty(entity.AccountDueVacationCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueVacationCode, entity.FacilityId ?? 0);


                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("DueVacationAccount")} لا يوجد");

                    item.AccountDueVacationId = getAccountByCode;
                }


                // AccountGosiCode
                item.AccountGosiId = 0;
                if (!string.IsNullOrEmpty(entity.AccountGosiCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountGosiCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("GosiAccount")} لا يوجد");

                    item.AccountGosiId = getAccountByCode;
                }


                // AccountDueGosiCode
                item.AccountDueGosiId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueGosiCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueGosiCode, entity.FacilityId ?? 0);


                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("DueGosiAccount")} لا يوجد");

                    item.AccountDueGosiId = getAccountByCode;
                }


                // AccountMandateCode
                item.AccountMandateId = 0;
                if (!string.IsNullOrEmpty(entity.AccountMandateCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountMandateCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("MandateAccount")} لا يوجد");

                    item.AccountMandateId = getAccountByCode;
                }


                // AccountDueMandateCode
                item.AccountDueMandateId = 0;

                if (!string.IsNullOrEmpty(entity.AccountDueMandateCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueMandateCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("DueMandateAccount")} لا يوجد");

                    item.AccountDueMandateId = getAccountByCode;
                }


                // AccountCommissionCode
                item.AccountCommissionId = 0;
                if (!string.IsNullOrEmpty(entity.AccountCommissionCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountCommissionCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("CommissionAccount")} لا يوجد");

                    item.AccountCommissionId = getAccountByCode;
                }

                // AccountDueCommissionCode
                item.AccountDueCommissionId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueCommissionCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountDueCommissionCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("AccountMedicalInsuranceCode")} لا يوجد");

                    item.AccountDueCommissionId = getAccountByCode;
                }

                // AccountMedicalInsuranceCode
                item.AccountMedicalInsuranceId = 0;
                if (!string.IsNullOrEmpty(entity.AccountMedicalInsuranceCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountMedicalInsuranceCode, entity.FacilityId ?? 0);

                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("AccountMedicalInsurance")} لا يوجد");

                    item.AccountMedicalInsuranceId = getAccountByCode;
                }

                // AccountPrepaidExpensesCode
                item.AccountPrepaidExpensesId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueCommissionCode))
                {

                    var getAccountByCode = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountPrepaidExpensesCode, entity.FacilityId ?? 0);


                    if (getAccountByCode == 0)
                        return await Result<HrSalaryGroupEditDto>.FailAsync($"{localization.GetHrResource("AccountPrepaidExpenses")} لا يوجد");

                    item.AccountPrepaidExpensesId = getAccountByCode;
                }

                hrRepositoryManager.HrSalaryGroupRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrSalaryGroupEditDto>.SuccessAsync(_mapper.Map<HrSalaryGroupEditDto>(item), localization.GetResource1("SaveSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrSalaryGroupEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}
