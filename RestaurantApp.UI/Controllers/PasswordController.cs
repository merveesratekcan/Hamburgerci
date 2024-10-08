using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.Services.UserServices.EmailServices;
using RestaurantApp.UI.Models;

namespace RestaurantApp.UI.Controllers
{
    public class PasswordController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMailService _mailService;
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;

        public PasswordController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IMailService mailService, IStringLocalizer<ModelResource> stringLocalizer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mailService = mailService;
            _stringLocalizer = stringLocalizer;
        }

   
        public IActionResult ForgotPassword()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                //Notify(_stringLocalizer["False entry!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["False entry!"]);
                return View(model);

                //return RedirectToAction("ForgotPasswordConfirmation");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Password", new { token, email = user.Email }, protocol: HttpContext.Request.Scheme);

            await _mailService.SendMailAsync(
                model.Email,
                "Reset Password",
                $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
            //Notify(_stringLocalizer["Reset link sent to your email adress."], notificationType: NotificationType.success);
            NotifySuccess(_stringLocalizer["Reset link sent to your email adress."]);
            return RedirectToAction("ForgotPasswordConfirmation");
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordVM { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                Notify(_stringLocalizer["Your password has been changed successfully!"], notificationType: NotificationType.success);
                NotifySuccess(_stringLocalizer["Your password has been changed successfully!"]);

                return RedirectToAction("ResetPasswordConfirmation");
            }
            Notify(_stringLocalizer["Şifre kriterleri sağlanamadı!"], notificationType: NotificationType.error);
            NotifyError(_stringLocalizer["Şifre kriterleri sağlanamadı!"]);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }
       

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
