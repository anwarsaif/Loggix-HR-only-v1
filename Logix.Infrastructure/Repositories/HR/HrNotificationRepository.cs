using DocumentFormat.OpenXml.Office2013.Excel;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrNotificationRepository : GenericRepository<HrNotification>, IHrNotificationRepository
    {
        private readonly ApplicationDbContext context;

        public HrNotificationRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        //public async Task<object> LoadNotifications(string Subject, long FacilityId)
        //{
        //    var notifications =await (from notification in context.HrNotificationsVws
        //                         where notification.IsDeleted == false &&
        //                               notification.FacilityId == FacilityId &&
        //                               (string.IsNullOrEmpty(Subject) || Subject == "" ||
        //                                notification.Subject.Contains(Subject) ||
        //                                notification.Detailes.Contains(Subject))
        //                         select notification).ToListAsync();

        //    var notificationFiles = (from notification in notifications
        //                             from sf in context.SysFiles
        //                             where sf.IsDeleted == false && sf.TableId == 96 &&
        //                                   sf.PrimaryKey == notification.Id
        //                             orderby sf.Id descending
        //                             select new
        //                             {
        //                                 NotificationId = notification.Id,
        //                                 File_Name = sf.FileName.Trim(),
        //                                 File_URL = sf.FileUrl,
        //                                 File_Date = sf.FileDate
        //                             }).ToList();

        //    var notificationReplies = (from notification in notifications
        //                               from nr in context.hrNotificationsReplies
        //                               join user in context.SysUsers on nr.CreatedBy equals user.Id into userGroup
        //                               from user in userGroup.DefaultIfEmpty()
        //                               where nr.IsDeleted == false && notification.Id == nr.NotificationId
        //                               orderby nr.Id descending
        //                               select new
        //                               {
        //                                   NotificationId = notification.Id,
        //                                   Reply = nr.Reply.Trim(),
        //                                   CreatedOn = ((DateTime)nr.CreatedOn).ToString("yyyy/MM/dd"),
        //                                   CreatedBy = user.UserFullname
        //                               }).ToList();

        //    var result = notifications.Select(notification => new
        //    {
        //        notification,
        //        NotificationFiles = notificationFiles.Where(f => f.NotificationId == notification.Id).ToList(),
        //        NotificationReplies = notificationReplies.Where(r => r.NotificationId == notification.Id).ToList()
        //    }).ToList();

        //    return result;
        //}







        //public async Task<object> LoadNotifications(string subject, long facilityId)
        //{

        //        // Fetch notifications
        //        var notifications = await (from notification in context.HrNotificationsVws
        //                                   where notification.IsDeleted == false &&
        //                                         notification.FacilityId == facilityId &&
        //                                         (string.IsNullOrEmpty(subject) || subject == "" ||
        //                                          notification.Subject.Contains(subject) ||
        //                                          notification.Detailes.Contains(subject))
        //                                   select notification).ToListAsync();

        //        // Fetch notification files
        //        var notificationFileIds = notifications.Select(n => n.Id).ToList();
        //        var notificationFiles = await (from sf in context.SysFiles
        //                                       where sf.IsDeleted == false && sf.TableId == 96 && notificationFileIds.Contains((long)sf.PrimaryKey)
        //                                       orderby sf.Id descending
        //                                       select new
        //                                       {
        //                                           NotificationId = sf.PrimaryKey,
        //                                           File_Name = sf.FileName.Trim(),
        //                                           File_URL = sf.FileUrl,
        //                                           File_Date = sf.FileDate
        //                                       }).ToListAsync();

        //        // Fetch notification replies
        //        var notificationReplyIds = notifications.Select(n => n.Id).ToList();
        //        var notificationReplies = await (from nr in context.hrNotificationsReplies
        //                                         join user in context.SysUsers on nr.CreatedBy equals user.Id into userGroup
        //                                         from user in userGroup.DefaultIfEmpty()
        //                                         where nr.IsDeleted == false && notificationReplyIds.Contains((long)nr.NotificationId)
        //                                         orderby nr.Id descending
        //                                         select new
        //                                         {
        //                                             NotificationId = nr.NotificationId,
        //                                             Reply = nr.Reply.Trim(),
        //                                             CreatedOn = ((DateTime)nr.CreatedOn).ToString("yyyy/MM/dd"),
        //                                             CreatedBy = user.UserFullname
        //                                         }).ToListAsync();

        //        // Combine the data
        //        var result = notifications.Select(notification => new
        //        {
        //            notification,
        //            NotificationFiles = notificationFiles.Where(f => f.NotificationId == notification.Id).ToList(),
        //            NotificationReplies = notificationReplies.Where(r => r.NotificationId == notification.Id).ToList()
        //        }).ToList();

        //        return result;
        //    }



        public async Task<object> LoadNotifications(string Subject, long FacilityId)
        {
            // Load notifications asynchronously
            var notifications = await (from notification in context.HrNotificationsVws
                                       where notification.IsDeleted == false &&
                                             notification.FacilityId == FacilityId &&
                                             (string.IsNullOrEmpty(Subject) ||
                                              notification.Subject.Contains(Subject) ||
                                              notification.Detailes.Contains(Subject))
                                       select notification).ToListAsync();
     
            // Load notification files asynchronously
            var notificationIds = notifications.Select(n => n.Id).ToList();
            var notificationFiles = await (from sf in context.SysFiles
                                           where sf.IsDeleted == false && sf.TableId == 96 &&
                                                 notificationIds.Contains((long)sf.PrimaryKey)
                                           orderby sf.Id descending
                                           select new
                                           {
                                               NotificationId = sf.PrimaryKey,
                                               File_Name = sf.FileName.Trim(),
                                               File_URL = sf.FileUrl,
                                               File_Date = sf.FileDate
                                           }).ToListAsync();

            // Load notification replies asynchronously
            var notificationReplies = await (from nr in context.hrNotificationsReplies
                                             join user in context.SysUsers on nr.CreatedBy equals user.Id into userGroup
                                             from user in userGroup.DefaultIfEmpty()
                                             where nr.IsDeleted == false && notificationIds.Contains((long)nr.NotificationId)
                                             orderby nr.Id descending
                                             select new
                                             {
                                                 NotificationId = nr.NotificationId,
                                                 Reply = nr.Reply.Trim(),
                                                 CreatedOn = ((DateTime)nr.CreatedOn).ToString("yyyy/MM/dd"),
                                                 CreatedBy = user.UserFullname
                                             }).ToListAsync();

            // Combine results
            var result = notifications.Select(notification => new
            {
                notification,
                NotificationFiles = notificationFiles.Where(f => f.NotificationId == notification.Id).ToList(),
                NotificationReplies = notificationReplies.Where(r => r.NotificationId == notification.Id).ToList()
            }).ToList();

            return result;
        }


    }



}
