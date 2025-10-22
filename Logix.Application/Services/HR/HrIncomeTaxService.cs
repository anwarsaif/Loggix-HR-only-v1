using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrIncomeTaxService : GenericQueryService<HrIncomeTax, HrIncomeTaxDto, HrIncomeTaxVw>, IHrIncomeTaxService
    {
        private readonly IMapper mapper;
        private readonly IAccRepositoryManager accRepositoryManager;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly ICurrentData session;
        private readonly IUnitOfWork unitOfWork;

        public HrIncomeTaxService(IQueryRepository<HrIncomeTax> queryRepository,
            IMapper mapper,
            IAccRepositoryManager accRepositoryManager,
            IHrRepositoryManager hrRepositoryManager,
            ILocalizationService localization,
            ICurrentData session,
            IUnitOfWork unitOfWork
            ) : base(queryRepository, mapper)
        {
            this.mapper = mapper;
            this.accRepositoryManager = accRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.session = session;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IResult<HrIncomeTaxDto>> Add(HrIncomeTaxDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrIncomeTaxDto>.FailAsync(localization.GetMessagesResource("AddNullEntity"));

            try
            {
                long Acc_Account_ID = 0;
                if (!string.IsNullOrEmpty(entity.AccountCode))
                {
                    var account = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountCode, session.FacilityId);

                    if (account == 0)
                        return await Result<HrIncomeTaxDto>.FailAsync(localization.GetMessagesResource("AccountCodeNotFound"));
                    Acc_Account_ID = account;
                }

                var item = mapper.Map<HrIncomeTax>(entity);
                item.TaxCode = entity.TaxCode;
                item.TaxName = entity.TaxName;
                item.TaxName2 = entity.TaxName2;
                item.AccountId = Acc_Account_ID;
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;

                var newEntity = await hrRepositoryManager.HrIncomeTaxRepository.AddAndReturn(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = mapper.Map<HrIncomeTaxDto>(newEntity);
                return await Result<HrIncomeTaxDto>.SuccessAsync(entityMap, localization.GetMessagesResource("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrIncomeTaxDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
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

        public async Task<IResult<HrIncomeTaxEditDto>> Update(HrIncomeTaxEditDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrIncomeTaxEditDto>.FailAsync(localization.GetMessagesResource("UpdateNullEntity"));

            var item = await hrRepositoryManager.HrIncomeTaxRepository.GetById(entity.Id);

            if (item == null) return await Result<HrIncomeTaxEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));


            long Acc_Account_ID = 0;
            if (!string.IsNullOrEmpty(entity.AccountCode))
            {
                var account = await accRepositoryManager.AccAccountRepository.GetAccountIdByCode(entity.AccountCode, session.FacilityId);
                if (account == 0)
                    return await Result<HrIncomeTaxEditDto>.FailAsync(localization.GetMessagesResource("AccountCodeNotFound"));
                Acc_Account_ID = account;
            }

            item.TaxCode = entity.TaxCode;
            item.TaxName = entity.TaxName;
            item.TaxName2 = entity.TaxName2;
            item.AccountId = Acc_Account_ID;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;

            hrRepositoryManager.HrIncomeTaxRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrIncomeTaxEditDto>.SuccessAsync(mapper.Map<HrIncomeTaxEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrIncomeTaxEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}
