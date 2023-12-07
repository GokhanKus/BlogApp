using DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS.Abstract
{
	public interface IPostRepository
	{
		IQueryable<Post> Posts { get; }
		public void CreatePost(Post post);
		
	}
}
