using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchedulingBusinessSoftware.Data.DbContexts;
using SchedulingBusinessSoftware.Entities;
using SchedulingBusinessSoftware.Interfaces;
using SchedulingBusinessSoftware.Services;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SchedulingBusinessSoftware.Data.Seeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<AppointmentSeeader>();
builder.Services.AddConfigurationService(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/Account/Login");

//session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(59);
});

// Add services to the container.
builder.Services.AddControllersWithViews();
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
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseSession(); //session
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LogIn}/{id?}");

app.Run();
