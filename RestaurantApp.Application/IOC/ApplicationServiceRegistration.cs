using Microsoft.Extensions.DependencyInjection;
using RestaurantApp.Application.Services.OrderServices.AboutServices;
using RestaurantApp.Application.Services.ProductsServices.CampaignServices;
using RestaurantApp.Application.Services.ProductsServices.CategoryServices;
using RestaurantApp.Application.Services.ProductsServices.IngredientServices;
using RestaurantApp.Application.Services.ProductsServices.ProductIngredientServices;
using RestaurantApp.Application.Services.ProductsServices.ProductServices;
using RestaurantApp.Application.Services.UserServices.AccountServices;
using RestaurantApp.Application.Services.UserServices.AdminServices;
using RestaurantApp.Application.Services.UserServices.AppUserEmailServices;
using RestaurantApp.Application.Services.UserServices.AppUserServices;
using RestaurantApp.Application.Services.UserServices.EmailServices;
using RestaurantApp.Application.Services.UserServices.GoogleCaptchaServices;
using RestaurantApp.Application.Services.UserServices.UserAddressServices;
using RestaurantApp.Application.Services.WebServices.ContactServices;
using RestaurantApp.Application.Services.WebServices.FeatureServices;
using RestaurantApp.Application.Services.WebServices.SliderServices;
using RestaurantApp.Domain.Entities.Users.AppUser;

namespace RestaurantApp.Application.IOC;

public static class ApplicationServiceRegistration
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{

        //User
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAppUserService, AppUserService>();
        services.AddScoped<IUserAddressService, UserAddressService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IGoogleCaptchaService, GoogleCaptchaService>();
        services.AddScoped<IAppUserEmailService, AppUserEmailService>();
        //Product
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IIngredientService, IngredientService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICampaignService, CampaignService>();
        services.AddScoped<IAboutService, AboutService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<ISliderService, SliderService>();
        services.AddScoped<IFeatureService, FeatureService>();
        services.AddScoped<IProductIngredientService, ProductIngredientService>();
    
        return services;
    }
}
