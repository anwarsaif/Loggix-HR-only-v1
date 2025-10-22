using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using Org.BouncyCastle.Tls;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Logix.Application.Services.HR
{
    public class HrNotificationService : GenericQueryService<HrNotification, HrNotificationDto, HrNotificationsVw>, IHrNotificationService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrNotificationService(IQueryRepository<HrNotification> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrNotificationDto>> Add(HrNotificationDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrNotificationDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                var checkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrNotificationDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));


                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.EmpId = checkEmp.Id;
                entity.IsDeleted = false;
                entity.FacilityId = session.FacilityId;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var newEntity = await hrRepositoryManager.HrNotificationRepository.AddAndReturn(_mapper.Map<HrNotification>(entity));

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                var senderName = await mainRepositoryManager.SysUserRepository.GetOne(x => x.UserFullname, x => x.Isdel == false && x.IsDeleted == false && x.Id == session.UserId);
                var url = $"/Apps/HR/Notifications/EmpNotificationInbox.aspx?Notification_ID={newEntity.Id}";
                var getfromSysUser = await mainRepositoryManager.SysUserRepository.GetAll(x => x.Isdel == false && x.IsDeleted == false);

                var usersIdList = getfromSysUser.Select(x => x.EmpId);
                var getfromNotifications = await hrRepositoryManager.HrNotificationRepository.GetAll(x => x.IsDeleted == false && x.Id == session.UserId);
                var finalResult = getfromNotifications.Where(x => usersIdList.Any(y => y == x.EmpId)).FirstOrDefault();

                if (finalResult != null)
                {
                    string message = (senderName ?? "") + " يوجد لديك اشعار من قبل";

                    var newSysNotifications = new SysNotification
                    {
                        UserId = finalResult.Id,
                        MsgTxt = message,
                        IsRead = false,
                        CreatedBy = session.UserId,
                        Url = url,
                        TableId = null,
                        ActivityLogId = null,
                    };

                    var newSysNotificationEntity = await mainRepositoryManager.SysNotificationRepository.AddAndReturn(newSysNotifications);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 96);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                var entityMap = _mapper.Map<HrNotificationDto>(newEntity);

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrNotificationDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {
                return await Result<HrNotificationDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrNotificationEditDto>> Update(HrNotificationEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<object>> LoadNotifications(string Subject, long FacilityId)
        {
            try
            {
                var resul = await hrRepositoryManager.HrNotificationRepository.LoadNotifications("", session.FacilityId);
                return await Result<object>.SuccessAsync(resul, "", 200);
            }
            catch (Exception ex)
            {
                return await Result<object>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<object>> SendDisciplinaryDeletionNotice(long EmpId, string CaseName)
        {
            try
            {

                string Detailes = localization.GetMainResource("ApologyNoticeForFollowingViolations ") + CaseName;
                var add = new HrNotification
                {
                    TypeId = 3,
                    NotificationDate = DateTime.Now.ToString(),
                    EmpId = EmpId,
                    Subject = localization.GetMessagesResource("ApologyNoticeForViolations"),
                    Detailes = Detailes,
                    FacilityId = session.FacilityId,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    IsRead = false
                };

                await hrRepositoryManager.HrNotificationRepository.Add(add);
                return await Result<object>.SuccessAsync(200);
            }
            catch (Exception ex)
            {
                return await Result<object>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<string>> AddNotificationsReply(HrNotificationsReplyDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                string message = "";
                int? NotificationRecieverID = 0;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var HrNotificationReply = new HrNotificationsReply
                {
                    NotificationId = entity.NotificationId,
                    IsDeleted = false,
                    Reply = entity.Reply,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                };

                var newHrNotificationReplyEntity = await hrRepositoryManager.HrNotificationsReplyRepository.AddAndReturn(HrNotificationReply);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var url = $"/Apps/HR/Notifications/EmpNotificationInbox.aspx?Notification_ID={entity.NotificationId}";
                if (entity.Source == "inbox")
                    url = $"/Apps/HR/Notifications/EmpNotificationOutbox.aspx?Notification_ID={entity.NotificationId}";

                var senderName = await mainRepositoryManager.SysUserRepository.GetOne(x => x.UserFullname, x => x.Isdel == false && x.IsDeleted == false && x.Id == session.UserId);

                if (entity.Source == "inbox")
                {
                    var GetNotificationRecieverID = await hrRepositoryManager.HrNotificationRepository.GetOne(x => x.IsDeleted == false && x.Id == entity.NotificationId);
                    NotificationRecieverID = (int?)GetNotificationRecieverID.CreatedBy;
                }
                if (entity.Source == "outbox")
                {
                    var getfromNotifications = await hrRepositoryManager.HrNotificationRepository.GetAll(x => x.IsDeleted == false && x.Id == session.UserId);
                    var notificationsEmpIdList = getfromNotifications.Select(x => x.EmpId);

                    var getfromSysUser = await mainRepositoryManager.SysUserRepository.GetAll(x => x.Isdel == false && x.IsDeleted == false);

                    var finalResult = getfromSysUser.Where(x => notificationsEmpIdList.Any(y => y == x.EmpId)).FirstOrDefault();
                    NotificationRecieverID = (int?)finalResult.Id;
                }


                message = $"   يوجد لديك رد على الاشعار من قبل  {senderName}";
                if (NotificationRecieverID > 0)
                {
                    var newSysNotification = new SysNotification
                    {
                        UserId = NotificationRecieverID,
                        MsgTxt = message,
                        IsRead = false,
                        CreatedBy = session.UserId,
                        Url = url,
                        TableId = null,
                        ActivityLogId = null,
                    };

                    await mainRepositoryManager.SysNotificationRepository.Add(newSysNotification);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<string>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }
    }
}