using DATA.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class Post : BaseEntity
	{
		public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public string? Url{ get; set; }
        public bool IsActive { get; set; }
        public string? Image { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
