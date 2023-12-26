using BlogApp.Models;
using BUSINESS.Abstract;
using DATA.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userRepository.Users.FirstOrDefaultAsync(i => i.UserName == model.UserName || i.Email == model.Email);
				if (user == null)
				{
					_userRepository.CreateUser(new User
					{
						Email = model.Email,
						Name = model.Name,
						UserName = model.UserName,
						Password = model.Password,
						Image = "avatar.jpg"
					});
					return RedirectToAction("Login", "users");
				}
				else
				{
					ModelState.AddModelError("", "email, ya da username kullanimda");
				}
			}
			return View(model);
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
					var userClaims = new List<Claim>();

					userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
					userClaims.Add(new Claim(ClaimTypes.Name, user.UserName ?? ""));
					userClaims.Add(new Claim(ClaimTypes.GivenName, user.Name ?? ""));
					userClaims.Add(new Claim(ClaimTypes.UserData, user.Image ?? "")); //image bilgisi

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
		public IActionResult Profile(string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				return NotFound(); //username bulunamadiysa 404 gonderelim
			}
			var user = _userRepository.Users
				.Include(p => p.Posts) //user'a ait olan postlar
				.Include(c => c.Comments)
				.ThenInclude(p => p.Post) //yapılan yorumun hangi posta ait oldugu 
				.FirstOrDefault(u => u.UserName == username);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}
	}
}
//authorization  :kısıtlamalar icin kullanılır sitenin belirli partlarına belirli rollerdeki kisilerin girebilmesi..
//authentication :uygulamaya giris yapmak, kimlik dogrulaması gibi islemler	