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
	public class HrJobGroupsService : GenericQueryService<HrJobGroups, HrJobGroupsDto, HrJobGroupsVw>, IHrJobGroupsService
	{
		private readonly IHrRepositoryManager hrRepositoryManager;

		private readonly IMainRepositoryManager _mainRepositoryManager;
		private readonly IMapper _mapper;
		private readonly ICurrentData session;
		private readonly ILocalizationService localization;



		public HrJobGroupsService(IQueryRepository<HrJobGroups> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
		{
			this._mainRepositoryManager = mainRepositoryManager;
			this._mapper = mapper;
			this.session = session;
			this.hrRepositoryManager = hrRepositoryManager;
			this.localization = localization;
		}

		public async Task<IResult<HrJobGroupsDto>> Add(HrJobGroupsDto entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) return await Result<HrJobGroupsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

			try
			{
				entity.IsDeleted = false;

				var item = _mapper.Map<HrJobGroups>(entity);
				var newEntity = await hrRepositoryManager.HrJobGroupsRepository.AddAndReturn(item);

				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				var entityMap = _mapper.Map<HrJobGroupsDto>(newEntity);


				return await Result<HrJobGroupsDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
			}
			catch (Exception exc)
			{

				return await Result<HrJobGroupsDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
			}
		}

		public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await hrRepositoryManager.HrJobGroupsRepository.GetById(Id);
				if (item == null) return Result<HrJobGroups>.Fail($"--- there is no Data with this id: {Id}---");
				item.IsDeleted = true;
				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)session.UserId;
				hrRepositoryManager.HrJobGroupsRepository.Update(item);

				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobGroupsDto>.SuccessAsync(_mapper.Map<HrJobGroupsDto>(item), localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobGroupsDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}

		public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await hrRepositoryManager.HrJobGroupsRepository.GetById(Id);
				if (item == null) return Result<HrJobGroups>.Fail($"--- there is no Data with this id: {Id}---");
				item.IsDeleted = true;
				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)session.UserId;
				hrRepositoryManager.HrJobGroupsRepository.Update(item);

				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobGroupsDto>.SuccessAsync(_mapper.Map<HrJobGroupsDto>(item), localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobGroupsDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}

		}

		public async Task<IResult<HrJobGroupsEditDto>> Update(HrJobGroupsEditDto entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) return await Result<HrJobGroupsEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");
			try
			{
				var item = await hrRepositoryManager.HrJobGroupsRepository.GetById(entity.Id??0);

				if (item == null) return await Result<HrJobGroupsEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)session.UserId;
				_mapper.Map(entity, item);

				hrRepositoryManager.HrJobGroupsRepository.Update(item);


				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobGroupsEditDto>.SuccessAsync(_mapper.Map<HrJobGroupsEditDto>(item), localization.GetResource1("UpdateSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobGroupsEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}
	}
}
