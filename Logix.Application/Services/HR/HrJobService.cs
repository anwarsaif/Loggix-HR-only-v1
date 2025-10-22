using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
	public class HrJobService : GenericQueryService<HrJob, HrJobDto, HrJobVw>, IHrJobService
	{
		private readonly IHrRepositoryManager hrRepositoryManager;
		private readonly IMainRepositoryManager mainRepositoryManager;
		private readonly IMapper _mapper;
		private readonly ICurrentData session;
		private readonly ILocalizationService localization;
		private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;


		public HrJobService(IQueryRepository<HrJob> queryRepository,
			IHrRepositoryManager hrRepositoryManager,
			IMainRepositoryManager mainRepositoryManager,
			IMapper mapper, ICurrentData session, ILocalizationService localization, ISysConfigurationAppHelper sysConfigurationAppHelper) : base(queryRepository, mapper)
		{
			this.hrRepositoryManager = hrRepositoryManager;
			this.mainRepositoryManager = mainRepositoryManager;
			_mapper = mapper;
			this.sysConfigurationAppHelper = sysConfigurationAppHelper;
			this.session = session;
			this.localization = localization;
		}

		public async Task<IResult<HrJobDto>> Add(HrJobDto entity, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await hrRepositoryManager.HrJobRepository.GetAll(x => x.IsDeleted == false && x.FacilityId == session.FacilityId && x.JobNo == entity.JobNo && x.LevelId == entity.LevelId);
				if (item.Any()) return await Result<HrJobDto>.FailAsync("كود الوظيفة موجود مسبقاً");
				entity.CreatedBy = session.UserId;
				entity.CreatedOn = DateTime.Now;
				entity.IsDeleted = false;
				entity.DecSourceId = 1;
				// entity.StatusId = 1;
				entity.FacilityId = session.FacilityId;

				var newEntity = await hrRepositoryManager.HrJobRepository.AddAndReturn(_mapper.Map<HrJob>(entity));

				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				var entityMap = _mapper.Map<HrJobDto>(newEntity);

				return await Result<HrJobDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
			}
			catch (Exception exc)
			{
				return await Result<HrJobDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
			}
		}

		public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await hrRepositoryManager.HrJobRepository.GetById(Id);
				if (item == null) return Result<HrJobDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

				item.ModifiedBy = session.UserId;
				item.ModifiedOn = DateTime.Now;
				item.IsDeleted = true;

				hrRepositoryManager.HrJobRepository.Update(item);

				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobDto>.SuccessAsync(_mapper.Map<HrJobDto>(item), localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}

		public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
		{
			try
			{
				var item = await hrRepositoryManager.HrJobRepository.GetById(Id);
				if (item == null) return Result<HrJobDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

				item.ModifiedBy = session.UserId;
				item.ModifiedOn = DateTime.Now;
				item.IsDeleted = true;

				hrRepositoryManager.HrJobRepository.Update(item);

				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobDto>.SuccessAsync(_mapper.Map<HrJobDto>(item), localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrJobDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}

		public async Task<IResult<HrJobEditDto>> Update(HrJobEditDto entity, CancellationToken cancellationToken = default)
		{
			try
			{
				//var job = await hrRepositoryManager.HrJobRepository.GetAll(x => x.IsDeleted == false && x.FacilityId == session.FacilityId && x.JobNo == entity.JobNo && x.LevelId == entity.LevelId);
				//if (job.Count() > 1) return await Result<HrJobEditDto>.FailAsync("كود الوظيفة موجود مسبقاً");
				var item = await hrRepositoryManager.HrJobRepository.GetById(entity.Id);
				if (item == null) return await Result<HrJobEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");

				entity.ModifiedBy = session.UserId;
				entity.ModifiedOn = DateTime.Now;
				entity.DecSourceId = 1;
				entity.StatusId = 1;
				entity.FacilityId = session.FacilityId;
				_mapper.Map(entity, item);

				hrRepositoryManager.HrJobRepository.Update(item);
				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				return await Result<HrJobEditDto>.SuccessAsync(_mapper.Map<HrJobEditDto>(item), "Item updated successfully");

			}
			catch (Exception exc)
			{
				return await Result<HrJobEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
			}
		}
	}
}