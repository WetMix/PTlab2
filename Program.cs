using Microsoft.EntityFrameworkCore;
using Shop.DAL;
using Shop.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlite("Data Source=shop.db"));

builder.Services.AddScoped<ShopService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShopContext>();
    ShopInitializer.Initialize(context);
}

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
//app.Urls.Add($"http://0.0.0.0:{port}");
app.Run();
