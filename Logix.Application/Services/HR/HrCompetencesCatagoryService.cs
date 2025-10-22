using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrCompetencesCatagoryService : GenericQueryService<HrCompetencesCatagory, HrCompetencesCatagoryDto, HrCompetencesCatagory>, IHrCompetencesCatagoryService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrCompetencesCatagoryService(IQueryRepository<HrCompetencesCatagory> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session,ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrCompetencesCatagoryDto>> Add(HrCompetencesCatagoryDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrCompetencesCatagoryDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;

                var item = _mapper.Map<HrCompetencesCatagory>(entity);
                var newEntity = await hrRepositoryManager.HrCompetencesCatagoryRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrCompetencesCatagoryDto>(newEntity);


                return await Result<HrCompetencesCatagoryDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrCompetencesCatagoryDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        
        }

        public async   Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrCompetencesCatagoryRepository.GetById(Id);
                if (item == null) return Result<HrCompetencesCatagoryDto>.Fail($"--- there is no Data with this id: {Id}---");
                var CompetenceItems = await hrRepositoryManager.HrCompetenceRepository.GetAll(x => x.CatId == item.Id && x.IsDeleted == false);
                if (CompetenceItems != null && CompetenceItems.Count() >= 1) return Result<HrCompetencesCatagoryDto>.Fail($"لا يمكن حذف نوع الكفاءة هذا  يجب حذف كل الكفاءات المرتبطة به");



                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrCompetencesCatagoryRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCompetencesCatagoryDto>.SuccessAsync(_mapper.Map<HrCompetencesCatagoryDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrCompetencesCatagoryDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrCompetencesCatagoryRepository.GetById(Id);
            if (item == null) return Result<HrCompetencesCatagoryDto>.Fail($"--- there is no Data with this id: {Id}---");
           var CompetenceItems= await hrRepositoryManager.HrCompetenceRepository.GetAll(x=>x.CatId== item.Id &&x.IsDeleted==false);
            if (CompetenceItems != null && CompetenceItems.Count()>=1) return Result<HrCompetencesCatagoryDto>.Fail($"لا يمكن حذف نوع الكفاءة هذا  يجب حذف كل الكفاءات المرتبطة به");



            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrCompetencesCatagoryRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCompetencesCatagoryDto>.SuccessAsync(_mapper.Map<HrCompetencesCatagoryDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrCompetencesCatagoryDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrCompetencesCatagoryEditDto>> Update(HrCompetencesCatagoryEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrCompetencesCatagoryEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            var item = await hrRepositoryManager.HrCompetencesCatagoryRepository.GetById(entity.Id);

            if (item == null) return await Result<HrCompetencesCatagoryEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            hrRepositoryManager.HrCompetencesCatagoryRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCompetencesCatagoryEditDto>.SuccessAsync(_mapper.Map<HrCompetencesCatagoryEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrCompetencesCatagoryEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
    }
}