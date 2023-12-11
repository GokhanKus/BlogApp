using DAL.Context;
using DATA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
	public static class DataSeeding
	{
		public static void SeedData(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetService<BlogContext>();

			if (context != null)
			{
				if (context.Database.GetPendingMigrations().Any())//olusturulmus ancak uygulanmamis migration varsa;
				{
					context.Database.Migrate();//update-database yapılsın
				}

				if (!context.Tags.Any()) //dbde tags tablosunda hic kayit yoksa;
				{
					context.Tags.AddRange(
						new Tag { Text = "Web Programlama", Url="web-programlama"},
						new Tag { Text = "Front End" , Url = "front-end" },
						new Tag { Text = "Back End", Url = "back-end" },
						new Tag { Text = "Unity", Url = "unity" },
						new Tag { Text = "Unreal Engine" , Url = "unreal-engine" },
						new Tag { Text = "Game Development ", Url = "game-development" },
						new Tag { Text = "Yazilim",Url = "yazilim", Posts = context.Posts.ToList()}
					);
					context.SaveChanges();
				}

				if (!context.Users.Any())//dbde users tablosunda hic kayit yoksa;
				{
					context.Users.AddRange(
						new User { UserName = "GokhanKus", CreatedTime = DateTime.Now },
						new User { UserName = "AhmetYilmaz", CreatedTime = DateTime.Now.AddDays(-10) } //10 gun once kayit olmus olsun 
					);
					context.SaveChanges();
				}
				if (!context.Posts.Any())//dbde posts tablosunda hic kayit yoksa;
				{
					context.AddRange(
						new Post
						{
							Title = "Asp.net Core",
							Content = "AspNET core dersleri",
							Url = "aspnet-core",
							IsActive = true,
							Image = "1.jpg",
							CreatedTime = DateTime.Now.AddDays(-15), //15 gun once kayit edilmis olsun
							Tags = context.Tags.Take(3).ToList(), //bu post dbdeki tags tablosunun ilk 3 tage sahip olsun
							UserId = 1
						},
						new Post
						{
							Title = "PHP",
							Content = "PHP dersleri",
							Url="php",
							IsActive = true,
							Image = "2.jpg",
							CreatedTime = DateTime.Now.AddDays(-10), //15 gun once kayit edilmis olsun
							Tags = new List<Tag> { new Tag { Text = "Full-Stack", Url="full-stack"}, new Tag { Text = "Test Tag",Url="test-tag" } }, //bu post'a burada da tag ekleyebiliriz.
							UserId = 1 //userId'si 1 olan kisiye ait olsun bu post
						},
						new Post
						{
							Title = "Unreal Engine",
							Content = "Unreal Engine dersleri",
							Url ="unreal-engine",
							IsActive = true,
							Image = "3.jpg",
							CreatedTime = DateTime.Now,
							Tags = context.Tags.Skip(4).Take(2).ToList(), //bu post dbdeki tag tablosunun ilk 4'unu es gecip ondan sonraki 2 tane tage sahip olsun
							UserId = 2
						},
						new Post
						{
							Title = "React",
							Content = "React dersleri",
							Url ="react",
							IsActive = true,
							Image = "3.jpg",
							CreatedTime = DateTime.Now.AddDays(-45),
							Tags = context.Tags.Take(1).ToList(), //bu post dbdeki tag tablosunun ilk 4'unu es gecip ondan sonraki 2 tane tage sahip olsun
							UserId = 1
						},
						new Post
						{
							Title = "Django",
							Content = "Django dersleri",
							Url ="Django",
							IsActive = true,
							Image = "3.jpg",
							CreatedTime = DateTime.Now,
							Tags = context.Tags.Skip(4).Take(2).ToList(), //bu post dbdeki tag tablosunun ilk 4'unu es gecip ondan sonraki 2 tane tage sahip olsun
							UserId = 2
						},
						new Post
						{
							Title = "Angular",
							Content = "Angular dersleri",
							Url ="angular",
							IsActive = true,
							Image = "3.jpg",
							CreatedTime = DateTime.Now.AddDays(-60),
							Tags = context.Tags.Take(2).ToList(), //bu post dbdeki tag tablosunun ilk 4'unu es gecip ondan sonraki 2 tane tage sahip olsun
							UserId = 1
						}
					);
					context.SaveChanges();
				}

			}
		}

	}
}
