using Asp.net_mini_project.Data;
using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conString = builder.Configuration.GetConnectionString("Default") ??
     throw new InvalidOperationException("Connection string 'Default'" +
    " not found.");




builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conString));


builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ISliderInfoService, SliderInfoService>();
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
