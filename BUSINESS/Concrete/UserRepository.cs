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
	public class UserRepository : IUserRepository
	{
		private readonly BlogContext _context;

		public UserRepository(BlogContext context)
        {
			_context = context;
		}
		public IQueryable<User> Users => _context.Users;

		public void CreateUser(User user)
		{
			_context.Add(user);
			_context.SaveChanges();
		}
	}
}
