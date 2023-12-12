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
	public class CommentRepository : ICommentRepository
	{
		private readonly BlogContext _context;
        public CommentRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<Comment> Comments => _context.Comments;

		public void CreateComment(Comment comment)
		{
			_context.Comments.Add(comment);
			_context.SaveChanges();
		}
	}
}
