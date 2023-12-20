using BlogApp.Models;
using BUSINESS.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Security.Claims;

namespace BlogApp.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUserRepository _userRepository;

		public UsersController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		public IActionResult Login()
		{
			if (User.Identity!.IsAuthenticated) // kullanici giris yaptiysa login sayfasina erisemesin "!" diyerek null olmadigini belirtiyoruz
			{
				return RedirectToAction("Index", "Posts");
			}
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = _userRepository.Users.FirstOrDefault(i => i.Email == model.Email && i.Password == model.Password);

				if (user != null)
				{
					var userClaims=new List<Claim>();

					userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
					userClaims.Add(new Claim(ClaimTypes.Name, user.UserName ?? ""));
					userClaims.Add(new Claim(ClaimTypes.GivenName, user.Name ?? ""));

					if (user.Email == "info@gokhankus.com")
					{
						userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
					}
					var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
					var authProperties = new AuthenticationProperties
					{
						IsPersistent = true,
					};

					await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity),
						authProperties
						);

					return RedirectToAction("index", "posts");
				}
				else
				{
					ModelState.AddModelError("", "kullanici adi veya sifre yanlis");
				}
			}
				return View(model);
		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //logouttan sonra browser uzerindeki cookieyi siler
			return RedirectToAction("Login", "Users");
		}
	}
}
//authorization  :kısıtlamalar icin kullanılır sitenin belirli partlarına belirli rollerdeki kisilerin girebilmesi..
//authentication :uygulamaya giris yapmak, kimlik dogrulaması gibi islemler	