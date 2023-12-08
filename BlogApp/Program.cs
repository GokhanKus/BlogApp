using BUSINESS.Abstract;
using BUSINESS.Concrete;
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

			var connectionString = builder.Configuration.GetConnectionString("sqLiteConnection"); //sqlite icin baaglanti
			builder.Services.AddDbContext<BlogContext>(options => options.UseSqlite(connectionString));

			//var version = new MySqlServerVersion(new Version(8, 0, 35));
			//var connectionString = builder.Configuration.GetConnectionString("mySqlConnection"); //mysql icin baglanti
			//builder.Services.AddDbContext<BlogContext>(options => options.UseMySql(connectionString,version));

			#endregion

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			#region Injectionlar

			builder.Services.AddScoped<IPostRepository, PostRepository>();
			builder.Services.AddScoped<ITagRepository, TagRepository>();

			#endregion

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

			//app.MapDefaultControllerRoute();

			app.Run();
		}
	}
}
/*
repository pattern
Url parametreleri Farklý routing patternler
yorum ekleme
ajax request
authentication & authorization
many to many relations
 */