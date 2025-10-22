using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.PM;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrAttLocationService : GenericQueryService<HrAttLocation, HrAttLocationDto, HrAttLocation>, IHrAttLocationService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrAttLocationService(IQueryRepository<HrAttLocation> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrAttLocationDto>> Add(HrAttLocationDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAttLocationDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                var item = _mapper.Map<HrAttLocation>(entity);
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateHelper.GetCurrentDateTime();
                entity.IsDeleted = false;
                var newEntity = await hrRepositoryManager.HrAttLocationRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrAttLocationDto>(newEntity);


                return await Result<HrAttLocationDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {

                return await Result<HrAttLocationDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrAttLocationRepository.GetById(Id);
                if (item == null) return Result<HrAttLocationDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.IsDeleted = true;
                item.ModifiedOn = DateHelper.GetCurrentDateTime(); ;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrAttLocationRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttLocationDto>.SuccessAsync(_mapper.Map<HrAttLocationDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrAttLocationDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrAttLocationRepository.GetById(Id);
                if (item == null) return Result<HrAttLocationDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.IsDeleted = true;
                item.ModifiedOn = DateHelper.GetCurrentDateTime(); ;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrAttLocationRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttLocationDto>.SuccessAsync(_mapper.Map<HrAttLocationDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrAttLocationDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrAttLocationEditDto>> Update(HrAttLocationEditDto entity, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrAttLocationRepository.GetById(entity.Id);
                if (item == null) return await Result<HrAttLocationEditDto>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");
                item.ModifiedOn = DateHelper.GetCurrentDateTime(); ;
                item.ModifiedBy = (int)session.UserId;
                item.LocationName = entity.LocationName;
                item.Longitude = entity.Longitude;
                item.Latitude = entity.Latitude;
                hrRepositoryManager.HrAttLocationRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttLocationEditDto>.SuccessAsync(_mapper.Map<HrAttLocationEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrAttLocationEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}
