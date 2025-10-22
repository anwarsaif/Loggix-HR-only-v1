using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrVacationsTypeService : GenericQueryService<HrVacationsType, HrVacationsTypeDto, HrVacationsTypeVw>, IHrVacationsTypeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData currentData;


        public HrVacationsTypeService(IQueryRepository<HrVacationsType> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData currentData, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.currentData = currentData;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }


        public async Task<IResult<HrVacationsTypeDto>> Add(HrVacationsTypeDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrVacationsTypeDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                // إعداد الحقول الثابتة كما في الكود القديم
                entity.CreatedBy = currentData.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.EmpTypeId = 1;

                // تحويل SsCode الفارغ إلى "0" كما في:
                // .SS_Code = IIf(SS_Code = "", 0, SS_Code)
                entity.SsCode = string.IsNullOrEmpty(entity.SsCode) ? "0" : entity.SsCode;

                // تعيين القيم الافتراضية المنطقية عند الحاجة (اختياري حسب التصميم)
                entity.ValidateBalance ??= false;
                entity.WeekendInclude ??= false;
                entity.DeductedServicePeriod ??= false;
                entity.DeductedBalanceVacation ??= false;
                entity.NeedJoinRequest ??= false;
                entity.AttachRequired ??= false;
                entity.AlternativeEmpRequired ??= false;
                entity.IsDeleted ??= false;

                // التحويل إلى الكيان الفعلي
                var item = _mapper.Map<HrVacationsType>(entity);

                // الإضافة إلى قاعدة البيانات
                var newEntity = await hrRepositoryManager.HrVacationsTypeRepository.AddAndReturn(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // إعادة النتيجة
                var entityMap = _mapper.Map<HrVacationsTypeDto>(newEntity);
                return await Result<HrVacationsTypeDto>.SuccessAsync(entityMap, localization.GetMessagesResource("success"));
            }
            catch (Exception exc)
            {
                return await Result<HrVacationsTypeDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }


        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var VacationTypeId = await hrRepositoryManager.HrVacationsRepository.Check_Have_Vacation_Type_Id((int)Id);
                if (VacationTypeId > 0)
                {
                    return Result<HrVacationsTypeDto>.Fail(localization.GetResource1("TypeOfVacationsLinkedToEmployeeVacations"));
                }
                var item = await hrRepositoryManager.HrVacationsTypeRepository.GetById(Id);
                if (item == null) return Result<AccAccountDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)currentData.UserId;
                hrRepositoryManager.HrVacationsTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVacationsTypeDto>.SuccessAsync(_mapper.Map<HrVacationsTypeDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrVacationsTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var VacationTypeId = await hrRepositoryManager.HrVacationsRepository.Check_Have_Vacation_Type_Id(Id);
                if (VacationTypeId > 0)
                {
                    return Result<HrVacationsTypeDto>.Fail(localization.GetResource1("TypeOfVacationsLinkedToEmployeeVacations"));
                }
                var item = await hrRepositoryManager.HrVacationsTypeRepository.GetById(Id);
                if (item == null) return Result<AccAccountDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)currentData.UserId;
                hrRepositoryManager.HrVacationsTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVacationsTypeDto>.SuccessAsync(_mapper.Map<HrVacationsTypeDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrVacationsTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrVacationsTypeEditDto>> Update(HrVacationsTypeEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrVacationsTypeEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            try
            {
                // إعداد الحقول الثابتة كما في الكود القديم
                entity.EmpTypeId = 1;

                // تحويل SsCode الفارغ إلى "0" كما في:
                // .SS_Code = IIf(SS_Code = "", 0, SS_Code)
                entity.SsCode = string.IsNullOrEmpty(entity.SsCode) ? "0" : entity.SsCode;

                // تعيين القيم الافتراضية المنطقية عند الحاجة (اختياري حسب التصميم)
                entity.ValidateBalance ??= false;
                entity.WeekendInclude ??= false;
                entity.DeductedServicePeriod ??= false;
                entity.DeductedBalanceVacation ??= false;
                entity.NeedJoinRequest ??= false;
                entity.AttachRequired ??= false;
                entity.AlternativeEmpRequired ??= false;
                var item = await hrRepositoryManager.HrVacationsTypeRepository.GetById(entity.VacationTypeId);

                if (item == null) return await Result<HrVacationsTypeEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)currentData.UserId;
                item.EmpTypeId = 1;
                if (string.IsNullOrEmpty(entity.SsCode)) entity.SsCode = "0";
                _mapper.Map(entity, item);

                hrRepositoryManager.HrVacationsTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVacationsTypeEditDto>.SuccessAsync(_mapper.Map<HrVacationsTypeEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrVacationsTypeEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<HrVacationsTypeEditDto>> AddEditVacationPolicies(HrVacationsTypeEditVacationPoliciesDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrVacationsTypeEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            try
            {
                var item = await hrRepositoryManager.HrVacationsTypeRepository.GetById(entity.VacationTypeId);

                if (item == null) return await Result<HrVacationsTypeEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

                item.RateType = entity.RateType;
                item.SalaryBasic = entity.SalaryBasic;
                item.Allowance = entity.Allowance;
                item.Deduction = entity.Deduction;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)currentData.UserId;

                hrRepositoryManager.HrVacationsTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVacationsTypeEditDto>.SuccessAsync(_mapper.Map<HrVacationsTypeEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrVacationsTypeEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}