using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrExpensesTypeService : GenericQueryService<HrExpensesType, HrExpensesTypeDto, HrExpensesTypeVw>, IHrExpensesTypeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMapper mapper;
        private readonly ICurrentData session;


        public HrExpensesTypeService(IQueryRepository<HrExpensesType> queryRepository, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrExpensesTypeDto>> Add(HrExpensesTypeDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrExpensesTypeDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {


                var newItem = mapper.Map<HrExpensesType>(entity);
                newItem.CreatedBy = session.UserId;
                newItem.CreatedOn = DateTime.Now;
                newItem.IsDeleted = false;
                var newAddEntity = await hrRepositoryManager.HrExpensesTypeRepository.AddAndReturn(newItem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = mapper.Map<HrExpensesTypeDto>(newAddEntity);

                return await Result<HrExpensesTypeDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrExpensesTypeDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrExpensesTypeRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrExpensesTypeDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrExpensesTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrExpensesTypeDto>.SuccessAsync(mapper.Map<HrExpensesTypeDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrExpensesTypeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrExpensesTypeRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrExpensesTypeDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrExpensesTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrExpensesTypeDto>.SuccessAsync(mapper.Map<HrExpensesTypeDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrExpensesTypeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrExpensesTypeEditDto>> Update(HrExpensesTypeEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrExpensesTypeEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrExpensesTypeRepository.GetById(entity.Id);

                if (item == null) return await Result<HrExpensesTypeEditDto>.FailAsync(localization.GetMessagesResource("NoItemFoundToEdit"));

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = false;
                item.Amount = entity.Amount;
                item.AccountDueId = entity.AccountDueId;
                item.AccountExpId = entity.AccountExpId;
                item.AccountPaidAdvanceId = entity.AccountPaidAdvanceId;
                item.Name = entity.Name;
                item.Name2 = entity.Name2;
                item.VatRate = entity.VatRate;
                item.NeedSchedul = entity.NeedSchedul;
                item.AppTypeIds = entity.AppTypeIds;
                hrRepositoryManager.HrExpensesTypeRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrExpensesTypeEditDto>.SuccessAsync(mapper.Map<HrExpensesTypeEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrExpensesTypeEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}