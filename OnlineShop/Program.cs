using OnlineShop.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<DatabaseContext, DatabaseContext>();

const string scheme = "onlineShop";

builder.Services.AddAuthentication(scheme).AddCookie(scheme, option =>
{
    option.LoginPath = "/Account/Login";
    option.AccessDeniedPath = "/Account/Login";
    option.ExpireTimeSpan = TimeSpan.FromDays(30);//not for host version
});

//builder.Services.ConfigureApplicationCookie(option =>
//{
//    option.LoginPath = "/Account/Login";
//    option.AccessDeniedPath = "/Account/Login";
//    option.ExpireTimeSpan = TimeSpan.FromDays(30);//not for host version
//});

var app = builder.Build();
app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

});

app.Run();
