using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.WithOrigins("https://localhost:5281")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });

        services.AddControllers();

        services.AddHttpsRedirection(options =>
        {
            options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            options.HttpsPort = 5281;
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = "Bungie";
        })
        .AddCookie()
        .AddOAuth("Bungie", options =>
        {
            options.ClientId = Configuration["Bungie:ClientId"];
            options.ClientSecret = Configuration["Bungie:ClientSecret"];
            options.CallbackPath = new PathString("/signin-bungie");

            options.AuthorizationEndpoint = "https://www.bungie.net/en/oauth/authorize";
            options.TokenEndpoint = "https://www.bungie.net/platform/app/oauth/token/";

            options.SaveTokens = true;

            options.Events = new OAuthEvents
            {
                OnCreatingTicket = async context =>
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://www.bungie.net/Platform/User/GetMembershipsForCurrentUser/");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                    request.Headers.Add("X-API-Key", Configuration["Bungie:ApiKey"]);

                    var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                    response.EnsureSuccessStatusCode();

                    var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                    var userId = user.RootElement.GetProperty("Response").GetProperty("bungieNetUser").GetProperty("membershipId").GetString();

                    context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
                    context.Identity.AddClaim(new Claim(ClaimTypes.Name, userId));
                },
            };
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseCors("CorsPolicy");
        //app.UseCors(builder => builder.AllowAnyOrigin());

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
