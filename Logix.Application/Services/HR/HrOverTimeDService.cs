using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrOverTimeDService : GenericQueryService<HrOverTimeD, HrOverTimeDDto, HrOverTimeDVw>, IHrOverTimeDService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrOverTimeDService(IQueryRepository<HrOverTimeD> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;

            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrOverTimeDDto>> Add(HrOverTimeDDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrOverTimeDDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                var item = _mapper.Map<HrOverTimeD>(entity);
                item.CreatedBy= session.UserId;
                item.CreatedOn = DateTime.Now;
                var newEntity = await hrRepositoryManager.HrOverTimeDRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrOverTimeDDto>(newEntity);


                return await Result<HrOverTimeDDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrOverTimeDDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }



        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrOverTimeDRepository.GetById(Id);
                if (item == null) return Result<HrOverTimeDDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrOverTimeDRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrOverTimeDDto>.SuccessAsync(_mapper.Map<HrOverTimeDDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrOverTimeDDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrOverTimeDRepository.GetById(Id);
                if (item == null) return Result<HrOverTimeDDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrOverTimeDRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrOverTimeDDto>.SuccessAsync(_mapper.Map<HrOverTimeDDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrOverTimeDDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrOverTimeDEditDto>> Update(HrOverTimeDEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrOverTimeDEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            var item = await hrRepositoryManager.HrOverTimeDRepository.GetById(entity.Id);

            if (item == null) return await Result<HrOverTimeDEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            hrRepositoryManager.HrOverTimeDRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrOverTimeDEditDto>.SuccessAsync(_mapper.Map<HrOverTimeDEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrOverTimeDEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

    }
}