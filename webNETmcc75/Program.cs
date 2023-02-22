using Microsoft.EntityFrameworkCore;
using webNETmcc75.Contexts;
using webNETmcc75.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

//configure dbcontext to sql server db
var connectionString = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));

//depedency injection
builder.Services.AddScoped<UniversityRepository>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
