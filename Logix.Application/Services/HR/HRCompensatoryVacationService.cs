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

    public class HrCompensatoryVacationService : GenericQueryService<HrCompensatoryVacation, HrCompensatoryVacationDto, HrCompensatoryVacationsVw>, IHrCompensatoryVacationService

    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IWorkflowHelper workflowHelper;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;


        public HrCompensatoryVacationService(IQueryRepository<HrCompensatoryVacation> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager, IWorkflowHelper workflowHelper, ISysConfigurationAppHelper sysConfigurationAppHelper) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
            this.workflowHelper = workflowHelper;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
        }


        public Task<IResult<HrCompensatoryVacationDto>> Add(HrCompensatoryVacationDto entity, CancellationToken cancellationToken = default)

        {
            throw new NotImplementedException();
        }

        public async Task<IResult<bool>> AddNewHrCompensatoryVacation(HrCompensatoryVacationAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                long? appId = 0;
                if (entity == null) return await Result<bool>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<bool>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                entity.AppTypeId ??= 0;

                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(checkEmpExist.Id, 1788, entity.AppTypeId);
                appId = GetApp_ID;
                var hrCompensatoryVacation = new HrCompensatoryVacation
                {
                    AppId = appId,
                    StatusId = 1,
                    VacationSdate = entity.VacationSdate,
                    VacationEdate = entity.VacationEdate,
                    VacationTypeId = entity.VacationTypeId,
                    Note = entity.Note,
                    EmpId = checkEmpExist.Id,
                    VacationAccountDay = entity.VacationAccountDay,
                    IsDeleted = false,
                };

                var newEntity = await hrRepositoryManager.HrCompensatoryVacationRepository.AddAndReturn(hrCompensatoryVacation);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.fileDtos != null && entity.fileDtos.Any())
                {
                    await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.CompensatoryId, 119, cancellationToken);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<bool>.SuccessAsync(true, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<bool>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult<string>> CompensatoryVacationApprove(long CompensatoryId)
        {
            if (CompensatoryId <= 0) return await Result<string>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrCompensatoryVacationRepository.GetOne(x => x.CompensatoryId == CompensatoryId);

                if (item == null) return await Result<string>.FailAsync($"--- there is no Data with this id: {CompensatoryId}---");

                item.StatusId = 2;
                hrRepositoryManager.HrCompensatoryVacationRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync();

                return await Result<string>.SuccessAsync("تمت عملية الاعتماد بنجاح");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in CompensatoryVacationApprove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> GetVacationDaysCount(string SDate, string EDate, int VacationTypeId)
        {
            try
            {
                string startDate = SDate;
                string endDate = EDate;
                int? catId = 0;
                int CountDaysholidays = 0;
                int totWeeks = 0;
                bool? WeekendInclude = true;
                var getCalenderType = await sysConfigurationAppHelper.GetValue(19);
                if (getCalenderType == "2")
                {
                    DateHelper.Initialize(mainRepositoryManager);
                    startDate = await DateHelper.DateFormattYYYYMMDD_H_G(SDate);
                    endDate = await DateHelper.DateFormattYYYYMMDD_H_G(EDate);
                }

                var getFromVacationsType = await hrRepositoryManager.HrVacationsTypeRepository.GetOne(x => x.VacationTypeId == VacationTypeId);
                if (getFromVacationsType != null)
                {
                    WeekendInclude = getFromVacationsType.WeekendInclude;
                    catId = getFromVacationsType.CatId;
                }
                if (WeekendInclude == false)
                {
                    var weekendsInRangeData = await mainRepositoryManager.SysCalendarRepository.GetAll(x => x.GDate != null && (x.GDate.Contains(startDate.Substring(0, 4)) || x.GDate.Contains(endDate.Substring(0, 4))));
                    var weekendsInRange = weekendsInRangeData.AsEnumerable()
                        .Where(x => x.GDate != null && DateHelper.StringToDate(x.GDate) >= DateHelper.StringToDate(startDate) && DateHelper.StringToDate(x.GDate) <= DateHelper.StringToDate(endDate)
                                 && (DateHelper.StringToDate(x.GDate).DayOfWeek == DayOfWeek.Saturday || DateHelper.StringToDate(x.GDate).DayOfWeek == DayOfWeek.Friday));
                    totWeeks = weekendsInRange.Count();
                }
                //   احتساب ايام العطل الرسمية ليتم خصمها
                var getHoliday = await hrRepositoryManager.HrHolidayRepository.GetAll(x => x.IsDeleted == false && x.FacilityId == 1);
                if (getHoliday != null)
                {
                    var getHolidayAfterFilter = getHoliday.Where(x => x.HolidayDateFrom != null && x.HolidayDateTo != null && DateHelper.StringToDate(x.HolidayDateFrom) >= DateHelper.StringToDate(startDate) && DateHelper.StringToDate(x.HolidayDateTo) <= DateHelper.StringToDate(endDate));
                    foreach (var item in getHolidayAfterFilter)
                    {
                        CountDaysholidays += await mainRepositoryManager.DbFunctionsRepository.DateDiff_day2(item.HolidayDateFrom, item.HolidayDateTo);
                    }
                }
                //  في حال الاجازة المرضية لايتم استبعاد العطل الرسمية
                if (catId == 2)
                {
                    CountDaysholidays = 0;
                }
                var returnedValue = await mainRepositoryManager.DbFunctionsRepository.DateDiff_day2(startDate, endDate) - totWeeks - CountDaysholidays;

                return await Result<string>.SuccessAsync(returnedValue.ToString(), "");

            }
            catch (Exception)
            {
                return await Result<string>.FailAsync("حدث خطاء اثناء احتساب مدة الإجازة التعويضية ");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrCompensatoryVacationRepository.GetOne(x => x.CompensatoryId == Id);
                if (item == null) return Result<HrCompensatoryVacationDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrCompensatoryVacationRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCompensatoryVacationDto>.SuccessAsync(_mapper.Map<HrCompensatoryVacationDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrCompensatoryVacationDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrCompensatoryVacationRepository.GetOne(x => x.CompensatoryId == Id);
                if (item == null) return Result<HrCompensatoryVacationDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrCompensatoryVacationRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCompensatoryVacationDto>.SuccessAsync(_mapper.Map<HrCompensatoryVacationDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrCompensatoryVacationDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<HrCompensatoryVacationEditDto>> Update(HrCompensatoryVacationEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrCompensatoryVacationEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrCompensatoryVacationEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrCompensatoryVacationRepository.GetById(entity.CompensatoryId);
                if (item == null) return await Result<HrCompensatoryVacationEditDto>.FailAsync("the Record Is Not Found");

                item.EmpId = checkEmpExist.Id;
                item.VacationEdate = entity.VacationEdate;
                item.VacationSdate = entity.VacationSdate;
                item.VacationAccountDay = entity.VacationAccountDay;
                item.Note = entity.Note;
                item.StatusId = 1;

                hrRepositoryManager.HrCompensatoryVacationRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.fileDtos != null && entity.fileDtos.Any())
                {
                    await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, item.CompensatoryId, 119, cancellationToken);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrCompensatoryVacationEditDto>.SuccessAsync(_mapper.Map<HrCompensatoryVacationEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrCompensatoryVacationEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}
