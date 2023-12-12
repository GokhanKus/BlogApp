using DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS.Abstract
{
	public interface ICommentRepository
	{
		IQueryable<Comment> Comments { get; }
		public void CreateComment(Comment comment);
	}
}
