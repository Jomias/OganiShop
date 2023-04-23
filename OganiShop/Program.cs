using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using OganiShop.Helpers;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        o.JsonSerializerOptions.PropertyNamingPolicy = null;
    }); ;

var connectionString = builder.Configuration.GetConnectionString("OganiShopContext");
builder.Services.AddDbContext<OganiShopContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<IFileStorageService, InAppStorageService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/Admin/Account/Login";
});
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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.MapWhen(context => context.Request.Path.StartsWithSegments("/Admin/Account/Login") && context.Request.Method == "POST", appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        await next.Invoke();

        if (context.Response.StatusCode == 302 && context.Response.Headers["Location"].ToString().Contains("/Admin/Account/Login"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid login attempt.");
        }
    });
});

app.MapWhen(context => !context.Request.Path.StartsWithSegments("/Admin/Account/Login") || context.User.Identity.IsAuthenticated, appBuilder =>
{
    appBuilder.UseAuthentication();

    appBuilder.Use(async (context, next) =>
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var role = context.User.FindFirstValue(ClaimTypes.Role);
            if (role == "admin")
            {
                await next.Invoke();
            }
            else
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.Response.Redirect("/Admin/Account/Login");
            }
        }
        else
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            context.Response.Redirect("/Admin/Account/Login");
        }
    });
});
app.Run();
