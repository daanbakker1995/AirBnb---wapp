using Microsoft.EntityFrameworkCore;
using AirBnb.Data;
using AirBnb.Repository;
using AirBnb.Repository.Interfaces;
using AirBnb.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddDbContext<Airbnb2022Context>(options =>
//        options.UseSqlServer(builder.Configuration.GetConnectionString("AirBnbContext") ?? throw new InvalidOperationException("Connection string 'AirBnbContext' not found.")));
builder.Services.AddDbContext<AirbnbV2Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Airbnb_v2Context") ?? throw new InvalidOperationException("Connection string 'AirbnbV2Context' not found.")));

builder.Services.AddScoped<IListingsRepository, ListingsRepository>();
builder.Services.AddScoped<INeighbourhoodsRepository, NeighbourhoodsRepository>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
