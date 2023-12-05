using DATA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
	public class BlogContext : DbContext
	{
        public BlogContext(DbContextOptions<BlogContext> options):base(options)
        {
            
        }
        public DbSet<Comment> Comments => Set<Comment>(); 
		public DbSet<Post> Posts => Set<Post>();	//public DbSet<Post> Posts{ get; set; } bu 2 kullanım arasında bir fark yok?
		public DbSet<Tag> Tags => Set<Tag>();
		public DbSet<User> Users => Set<User>();

	}
}
/*
DbSet özelliği tanımladik ve bu özelliği otomatik olarak Entity Framework tarafından belirli bir veritabanı tablosuyla eşleştirdik
Bu özellik, DbContext sınıfının bir parçasıdır ve sıklıkla Entity Framework Code-First yaklaşımında kullanılır.
 */