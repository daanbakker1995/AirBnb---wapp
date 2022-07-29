using Microsoft.EntityFrameworkCore;
using AirBnb.Data;
using AirBnb.Repository;
using AirBnb.Repository.Interfaces;
using AirBnb.Service;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using StackExchange.Profiling.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

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


builder.Services.AddMiniProfiler(options =>
{
    options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
    options.PopupShowTimeWithChildren = true;
    options.Storage = new SqlServerStorage(builder.Configuration.GetConnectionString("MiniProfilerDatabase"));
}).AddEntityFramework();

// Performance action: Register Response  in DI container
builder.Services.AddResponseCompression();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddDbContext<AirbnbV2Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Airbnb_v2Context") ?? throw new InvalidOperationException("Connection string 'AirbnbV2Context' not found.")));

builder.Services.AddScoped<IListingsRepository, ListingsRepository>();
builder.Services.AddScoped<INeighbourhoodsRepository, NeighbourhoodsRepository>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

var app = builder.Build();

// Performance action: Add response compression to Middleware
app.UseResponseCompression();

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

if (app.Environment.IsDevelopment())
{
    app.UseMiniProfiler();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
