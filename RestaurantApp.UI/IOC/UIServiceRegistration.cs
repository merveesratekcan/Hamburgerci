using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using RestaurantApp.Infrastructure.Context;
using System.Globalization;

namespace RestaurantApp.UI.IOC;

public static class UIServiceRegistration
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);
        return services;
    }
    public static IServiceCollection AddUIServices(this IServiceCollection services)
    {
        AddIdentityService(services);
        services.AddNotyf(config =>
        {
            config.HasRippleEffect = true;
            config.DurationInSeconds = 5;
            config.Position = NotyfPosition.BottomRight;
            config.IsDismissable = true;
            //bilgi kutucuğunun ekranda kaç sn duracağını ayarlar
            //kutucuğun konumunu ayarlar
            //çarpıya basıp kapatılıp kapatılamayacağını ayarlarız.
        });

        
        services.AddLocalization(opt => opt.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(opt =>
        {
            var supCultures = new List<CultureInfo>()
                {
                    new CultureInfo("tr"),
                    new CultureInfo("en")
                };
            opt.DefaultRequestCulture = new RequestCulture("tr");
            opt.SupportedUICultures = supCultures;
        });
        return services;

    }
}
