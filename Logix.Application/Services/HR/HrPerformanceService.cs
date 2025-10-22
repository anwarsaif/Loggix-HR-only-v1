using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using System.Linq;

namespace Logix.Application.Services.HR
{
    public class HrPerformanceService : GenericQueryService<HrPerformance, HrPerformanceDto, HrPerformanceVw>, IHrPerformanceService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper mapper;
        private readonly ICurrentData session;


        public HrPerformanceService(IQueryRepository<HrPerformance> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrPerformanceDto>> Add(HrPerformanceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPerformanceDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                if (string.IsNullOrEmpty(entity.DepartmentsIds)) entity.DepartmentsIds = "0";
                if (string.IsNullOrEmpty(entity.GroupsId)) entity.GroupsId = "0";
                if (string.IsNullOrEmpty(entity.LocationIds)) entity.LocationIds = "0";
                if (string.IsNullOrEmpty(entity.JobsCatIds)) entity.JobsCatIds = "0";
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                var newItem = mapper.Map<HrPerformance>(entity);

                var newAddEntity = await hrRepositoryManager.HrPerformanceRepository.AddAndReturn(newItem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = mapper.Map<HrPerformanceDto>(newAddEntity);

                return await Result<HrPerformanceDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrPerformanceDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrPerformanceRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrPerformanceDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrPerformanceRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPerformanceDto>.SuccessAsync(mapper.Map<HrPerformanceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPerformanceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrPerformanceRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrPerformanceDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrPerformanceRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPerformanceDto>.SuccessAsync(mapper.Map<HrPerformanceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPerformanceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }



        public async Task<IResult<HrPerformanceEditDto>> Update(HrPerformanceEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPerformanceEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrPerformanceRepository.GetOne(x => x.Id == entity.Id);

                if (item == null) return await Result<HrPerformanceEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
                if (string.IsNullOrEmpty(entity.DepartmentsIds)) entity.DepartmentsIds = "0";
                if (string.IsNullOrEmpty(entity.GroupsId)) entity.GroupsId = "0";
                if (string.IsNullOrEmpty(entity.LocationIds)) entity.LocationIds = "0";
                if (string.IsNullOrEmpty(entity.JobsCatIds)) entity.JobsCatIds = "0";

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                mapper.Map(entity, item);

                hrRepositoryManager.HrPerformanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPerformanceEditDto>.SuccessAsync(mapper.Map<HrPerformanceEditDto>(item), localization.GetMessagesResource("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrPerformanceEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        // إرسال إشعار بإضافة مؤشرات أداء الموظف

        public async Task<IResult<string>> SendEmployeePerformanceIndicators(long PerformanceId, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrPerformanceRepository.GetOne(x => x.Id == PerformanceId);
                if (item == null) return await Result<string>.FailAsync("العنصر غير مضاف ");
                var locationIds = item.LocationIds?.Split(',').ToList() ?? new List<string>();
                var jobsCatIds = item.JobsCatIds?.Split(',').ToList() ?? new List<string>();
                var departmentsIds = item.DepartmentsIds?.Split(',').ToList() ?? new List<string>();
                var groupsIds = item.GroupsId?.Split(',').ToList() ?? new List<string>();

                var Allusers = await mainRepositoryManager.SysUserRepository.GetAll(x => x.Isdel == false && x.Enable == 1 && x.FacilityId == session.FacilityId && x.EmpId != null && x.EmpId != 0);
                var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(y => y.IsDeleted == false && y.StatusId != 2);
                var filteredUsers = (from user in Allusers.AsEnumerable()
                                     join employee in investEmployees.AsEnumerable() on (long)user.EmpId equals employee.Id into employeeGroup
                                     from employee in employeeGroup.DefaultIfEmpty() // Handle null EmpId
                                     where !user.Isdel.HasValue || !user.Isdel.Value && user.Enable == 1 && user.FacilityId == session.FacilityId
                                           && !employee.IsDeleted.HasValue || !employee.IsDeleted.Value && employee.StatusId != 2
                                           && (locationIds.Count() != 0 && locationIds.Contains(employee.Location.ToString())
                                               || (jobsCatIds.Count() != 0 && jobsCatIds.Contains(employee.JobCatagoriesId.ToString()))
                                               || (departmentsIds.Count() != 0 && departmentsIds.Contains(employee.DeptId.ToString()))
                                               || (groupsIds.Count() != 0 && groupsIds.Contains(user.GroupsId))
                                               )
                                     select user).ToList();

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var user in filteredUsers)
                {
                    var newSysNotification = new SysNotification
                    {
                        UserId = user.Id,
                        Url = $"/Apps/HR/kpi/EmpGoalIndicators/EmpGoalIndicators_Add.aspx?PerformanceID={PerformanceId}",
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        MsgTxt = "لديك طلب إضافة مؤشرات أداء الموظف ",
                        IsRead = false,
                    };
                    await mainRepositoryManager.SysNotificationRepository.Add(newSysNotification);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync("تم ارسال الاشعار بنجاح ");

            }
            catch (Exception exp)
            {

                return await Result<string>.FailAsync($"EXP in SendNotificationsEvaluation at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        //  ارسال اشعار  ل  لتقيم  الموظفين
        public async Task<IResult<string>> SendNotificationsEvaluation(long PerformanceId, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrPerformanceRepository.GetOne(x => x.Id == PerformanceId);
                if (item == null) return await Result<string>.FailAsync("العنصر غير مضاف ");
                var locationIds = item.LocationIds?.Split(',').ToList() ?? new List<string>();
                var jobsCatIds = item.JobsCatIds?.Split(',').ToList() ?? new List<string>();
                var departmentsIds = item.DepartmentsIds?.Split(',').ToList() ?? new List<string>();
                var groupsIds = item.GroupsId?.Split(',').ToList() ?? new List<string>();

                var Allusers = await mainRepositoryManager.SysUserRepository.GetAll(x => x.Isdel == false && x.Enable == 1 && x.FacilityId == session.FacilityId && x.EmpId != null && x.EmpId != 0);
                var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(y => y.IsDeleted == false && y.StatusId != 2);
                var filteredUsers = (from user in Allusers.AsEnumerable()
                                     join employee in investEmployees.AsEnumerable() on (long)user.EmpId equals employee.Id into employeeGroup
                                     from employee in employeeGroup.DefaultIfEmpty() // Handle null EmpId
                                     where !user.Isdel.HasValue || !user.Isdel.Value && user.Enable == 1 && user.FacilityId == session.FacilityId
                                           && !employee.IsDeleted.HasValue || !employee.IsDeleted.Value && employee.StatusId != 2
                                           && (locationIds.Count() != 0 && locationIds.Contains(employee.Location.ToString())
                                               || (jobsCatIds.Count() != 0 && jobsCatIds.Contains(employee.JobCatagoriesId.ToString()))
                                               || (departmentsIds.Count() != 0 && departmentsIds.Contains(employee.DeptId.ToString()))
                                               || (groupsIds.Count() != 0 && groupsIds.Contains(user.GroupsId))
                                               )
                                     select user).ToList();

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var user in filteredUsers)
                {
                    var newSysNotification = new SysNotification
                    {
                        UserId = user.Id,
                        Url = $"/Apps/Workflow/Performance/Performance.aspx?ID={PerformanceId}",
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        MsgTxt = "لديك طلب تقييم أداء ",
                        IsRead = false,
                    };
                    await mainRepositoryManager.SysNotificationRepository.Add(newSysNotification);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync("تم ارسال الاشعار بنجاح ");

            }
            catch (Exception exp)
            {

                return await Result<string>.FailAsync($"EXP in SendNotificationsEvaluation at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


    }
}