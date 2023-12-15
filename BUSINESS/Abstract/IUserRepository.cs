using DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS.Abstract
{
	public interface IUserRepository
	{
		IQueryable<User> Users { get; }
		public void CreateUser(User user);
	}
}
