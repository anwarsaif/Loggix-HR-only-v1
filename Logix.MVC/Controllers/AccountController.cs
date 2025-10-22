using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.Main;
using Logix.Domain.ACC;
using Logix.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using System.Globalization;

namespace Logix.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMainServiceManager serviceManager;
        private readonly ISysConfigurationHelper configurationHelper;
        private readonly IAccServiceManager accServiceManager;
        private readonly IMvcSession mvcSession;
        private readonly IAuthService authService;

        public AccountController(IMainServiceManager serviceManager,
            ISysConfigurationHelper configurationHelper,
            IAccServiceManager accServiceManager,
            IMvcSession mvcSession,
            IAuthService authService)
        {
            this.serviceManager = serviceManager;
            this.configurationHelper = configurationHelper;
            this.accServiceManager = accServiceManager;
            this.mvcSession = mvcSession;
            this.authService = authService;
        }
        public async Task<IActionResult> Login()
        {

            var per = await serviceManager.SysScreenPermissionService.GetAll();
            if (per.Succeeded)
            {
                Console.WriteLine($"================ init Db befor login: {per.Data.Where(s => s.ScreenId == 1).Count()}");
                Console.WriteLine($"================ first per screen: {per.Data.First().ScreenId}");
                Console.WriteLine($"================ first per group : {per.Data.First().GroupId}");
                Console.WriteLine($"================ first per add: {per.Data.First().ScreenAdd}");
                Console.WriteLine($"================ first per edit: {per.Data.First().ScreenEdit}");
                return View();
            }
            else
            {
                return View();
            }

        }
        /* [NonAction]
         public void TestWa()
         {
             try
             {
                 var msgData = new { USER_FULLNAME = }
                 WhatsappBusinessService waService = new WhatsappBusinessService();
                 var data = new WhatsappBusinessMessageData
                 {
                     DocumentUrl = "https://www.pdf995.com/samples/pdf.pdf",
                     RecipientPhoneNumber = "775699645",
                     DataMessage = user,
                     HasDocument = false,
                     //WaTemplateMessageValue = add.Data,
                 };

                 var waResult = waService.SendWhatsappMessage(data, screenId: 1300, facilityId: 1);
                 return Ok(waResult);
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }

         }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto login)
        {
            string CalendarType = "1";
            if (ModelState.IsValid)
            {
                if (login.FacilityId == 0)
                {
                    ModelState.AddModelError(string.Empty, "يرجى اختيار شركة");
                    return View(login);
                }
                var result = await authService.Login2(login);
                if (result.Succeeded && result.Data != null)
                {
                    var getUser = await authService.GetByName(login.UserName);
                    if (getUser.Succeeded)
                    {
                        int DeptId = 0;
                        int LocationId = 0;
                        int SalesType = 0;

                        var currentUser = getUser.Data;
                        if (result.Data.SalesType != null)
                        {
                            SalesType = result.Data.SalesType ?? 0;
                        }

                        LocationId = result.Data.Location;
                        DeptId = result.Data.DeptId;
                        /* var msgData = new { USER_FULLNAME = currentUser.UserFullname, USER_NAME = currentUser.UserName };
                         WhatsappBusinessService waService = new WhatsappBusinessService();
                         var data = new WhatsappBusinessMessageData
                         {
                             DocumentUrl = "https://www.pdf995.com/samples/pdf.pdf",
                             RecipientPhoneNumber = "775699645",
                             DataMessage = msgData,
                             HasDocument = true,
                             //WaTemplateMessageValue = add.Data,
                         };

                         var waResult = waService.SendWhatsappMessage(data, screenId: 1301, facilityId: currentUser.FacilityId??1);*/
                        mvcSession.AddData<Domain.Main.SysUser>("user", currentUser);


                        var secret = await configurationHelper.GetValue(265, 1);
                        var oldBaseUrl = await configurationHelper.GetValue(266, 1);
                        var coreBaseUrl = await configurationHelper.GetValue(267, 1);
                        /* var integrationConfig = configuration.GetSection("IntegrationConfig");
                         var oldBaseUrl = integrationConfig.GetValue<string>("OldBaseUrl");
                         var coreBaseUrl = integrationConfig.GetValue<string>("CoreBaseUrl");*/

                        mvcSession.AddData<string>("OldBaseUrl", oldBaseUrl);
                        mvcSession.AddData<string>("CoreBaseUrl", coreBaseUrl);

                        long currFinYear = 0;
                        int currFinYearGregorian = 0;
                        var getFinYears = await serviceManager.SysSystemService.GetFinancialYears(currentUser.FacilityId ?? 0);
                        if (getFinYears.Succeeded && getFinYears.Data.Any())
                        {
                            currFinYear = getFinYears.Data.Where(n => n.FacilityId == currentUser.FacilityId && n.IsDeleted == false).Last().FinYear;
                            currFinYearGregorian = getFinYears.Data.Where(n => n.FacilityId == currentUser.FacilityId && n.IsDeleted == false).Last().FinYearGregorian;
                        }
                        mvcSession.AddData<int>("FinyearGregorian", currFinYearGregorian);
                        mvcSession.AddData<long>("FinYear", currFinYear);

                        var getLang = CultureInfo.CurrentCulture.Name;
                        var currLang = 1;
                        if (getLang.StartsWith("ar"))
                        {
                            currLang = 1;
                            mvcSession.AddData<int>("language", 1);
                        }
                        else
                        {
                            currLang = 2;
                            mvcSession.AddData<int>("language", 2);
                        }

                        long currFacilityId = 0;
                        var getFacilities = await serviceManager.SysSystemService.GetFacilities();
                        if (getFacilities.Succeeded)
                        {
                            var facility = getFacilities.Data.Where(f => f.FacilityId == login.FacilityId).FirstOrDefault();
                            if (facility != null)
                            {
                                currFacilityId = facility.FacilityId;
                                mvcSession.AddData<AccFacilitiesVw>("facility", facility);
                            }
                        }


                        /////////  to get Calendat Type
                        var getCalendarType = await configurationHelper.GetValue(19, currFacilityId);
                        if (!string.IsNullOrEmpty(getCalendarType))
                        {
                            CalendarType = getCalendarType;
                        }
                        long currPeriodId = 0;

                        var getPeriodId = await accServiceManager.AccPeriodsService.GetAll();
                        if (getPeriodId.Succeeded)
                        {

                            var Period = getPeriodId.Data.Where(x => x.FlagDelete == false && x.PeriodState == 1 && x.FinYear == currFinYear).FirstOrDefault();
                            if (Period != null)
                            {
                                currPeriodId = Period.PeriodId;
                                mvcSession.AddData<long>("Period", currPeriodId);
                            }
                        }
                        int groupId = 0;

                        if (!string.IsNullOrEmpty(currentUser.GroupsId))
                        {
                            var hasGroup = int.TryParse(currentUser.GroupsId, out groupId);
                        }
                        var token = authService.GetJWTToken(result.Data, secret, currFinYear, currPeriodId, currLang, currFinYearGregorian, CalendarType);
                        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VybmFtZSI6ImFkbWluIiwiRnVsbE5hbWUiOiLZhdiz2KrYrtiv2YUg2KfZhNmF2YjYuNmBINix2YLZhSAgIDIwNzkiLCJFbXBDb2RlIjoiMjA3OSIsIkVtcElkIjoiMTAyIiwiR3JvdXBJZCI6IjEiLCJVc2VySWQiOiIxIiwiRGVwdElkIjoiMzgiLCJCcmFuY2hlcyI6IjEsMiIsIkJyYW5jaElkIjoiMSIsIkxvY2F0aW9uSWQiOiIzOSIsIkZhY2lsaXR5SWQiOiIxIiwiRmluWWVhciI6IjI3IiwiUGVyaW9kSWQiOiI0MCIsIkxhbmd1YWdlIjoiMiIsIkZpbnllYXJHcmVnb3JpYW4iOiIyMDI1IiwiQ2FsZW5kYXJUeXBlIjoiMSIsIlNhbGVzVHlwZSI6IjAiLCJJc0F6dXJlQXV0aGVudGljYXRlZCI6IjAiLCJpc0FncmVlIjoiVHJ1ZSIsImV4cCI6MTc1NTQ2NDM5OH0.fohLXpZxK33F5w8JjaDCo0HI4WhyM-MRmwLbGuOmhdo
                        await Task.Run(() => mvcSession.SetMainData(currentUser.Id, currentUser.EmpId ?? 0, groupId, currFacilityId, currFinYear, currLang, currentUser.BranchsId ?? "", CalendarType, currentUser.UserPkId ?? 0, currFinYearGregorian, LocationId, DeptId, SalesType, currPeriodId));


                        return Redirect($"{Request.Scheme}://{Request.Host}/swagger/index.html");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Error: {getUser.Status.message}");
                        return View(login);
                    }
                }
                else
                {
                    // foreach(var er in result.)
                    ModelState.AddModelError(string.Empty, $"Error: {result.Status.message}");
                    return View(login);
                }
            }
            else
            {
                Console.WriteLine("in else in login user");
                return View(login);
            }
            //return View();
        }

        public IActionResult Logout()
        {
            var logout = mvcSession.ClearSession();
            return RedirectToAction("Login", "Account");
        }


        //[HttpGet]
        //public async Task<IActionResult> TestReport()
        //{
        //    try
        //    {
        //        // Create the report instance
        //        XtraReport report = new TestReport();

        //        // Wrap the report with CachedReportSourceWeb
        //        var cachedReportSource = new CachedReportSourceWeb(report);

        //        // Use ViewData to pass the CachedReportSourceWeb
        //        ViewData["ReportName"] = cachedReportSource;
        //        ViewData["ReportHeader"] = "Test DevExpress Report";

        //        //ViewBag.ReportHeader = "Test DevExpress Report";
        //        //ViewBag.ReportName = cachedReportSource;
        //        return View("~/Views/_ReportViewer.cshtml");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(await Result.FailAsync(ex.Message));
        //    }
        //}
    }
}
