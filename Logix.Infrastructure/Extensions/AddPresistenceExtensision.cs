using Logix.Application.Common;
using Logix.Infrastructure.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Internal;
using System.Globalization;
using WhatsappBusiness.CloudApi.Configurations;
using WhatsappBusiness.CloudApi.Extensions;

namespace Logix.Infrastructure.Extensions
{
    public static class AddPresistenceExtensision
    {
        public static IServiceCollection AddPresistence(this IServiceCollection services, IConfiguration configuration)
        {
            /* services.AddControllersWithViews()
             .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
             .AddDataAnnotationsLocalization();*/
            services.AddDbContext<DbContexts.ApplicationDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("LogixLocal"));
                op.EnableSensitiveDataLogging();
            }
            );



            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });




            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("ar-SA"),
                new CultureInfo("en-US"),
                //new CultureInfo("ar-YE"),
                // Add other supported cultures here...
            };

                var cookieProvider = new CookieRequestCultureProvider
                {
                    CookieName = "culture"
                };

                options.DefaultRequestCulture = new RequestCulture("ar-SA", "ar-SA");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, cookieProvider);

            });
            services.AddSingleton<ILocalizationService, LocalizationService>();


            services.TryAddSingleton<ISystemClock, SystemClock>();

            WhatsAppBusinessCloudApiConfig whatsAppConfig = new WhatsAppBusinessCloudApiConfig();
            whatsAppConfig.WhatsAppBusinessPhoneNumberId = configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessPhoneNumberId"];
            whatsAppConfig.WhatsAppBusinessAccountId = configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessAccountId"];
            whatsAppConfig.WhatsAppBusinessId = configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessId"];
            whatsAppConfig.AccessToken = configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["AccessToken"];

            services.AddWhatsAppBusinessCloudApiService(whatsAppConfig);





            return services;

        }
    }
}
