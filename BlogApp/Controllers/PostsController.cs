using BlogApp.Models;
using BUSINESS.Abstract;
using DAL.Context;
using DATA.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Security.Claims;

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

		private readonly IPostRepository _postRepository;
		private readonly ICommentRepository _commentRepository;
		//private readonly ITagRepository _tagRepository; 
		public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)
		{
			_postRepository = postRepository;
			_commentRepository = commentRepository;
		}
		public async Task<IActionResult> Index(string tag)
		{
			var claims = User.Claims;
			var posts = _postRepository.Posts;

			if (!string.IsNullOrEmpty(tag))
			{
				posts = posts.Where(x => x.Tags.Any(p => p.Url == tag));
			}


			var model = new PostsViewModel
			{
				Posts = await posts.ToListAsync()
				//Posts = _postRepository.Posts.ToList(),
				//Tags = _tagRepository.Tags.ToList() viewcomponent kullandık artık her seferinde tekrar tekrar aynı kodu yazmayacagiz
			};

			return View(model);

		}
		//kosulu henuz veritabanına gondermedik tolist() diyerek gondeririz.
		public async Task<IActionResult> Details(string? url) //detay sayfasına giderken url kısmı bizim belirledigimiz gibi olsun
		{
			var model = await _postRepository
				.Posts
				.Include(t => t.Tags)
				.Include(c => c.Comments)
				.ThenInclude(u => u.User) //then include diyerek commenttin icindeki yani commentten sonra usera gidelim dedik.
				.FirstOrDefaultAsync(p => p.Url == url);
			return View(model);
		}
		#region Ajax'dan once
		//public IActionResult AddComment(int postId, string userName, string text, string url)
		//{
		//	var comment = new Comment
		//	{
		//		Text = text,
		//		CreatedTime = DateTime.Now,
		//		PostId = postId,
		//		User = new User { UserName = userName, Image = "avatar.jpg" }
		//	};
		//	_commentRepository.CreateComment(comment);

		//	return Redirect("/posts/details/" + url); //2side calisir.
		//	//return RedirectToRoute("post_details", new { url = url });
		//}
		#endregion


		#region Ajax'li kullanim
		[HttpPost]
		public JsonResult AddComment(int postId, string text)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //userid bilgisini aldik
			var userName = User.FindFirstValue(ClaimTypes.Name);		 //username bilgisini aldik 
			var avatar = User.FindFirstValue(ClaimTypes.UserData);			 //userin image bilgisi


			var comment = new Comment
			{
				PostId = postId,
				Text = text,
				CreatedTime = DateTime.Now,
				UserId = int.Parse(userId??"")
			};
			_commentRepository.CreateComment(comment);

			return Json(new
			{
				userName,
				text,
				comment.CreatedTime,
				avatar
			});
		}
		#endregion

	}
}
/*
 Partial view bir controller tetiklemek zorunda ve calısmazsa hata fırlatır, ama viewcomponent tek tabanccadır kendi componenti vardır kendi kendine çalışır. calısmazsa hata vermez
 */