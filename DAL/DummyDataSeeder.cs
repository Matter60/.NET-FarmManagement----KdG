using FarmManagement.BL.Domain;
namespace FarmManagement.DAL;

public class DummyDataSeeder
{
    public void Seed()
    {
        // Farms
        Farm sunnyMeadowFarm = new Farm("Sunny Meadow Farm", "Netherlands", 1995, 50);
        Farm greenValleyEstate = new Farm("Green Valley Estate", "Belgium", 2000, 74.2);
        Farm oldOakHomestead = new Farm("Old Oak Homestead", "Germany", 1980);
        Farm riverbendFarmstead = new Farm("Riverbend Farmstead", "Netherlands", 2010, 60);
        

        // Adding to list
        InMemoryRepository.Farms.AddRange(new Farm[] { sunnyMeadowFarm, greenValleyEstate, oldOakHomestead, riverbendFarmstead });
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



        InMemoryRepository.Animals.AddRange(new Animal[]
        {
            holsteinCow, jerseyCow, angusCow, berkshirePig, largeWhitePig, durocPig, silkieChicken, leghornChicken, rhodeIslandChicken, merinoSheep, suffolkSheep, alpineGoat, boerGoat, friesianHorse, arabianHorse, thoroughbredHorse
        });


       // Linking
        /*holsteinCow.ConnectToFarm(sunnyMeadowFarm);
        jerseyCow.ConnectToFarm(sunnyMeadowFarm); 
        jerseyCow.ConnectToFarm(greenValleyEstate);
        angusCow.ConnectToFarm(greenValleyEstate);

        berkshirePig.ConnectToFarm(sunnyMeadowFarm);
        largeWhitePig.ConnectToFarm(greenValleyEstate); 
        largeWhitePig.ConnectToFarm(oldOakHomestead);
        durocPig.ConnectToFarm(oldOakHomestead);

        silkieChicken.ConnectToFarm(oldOakHomestead);
        leghornChicken.ConnectToFarm(riverbendFarmstead);
        rhodeIslandChicken.ConnectToFarm(greenValleyEstate);

        merinoSheep.ConnectToFarm(greenValleyEstate);
        suffolkSheep.ConnectToFarm(riverbendFarmstead);

        alpineGoat.ConnectToFarm(sunnyMeadowFarm);
        boerGoat.ConnectToFarm(oldOakHomestead);

        friesianHorse.ConnectToFarm(riverbendFarmstead);
        arabianHorse.ConnectToFarm(greenValleyEstate);
        thoroughbredHorse.ConnectToFarm(sunnyMeadowFarm);*/


        // harvests
      
        sunnyMeadowFarm.Harvests.Add(new Harvest(CropType.Carrot, new DateOnly(2019, 4, 15), 28000.75, sunnyMeadowFarm));
        sunnyMeadowFarm.Harvests.Add(new Harvest(CropType.Wheat, new DateOnly(2020, 7, 18), 13500.40, sunnyMeadowFarm));
        sunnyMeadowFarm.Harvests.Add(new Harvest(CropType.Corn, new DateOnly(2021, 6, 5), 4500.20, sunnyMeadowFarm));


        greenValleyEstate.Harvests.Add(new Harvest(CropType.Tomato, new DateOnly(2019, 6, 30), 9200.00, greenValleyEstate));
        greenValleyEstate.Harvests.Add(new Harvest(CropType.Carrot, new DateOnly(2020, 8, 25), 11000.90, greenValleyEstate));
        greenValleyEstate.Harvests.Add(new Harvest(CropType.Lettuce, new DateOnly(2021, 9, 10), 8800.60, greenValleyEstate));
        greenValleyEstate.Harvests.Add(new Harvest(CropType.Wheat, new DateOnly(2022, 10, 12), 7000.30, greenValleyEstate));
        greenValleyEstate.Harvests.Add(new Harvest(CropType.Potato, new DateOnly(2022, 9, 5), 12000.00, greenValleyEstate));

        oldOakHomestead.Harvests.Add(new Harvest(CropType.Lettuce, new DateOnly(2019, 5, 22), 6700.80, oldOakHomestead));
        oldOakHomestead.Harvests.Add(new Harvest(CropType.Potato, new DateOnly(2020, 10, 14), 14500.25, oldOakHomestead));
        oldOakHomestead.Harvests.Add(new Harvest(CropType.Corn, new DateOnly(2021, 9, 2), 16000.00, oldOakHomestead));
        oldOakHomestead.Harvests.Add(new Harvest(CropType.Wheat, new DateOnly(2022, 7, 28), 9300.55, oldOakHomestead));
        
        riverbendFarmstead.Harvests.Add(new Harvest(CropType.Lettuce, new DateOnly(2019, 6, 12), 8700.40, riverbendFarmstead));
        riverbendFarmstead.Harvests.Add(new Harvest(CropType.Apple, new DateOnly(2020, 9, 21), 12500.75, riverbendFarmstead));
        riverbendFarmstead.Harvests.Add(new Harvest(CropType.Tomato, new DateOnly(2021, 10, 5), 5800.10, riverbendFarmstead));

    }
}