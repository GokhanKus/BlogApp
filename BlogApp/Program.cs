using BUSINESS.Abstract;
using BUSINESS.Concrete;
using DAL.Context;
using DAL.DataSeeding;
using Microsoft.AspNetCore.Authentication.Cookies;
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

			builder.Services.AddAutoMapper(typeof(Program));    //BlogApp=>MappingProfile.cs

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			#region Injectionlar

			builder.Services.AddScoped<IPostRepository, PostRepository>();
			builder.Services.AddScoped<ITagRepository, TagRepository>();
			builder.Services.AddScoped<ICommentRepository, CommentRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();

			#endregion

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
			options.LoginPath = "/Users/Login"
			);


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
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllerRoute( //url'de detaylarda id yerine tanýmladigimiz url gelsin
				name: "post_details",
				pattern: "posts/details/{url}", //posts/details hep sabit url degisken, hangisiyle eslesirse o controller'a yonlendirir
				defaults: new { controller = "Posts", action = "Details" });

			app.MapControllerRoute(
				name: "posts_by_tag",
				pattern: "posts/tag/{tag}",
				defaults: new { controller = "Posts", action = "Index" });

			app.MapControllerRoute(
				name: "user_profile",
				pattern: "Users/Profile/{username}",
				defaults: new { controller = "Users", action = "Profile" });

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