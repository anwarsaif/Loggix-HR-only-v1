using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrVacationsCatagoryService : GenericQueryService<HrVacationsCatagory, HrVacationsCatagoryDto, HrVacationsCatagory>, IHrVacationsCatagoryService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrVacationsCatagoryService(IQueryRepository<HrVacationsCatagory> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager; 
        }

        public Task<IResult<HrVacationsCatagoryDto>> Add(HrVacationsCatagoryDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrVacationsCatagoryEditDto>> Update(HrVacationsCatagoryEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //public async Task<IResult<HrVacationsCatagoryDto>> Add(HrVacationsCatagoryDto entity, CancellationToken cancellationToken = default)
        //{
        //   ret
        //}

        //public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        //{
        //    var item = await hrRepositoryManager.HrVacationsCatagoryRepository.GetById(Id);
        //    if (item == null) return Result<HrVacationsCatagoryDto>.Fail($"--- there is no Data with this id: {Id}---");
        //    var CompetenceItems = await hrRepositoryManager.HrVacationsCatagoryRepository.GetAll(x => x.CatId == item.CatId && x.IsDeleted == false);
        //    if (CompetenceItems != null && CompetenceItems.Count() >= 1) return Result<HrVacationsCatagoryDto>.Fail($"لا يمكن حذف نوع الكفاءة هذا  يجب حذف كل الكفاءات المرتبطة به");



        //    item.IsDeleted = true;
        //    item.ModifiedOn = DateTime.Now;
        //    item.ModifiedBy = (int)session.UserId;
        //    hrRepositoryManager.HrVacationsCatagoryRepository.Update(item);
        //    try
        //    {
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        return await Result<HrVacationsCatagoryDto>.SuccessAsync(_mapper.Map<HrVacationsCatagoryDto>(item), " record removed");
        //    }
        //    catch (Exception exp)
        //    {
        //        return await Result<HrVacationsCatagoryDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
        //    }
        //}

        //public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        //{
        //    var item = await hrRepositoryManager.HrVacationsCatagoryRepository.GetById(Id);
        //    if (item == null) return Result<HrVacationsCatagoryDto>.Fail($"--- there is no Data with this id: {Id}---");
        //    var CompetenceItems = await hrRepositoryManager.HrVacationsCatagoryRepository.GetAll(x => x.CatId == item.CatId && x.IsDeleted == false);
        //    if (CompetenceItems != null && CompetenceItems.Count() >= 1) return Result<HrVacationsCatagoryDto>.Fail($"لا يمكن حذف نوع الكفاءة هذا  يجب حذف كل الكفاءات المرتبطة به");



        //    item.IsDeleted = true;
        //    item.ModifiedOn = DateTime.Now;
        //    item.ModifiedBy = (int)session.UserId;
        //    hrRepositoryManager.HrVacationsCatagoryRepository.Update(item);
        //    try
        //    {
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        return await Result<HrVacationsCatagoryDto>.SuccessAsync(_mapper.Map<HrVacationsCatagoryDto>(item), " record removed");
        //    }
        //    catch (Exception exp)
        //    {
        //        return await Result<HrVacationsCatagoryDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
        //    }
        //}

        //public async Task<IResult<HrVacationsCatagoryEditDto>> Update(HrVacationsCatagoryEditDto entity, CancellationToken cancellationToken = default)
        //{
        //    if (entity == null) return await Result<HrVacationsCatagoryEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

        //    var item = await hrRepositoryManager.HrVacationsCatagoryRepository.GetById(entity.CatId);

        //    if (item == null) return await Result<HrVacationsCatagoryEditDto>.FailAsync($"--- there is no Data with this id: {entity.CatId}---");
        //    item.ModifiedOn = DateTime.Now;
        //    item.ModifiedBy = (int)session.UserId;
        //    _mapper.Map(entity, item);

        //    hrRepositoryManager.HrVacationsCatagoryRepository.Update(item);

        //    try
        //    {
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        return await Result<HrVacationsCatagoryEditDto>.SuccessAsync(_mapper.Map<HrVacationsCatagoryEditDto>(item), "Item updated successfully");
        //    }
        //    catch (Exception exp)
        //    {
        //        Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
        //        return await Result<HrVacationsCatagoryEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
        //    }
        //}


    }


}