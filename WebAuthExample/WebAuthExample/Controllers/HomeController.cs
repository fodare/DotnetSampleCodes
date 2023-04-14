using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebAuthExample.Models;
using WebAuthExample.Services;

namespace WebAuthExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISecretInterface _secretService;

        public HomeController(ILogger<HomeController> logger, ISecretInterface secretService)
        {
            _logger = logger;
            _secretService = secretService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var result = await _secretService.GetSrecretsAsync();
            return View(result);
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate(string userName, string userPassword, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (userName == "test" && userPassword == "test") // Ideally you want a DB search for this.
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
                claims.Add(new Claim(ClaimTypes.Name, userName));
                claims.Add(new Claim("username", userName));
                claims.Add(new Claim(ClaimTypes.Role, "Admin")); // Ideally you want to read this from the Database
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);
            }
            TempData["Error"] = "Error. Invalid username or password.";
            return View("login");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}