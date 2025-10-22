using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrPoliciesTypeService : GenericQueryService<HrPoliciesType, HrPoliciesTypeDto, HrPoliciesType>, IHrPoliciesTypeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrPoliciesTypeService(IQueryRepository<HrPoliciesType> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrPoliciesTypeDto>> Add(HrPoliciesTypeDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPoliciesTypeDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {

                var item = _mapper.Map<HrPoliciesType>(entity);
                var newEntity = await hrRepositoryManager.HrPoliciesTypeRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrPoliciesTypeDto>(newEntity);


                return await Result<HrPoliciesTypeDto>.SuccessAsync(entityMap, localization.GetMessagesResource("success"));
            }
            catch (Exception exc)
            {

                return await Result<HrPoliciesTypeDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrPoliciesTypeRepository.GetById(Id);
            if (item == null) return Result<HrPoliciesTypeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
            hrRepositoryManager.HrPoliciesTypeRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPoliciesTypeDto>.SuccessAsync(_mapper.Map<HrPoliciesTypeDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPoliciesTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrPoliciesTypeRepository.GetById(Id);
            if (item == null) return Result<HrPoliciesTypeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
            hrRepositoryManager.HrPoliciesTypeRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPoliciesTypeDto>.SuccessAsync(_mapper.Map<HrPoliciesTypeDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPoliciesTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrPoliciesTypeEditDto>> Update(HrPoliciesTypeEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPoliciesTypeEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            var item = await hrRepositoryManager.HrPoliciesTypeRepository.GetById(entity.TypeId);

            if (item == null) return await Result<HrPoliciesTypeEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            _mapper.Map(entity, item);

            hrRepositoryManager.HrPoliciesTypeRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPoliciesTypeEditDto>.SuccessAsync(_mapper.Map<HrPoliciesTypeEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrPoliciesTypeEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
    }
}