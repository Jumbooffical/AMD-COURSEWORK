using URLShortener.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<URLDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
	name: "default",
	pattern: "{controller=URLs}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "delete",
    pattern: "{controller=URLs}/{action=Delete}/{id?}");

//app.MapControllerRoute(
//	name: "redirect",
//	pattern: "{controller =URLs}/{ action=Index}/{ id ?}",

//    defaults: new { controller = "URLs", action = "RedirectToOriginal" }
//);

app.MapDefaultControllerRoute();

app.Run();
