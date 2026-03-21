using System.Text.Json.Serialization;
using AspNetCoreLiveMonitoring.Extensions;
using FarmManagement.BL;
using FarmManagement.DAL;
using FarmManagement.DAL.EF;
using FarmManagement.UI.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FarmManagementDbContextConnection") ?? throw new InvalidOperationException("Connection string 'FarmManagementDbContextConnection' not found.");;

// Add services to the container.


builder.Services.AddDbContext<FarmManagementDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlite(connectionString);
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FarmManagementDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.MaxFailedAccessAttempts = 3;
});

builder.Services.AddAuthorization();
// Fix api: authorization status codes
builder.Services.ConfigureApplicationCookie (cfg =>
{
    cfg.Events.OnRedirectToLogin += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments ("/api"))
        {
            ctx.Response.StatusCode = 401;
        }
        return Task.CompletedTask;
    };
    cfg.Events.OnRedirectToAccessDenied += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments ("/api"))
        {
            ctx.Response.StatusCode = 403;
        }
        return Task.CompletedTask;
    };
});


builder.Services.AddLiveMonitoring();


var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    bool executeDropDatabase = true;
    FarmManagementDbContext farmManagementDbContext = scope.ServiceProvider.GetService<FarmManagementDbContext>();
    bool isDbCreated = farmManagementDbContext.CreateDatabase(executeDropDatabase);
    if (isDbCreated)
    {
        var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
        
        var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
        
        IdentitySeeder identitySeeder = new IdentitySeeder(userManager, roleManager);
        identitySeeder.Seed();
        
        
        DataSeeder.Seed(farmManagementDbContext);
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAndMapLiveMonitoring();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

public partial class Program {}