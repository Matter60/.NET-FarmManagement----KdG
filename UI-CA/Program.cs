using FarmManagement.BL;
using FarmManagement.DAL;
using FarmManagement.DAL.EF;
using FarmManagement.UI.CA;
using Microsoft.EntityFrameworkCore;

string connectionString = @"Data Source=..\..\..\..\FarmManagementDb.sqlite";

//DummyDataSeeder seeder = new DummyDataSeeder();
//seeder.Seed();

bool executeDropDatabase = true;

//IRepository repository = new InMemoryRepository();
DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseSqlite(connectionString);
FarmManagementDbContext farmManagementDbContext = new FarmManagementDbContext(optionsBuilder.Options);

bool isDbCreated = farmManagementDbContext.CreateDatabase(executeDropDatabase);
if (isDbCreated)
{
    DataSeeder.Seed(farmManagementDbContext);
}
IRepository repository = new Repository(farmManagementDbContext);

IManager manager = new Manager(repository);
ConsoleUI consoleUi = new ConsoleUI(manager);
consoleUi.Run();    