using BUSINESS.Abstract;
using DAL.Context;
using DATA.Entities;
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
	}
}
