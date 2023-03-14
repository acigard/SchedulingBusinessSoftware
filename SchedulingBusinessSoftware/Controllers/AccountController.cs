using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Abstractions;
using SchedulingBusinessSoftware.Data.DbContexts;
using SchedulingBusinessSoftware.Entities;
using SchedulingBusinessSoftware.Models;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
//using SchedulingBusinessSoftware.Services;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace SchedulingBusinessSoftware.Controllers
{
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<ApplicationUser> _logger;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<ApplicationUser> userManager, AppDbContext context, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, ILogger<ApplicationUser> logger, IEmailSender emailSender)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            return user.UserName;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }


        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    HttpContext.Session.SetString("ssuserName", user.Email);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(model);
        }*/


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                /*var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));*/

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName and Password!");
                return View(); 
            }
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email

                };
                var getUser = await _userManager.GetUserIdAsync(user);

                if(getUser != null)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var administratorRole = new ApplicationRole("Candidate");

                        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
                        {
                            await _roleManager.CreateAsync(administratorRole);
                        }


                        await _userManager.AddToRolesAsync(user, new[] { administratorRole.Name });

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Appointment");

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                ModelState.AddModelError("", "User already exist!");

            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //change Password method 
        [HttpPost]
        public async Task ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var Identity = User.Identity as ClaimsIdentity;
                var Principle = new ClaimsPrincipal(Identity);

                var user = await _userManager.GetUserAsync(Principle);
                if (user == null)
                {
                    RedirectToAction("Login");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    View();
                }
                await _signInManager.RefreshSignInAsync(user);
                RedirectToAction("ChangePasswordConfirmation","Account" );
            }   
          
        }

        //Forgot password method
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult>ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Looking up user with email address {model.Email}...");

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = token }, Request.Scheme);

                    await _emailSender.SendEmailAsync("", "Password reset",
                        $"Please click this link to reset your password: {passwordResetLink}");

                    _logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, passwordResetLink);
                    return View("ForgotPasswordConfirmation");

                }
                return View("ForgotPasswordConfirmation");

            }
            else
            {
                _logger.LogInformation($"User with email address {model.Email} not found.");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            if(token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            ViewData["Token"] = token;
            ViewData["Email"] = email;
            return View();
        }
        [HttpPost]

        public async Task<IActionResult>ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User not found");
                }
            }
            return View(model);
        }
    }
}
