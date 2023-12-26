using BlogApp.Models;
using BUSINESS.Abstract;
using BUSINESS.Concrete;
using DAL.Context;
using DATA.Entities;
using Microsoft.AspNetCore.Authorization;
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
		private readonly ITagRepository _tagRepository;
		//private readonly ITagRepository _tagRepository; 
		public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
		{
			_postRepository = postRepository;
			_commentRepository = commentRepository;
			_tagRepository = tagRepository;
		}
		public async Task<IActionResult> Index(string tag)
		{
			var claims = User.Claims;
			var posts = _postRepository.Posts.Where(i => i.IsActive); //aktif olan postlar gelsin

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
			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""); //userid bilgisini aldik
			var userName = User.FindFirstValue(ClaimTypes.Name);         //username bilgisini aldik 
			var avatar = User.FindFirstValue(ClaimTypes.UserData);           //userin image bilgisi


			var comment = new Comment
			{
				PostId = postId,
				Text = text,
				CreatedTime = DateTime.Now,
				UserId = userId
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
		[Authorize]//login yapmayan user bu sayfaya giremesin, url kısmına posts/createpost yazınca default olarak account altına atıyor bunu degistirelim. program.cste
		public IActionResult CreatePost()
		{
			return View();
		}

		[HttpPost]
		[Authorize]
		public IActionResult CreatePost(PostCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				_postRepository.CreatePost(new Post
				{
					Title = model.Title,
					Content = model.Content,
					Url = model.Url,
					UserId = int.Parse(userId ?? ""),
					CreatedTime = DateTime.Now,
					Image = "blog.jpg",
					IsActive = false, //post eklenir eklenmez aktif olmasini istemiyorum
				});
				return RedirectToAction("Index");
			}
			return View(model);
		}
		public async Task<IActionResult> List()
		{
			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
			var userRole = User.FindFirstValue(ClaimTypes.Role);

			var posts = _postRepository.Posts;

			if (string.IsNullOrEmpty(userRole))
			{
				posts = posts.Where(i => i.UserId == userId); //user eger any bir role sahip degilse sadece kendi postlarını gorebilsin
			}

			return View(await posts.ToListAsync());
		}

		[Authorize]
		public IActionResult EditPost(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var post = _postRepository.Posts
				.Include(t=>t.Tags)
				.FirstOrDefault(i => i.Id == id);
			if (post == null)
			{
				return NotFound();
			}

			ViewBag.Tags = _tagRepository.Tags.ToList();

			var entity = new PostCreateViewModel
			{
				Title = post.Title,
				Content = post.Content,
				Description = post.Description,
				IsActive = post.IsActive,
				Url = post.Url,
				PostId = post.Id,
				Tags = post.Tags
			};
			return View(entity);
		}
		[Authorize]
		[HttpPost]
		public IActionResult EditPost(PostCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var entityToUpdate = new Post
				{
					Id = model.PostId,
					Title = model.Title,
					Description = model.Description,
					Content = model.Content,
					Url = model.Url,
				};
				if (User.FindFirstValue(ClaimTypes.Role) == "admin")
				{
					entityToUpdate.IsActive = model.IsActive; //postu editleyen kisi admin ise postu aktif etme yetkisine sahip olsun
				}
				_postRepository.EditPostAsync(entityToUpdate);
				return RedirectToAction("List");
			}
			return View(model);
		}
	}
}
/*
 Partial view bir controller tetiklemek zorunda ve calısmazsa hata fırlatır, ama viewcomponent tek tabanccadır kendi componenti vardır kendi kendine çalışır. calısmazsa hata vermez
 */