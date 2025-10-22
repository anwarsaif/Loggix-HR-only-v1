using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Globalization;
using System.Threading;

namespace Logix.Application.Services.HR
{
    public class HrFlexibleWorkingMasterService : GenericQueryService<HrFlexibleWorkingMaster, HrFlexibleWorkingMasterDto, HrFlexibleWorkingMasterDto>, IHrFlexibleWorkingMasterService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrFlexibleWorkingMasterService(IQueryRepository<HrFlexibleWorkingMaster> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrFlexibleWorkingMasterDto>> Add(HrFlexibleWorkingMasterDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> Add(HrFlexibleWorkingMasterAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                int Count = 0;
                long MaxCode = 1;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var getAllForMax = await hrRepositoryManager.HrFlexibleWorkingMasterRepository.GetAll();
                if (getAllForMax.Count() > 0)
                {
                    MaxCode = getAllForMax.Max(x => x.Id) + 1;
                }
                var newMaster = new HrFlexibleWorkingMaster
                {
                    Note = "الملاحظات",
                    DateFrom = entity.DateFrom,
                    DateTo = entity.DateTo,
                    Code = MaxCode.ToString(),
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                };
                var newItem = await hrRepositoryManager.HrFlexibleWorkingMasterRepository.AddAndReturn(newMaster);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                foreach (var item in entity.Details)
                {
                    var Hours = item.Minute / 60;
                    var Minutes = item.Minute - (Hours * 60);
                    var newFlexWork = new HrFlexibleWorking
                    {
                        EmpId = item.EmpId,
                        StatusId = 1,
                        MasterId = newItem.Id,
                        AttendanceDate = item.DayDateGregorian,
                        ActualMinute = item.ActualMinute,
                        Minute = item.Minute,
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        TotalPrice = (item.HourCost / 60) * item.ActualMinute,
                        TotalHours = $"{(Hours / 60):D2}:{(Minutes % 60):D2}"

                    };
                    var changedDate1 = DateHelper.ChangeFormatDate(item.DayDateGregorian, item.TimeInString);
                    newFlexWork.TimeIn = DateTime.ParseExact(changedDate1, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                    var changedDate2 = DateHelper.ChangeFormatDate(item.DayDateGregorian, item.TimeOutString);
                    newFlexWork.TimeOut = DateTime.ParseExact(changedDate2, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                    await hrRepositoryManager.HrFlexibleWorkingRepository.Add(newFlexWork);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    Count = Count + 1;
                }


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync("  تمت عملية الترحيل لعدد  " + Count + $" حركة بنجاح");
            }
            catch (Exception)
            {

                return await Result<string>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrFlexibleWorkingMasterRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrFlexibleWorkingMasterDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrFlexibleWorkingMasterRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var getAllDetails = await hrRepositoryManager.HrFlexibleWorkingRepository.GetAll(x => x.IsDeleted == false && x.MasterId == Id);
                if (getAllDetails.Count() > 0)
                {
                    foreach (var FlexWork in getAllDetails)
                    {
                        FlexWork.IsDeleted = true;
                        FlexWork.ModifiedBy = session.UserId;
                        FlexWork.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrFlexibleWorkingRepository.Update(FlexWork);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrFlexibleWorkingMasterDto>.SuccessAsync(_mapper.Map<HrFlexibleWorkingMasterDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrFlexibleWorkingMasterDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrFlexibleWorkingMasterRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrFlexibleWorkingMasterDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrFlexibleWorkingMasterRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var getAllDetails = await hrRepositoryManager.HrFlexibleWorkingRepository.GetAll(x => x.IsDeleted == false && x.MasterId == Id);
                if (getAllDetails.Count() > 0)
                {
                    foreach (var FlexWork in getAllDetails)
                    {
                        FlexWork.IsDeleted = true;
                        FlexWork.ModifiedBy = session.UserId;
                        FlexWork.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrFlexibleWorkingRepository.Update(FlexWork);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrFlexibleWorkingMasterDto>.SuccessAsync(_mapper.Map<HrFlexibleWorkingMasterDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrFlexibleWorkingMasterDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult<HrFlexibleWorkingMasterDto>> Update(HrFlexibleWorkingMasterDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}