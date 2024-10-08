
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.UsersDTOs.MailDTOs;
using RestaurantApp.Application.Services.UserServices.AppUserServices;
using RestaurantApp.Application.Services.UserServices.GoogleCaptchaServices;
using RestaurantApp.Domain.Enums;
using RestaurantApp.UI.Models;
using Mapster;
using RestaurantApp.Application.DTOs.UsersDTOs.AppUserDTOs;
using RestaurantApp.Application.Services.UserServices.AppUserEmailServices;
using RestaurantApp.UI.Areas.Admin.Models.AdminVMs;
using RestaurantApp.UI.Models.AppUser;

namespace RestaurantApp.UI.Controllers
{
    public class AppUserAccountController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAppUserService _appUserService;
        private readonly IAppUserEmailService _mailService;
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;
        private readonly IGoogleCaptchaService _googleCaptchaService;
        private const string userPassword = "Password.1234";
        public AppUserAccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAppUserEmailService mailService, IAppUserService appUserService, IStringLocalizer<ModelResource> stringLocalizer, IGoogleCaptchaService googleCaptchaService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _appUserService = appUserService;
            _stringLocalizer = stringLocalizer;
            _googleCaptchaService = googleCaptchaService;
        }
       public async Task<IActionResult> UserProfile()
        {
            var appUserIdentityId = await _userManager.GetUserAsync(User);
            var appUserId = await _appUserService.GetAppUserIdByIdentityId(appUserIdentityId.Id);
            var result = await _appUserService.GetByIdAsync(appUserId);
            var userProfileVM = result.Data.Adapt<UserProfileVM>();
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["User not found!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["User not found!"]);
                return View(userProfileVM);
            }
            return View(userProfileVM);
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM model)
        {
            var appUserCreateDTO = model.Adapt<AppUserCreateDTO>();
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                //var result = await _userManager.CreateAsync(user, userPassword);
                var result = await _appUserService.CreateAsync(appUserCreateDTO);

                if (result.IsSuccess)
                {
                    //await _userManager.AddToRoleAsync(user, Roles.AppUser.ToString());
                    var identityUser = await _userManager.FindByEmailAsync(model.Email);
                    var confirmCode = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                    var url = Url.Action("ConfirmEmail", "AppUserAccount", new { userId = identityUser.Id, code = confirmCode }, protocol: Request.Scheme);
                    var mailDTO = new SendMailDTO();
                    mailDTO.Email = model.Email;
                    mailDTO.Subject = "Üyelik İşlemleri";
                    mailDTO.Message = $"Hesabınızın onaylanması için <a href='{url}'>link</a>e tıklayınız!..<br/> Şifreniz = {userPassword}";

                    await _mailService.SendAppMailAsync(mailDTO);
                    var userCreateDto = model.Adapt<AppUserCreateDTO>();
                    userCreateDto.IdentityId = identityUser.Id;
                    await _appUserService.CreateAsync(userCreateDto);
                   
                    Notify(_stringLocalizer["Üye işlemleriniz tamamlanması için mailinizi kontrol etmelisiniz"], notificationType: UI.Models.NotificationType.success);
                    return RedirectToAction("Login");
                }
            }
           
            Notify(_stringLocalizer["Üyelik işlemi yapılırken hata oluştu"], notificationType: UI.Models.NotificationType.error);
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, code);
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var captchaResult = await _googleCaptchaService.VerifyToken(model.Token);
            if (!captchaResult)
            {

                NotifyError(_stringLocalizer["Captcha authentication has failed!"]);
                return View(model);
            }
            if (ModelState.IsValid)
            {
                
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                {
                    NotifyError("Kullanıcı veya şifre hatalı");
                    return View(model);
                }
                var checkPassword = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (!checkPassword.Succeeded)
                {
                    NotifyError("Kullanıcı veya şifre hatalı");
                    return View(model);
                }
                var userRole = await _userManager.GetRolesAsync(user);
                if (userRole == null)
                {
                    NotifyError("Kullanıcı veya şifre hatalı");
                    return View(model);
                }
                Notify(_stringLocalizer["Welcome to the MelodyBurger!"], notificationType: NotificationType.success);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}

