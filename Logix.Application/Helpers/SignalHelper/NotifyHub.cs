using Logix.Application.Common;
using Logix.Application.Interfaces.IServices;
using Microsoft.AspNetCore.SignalR;
using System.Globalization;

namespace Logix.Application.Helpers.SignalHelper
{
    /// SignalR Hub لإدارة الإشعارات والاتصال المباشر.
    /// - يدير اتصالات المستخدمين والانضمام لمجموعات خاصة بكل مستخدم على شكل: <c>User_{userId}</c>.
    /// - يبث الإشعارات على الحدث الموحد: <c>GetUserNotifications</c>.


    public class NotifyHub : Hub
    {
        private readonly IMainServiceManager _mainServiceManager;
        private readonly ICurrentData _currentData;

        public NotifyHub(IMainServiceManager mainServiceManager, ICurrentData currentData)
        {
            _mainServiceManager = mainServiceManager;
            _currentData = currentData;
        }

        private string? GetAuthenticatedUserId()
        {
            var userId = _currentData?.UserId ?? -1;
            if (userId > 0)
            {
                return userId.ToString();
            }

            var identityName = Context.User?.Identity?.Name;
            return string.IsNullOrWhiteSpace(identityName) ? null : identityName;
        }

        #region Connection Management


        /// عند اتصال المستخدم: يضيف الاتصال إلى مجموعة عامة <c>AllUsers</c>
        /// وإذا كان المستخدم مصادق عليه يضيفه كذلك إلى مجموعته الخاصة <c>User_{userId}</c>.

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "AllUsers");

            if (Context.User?.Identity?.IsAuthenticated == true)
            {
                var userId = GetAuthenticatedUserId();
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");
                }
            }

            await base.OnConnectedAsync();
        }

        /// عند قطع اتصال المستخدم: يزيل الاتصال من <c>AllUsers</c> ومن مجموعة المستخدم الخاصة إن وُجدت.
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "AllUsers");

            if (Context.User?.Identity?.IsAuthenticated == true)
            {
                var userId = GetAuthenticatedUserId();
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"User_{userId}");
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        #endregion

        #region Group Management


        /// انضمام اتصال العميل الحالي إلى مجموعة المستخدم الخاصة <c>User_{userId}</c>.
        public async Task JoinUserGroup(string userId)
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            if (!string.IsNullOrWhiteSpace(authenticatedUserId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{authenticatedUserId}");
            }
        }

        /// مغادرة اتصال العميل الحالي لمجموعة المستخدم الخاصة <c>User_{userId}</c>.
        public async Task LeaveUserGroup(string userId)
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            if (!string.IsNullOrWhiteSpace(authenticatedUserId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"User_{authenticatedUserId}");
            }
        }

        #endregion

        #region Notification Methods

        /// استدعاء مباشر من العميل لجلب إشعارات المستخدم الحالية من الخدمة
        /// ثم يبثها إلى مجموعة <c>User_{userId}</c> على الحدث: <c>GetUserNotifications</c>.

        public async Task RequestUserNotifications(string userId)
        {
            try
            {
                var getNotifs = await _mainServiceManager.SysNotificationService.GetTopVw();
                if (getNotifs.Succeeded && getNotifs.Data != null)
                {
                    var result = getNotifs.Data.Select(item => new
                    {
                        item.Id,
                        item.MsgTxt,
                        item.Url,
                        item.UserFullname,
                        CreatedOn = item.CreatedOn != null
                            ? item.CreatedOn.Value.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.InvariantCulture)
                            : string.Empty
                    }).ToList();

                    var authenticatedUserId = GetAuthenticatedUserId();
                    if (!string.IsNullOrWhiteSpace(authenticatedUserId))
                    {
                        await Clients.Group($"User_{authenticatedUserId}").SendAsync("GetUserNotifications", result);
                    }
                }
                else
                {
                    var authenticatedUserId = GetAuthenticatedUserId();
                    if (!string.IsNullOrWhiteSpace(authenticatedUserId))
                    {
                        await Clients.Group($"User_{authenticatedUserId}").SendAsync("GetUserNotifications", Array.Empty<object>());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RequestUserNotifications: {ex.Message}");
            }
        }

        #endregion
    }
}



