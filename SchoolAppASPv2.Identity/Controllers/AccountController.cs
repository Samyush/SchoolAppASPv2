using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolAppASPv2.Identity.Models;
using SchoolAppASPv2.Identity.Models.AccountModels;
using SchoolAppASPv2.Identity.Services;

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
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            { 
            var user = await _loginService.FindByUserName(model.Email!);

                if (await _loginService.ValidateCredentials(user, model.Password!))
                {
                    var isInRole = await _userManager.IsInRoleAsync(user, "ADMIN");

                    if (!isInRole)
                    {
                        IdentityResult result = new();
                        result.Errors.Select(error => new IdentityError
                        {
                            Code = "405",
                            Description = "User not allowed to Login",
                        });

                        AddErrors(result);
                        return Ok(result);
                    }

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

                    return RedirectToLocal(returnUrl!);

                }
            }
            return Ok();
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
                    return RedirectToLocal(returnUrl!);
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
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
