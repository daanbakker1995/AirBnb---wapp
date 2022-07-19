using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AirBnb.Data;
using AirBnb.Repository;
using AirBnb.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Airbnb2022Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AirBnbContext") ?? throw new InvalidOperationException("Connection string 'AirBnbContext' not found.")));

builder.Services.AddScoped<IListingsRepository, ListingsRepository>();

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
