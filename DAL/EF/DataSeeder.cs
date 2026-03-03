using FarmManagement.BL.Domain;

namespace FarmManagement.DAL.EF;

public static class DataSeeder
{
    public static void Seed(FarmManagementDbContext farmManagementDbContext)
    {
        var userLars = farmManagementDbContext.Users.Single(u => u.Email == "lars@kdg.be");
        var userAnna = farmManagementDbContext.Users.Single(u => u.Email == "anna@kdg.be");
        var userBob = farmManagementDbContext.Users.Single(u => u.Email == "bob@kdg.be");
        var userChef = farmManagementDbContext.Users.Single(u => u.Email == "chef@kdg.be");
        // Farms
        Farm sunnyMeadowFarm = new Farm("Sunny Meadow Farm", "Netherlands", 1995, userLars, 50);
        Farm greenValleyEstate = new Farm("Green Valley Estate", "Belgium", 2000, userAnna, 74.2);
        Farm oldOakHomestead = new Farm("Old Oak Homestead", "Germany", 1980, userBob);
        Farm riverbendFarmstead = new Farm("Riverbend Farmstead", "Netherlands", 2010, userChef, 60);


        // Adding to list
        farmManagementDbContext.Farms.AddRange(
            sunnyMeadowFarm, greenValleyEstate, oldOakHomestead, riverbendFarmstead );
        // Animals
        Animal holsteinCow = new Animal("Holstein", 10, 500, AnimalType.Cow);
        Animal jerseyCow = new Animal("Jersey", 12, 520, AnimalType.Cow);
        Animal angusCow = new Animal("Angus", 11, 540, AnimalType.Cow);

        Animal berkshirePig = new Animal("Berkshire", 5, 200, AnimalType.Pig);
        Animal largeWhitePig = new Animal("Large White", 6, 250, AnimalType.Pig);
        Animal durocPig = new Animal("Duroc", 4, 220, AnimalType.Pig);

        Animal silkieChicken = new Animal("Silkie", 2, 1.5, AnimalType.Chicken);
        Animal leghornChicken = new Animal("Leghorn", 3, 1.8, AnimalType.Chicken);
        Animal rhodeIslandChicken = new Animal("Rhode Island", 2, 2, AnimalType.Chicken);

        Animal merinoSheep = new Animal("Merino", 8, 70, AnimalType.Sheep);
        Animal suffolkSheep = new Animal("Suffolk", 9, 75, AnimalType.Sheep);

        Animal alpineGoat = new Animal("Alpine", 9, 65, AnimalType.Goat);
        Animal boerGoat = new Animal("Boer", 8, 68, AnimalType.Goat);

        Animal friesianHorse = new Animal("Friesian", 15, 400, AnimalType.Horse);
        Animal arabianHorse = new Animal("Arabian", 12, 380, AnimalType.Horse);
        Animal thoroughbredHorse = new Animal("Thoroughbred", 14, 420, AnimalType.Horse);


        farmManagementDbContext.Animals.AddRange(
        
            holsteinCow, jerseyCow, angusCow, berkshirePig, largeWhitePig, durocPig, silkieChicken, leghornChicken,
            rhodeIslandChicken, merinoSheep, suffolkSheep, alpineGoat, boerGoat, friesianHorse, arabianHorse,
            thoroughbredHorse
        );


        var farmAnimals = new List<FarmAnimal>
        {
            new() { Farm = sunnyMeadowFarm, Animal = holsteinCow, Count = 5 },
            new() { Farm = sunnyMeadowFarm, Animal = jerseyCow, Count = 3 },

            new() { Farm = greenValleyEstate, Animal = jerseyCow, Count = 2 },
            new() { Farm = greenValleyEstate, Animal = angusCow, Count = 4 },

            new() { Farm = sunnyMeadowFarm, Animal = berkshirePig, Count = 6 },
            new() { Farm = greenValleyEstate, Animal = largeWhitePig, Count = 8 },
            new() { Farm = oldOakHomestead, Animal = largeWhitePig, Count = 9 },
            new() { Farm = oldOakHomestead, Animal = durocPig, Count = 3 },

            new() { Farm = oldOakHomestead, Animal = silkieChicken, Count = 30 },
            new() { Farm = riverbendFarmstead, Animal = leghornChicken, Count = 25 },
            new() { Farm = greenValleyEstate, Animal = rhodeIslandChicken, Count = 20 },

            new() { Farm = greenValleyEstate, Animal = merinoSheep, Count = 12 },
            new() { Farm = riverbendFarmstead, Animal = suffolkSheep, Count = 10 },

            new() { Farm = sunnyMeadowFarm, Animal = alpineGoat, Count = 7 },
            new() { Farm = oldOakHomestead, Animal = boerGoat, Count = 5 },

            new() { Farm = riverbendFarmstead, Animal = friesianHorse, Count = 2 },
            new() { Farm = greenValleyEstate, Animal = arabianHorse, Count = 1 },
            new() { Farm = sunnyMeadowFarm, Animal = thoroughbredHorse, Count = 1 }
        };
        
        farmManagementDbContext.FarmAnimals.AddRange(farmAnimals);


        // harvests

// Sunny Meadow Farm Harvests
        var carrotHarvestSunny = new Harvest(CropType.Carrot, new DateOnly(2019, 4, 15), 28000.75, sunnyMeadowFarm);
        var wheatHarvestSunny = new Harvest(CropType.Wheat, new DateOnly(2020, 7, 18), 13500.40, sunnyMeadowFarm);
        var cornHarvestSunny = new Harvest(CropType.Corn, new DateOnly(2021, 6, 5), 4500.20, sunnyMeadowFarm);

// Green Valley Estate Harvests
        var tomatoHarvestGreen = new Harvest(CropType.Tomato, new DateOnly(2019, 6, 30), 9200.00, greenValleyEstate);
        var carrotHarvestGreen = new Harvest(CropType.Carrot, new DateOnly(2020, 8, 25), 11000.90, greenValleyEstate);
        var lettuceHarvestGreen = new Harvest(CropType.Lettuce, new DateOnly(2021, 9, 10), 8800.60, greenValleyEstate);
        var wheatHarvestGreen = new Harvest(CropType.Wheat, new DateOnly(2022, 10, 12), 7000.30, greenValleyEstate);
        var potatoHarvestGreen = new Harvest(CropType.Potato, new DateOnly(2022, 9, 5), 12000.00, greenValleyEstate);

// Old Oak Homestead Harvests
        var lettuceHarvestOldOak = new Harvest(CropType.Lettuce, new DateOnly(2019, 5, 22), 6700.80, oldOakHomestead);
        var potatoHarvestOldOak = new Harvest(CropType.Potato, new DateOnly(2020, 10, 14), 14500.25, oldOakHomestead);
        var cornHarvestOldOak = new Harvest(CropType.Corn, new DateOnly(2021, 9, 2), 16000.00, oldOakHomestead);
        var wheatHarvestOldOak = new Harvest(CropType.Wheat, new DateOnly(2022, 7, 28), 9300.55, oldOakHomestead);

// Riverbend Farmstead Harvests
        var lettuceHarvestRiverbend =
            new Harvest(CropType.Lettuce, new DateOnly(2019, 6, 12), 8700.40, riverbendFarmstead);
        var appleHarvestRiverbend =
            new Harvest(CropType.Apple, new DateOnly(2020, 9, 21), 12500.75, riverbendFarmstead);
        var tomatoHarvestRiverbend =
            new Harvest(CropType.Tomato, new DateOnly(2021, 10, 5), 5800.10, riverbendFarmstead);


        farmManagementDbContext.Harvests.AddRange(
            carrotHarvestSunny, wheatHarvestSunny, cornHarvestSunny,
            tomatoHarvestGreen, carrotHarvestGreen, lettuceHarvestGreen, wheatHarvestGreen, potatoHarvestGreen,
            lettuceHarvestOldOak, potatoHarvestOldOak, cornHarvestOldOak, wheatHarvestOldOak,
            lettuceHarvestRiverbend, appleHarvestRiverbend, tomatoHarvestRiverbend
        );


        farmManagementDbContext.SaveChanges();
        farmManagementDbContext.ChangeTracker.Clear();
    }
}