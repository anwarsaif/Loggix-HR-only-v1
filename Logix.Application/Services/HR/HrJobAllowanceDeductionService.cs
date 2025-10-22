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
	public class HrJobAllowanceDeductionService :
		GenericQueryService<HrJobAllowanceDeduction, HrJobAllowanceDeductionDto, HrJobAllowanceDeductionVw>,
		IHrJobAllowanceDeductionService
	{
		private readonly IHrRepositoryManager _hrRepositoryManager;
		private readonly ILocalizationService _localization;
		private readonly IMainRepositoryManager _mainRepositoryManager;
		private readonly IMapper _mapper;
		private readonly ICurrentData _session;

		public HrJobAllowanceDeductionService(
			IQueryRepository<HrJobAllowanceDeduction> queryRepository,
			IMainRepositoryManager mainRepositoryManager,
			IMapper mapper,
			ICurrentData session,
			ILocalizationService localization,
			IHrRepositoryManager hrRepositoryManager)
			: base(queryRepository, mapper)
		{
			_mainRepositoryManager = mainRepositoryManager;
			_localization = localization;
			_mapper = mapper;
			_session = session;
			_hrRepositoryManager = hrRepositoryManager;
		}

		public async Task<IResult<HrJobAllowanceDeductionDto>> Add(HrJobAllowanceDeductionDto entity, CancellationToken cancellationToken = default)
		{
			if (entity == null)
				return await Result<HrJobAllowanceDeductionDto>.FailAsync($"Error in Add of: {GetType()}, the passed entity is NULL!");

			try
			{
				entity.IsDeleted = false;

				var item = _mapper.Map<HrJobAllowanceDeduction>(entity);
				var newEntity = await _hrRepositoryManager.HrJobAllowanceDeductionRepository.AddAndReturn(item);

				await _hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				var entityMap = _mapper.Map<HrJobAllowanceDeductionDto>(newEntity);

				return await Result<HrJobAllowanceDeductionDto>.SuccessAsync(entityMap, _localization.GetResource1("CreateSuccess"));
			}
			catch (Exception exc)
			{
				return await Result<HrJobAllowanceDeductionDto>.FailAsync($"Exception in {GetType()}, Message: {exc.Message}");
			}
		}

		public async Task<IResult<List<HrJobAllowanceDeductionDto>>> AddRange(List<HrJobAllowanceDeductionDto> entities, CancellationToken cancellationToken = default)
		{
			if (entities == null || !entities.Any())
				return await Result<List<HrJobAllowanceDeductionDto>>.FailAsync("Error: No items to add");

			try
			{
				// بدء المعاملة
				await _hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

				// الحصول على السجلات القديمة
				var firstItem = entities.First();
				var existingItems = await _hrRepositoryManager.HrJobAllowanceDeductionRepository
					.GetAll(x => x.JobId == firstItem.JobId);

				if (existingItems.Any())
				{
					foreach (var item in existingItems)
					{
						_hrRepositoryManager.HrJobAllowanceDeductionRepository.Remove(item);
					}
					await _hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				}

				// إضافة السجلات الجديدة
				var newEntities = new List<HrJobAllowanceDeduction>();
				foreach (var entity in entities)
				{
					entity.IsDeleted = false;
					var item = _mapper.Map<HrJobAllowanceDeduction>(entity);
					var newEntity = await _hrRepositoryManager.HrJobAllowanceDeductionRepository.AddAndReturn(item);
					newEntities.Add(newEntity);
				}

				await _hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				await _hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

				var entitiesMap = _mapper.Map<List<HrJobAllowanceDeductionDto>>(newEntities);

				return await Result<List<HrJobAllowanceDeductionDto>>.SuccessAsync(
					entitiesMap,
					_localization.GetResource1("CreateSuccess"));
			}
			catch (Exception exc)
			{
				await _hrRepositoryManager.UnitOfWork.RollbackTransactionAsync(cancellationToken);
				return await Result<List<HrJobAllowanceDeductionDto>>.FailAsync(
					$"Error in AddRange: {exc.Message}");
			}
		}

		public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await _hrRepositoryManager.HrJobAllowanceDeductionRepository.GetById(Id);
				if (item == null)
					return await Result<HrJobAllowanceDeductionDto>.FailAsync($"No data found with id: {Id}");

				item.IsDeleted = true;
				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)_session.UserId;
				_hrRepositoryManager.HrJobAllowanceDeductionRepository.Update(item);

				await _hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobAllowanceDeductionDto>.SuccessAsync(
					_mapper.Map<HrJobAllowanceDeductionDto>(item),
					_localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobAllowanceDeductionDto>.FailAsync(
					$"Exception in Remove at {GetType()}, Message: {exp.Message}");
			}
		}

		public async Task<IResult<HrJobAllowanceDeductionDto>> Update(HrJobAllowanceDeductionDto entity, CancellationToken cancellationToken = default)
		{
			if (entity == null)
				return await Result<HrJobAllowanceDeductionDto>.FailAsync($"Error in {GetType()}: Passed entity is NULL.");

			try
			{
				var item = await _hrRepositoryManager.HrJobAllowanceDeductionRepository.GetById(entity.Id);

				if (item == null)
					return await Result<HrJobAllowanceDeductionDto>.FailAsync($"No data found with id: {entity.Id}");

				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)_session.UserId;
				_mapper.Map(entity, item);

				_hrRepositoryManager.HrJobAllowanceDeductionRepository.Update(item);

				await _hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobAllowanceDeductionDto>.SuccessAsync(
					_mapper.Map<HrJobAllowanceDeductionDto>(item),
					_localization.GetResource1("UpdateSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobAllowanceDeductionDto>.FailAsync(
					$"Exception in Update at {GetType()}, Message: {exp.Message}");
			}
		}
		public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await _hrRepositoryManager.HrJobAllowanceDeductionRepository.GetById(Id);
				if (item == null) return Result<HrJobAllowanceDeductionDto>.Fail($"--- there is no Data with this id: {Id}---");
				item.IsDeleted = true;
				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)_session.UserId;
				_hrRepositoryManager.HrJobAllowanceDeductionRepository.Update(item);

				await _hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobAllowanceDeductionDto>.SuccessAsync(_mapper.Map<HrJobAllowanceDeductionDto>(item), _localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobAllowanceDeductionDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}
	}
}