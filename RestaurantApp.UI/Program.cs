using Microsoft.EntityFrameworkCore;
using RestaurantApp.Infrastructure.IOC;
using RestaurantApp.Application.IOC;
using RestaurantApp.UI.IOC;
using RestaurantApp.Domain.Entities.GoogleCaptchas;
using RestaurantApp.Infrastructure.Context;
using RestaurantApp.Application.Services.UserServices.GoogleCaptchaServices;
using Microsoft.AspNetCore.Mvc.Razor;
using Hangfire;
using RestaurantApp.Hangfire;
using RestaurantApp.UI.Hubs;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews().AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddUIServices();
builder.Services.Configure<GoogleCaptchaConfig>(builder.Configuration.GetSection("GoogleReCaptcha"));
builder.Services.AddTransient(typeof(GoogleCaptchaService));


var connectionString = builder.Configuration.GetConnectionString("MelodyAppDev"); // veya "AppConnectionDev"
builder.Services.AddHangfireServices(connectionString);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString).UseLazyLoadingProxies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();
app.UseHangfireServer();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();
app.MapHub<SignalRHub>("/signalrhub");
app.Run();
