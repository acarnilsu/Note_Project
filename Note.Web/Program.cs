using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Note.Web.Areas.Manager.Claims;
using Note.BusinessLayer.BusinessRegistration;
using Note.DataAccessLayer.Concrete;
using Note.EntityLayer.Concrete;
using Note.Web.CookieConfiguration;
using Note.Web.Mapping;
using Note.Web.Moduls;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddBusinessRegistration();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AnkaraPolicy", policy =>
    {
        policy.RequireClaim("City", "Ankara");
    });
});

builder.Services.AddCookiest();


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new ServiceDalModul()));

builder.Services.AddScoped<IClaimsTransformation, ClaimsProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Error/Error{0}");


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();


app.UseRouting();

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
