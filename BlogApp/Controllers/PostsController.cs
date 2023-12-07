using BUSINESS.Abstract;
using DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
	public class PostsController : Controller
	{
		//biz her seferinde bu sekilde D.inject islemi yapmamıza gerek yok bir tane posta ait IRepository interfacesi olusturup onu injectleyebiliriz tek sefer yazmıs oluruz.
		#region KlasikInject

		//private readonly BlogContext _context;
		//public PostsController(BlogContext context)
		//{
		//	_context = context;
		//}
		#endregion
		
		private readonly IPostRepository _repository;
		public PostsController(IPostRepository repository)
        {
			_repository = repository;
		}
        public IActionResult Index()
		{
			var model = _repository.Posts.ToList();
			return View(model);
		}
	}
}
