using BUSINESS.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
	public class NewPosts:ViewComponent
	{
		private readonly IPostRepository _postRepository;
		public NewPosts(IPostRepository postRepository)
        {
			_postRepository = postRepository;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var model = await _postRepository
				.Posts
				.OrderByDescending(d => d.CreatedTime)
				.Take(5)
				.ToListAsync();

			return View(model);
		}
    }
}
