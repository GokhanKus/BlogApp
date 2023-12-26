using BUSINESS.Abstract;
using DAL.Context;
using DATA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS.Concrete
{
	public class PostRepository : IPostRepository
	{
		private readonly BlogContext _context;

		public PostRepository(BlogContext context)
		{
			_context = context;
		}
		public IQueryable<Post> Posts => _context.Posts;

		public void CreatePost(Post post)
		{
			_context.Posts.Add(post);
			_context.SaveChanges();
		}

		//public async Task EditPostAsync(Post post)
		//{
		//	var entity = await _context.Posts.FirstOrDefaultAsync(i => i.Id == post.Id);
		//	if (entity != null)
		//	{
		//		entity.Title = post.Title;
		//		entity.Description = post.Description;
		//		entity.Content = post.Content;
		//		entity.Url = post.Url;
		//		entity.IsActive = post.IsActive;

		//		await _context.SaveChangesAsync();
		//	}
		//}

		public async Task EditPostAsync(Post post, int[] tagIds)
		{
			var entity = await _context.Posts.Include(t => t.Tags).FirstOrDefaultAsync(i => i.Id == post.Id);
			if (entity != null)
			{
				entity.Title = post.Title;
				entity.Description = post.Description;
				entity.Content = post.Content;
				entity.Url = post.Url;
				entity.IsActive = post.IsActive;

				entity.Tags = await _context.Tags.Where(tag => tagIds.Contains(tag.Id)).ToListAsync();
				await _context.SaveChangesAsync();
			}
		}
	}
}
