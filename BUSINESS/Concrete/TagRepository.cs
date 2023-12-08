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
	public class TagRepository : ITagRepository
	{
		private readonly BlogContext _context;

		public TagRepository(BlogContext context)
		{
			_context = context;
		}

		public IQueryable<Tag> Tags => _context.Tags;

		public void CreateTag(Tag tag)
		{
			_context.Tags.Add(tag);
			_context.SaveChanges();
		}
	}
}
