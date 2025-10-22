using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrAttShiftTimeTableService : GenericQueryService<HrAttShiftTimeTable, HrAttShiftTimeTableDto, HrAttShiftTimeTableVw>, IHrAttShiftTimeTableService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        public HrAttShiftTimeTableService(IQueryRepository<HrAttShiftTimeTable> queryRepository, IMainRepositoryManager mainRepositoryManager, ILocalizationService localization, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrAttShiftTimeTableDto>> Add(HrAttShiftTimeTableDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAttShiftTimeTableDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                var item = _mapper.Map<HrAttShiftTimeTable>(entity);

                var newEntity = await hrRepositoryManager.HrAttShiftTimeTableRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                var entityMap = _mapper.Map<HrAttShiftTimeTableDto>(newEntity);

                return await Result<HrAttShiftTimeTableDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<HrAttShiftTimeTableDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrAttShiftTimeTableRepository.GetById(Id);
                if (item == null) return Result<HrAttShiftTimeTableDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrAttShiftTimeTableRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttShiftTimeTableDto>.SuccessAsync(_mapper.Map<HrAttShiftTimeTableDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrAttShiftTimeTableDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrAttShiftTimeTableRepository.GetById(Id);
                if (item == null) return Result<HrAttShiftTimeTableDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrAttShiftTimeTableRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttShiftTimeTableDto>.SuccessAsync(_mapper.Map<HrAttShiftTimeTableDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrAttShiftTimeTableDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrAttShiftTimeTableEditDto>> Update(HrAttShiftTimeTableEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAttShiftTimeTableEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrAttShiftTimeTableRepository.GetById(entity.Id);

                if (item == null) return await Result<HrAttShiftTimeTableEditDto>.FailAsync(localization.GetResource1("UpdateError"));

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                _mapper.Map(entity, item);

                hrRepositoryManager.HrAttShiftTimeTableRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttShiftTimeTableEditDto>.SuccessAsync(_mapper.Map<HrAttShiftTimeTableEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrAttShiftTimeTableEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}
