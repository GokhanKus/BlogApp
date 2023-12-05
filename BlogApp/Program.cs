using DAL.Context;
using DAL.DataSeeding;
using Microsoft.EntityFrameworkCore;

namespace BlogApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region DataBaseBaglanti

			var connectionString = builder.Configuration.GetConnectionString("sqLiteConnection");
			builder.Services.AddDbContext<BlogContext>(options=>options.UseSqlite(connectionString));

			#endregion

			// Add services to the container.
			builder.Services.AddControllersWithViews();


			var app = builder.Build();

			#region DataSeeding

			DataSeeding.SeedData(app.Services);

			#endregion

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
/*
repository pattern
Url parametreleri Farkl� routing patternler
yorum ekleme
ajax request
authentication & authorization
many to many relations
 */