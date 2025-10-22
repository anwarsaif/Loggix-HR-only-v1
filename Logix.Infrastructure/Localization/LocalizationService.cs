using Logix.Application.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IStringLocalizerFactory _localizerFactory;

        public LocalizationService(IStringLocalizerFactory localizerFactory)
        {
            _localizerFactory = localizerFactory;
        }

        public IList<CultureInfo> GetSupportedCultures()
        {
            return new List<CultureInfo>
            {
                // we can add any language code here for other languages
                new CultureInfo("ar-SA"),
                new CultureInfo("en-US")
            };
        }

        public void ConfigureLocalization(IApplicationBuilder app)
        {
            var supportedCultures = GetSupportedCultures();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                //DefaultRequestCulture = new RequestCulture("en-US"),
                DefaultRequestCulture = new RequestCulture("ar-SA"),

                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
              
                
            });
            Console.WriteLine($"=========in infra, current culture: {CultureInfo.CurrentUICulture}");
        }

        public string GetLocalizedResource(string key, string resource, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create(resource, "Logix.Infrastructure");
            return localizer.GetString(key);
        }
        
        public string GetMessagesResource(string key, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("Messages", "Logix.Infrastructure");
            return localizer.GetString(key);
        }

        public string GetMainResource(string key, CultureInfo culture = default)
        {
            var localizer = _localizerFactory.Create("Main", "Logix.Infrastructure");
            return localizer.GetString(key);
        }

        public string GetAccResource(string key, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("Acc", "Logix.Infrastructure");
            return localizer.GetString(key);
        }

        public string GetCommonResource(string key, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("Common", "Logix.Infrastructure");
            return localizer.GetString(key);
        }
        
        public string GetHrResource(string key, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("Hr", "Logix.Infrastructure");
            return localizer.GetString(key);
        }

        public string GetPMResource(string key, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("PM", "Logix.Infrastructure");
            return localizer.GetString(key);
        }
        public string GetSALResource(string key, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("SAL", "Logix.Infrastructure");
            return localizer.GetString(key);
        }
        public string GetResource1(string key, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("Resource1", "Logix.Infrastructure");
            return localizer.GetString(key);
        }

        public string GetCoreResource(string key, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("Core", "Logix.Infrastructure");
            return localizer.GetString(key);
        }
        
        public string GetInventoryResource(string key, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("Inventory", "Logix.Infrastructure");
            return localizer.GetString(key);
        }
        
         public string GetPUResource(string key, CultureInfo culture = default)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("PUR", "Logix.Infrastructure");
            return localizer.GetString(key);
        }

        public string GetSSResources(string key, CultureInfo culture = null)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var localizer = _localizerFactory.Create("SS", "Logix.Infrastructure");
            return localizer.GetString(key);
        }
    }
}
