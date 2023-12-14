using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
	public class UsersController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
	}
}
