using DATA.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class Comment : BaseEntity
	{
        public string? Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
		public int UserId { get; set; }
		public User User { get; set; } = null!;
	}
}
