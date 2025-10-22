using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Logix.Application.Services.HR
{
    public class HrAttTimeTableService : GenericQueryService<HrAttTimeTable, HrAttTimeTableDto, HrAttTimeTableVw, HrAttTimeTable>, IHrAttTimeTableService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;



        public HrAttTimeTableService(IQueryRepository<HrAttTimeTable> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrAttTimeTableDto>> Add(HrAttTimeTableDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAttTimeTableDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                var item = _mapper.Map<HrAttTimeTable>(entity);

                TimeSpan OnDutyTime = TimeSpan.Parse(entity.OnDutyTimeString);
                item.OnDutyTime = OnDutyTime;
                ////////////////////////////////////////////////
                TimeSpan OffDutyTime = TimeSpan.Parse(entity.OffDutyTimeString);
                item.OffDutyTime = OffDutyTime;


                ////////////////////////////////////////////////
                TimeSpan BeginIn = TimeSpan.Parse(entity.BeginInString);
                item.BeginIn = BeginIn;
                ////////////////////////////////////////////////
                TimeSpan EndIn = TimeSpan.Parse(entity.EndInString);
                item.EndIn = EndIn;
                ////////////////////////////////////////////////
                TimeSpan BeginOut = TimeSpan.Parse(entity.BeginOutString);
                item.BeginOut = BeginOut;
                ////////////////////////////////////////////////
                TimeSpan EndOut = TimeSpan.Parse(entity.EndOutString);
                item.EndOut = EndOut;

                if (entity.FlexibleAttendance == true)
                {
                    TimeSpan FlexibleStart = TimeSpan.Parse(entity.FlexibleStartString);
                    item.FlexibleStart = FlexibleStart;

                    ////////////////////////////////////////////////
                    TimeSpan FlexibleEnd = TimeSpan.Parse(entity.FlexibleEndString);
                    item.FlexibleEnd = FlexibleEnd;

                }


                await mainRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var newEntity = await hrRepositoryManager.HrAttTimeTableRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrAttTimeTableDto>(newEntity);
                if (entity.DaysNumbers != null && entity.DaysNumbers != "")
                {
                    var daysArray = entity.DaysNumbers.Split(",");
                    foreach (var items in daysArray)
                    {
                        var daysEntity = new HrAttTimeTableDayDto()
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            DayNo = Convert.ToInt32(items),
                            TimeTableId = entityMap.Id

                        };
                        var Daysitem = _mapper.Map<HrAttTimeTableDay>(daysEntity);

                        var newDaysEntity = await hrRepositoryManager.HrAttTimeTableDayRepository.AddAndReturn(Daysitem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                }

                await mainRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrAttTimeTableDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {

                return await Result<HrAttTimeTableDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrAttTimeTableRepository.GetById(Id);
            if (item == null) return Result<HrAttTimeTableDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrAttTimeTableRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttTimeTableDto>.SuccessAsync(_mapper.Map<HrAttTimeTableDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrAttTimeTableDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrAttTimeTableRepository.GetById(Id);
            if (item == null) return Result<HrAttTimeTableDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrAttTimeTableRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttTimeTableDto>.SuccessAsync(_mapper.Map<HrAttTimeTableDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrAttTimeTableDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<List<HrAttTimeTableDto>>> Search(CancellationToken cancellationToken = default)
        {
            try
            {
                List<HrAttTimeTableDto> resultList = new List<HrAttTimeTableDto>();


                var items = await hrRepositoryManager.HrAttTimeTableRepository.GetAll(e => e.IsDeleted == false);
                if (!items.Any())
                {
                    return await Result<List<HrAttTimeTableDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

                }
                foreach (var item in items)
                {
                    var newItem = new HrAttTimeTableDto
                    {
                        Id = item.Id,
                        TimeTableName = item.TimeTableName,
                        OnDutyTimeString = string.Format("{0:hh\\:mm}", item.OnDutyTime),
                        OffDutyTimeString = string.Format("{0:hh\\:mm}", item.OffDutyTime)

                    };
                    resultList.Add(newItem);
                }

                return await Result<List<HrAttTimeTableDto>>.SuccessAsync(resultList.OrderBy(e => e.Id).ToList(), "", 200);

            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Search at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<List<HrAttTimeTableDto>>.FailAsync($"EXP in Search at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrAttTimeTableEditDto>> Update(HrAttTimeTableEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAttTimeTableEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");


            try
            {
                var item = await hrRepositoryManager.HrAttTimeTableRepository.GetById(entity.Id);

                if (item == null) return await Result<HrAttTimeTableEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                entity.ModifiedOn = DateTime.Now;
                entity.ModifiedBy = (int)session.UserId;
                await mainRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                _mapper.Map(entity, item);
                TimeSpan OnDutyTime = TimeSpan.Parse(entity.OnDutyTimeString);
                item.OnDutyTime = OnDutyTime;
                ////////////////////////////////////////////////
                TimeSpan OffDutyTime = TimeSpan.Parse(entity.OffDutyTimeString);
                item.OffDutyTime = OffDutyTime;


                ////////////////////////////////////////////////
                TimeSpan BeginIn = TimeSpan.Parse(entity.BeginInString);
                item.BeginIn = BeginIn;
                ////////////////////////////////////////////////
                TimeSpan EndIn = TimeSpan.Parse(entity.EndInString);
                item.EndIn = EndIn;
                ////////////////////////////////////////////////
                TimeSpan BeginOut = TimeSpan.Parse(entity.BeginOutString);
                item.BeginOut = BeginOut;
                ////////////////////////////////////////////////
                TimeSpan EndOut = TimeSpan.Parse(entity.EndOutString);
                item.EndOut = EndOut;

                if (entity.FlexibleAttendance == true)
                {
                    TimeSpan FlexibleStart = TimeSpan.Parse(entity.FlexibleStartString);
                    item.FlexibleStart = FlexibleStart;

                    ////////////////////////////////////////////////
                    TimeSpan FlexibleEnd = TimeSpan.Parse(entity.FlexibleEndString);
                    item.FlexibleEnd = FlexibleEnd;

                }

                hrRepositoryManager.HrAttTimeTableRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.DaysNumbers != null && entity.DaysNumbers != "")
                {
                    var itemInUpdate = entity.DaysNumbers.Split(',');
                    List<int?> temp = new List<int?>();
                    foreach (var element in itemInUpdate)
                    {
                        temp.Add(Convert.ToInt32(element));
                    }
                    IEnumerable<int?> daysListFromUser = temp;

                    var getdays = await hrRepositoryManager.HrAttTimeTableDayRepository.GetAll(x => x.TimeTableId == entity.Id && x.IsDeleted == false);
                    var daysArrayFromDB = getdays.Select(x => x.DayNo);
                    // Identify deleted items
                    var deletedItems = daysArrayFromDB.Except(daysListFromUser).ToList();
                    if (deletedItems.Any())
                    {
                        foreach (var deleteItem in deletedItems)
                        {
                            var dItem = await hrRepositoryManager.HrAttTimeTableDayRepository.GetOne(e => e.DayNo == deleteItem && e.TimeTableId == entity.Id);
                            dItem.IsDeleted = true;
                            hrRepositoryManager.HrAttTimeTableDayRepository.Update(dItem);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }

                    }
                    // Identify inserted items
                    var insertedItems = daysListFromUser.Except(daysArrayFromDB).ToList();
                    if (insertedItems.Any())
                    {
                        foreach (var insertItem in insertedItems)
                        {
                            var newDaysItem = new HrAttTimeTableDay()
                            {
                                DayNo = insertItem,
                                IsDeleted = false,
                                TimeTableId = entity.Id,
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now
                            };
                            var newDaysEntity = await hrRepositoryManager.HrAttTimeTableDayRepository.AddAndReturn(newDaysItem);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }

                    }

                }


                else
                {
                    var getdays = await hrRepositoryManager.HrAttTimeTableDayRepository.GetAll(x => x.TimeTableId == entity.Id && x.IsDeleted == false);
                    var daysArrayFromDB = getdays.Select(x => x.DayNo);
                    if (daysArrayFromDB.Any())
                    {
                        foreach (var deleteItem in daysArrayFromDB)
                        {
                            var dItem = await hrRepositoryManager.HrAttTimeTableDayRepository.GetOne(e => e.DayNo == deleteItem && e.TimeTableId == entity.Id && e.IsDeleted == false);
                            dItem.IsDeleted = true;
                            hrRepositoryManager.HrAttTimeTableDayRepository.Update(dItem);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }

                    }

                }

                await mainRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrAttTimeTableEditDto>.SuccessAsync(_mapper.Map<HrAttTimeTableEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrAttTimeTableEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}