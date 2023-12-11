﻿using BlogApp.Models;
using BUSINESS.Abstract;
using DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		//private readonly ITagRepository _tagRepository; 
		public PostsController(IPostRepository postRepository)
        {
			_postRepository = postRepository;
		}
        public async Task<IActionResult> Index(string tag)
		{
			var posts = _postRepository.Posts;

			if (!string.IsNullOrEmpty(tag))
			{
				posts = posts.Where(x=>x.Tags.Any(p=>p.Url == tag));
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
			var model = await _postRepository.Posts.FirstOrDefaultAsync(p=>p.Url == url);
			return View(model);
		}
	}
}
/*
 Partial view bir controller tetiklemek zorunda, ama viewcomponent tek tabanccadır kendi componenti vardır kendi kendine çalışır.
 */