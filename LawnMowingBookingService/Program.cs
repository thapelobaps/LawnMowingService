using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LawnMowingBookingService.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure the DbContext with SQL Server
builder.Services.AddDbContext<LawnMowingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<LawnMowingDbContext>()
        .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews(); // Add MVC support if necessary

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Add authentication
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
