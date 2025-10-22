using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrAttShiftService : GenericQueryService<HrAttShift, HrAttShiftDto, HrAttShift>, IHrAttShiftService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        public HrAttShiftService(IQueryRepository<HrAttShift> queryRepository, IMainRepositoryManager mainRepositoryManager, ILocalizationService localization, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrAttShiftDto>> Add(HrAttShiftDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAttShiftDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                var item = _mapper.Map<HrAttShift>(entity);
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var newEntity = await hrRepositoryManager.HrAttShiftRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                foreach (var singleItem in entity.Shift)
                {
                    var newAttendanceTimeTable = new HrAttShiftTimeTable
                    {
                        ShiftId = newEntity.Id,
                        TimeTableId = singleItem,
                        CreatedOn = DateTime.Now,
                        CreatedBy = session.UserId,
                        IsDeleted = false,

                    };
                    await hrRepositoryManager.HrAttShiftTimeTableRepository.Add(newAttendanceTimeTable);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                var entityMap = _mapper.Map<HrAttShiftDto>(newEntity);


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrAttShiftDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<HrAttShiftDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrAttShiftRepository.GetById(Id);
                if (item == null) return Result<HrAttShiftDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrAttShiftRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttShiftDto>.SuccessAsync(_mapper.Map<HrAttShiftDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrAttShiftDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrAttShiftRepository.GetById(Id);
                if (item == null) return Result<HrAttShiftDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrAttShiftRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttShiftDto>.SuccessAsync(_mapper.Map<HrAttShiftDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrAttShiftDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrAttShiftEditDto>> Update(HrAttShiftEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAttShiftEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrAttShiftRepository.GetById(entity.Id);

                if (item == null) return await Result<HrAttShiftEditDto>.FailAsync(localization.GetResource1("UpdateError"));
                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                _mapper.Map(entity, item);

                hrRepositoryManager.HrAttShiftRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttShiftEditDto>.SuccessAsync(_mapper.Map<HrAttShiftEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrAttShiftEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
    }
