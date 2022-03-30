using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolAppASPv2.Identity.Models;
using SchoolAppASPv2.Identity.Models.AccountModels;
using SchoolAppASPv2.Identity.Services;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        public AccountController(ILoginService<ApplicationUser> loginService, IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        //the addition and removal of [ValidateAntiForgeryToken] gives
        //400 and
        //415 error code
        [Route("loginOld")]
        public async Task<IActionResult> Login(LoginModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            { 
            var user = await _loginService.FindByUserName(model.Email!);

                if (await _loginService.ValidateCredentials(user, model.Password!))
                {

                    var result = await _loginService.ValidateCredentials(user, model.Password!);

                    if(result == false)
                    {
                        return Ok(result);
                    }
                    //var isInRole = await _userManager.IsInRoleAsync(user, "ADMIN");

                    //if (!isInRole)
                    //{
                    //    IdentityResult result = new();
                    //    result.Errors.Select(error => new IdentityError
                    //    {
                    //        Code = "405",
                    //        Description = "User not allowed to Login",
                    //    });

                    //    AddErrors(result);
                    //    return Ok(result);
                    //}

                    var tokenLifetime = _configuration.GetValue("TokenLifetimeMinutes", 120);

                    var props = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(tokenLifetime),
                        AllowRefresh = true,
                        RedirectUri = model.ReturnUrl
                    };

                    if (model.RememberMe)
                    {
                        var permanentTokenLifetime = _configuration.GetValue("PermanentTokenLifetimeDays", 365);

                        props.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(permanentTokenLifetime);
                        props.IsPersistent = true;
                    };

                    await _loginService.SignInAsync(user, props);

                    return Ok(result);

                    //return RedirectToLocal(returnUrl!);

                }
            }
            return Ok(false);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }



                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public ActionResult Logout()
        {
            var userName = User?.Identity?.Name;

            //JWT.RemoveRefreshTokenByUserName(userName); // can be more specific to ip, user agent, device name, etc.
            //_logger.LogInformation($"User [{userName}] logged out the system.");
            return Ok(userName);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel model, string? returnUrl = null)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    LastName = model.User!.LastName,
                    Name = model.User.Name,
                    PhoneNumber = model.User.PhoneNumber,

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(result);
                    //return RedirectToLocal(returnUrl!);
                }
                AddErrors(result);
                return Ok(result);
            }
            return Ok(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        
    }
}
