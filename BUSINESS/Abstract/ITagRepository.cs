using DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS.Abstract
{
	public interface ITagRepository
	{
		IQueryable<Tag> Tags { get; }
		public void CreateTag(Tag tag);
		
	}
}
