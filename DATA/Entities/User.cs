﻿using DATA.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
	public class User : BaseEntity
	{
		public string? UserName { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Image { get; set; }
		public List<Post> Posts { get; set; } = new List<Post>();
		public List<Comment> Comments { get; set; } = new List<Comment>();
	}
}
