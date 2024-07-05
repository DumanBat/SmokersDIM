using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

public class Startup
{
	public IConfiguration Configuration { get; }

	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddDbContext<DestinyDbContext>(options =>
			options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
			
		services.AddLogging();
		services.AddHttpClient();
		services.AddTransient<IBungieApiService, BungieApiService>();
		services.AddTransient<IEquipmentService, EquipmentService>();
		services.AddTransient<IDestinyManifectService, DestinyManifectService>();
        services.AddScoped<IDatabaseItemDataService, DatabaseItemDataService>();
        services.AddScoped<IProfileVaultService, ProfileVaultService>();
        services.AddScoped<IItemInstanceService, ItemInstanceService>();
		services.AddControllers();

		services.AddCors(options =>
		{
			options.AddPolicy("AllowSpecificOrigin",
				builder =>
				{
					builder.WithOrigins("https://localhost:5281")
						   .AllowAnyHeader()
						   .AllowAnyMethod()
						   .AllowCredentials();
				});
		});

		services.AddDistributedMemoryCache();
		services.AddSession(options =>
		{
			options.IdleTimeout = TimeSpan.FromMinutes(30);
			options.Cookie.HttpOnly = true;
			options.Cookie.IsEssential = true;
		});
		services.AddHttpContextAccessor();

		services.AddHttpsRedirection(options =>
		{
			options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
			options.HttpsPort = 5281;
		});

		services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/account/login";
				});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		
		app.UseHttpsRedirection();
		app.UseRouting();
		
		app.UseCors("AllowSpecificOrigin");

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseSession();
		app.UseStaticFiles();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
		app.Use(async (context, next) =>
		{
			if (context.Request.Path == "/")
			{
				context.Response.Redirect("/index.html");
				return;
			}

			await next();
		});
	}
}

