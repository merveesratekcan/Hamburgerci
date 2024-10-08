using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApp.Domain.Contracts.OrdersContracts.CartProductRepositories;
using RestaurantApp.Domain.Contracts.OrdersContracts.CartRepositories;
using RestaurantApp.Domain.Contracts.OrdersContracts.CommentRepositories;
using RestaurantApp.Domain.Contracts.OrdersContracts.OrderRepositories;
using RestaurantApp.Domain.Contracts.ProductContracts.CampaignRepositories;
using RestaurantApp.Domain.Contracts.ProductContracts.CategoryRepositories;
using RestaurantApp.Domain.Contracts.ProductContracts.IngredientRepositories;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductCampaignRepositories;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductIngredientRepositories;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductRepositories;
using RestaurantApp.Domain.Contracts.UserContracts.AdminRepositories;
using RestaurantApp.Domain.Contracts.UserContracts.AppUserRepositories;
using RestaurantApp.Domain.Contracts.WebsitesContracts.AboutUsRepositories;
using RestaurantApp.Domain.Contracts.WebsitesContracts.ContactUsRepositories;
using RestaurantApp.Domain.Contracts.WebsitesContracts.FeatureRepositories;
using RestaurantApp.Domain.Contracts.WebsitesContracts.SliderRepositories;
using RestaurantApp.Domain.Contracts.WebsitesContracts.SocialMediaRepositories;
using RestaurantApp.Infrastructure.Context;
using RestaurantApp.Infrastructure.Repositories.OrderRepositories.CartProductRepository;
using RestaurantApp.Infrastructure.Repositories.OrderRepositories.CartRepositories;
using RestaurantApp.Infrastructure.Repositories.OrderRepositories.CommentRepository;
using RestaurantApp.Infrastructure.Repositories.OrderRepositories.OrderRepository;
using RestaurantApp.Infrastructure.Repositories.ProductRepositories.CampaignRepository;
using RestaurantApp.Infrastructure.Repositories.ProductRepositories.CategoryRepository;
using RestaurantApp.Infrastructure.Repositories.ProductRepositories.IngredientRepository;
using RestaurantApp.Infrastructure.Repositories.ProductRepositories.ProductCampaignRepository;
using RestaurantApp.Infrastructure.Repositories.ProductRepositories.ProductIngredientRepository;
using RestaurantApp.Infrastructure.Repositories.ProductRepositories.ProductRepository;
using RestaurantApp.Infrastructure.Repositories.UserRepositories.AdminRepository;
using RestaurantApp.Infrastructure.Repositories.UserRepositories.AppUserRepository;
using RestaurantApp.Infrastructure.Repositories.WebsitesRepositories.AboutUsRepository;
using RestaurantApp.Infrastructure.Repositories.WebsitesRepositories.ContactUsRepository;
using RestaurantApp.Infrastructure.Repositories.WebsitesRepositories.FeatureRepository;
using RestaurantApp.Infrastructure.Repositories.WebsitesRepositories.SliderRepository;
using RestaurantApp.Infrastructure.Repositories.WebsitesRepositories.SocialMediaRepository;
using RestaurantApp.Infrastructure.Seeds;


namespace RestaurantApp.Infrastructure.IOC;

public static class InfrastructureServiceRegistration
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
	{
		services.AddDbContext<AppDbContext>(options =>
		{
			options.UseLazyLoadingProxies();
			options.UseSqlServer(configuration.GetConnectionString(AppDbContext.DevConnectionString));
			//appsettings.json dosyasındaki connection stringi alıyoruz.
		});

		//Users		
        services.AddScoped<IAdminRepository, AdminRepository>();
		services.AddScoped<IAppUserRepository, AppUserRepository>();
		services.AddScoped<IUserAddressRepository,UserAddressRepository>();
		//Products
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IIngredientRepository, IngredientRepository>();
		services.AddScoped<ICampaignRepository, CampaignRepository>();
		services.AddScoped<IProductCampaignRepository, ProductCampaignRepository>();
		services.AddScoped<IProductIngredientRepository, ProductIngredientRepository>();
		//Orders
		services.AddScoped<ICartRepository, CartRepository>();
		services.AddScoped<ICartProductRepository, CartProductRepository>();
		services.AddScoped<ICommentRepository, CommentRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		//Websites
		services.AddScoped<IAboutUsRepository, AboutUsRepository>();
		services.AddScoped<IContactUsRepository, ContactUsRepository>();
		services.AddScoped<IFeatureRepository, FeatureRepository>();
		services.AddScoped<ISliderRepository, SliderRepository>();
		services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
		//services.AddScoped<IMenuCampaignRepository, MenuCampaignRepository>();
		//services.AddScoped<IMenuRepository, MenuRepository>();

		//İlk migration'da yoruma al!!
		AdminSeed.SeedAsync(configuration).GetAwaiter().GetResult();

		return services;
	}
}
