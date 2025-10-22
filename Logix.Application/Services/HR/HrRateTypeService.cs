using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrRateTypeService : GenericQueryService<HrRateType, HrRateTypeDto, HrRateTypeVw>, IHrRateTypeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrRateTypeService(IQueryRepository<HrRateType> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager; 
        }

        public Task<IResult<HrRateTypeDto>> Add(HrRateTypeDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrRateTypeEditDto>> Update(HrRateTypeEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //public async Task<IResult<HrRateTypeDto>> Add(HrRateTypeDto entity, CancellationToken cancellationToken = default)
        //{
        //    if (entity == null) return await Result<HrRateTypeDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

        //    try
        //    {

        //        entity.CreatedBy = session.UserId;
        //        entity.CreatedOn = DateTime.Now;

        //        var item = _mapper.Map<HrRateType>(entity);
        //        var newEntity = await hrRepositoryManager.HrRateTypeRepository.AddAndReturn(item);

        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        var entityMap = _mapper.Map<HrRateTypeDto>(newEntity);


        //        return await Result<HrRateTypeDto>.SuccessAsync(entityMap, "item added successfully");
        //    }
        //    catch (Exception exc)
        //    {

        //        return await Result<HrRateTypeDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
        //    }
        //}

        //public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        //{
        //    var item = await hrRepositoryManager.HrRateTypeRepository.GetById(Id);
        //    if (item == null) return Result<HrRateTypeDto>.Fail($"--- there is no Data with this id: {Id}---");
        //    var CompetenceItems = await hrRepositoryManager.HrCompetenceRepository.GetAll(x => x.CatId == item.Id && x.IsDeleted == false);
        //    if (CompetenceItems != null && CompetenceItems.Count() >= 1) return Result<HrRateTypeDto>.Fail($"لا يمكن حذف نوع الكفاءة هذا  يجب حذف كل الكفاءات المرتبطة به");



        //    item.IsDeleted = true;
        //    item.ModifiedOn = DateTime.Now;
        //    item.ModifiedBy = (int)session.UserId;
        //    hrRepositoryManager.HrRateTypeRepository.Update(item);
        //    try
        //    {
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        return await Result<HrRateTypeDto>.SuccessAsync(_mapper.Map<HrRateTypeDto>(item), " record removed");
        //    }
        //    catch (Exception exp)
        //    {
        //        return await Result<HrRateTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
        //    }
        //}

        //public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        //{
        //    var item = await hrRepositoryManager.HrRateTypeRepository.GetById(Id);
        //    if (item == null) return Result<HrRateTypeDto>.Fail($"--- there is no Data with this id: {Id}---");
        //    var CompetenceItems = await hrRepositoryManager.HrCompetenceRepository.GetAll(x => x.CatId == item.Id && x.IsDeleted == false);
        //    if (CompetenceItems != null && CompetenceItems.Count() >= 1) return Result<HrRateTypeDto>.Fail($"لا يمكن حذف نوع الكفاءة هذا  يجب حذف كل الكفاءات المرتبطة به");



        //    item.IsDeleted = true;
        //    item.ModifiedOn = DateTime.Now;
        //    item.ModifiedBy = (int)session.UserId;
        //    hrRepositoryManager.HrRateTypeRepository.Update(item);
        //    try
        //    {
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        return await Result<HrRateTypeDto>.SuccessAsync(_mapper.Map<HrRateTypeDto>(item), " record removed");
        //    }
        //    catch (Exception exp)
        //    {
        //        return await Result<HrRateTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
        //    }
        //}

        //public async Task<IResult<HrRateTypeEditDto>> Update(HrRateTypeEditDto entity, CancellationToken cancellationToken = default)
        //{
        //    if (entity == null) return await Result<HrRateTypeEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

        //    var item = await hrRepositoryManager.HrRateTypeRepository.GetById(entity.Id);

        //    if (item == null) return await Result<HrRateTypeEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
        //    item.ModifiedOn = DateTime.Now;
        //    item.ModifiedBy = (int)session.UserId;
        //    _mapper.Map(entity, item);

        //    hrRepositoryManager.HrRateTypeRepository.Update(item);

        //    try
        //    {
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        return await Result<HrRateTypeEditDto>.SuccessAsync(_mapper.Map<HrRateTypeEditDto>(item), "Item updated successfully");
        //    }
        //    catch (Exception exp)
        //    {
        //        Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
        //        return await Result<HrRateTypeEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
        //    }
        //}


    }


    }