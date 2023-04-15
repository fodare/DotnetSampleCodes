using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebAuthExample.Models;
using WebAuthExample.Repositories;
using WebAuthExample.Services;

namespace WebAuthExample.Controllers
{
    public class HomeController : Controller
    {
        #region dependencies
        private readonly ILogger<HomeController> _logger;
        private readonly ISecretInterface _secretService;
        private readonly IUsersRepository _usersRepository;

        public HomeController(ILogger<HomeController> logger, ISecretInterface secretService,
            IUsersRepository usersRepository)
        {
            _logger = logger;
            _secretService = secretService;
            _usersRepository = usersRepository;
        }
        #endregion dependencies

        #region Get calls
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

        public IActionResult Denied()
        {
            ViewData["Message"] = "You are not authorized to view this page!. Please" +
                "contact your admin for access review.";
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            var userList = _usersRepository.GetUsers();
            if (userList.Count <= 0)
            {
                _usersRepository.SeedUserList();
                userList = _usersRepository.GetUsers();
            }
            return View(userList);
        }

        [Authorize]
        public IActionResult AddSecret()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion Get calls

        #region Post calls

        [Authorize(Roles = "Admin")]
        [HttpPost("/CreateUser")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewUser(User newUser)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "One or more input(s) is invalid!";
            }
            string result = _usersRepository.CreateUser(newUser);
            if (!string.Equals(result, "User added successfully!"))
            {
                ViewData["Error"] = result;
            }
            return RedirectToAction("Users");
        }

        [Authorize]
        [HttpPost("/newsecret")]
        [ValidateAntiForgeryToken]
        public IActionResult AddSecret(Secret newSecret)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Error"] = "One or more input is invalid!";
            }
            var secret = new Secret();
            secret.Id = newSecret.Id;
            secret.Creator = User.Identity.Name;
            secret.SercretName = newSecret.SercretName;
            secret.SercretValue = newSecret.SercretValue;
            secret.CreatedDate = DateTime.Now;
            var result = _secretService.CreateSecretasync(secret);
            if (string.Equals(result, "Secret added successfully!"))
            {
                ViewData["Error"] = result;
            }
            return RedirectToAction("Secret");
        }

        #endregion Post calls

        #region Login Logout
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate(string userName, string userPassword, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = _usersRepository.GetUser(userName, userPassword);
            if (user is null)
            {
                _usersRepository.SeedUserList();
                user = _usersRepository.GetUser(userName, userPassword);
            }
            if (user is not null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim("username", user.UserName));
                claims.Add(new Claim(ClaimTypes.Role, user.Role)); // Ideally you want to read this from the Database
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

        #endregion Login Logout

    }
}