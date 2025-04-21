using Asp.net_mini_project.Data;
using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conString = builder.Configuration.GetConnectionString("Default") ??
    throw new InvalidOperationException("Connection string 'Default' not found.");

// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conString));

// Register services
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ISliderInfoService, SliderInfoService>();
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<INewsletterService, NewsletterService>(); // Add NewsletterService

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Area routing configuration
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
