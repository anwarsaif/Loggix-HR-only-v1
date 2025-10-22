using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
	public class HrJobCategoryService : GenericQueryService<HrJobCategory, HrJobCategoryDto, HrJobCategoriesVw>, IHrJobCategoryService
	{
		private readonly IHrRepositoryManager hrRepositoryManager;
		private readonly IMainRepositoryManager _mainRepositoryManager;
		private readonly IMapper _mapper;
		private readonly ICurrentData session;
		private readonly ILocalizationService localization;

		public HrJobCategoryService(IQueryRepository<HrJobCategory> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
		{
			this.hrRepositoryManager = hrRepositoryManager;
			this._mainRepositoryManager = mainRepositoryManager;
			this._mapper = mapper;
			this.session = session;
			this.localization = localization;
		}

		public async Task<IResult<HrJobCategoryDto>> Add(HrJobCategoryDto entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) return await Result<HrJobCategoryDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

			try
			{
				entity.IsDeleted = false;

				var item = _mapper.Map<HrJobCategory>(entity);
				var newEntity = await hrRepositoryManager.HrJobCategoryRepository.AddAndReturn(item);

				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				var entityMap = _mapper.Map<HrJobCategoryDto>(newEntity);


				return await Result<HrJobCategoryDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
			}
			catch (Exception exc)
			{

				return await Result<HrJobCategoryDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
			}
		}

		public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await hrRepositoryManager.HrJobCategoryRepository.GetById(Id);
				if (item == null) return Result<HrJobCategoryDto>.Fail($"--- there is no Data with this id: {Id}---");
				item.IsDeleted = true;
				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)session.UserId;
				hrRepositoryManager.HrJobCategoryRepository.Update(item);

				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobCategoryDto>.SuccessAsync(_mapper.Map<HrJobCategoryDto>(item), localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobCategoryDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}

		public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await hrRepositoryManager.HrJobCategoryRepository.GetById(Id);
				if (item == null) return Result<HrJobCategoryDto>.Fail($"--- there is no Data with this id: {Id}---");
				item.IsDeleted = true;
				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)session.UserId;
				hrRepositoryManager.HrJobCategoryRepository.Update(item);

				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobCategoryDto>.SuccessAsync(_mapper.Map<HrJobCategoryDto>(item), localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobCategoryDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}

		public async Task<IResult<HrJobCategoryEditDto>> Update(HrJobCategoryEditDto entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) return await Result<HrJobCategoryEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");
			try
			{
				var item = await hrRepositoryManager.HrJobCategoryRepository.GetById(entity.Id);

				if (item == null) return await Result<HrJobCategoryEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)session.UserId;
				_mapper.Map(entity, item);

				hrRepositoryManager.HrJobCategoryRepository.Update(item);


				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobCategoryEditDto>.SuccessAsync(_mapper.Map<HrJobCategoryEditDto>(item), localization.GetResource1("UpdateSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobCategoryEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}
	}
}
