using DATA.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class Tag : BaseEntity
	{
        public string? Text { get; set; }
        public string? Url{ get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
