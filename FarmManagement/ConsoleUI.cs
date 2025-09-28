namespace FarmManagement;

public class ConsoleUI
{

    private readonly List<Farm> _farms = new List<Farm>();
    private readonly List<Animal> _animals = new List<Animal>();
    public void Run()
    {
        Seed();

        bool running = true;

        Console.WriteLine("Welcome to the Farm Management Project");

        while (running)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("0) Quit");
            Console.WriteLine("1) Show all farms");
            Console.WriteLine("2) Show farms from location");
            Console.WriteLine("3) Show all animals");
            Console.WriteLine("4) Show all animals with name and/or date of birth");

            Console.Write("Choice (0-4): ");
            string input = Console.ReadLine();
            Console.WriteLine();

            if (!Int32.TryParse(input, out int choice))
            {
                Console.WriteLine("Please enter a valid choice.");
                continue;
            }

            switch (choice)
            {
                case 0:
                    running = false;
                    break;
                case 1:
                    ShowAllFarms();
                    break;
                case 2: 
                   FilterFarms();
                    break;
                case 3:
                    ShowAllAnimals();
                    break;
                case 4:
                    break;
            }
        }
    }

    private void Seed()
    {
        
        // Farms
        Farm farm1 = new Farm("Sunny Meadow Farm", "Netherlands", 1995, 50);
        Farm farm2 = new Farm("Green Valley Estate", "Belgium", 2000, 74.2);
        Farm farm3 = new Farm("Old Oak Homestead", "Germany", 1980);
        Farm farm4 = new Farm("Riverbend Farmstead", "Netherlands", 2010, 60);

        // Adding to list
        _farms.AddRange(new Farm[] { farm1, farm2, farm3, farm4 }); 
        
        // Animals
        
        Animal animal1 = new Animal(new DateTime(2019,5,1), "Frank", 1090.3, AnimalType.Cow);
        Animal animal2 = new Animal(new DateTime(2020,3,12), "Eenhoorn", 532.12, AnimalType.Cow);
        Animal animal3 = new Animal(new DateTime(2018,7,23), "Jos", 320, AnimalType.Pig);
        Animal animal4 = new Animal(new DateTime(2021,1,30), "Yin", 0.6, AnimalType.Chicken);
        
        // Adding to list
        _animals.AddRange(new Animal[] { animal1, animal2, animal3, animal4 });

        animal1.Farms.Add(farm1); farm1.Animals.Add(animal1);
        animal1.Farms.Add(farm2); farm2.Animals.Add(animal1);

        animal2.Farms.Add(farm2); farm2.Animals.Add(animal2);
        animal2.Farms.Add(farm3); farm3.Animals.Add(animal2);

        animal3.Farms.Add(farm1); farm1.Animals.Add(animal3);
        animal3.Farms.Add(farm4); farm4.Animals.Add(animal3);

        animal4.Farms.Add(farm3); farm3.Animals.Add(animal4);
        animal4.Farms.Add(farm4); farm4.Animals.Add(animal4);
        
        
        farm1.Harvests.Add(new Harvest(CropType.Carrot, new DateTime(2019,5,1), 34000.12, farm1));
        farm1.Harvests.Add(new Harvest(CropType.Wheat, new DateTime(2020,8,15), 12000.50, farm1));
        farm1.Harvests.Add(new Harvest(CropType.Corn, new DateTime(2021,9,10), 15000.75, farm1));
        farm1.Harvests.Add(new Harvest(CropType.Potato, new DateTime(2022,7,20), 18000.00, farm1));

        farm2.Harvests.Add(new Harvest(CropType.Tomato, new DateTime(2019,6,12), 8000.25, farm2));
        farm2.Harvests.Add(new Harvest(CropType.Carrot, new DateTime(2020,5,30), 11000.50, farm2));
        farm2.Harvests.Add(new Harvest(CropType.Wheat, new DateTime(2021,8,5), 14000.00, farm2));
        farm2.Harvests.Add(new Harvest(CropType.Lettuce, new DateTime(2022,9,18), 9000.75, farm2));

        farm3.Harvests.Add(new Harvest(CropType.Lettuce, new DateTime(2019,7,22), 5000.50, farm3));
        farm3.Harvests.Add(new Harvest(CropType.Potato, new DateTime(2020,8,14), 13000.25, farm3));
        farm3.Harvests.Add(new Harvest(CropType.Wheat, new DateTime(2021,9,1), 7000.00, farm3));
        farm3.Harvests.Add(new Harvest(CropType.Corn, new DateTime(2022,7,30), 15000.80, farm3));

        farm4.Harvests.Add(new Harvest(CropType.Lettuce, new DateTime(2019,6,10), 9000.00, farm4));
        farm4.Harvests.Add(new Harvest(CropType.Wheat, new DateTime(2020,7,20), 10000.50, farm4));
        farm4.Harvests.Add(new Harvest(CropType.Carrot, new DateTime(2021,8,15), 12000.75, farm4));
        farm4.Harvests.Add(new Harvest(CropType.Tomato, new DateTime(2022,9,25), 8000.00, farm4));

        
        
        
        
    }

    private void ShowAllFarms()
    {
        Console.WriteLine("All Farms");
        Console.WriteLine("=========");
        foreach (Farm farm in _farms)
        {
            Console.WriteLine(farm);
        }

        Console.WriteLine();
    }
    
    private void FilterFarms()
    {
       List<Farm> filtered = new List<Farm>();
       Console.Write("Enter location: ");
       string location = Console.ReadLine();
       
       foreach (Farm farm in _farms)
       {
           if (farm.Location.IndexOf(location, StringComparison.OrdinalIgnoreCase) >= 0) // niet case sensitive
           {
               filtered.Add(farm);
           }
       }

       if (filtered.Count == 0)
       {
           Console.WriteLine("No farms found in {0}", location);
       }
       else
       {
           foreach (Farm farm in filtered)
           {
               Console.WriteLine(farm);
           }
       }

       Console.WriteLine();
    }

    private void ShowAllAnimals()
    {
        Console.WriteLine("All Animals");
        Console.WriteLine("===========");

        foreach (Animal animal in _animals)
        {
            Console.WriteLine(animal);
        }

        Console.WriteLine();
    }


    
}