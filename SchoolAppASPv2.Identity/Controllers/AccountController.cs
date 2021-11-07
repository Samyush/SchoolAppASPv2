using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SchoolAppASPv2.Identity.Models;
using SchoolAppASPv2.Identity.Models.AccountModels;
using SchoolAppASPv2.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILoginService<ApplicationUser> _loginService;
        private readonly IConfiguration _configuration;


        [HttpPost]
        [Route("try")]
        public IActionResult Try()
        {
            return Ok("Success");
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _loginService.FindByUserName(userLoginModel.Email);

                if(await _loginService.ValidateCredentials(user , userLoginModel.Password))
                {
                    //var isInRole = await _userManager.IsInRoleAsync(user, "ADMIN");

                    //if (!isInRole)

                   var tokenLifetime = _configuration.GetValue("TokenLifetimeMinutes", 120);

                    var props = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(tokenLifetime),
                        AllowRefresh = true,
                        RedirectUri = userLoginModel.ReturnUrl
                    };

                    if (userLoginModel.RememberMe)
                    {
                        var permanentTokenLifetime = _configuration.GetValue("PermanentTokenLifetimeDays", 365);

                        props.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(permanentTokenLifetime);
                        props.IsPersistent = true;
                    }

                    await _loginService.SignInAsync(user, props);

                    //Todo:: method below to be added
                    return RedirectToLocal(returnUrl);

                }
            }
            return Ok();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return null;
                //return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
