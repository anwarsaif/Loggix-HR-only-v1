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
	public class HrJobLevelsAllowanceDeductionService :
		GenericQueryService<HrJobLevelsAllowanceDeduction, HrJobLevelsAllowanceDeductionDto, HrJobLevelsAllowanceDeductionVw>,
		IHrJobLevelsAllowanceDeductionService
	{
		private readonly IHrRepositoryManager _hrRepositoryManager;
		private readonly ILocalizationService _localization;
		private readonly IMainRepositoryManager _mainRepositoryManager;
		private readonly IMapper _mapper;
		private readonly ICurrentData _session;

		public HrJobLevelsAllowanceDeductionService(
			IQueryRepository<HrJobLevelsAllowanceDeduction> queryRepository,
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

		public async Task<IResult<HrJobLevelsAllowanceDeductionDto>> Add(HrJobLevelsAllowanceDeductionDto entity, CancellationToken cancellationToken = default)
		{
			if (entity == null)
				return await Result<HrJobLevelsAllowanceDeductionDto>.FailAsync($"Error in Add of: {GetType()}, the passed entity is NULL!");

			try
			{
				entity.IsDeleted = false;

				var item = _mapper.Map<HrJobLevelsAllowanceDeduction>(entity);
				var newEntity = await _hrRepositoryManager.HrJobLevelsAllowanceDeductionRepository.AddAndReturn(item);

				await _hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				var entityMap = _mapper.Map<HrJobLevelsAllowanceDeductionDto>(newEntity);

				return await Result<HrJobLevelsAllowanceDeductionDto>.SuccessAsync(entityMap, _localization.GetResource1("CreateSuccess"));
			}
			catch (Exception exc)
			{
				return await Result<HrJobLevelsAllowanceDeductionDto>.FailAsync($"Exception in {GetType()}, Message: {exc.Message}");
			}
		}

		public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await _hrRepositoryManager.HrJobLevelsAllowanceDeductionRepository.GetById(Id);
				if (item == null)
					return await Result<HrJobLevelsAllowanceDeductionDto>.FailAsync($"No data found with id: {Id}");

				item.IsDeleted = true;
				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)_session.UserId;
				_hrRepositoryManager.HrJobLevelsAllowanceDeductionRepository.Update(item);

				await _hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobLevelsAllowanceDeductionDto>.SuccessAsync(
					_mapper.Map<HrJobLevelsAllowanceDeductionDto>(item),
					_localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobLevelsAllowanceDeductionDto>.FailAsync(
					$"Exception in Remove at {GetType()}, Message: {exp.Message}");
			}
		}

		public async Task<IResult<HrJobLevelsAllowanceDeductionEditDto>> Update(HrJobLevelsAllowanceDeductionEditDto entity, CancellationToken cancellationToken = default)
		{
			if (entity == null)
				return await Result<HrJobLevelsAllowanceDeductionEditDto>.FailAsync($"Error in {GetType()}: Passed entity is NULL.");

			try
			{
				var item = await _hrRepositoryManager.HrJobLevelsAllowanceDeductionRepository.GetById(entity.Id);

				if (item == null)
					return await Result<HrJobLevelsAllowanceDeductionEditDto>.FailAsync($"No data found with id: {entity.Id}");

				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)_session.UserId;
				_mapper.Map(entity, item);

				_hrRepositoryManager.HrJobLevelsAllowanceDeductionRepository.Update(item);

				await _hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobLevelsAllowanceDeductionEditDto>.SuccessAsync(
					_mapper.Map<HrJobLevelsAllowanceDeductionEditDto>(item),
					_localization.GetResource1("UpdateSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobLevelsAllowanceDeductionEditDto>.FailAsync(
					$"Exception in Update at {GetType()}, Message: {exp.Message}");
			}
		}
		public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await _hrRepositoryManager.HrJobLevelsAllowanceDeductionRepository.GetById(Id);
				if (item == null) return Result<HrJobLevelsAllowanceDeductionDto>.Fail($"--- there is no Data with this id: {Id}---");
				item.IsDeleted = true;
				item.ModifiedOn = DateTime.Now;
				item.ModifiedBy = (int)_session.UserId;
				_hrRepositoryManager.HrJobLevelsAllowanceDeductionRepository.Update(item);

				await _hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobLevelsAllowanceDeductionDto>.SuccessAsync(_mapper.Map<HrJobLevelsAllowanceDeductionDto>(item), _localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobLevelsAllowanceDeductionDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}
	}
}