using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    
    endpoints.MapControllerRoute(
        name: "forforget",
        pattern: "home/login/forget_password",
        defaults: new { Controller = "Home", actoin = "forgetPass" }
        );
});


app.Run();
