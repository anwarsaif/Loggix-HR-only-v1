using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrClearanceMonthService : GenericQueryService<HrClearanceMonth, HrClearanceMonthDto, HrClearanceMonthsVw>, IHrClearanceMonthService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMapper _mapper;

        public HrClearanceMonthService(IQueryRepository<HrClearanceMonth> queryRepository,
            IMapper mapper,
            IHrRepositoryManager hrRepositoryManager,
            ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mapper = mapper;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrClearanceMonthDto>> Add(HrClearanceMonthDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrClearanceMonthDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                var newItem = _mapper.Map<HrClearanceMonth>(entity);
                var newEntity = await hrRepositoryManager.HrClearanceMonthRepository.AddAndReturn(newItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                return await Result<HrClearanceMonthDto>.SuccessAsync(_mapper.Map<HrClearanceMonthDto>(newEntity), localization.GetResource1("AddSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrClearanceMonthDto>.FailAsync($"EXP in Add at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrClearanceMonthRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrClearanceMonthDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrClearanceMonthRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                return await Result<HrClearanceMonthDto>.SuccessAsync(_mapper.Map<HrClearanceMonthDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrClearanceMonthDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrClearanceMonthEditDto>> Update(HrClearanceMonthEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
