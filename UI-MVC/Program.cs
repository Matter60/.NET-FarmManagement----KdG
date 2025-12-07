using AspNetCoreLiveMonitoring.Extensions;
using FarmManagement.BL;
using FarmManagement.DAL;
using FarmManagement.DAL.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<FarmManagementDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlite(@"Data Source=..\..\..\..\FarmManagementDb.sqlite");
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddLiveMonitoring();

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    bool executeDropDatabase = true;
    FarmManagementDbContext farmManagementDbContext = scope.ServiceProvider.GetService<FarmManagementDbContext>();
    if(executeDropDatabase)
        farmManagementDbContext.Database.EnsureDeleted();
    bool isDbCreated = farmManagementDbContext.Database.EnsureCreated();
    if (isDbCreated)
    {
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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();