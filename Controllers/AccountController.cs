using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShadiWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShadiWebApplication.Controllers
{
    public class AccountController : BaseController
    {

        private IConfiguration configuration;


        public AccountController(ShadiWebApplication.Logger.ILogger logger, IConfiguration iConfig) : base(logger)
        {

            configuration = iConfig;

        }

        public IActionResult Login()
        {
            return View(new LoginViewModel() { ReturnUrl = string.Empty });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Logger.Info("Login action for Username: ", model.Username);
            try
            {

                if (ModelState.IsValid)
                {
                    //var result = await _signManager.PasswordSignInAsync(model.Username,
                    //   model.Password, model.RememberMe, false);
                    //var cnn = configuration.GetConnectionString("ReportSystemDbCnn");
                    //string strRes = string.Empty;
                    //var res = new UserRepository(cnn).IsUserValid(model, out strRes);
                    if (model.Username.ToUpper().Equals("shadi".ToUpper()))
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                    };

                        var userIdentity = new ClaimsIdentity(claims, "login");

                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(principal);

                        Logger.Info("Login Success for Username ", model.Username);

                        //Just redirect to our index after logging in. 
                        return RedirectToAction("Index", "Home");

                    }
                }
                Logger.Info("Login Invalid Password for Username ", model.Username);

                ModelState.AddModelError("", "نام کاربری یا رمز عبور نامعتبر می باشد");

            }
            catch (Exception exception)
            {
                Logger.Error("Login Exception: ", exception);
                ModelState.AddModelError("", "بروز خطای ناشناخته");

            }
            return View(model);
        }
        public async Task<IActionResult> LogOff()
        {

            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordInputModel();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var username = claimsIdentity.Claims.Where(c => c.Type.Equals(ClaimTypes.Name));
            if (username != null && username.Any())
            {
                model.Username = username.FirstOrDefault().Value;
            }
            return View(model);
        }
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> ChangePassword(ChangePasswordInputModel model)
        //{
        //    Logger.Info("ChangePassword action for Username: ", model.Username);
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var hasNumber = new Regex(@"[0-9]+");
        //            var hasUpperChar = new Regex(@"[A-Z]+");
        //            var hasMinimum8Chars = new Regex(@".{8,}");

        //            if (!model.NewPassword.Equals(model.ConfirmedNewPassword))
        //                ModelState.AddModelError("", "عدم تطابق رمز عبور جدید و تکرار رمز عبور ");

        //            var isValidated = hasNumber.IsMatch(model.ConfirmedNewPassword)
        //                && hasUpperChar.IsMatch(model.ConfirmedNewPassword)
        //                && hasMinimum8Chars.IsMatch(model.ConfirmedNewPassword);

        //            if (!isValidated)
        //                ModelState.AddModelError("", "رمز عبور باید حداقل 8 کاراکتر و شامل اعداد و حروف کوچک و بزرگ باشد ");
        //            else
        //            {
        //                //change password
        //                var cnn = configuration.GetConnectionString("ReportSystemDbCnn");
        //                string strRes = string.Empty;
        //                var res = new UserRepository(cnn).ChangePassword(model);
        //                if (res.Success)
        //                {
        //                    await HttpContext.SignOutAsync();
        //                    return RedirectToAction("Login");
        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("", res.ResultDescription);
        //                }
        //            }

        //        }

        //    }
        //    catch (Exception exception)
        //    {

        //        Logger.Error("ChangePassword Exception: ", exception);
        //        ModelState.AddModelError("", "بروز خطای ناشناخته");
        //    }

        //    return View(model);

        //}
    }
}
