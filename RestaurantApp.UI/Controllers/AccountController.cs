
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RestaurantApp.UI.Models;
using Microsoft.Extensions.Localization;
using Mapster;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Bcpg;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Application.Services.UserServices.EmailServices;
using RestaurantApp.Application.Services.UserServices.GoogleCaptchaServices;

namespace RestaurantApp.UI.Controllers;



public class AccountController : BaseController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMailService _mailService;
    private readonly IStringLocalizer<ModelResource> _stringLocalizer;
    private readonly IGoogleCaptchaService _captchaService;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IMailService mailService, IGoogleCaptchaService captchaService, IStringLocalizer<ModelResource> stringLocalize = null)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mailService = mailService;
        _stringLocalizer = stringLocalize;
        _captchaService = captchaService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        var captchaResult = await _captchaService.VerifyToken(model.Token);
        if (!captchaResult)
        {
            //Notify(_stringLocalizer["Captcha authentication has failed!"], notificationType: NotificationType.error);
            NotifyError(_stringLocalizer["Captcha authentication has failed!"]);
            return View(model);
        }
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                //Notify(_stringLocalizer["Email or password is false!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["Email or password is false!"]);
                return View(model);
            }

            var checkPass = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!checkPass.Succeeded)
            {
                //Notify(_stringLocalizer["Email or password is false!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["Email or password is false!"]);
                return View(model);
            }

            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole == null)
            {
                //Notify(_stringLocalizer["Email or password is false!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["Email or password is false!"]);
                return View(model);
            }
            Notify(_stringLocalizer["Welcome to the MelodyBurger administration panel!"], notificationType: NotificationType.success);
            NotifySuccess(_stringLocalizer["Successfully logged in!"]);
            return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        //Notify(_stringLocalizer["Logged out!"], notificationType: NotificationType.success);
        NotifySuccess(_stringLocalizer["Logged out!"]);
        return RedirectToAction("Login", "Account");
    }
    public async Task<IActionResult> UserLogout()
    {
        await _signInManager.SignOutAsync();
        //Notify(_stringLocalizer["Logged out!"], notificationType: NotificationType.success);
        NotifySuccess(_stringLocalizer["Logged out!"]);
        return RedirectToAction("Index", "DefaultMain");
    }
}
