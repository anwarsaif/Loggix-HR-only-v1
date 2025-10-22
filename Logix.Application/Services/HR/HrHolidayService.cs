using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrHolidayService : GenericQueryService<HrHoliday, HrHolidayDto, HrHolidayVw>, IHrHolidayService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        public HrHolidayService(IQueryRepository<HrHoliday> queryRepository, IMainRepositoryManager mainRepositoryManager, ILocalizationService localization, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrHolidayDto>> Add(HrHolidayDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrHolidayDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                if (entity.HolidayDateFrom == null || entity.HolidayDateTo == null || entity.HolidayDateFrom == "" || entity.HolidayDateTo == "") return await Result<HrHolidayDto>.FailAsync($"{localization.GetMessagesResource("dataRequire")}");
                if (DateHelper.StringToDate(entity.HolidayDateFrom) > DateHelper.StringToDate(entity.HolidayDateTo))

                    return await Result<HrHolidayDto>.FailAsync(" From Date Must be Less Than To Date");
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.FacilityId = session.FacilityId;
                var item = _mapper.Map<HrHoliday>(entity);

                var newEntity = await hrRepositoryManager.HrHolidayRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrHolidayDto>(newEntity);


                return await Result<HrHolidayDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<HrHolidayDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrHolidayRepository.GetById(Id);
                if (item == null) return Result<HrHolidayDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrHolidayRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrHolidayDto>.SuccessAsync(_mapper.Map<HrHolidayDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrHolidayDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrHolidayRepository.GetById(Id);
                if (item == null) return Result<HrHolidayDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrHolidayRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrHolidayDto>.SuccessAsync(_mapper.Map<HrHolidayDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrHolidayDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrHolidayEditDto>> Update(HrHolidayEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrHolidayEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                if (entity.HolidayDateFrom == null || entity.HolidayDateTo == null || entity.HolidayDateFrom == "" || entity.HolidayDateTo == "") return await Result<HrHolidayEditDto>.FailAsync($"{localization.GetMessagesResource("dataRequire")}");
                if (DateHelper.StringToDate(entity.HolidayDateFrom) > DateHelper.StringToDate(entity.HolidayDateTo))

                    return await Result<HrHolidayEditDto>.FailAsync(" From Date Must be Less Than To Date");
                var item = await hrRepositoryManager.HrHolidayRepository.GetById(entity.HolidayId);

                if (item == null) return await Result<HrHolidayEditDto>.FailAsync(localization.GetResource1("UpdateError"));

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                entity.FacilityId = session.FacilityId;
                _mapper.Map(entity, item);

                hrRepositoryManager.HrHolidayRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrHolidayEditDto>.SuccessAsync(_mapper.Map<HrHolidayEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrHolidayEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
    }
