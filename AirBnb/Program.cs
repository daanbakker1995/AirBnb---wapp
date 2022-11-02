using Microsoft.EntityFrameworkCore;
using AirBnb.Data;
using AirBnb.Repository;
using AirBnb.Repository.Interfaces;
using AirBnb.Service;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using StackExchange.Profiling.Storage;
using AirBnb.Models;

var builder = WebApplication.CreateBuilder(args);

// AUTHENTICATION
// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));
builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to 
    // the default policy
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToPage("/Index");
}).AddMvcOptions(Options => { }).AddMicrosoftIdentityUI();

// MINIPROFILER
builder.Services.AddMiniProfiler(options =>
{
    options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
    options.PopupShowTimeWithChildren = true;
    options.Storage = new SqlServerStorage(builder.Configuration.GetConnectionString("MiniProfilerDatabase"));
}).AddEntityFramework();

// OPTIMALISATION
// Performance action: Register Response  in DI container
builder.Services.AddResponseCompression();
builder.Services.AddResponseCaching();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "SampleInstance";
});
// ADD CONTROLLERS
builder.Services.AddControllers();
// DATABASE
builder.Services.AddDbContext<AirbnbV2Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Airbnb_v2Context") ?? throw new InvalidOperationException("Connection string 'AirbnbV2Context' not found.")));
// DEPENDENCY INJECTIONS
builder.Services.AddScoped<IListingsRepository, ListingsRepository>();
builder.Services.AddScoped<INeighbourhoodsRepository, NeighbourhoodsRepository>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

// OTPTIONS PATTERN Options pattern in ASP.NET Core: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0
builder.Services.AddOptions();
builder.Services.Configure<MapBoxSettings>(
    builder.Configuration.GetSection("Mapbox"));

var app = builder.Build();

// Performance action: Add response compression to Middleware
app.UseResponseCompression();
app.UseResponseCaching();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// USE MINIPROFILER
if (app.Environment.IsDevelopment())
{
    app.UseMiniProfiler();
}


// OWASP RESPONSE HEADERS
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:7276");
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Remove("X-Powered-By");
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' airbnb20220801163836.azurewebsites.net; " +
        "script-src 'self' 'unsafe-eval' 'unsafe-inline' api.mapbox.com cdnjs.cloudflare.com airbnb20220801163836.azurewebsites.net;" +
        "style-src 'self' 'unsafe-inline' api.mapbox.com ;" +
        "img-src 'self' data:;" +
        "worker-src 'self' blob:;" +
        "connect-src 'self' api.mapbox.com events.mapbox.com;");
    await next();
});


app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
