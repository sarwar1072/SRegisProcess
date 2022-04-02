using Membership.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistrationProcess.web.Areas.Identities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationProcess.web.Areas.Identities.Controllers
{
    [AllowAnonymous]
    [Area("Identities")]
    public class LoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public LoginController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {

            _userManager = userManager;
            _signInManager = signInManager;

        }
        [BindProperty]

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> Index()
        {
            var model = new LoginModel();

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginModel Input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(Input);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(returnUrl);
        }
    }
}
