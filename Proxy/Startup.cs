using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging();
        services.AddControllers();
        services.AddHttpClient();
        services.AddSingleton<ITokenService, TokenService>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins(UrlConstants.MainAppOrigin)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddHttpsRedirection(options =>
        {
            options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            options.HttpsPort = 5099;
        });

        _ = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = "Bungie";
        })
        .AddOAuth("Bungie", options =>
        {
            options.ClientId = Configuration["Bungie:ClientId"];
            options.ClientSecret = Configuration["Bungie:ClientSecret"];
            options.CallbackPath = new PathString("/signin-bungie");

            options.AuthorizationEndpoint = UrlConstants.BungieAuthorizationEndpoint;
            options.TokenEndpoint = UrlConstants.BungieTokenEndpoint;;
            options.SaveTokens = true;

            options.Events = new OAuthEvents
            {
                OnCreatingTicket = async context =>
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, UrlConstants.BungieGetMembershipsForCurrentUser);
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                    request.Headers.Add("X-API-Key", Configuration["Bungie:ApiKey"]);

                    var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                    response.EnsureSuccessStatusCode();

                    var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                    var userId = user.RootElement.GetProperty("Response").GetProperty("bungieNetUser").GetProperty("membershipId").GetString();

                    context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
                    context.Identity.AddClaim(new Claim(ClaimTypes.Name, userId));

                    var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                    tokenService.AccessToken = context.AccessToken;
                }
            };
        })
        .AddCookie();
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

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect(UrlConstants.MainAppHome);
                return;
            }

            await next();
        });
    }
}
