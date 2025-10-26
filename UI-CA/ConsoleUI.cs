using System.ComponentModel.DataAnnotations;
using CA.Extensions;
using FarmManagement.BL;
using FarmManagement.BL.Domain;
namespace FarmManagement.UI.CA;

public class ConsoleUI
{
    private readonly IManager _mgr;
    public ConsoleUI(IManager manager)
    {
        _mgr = manager;
    }
    public void Run()
    {
        bool running = true;

        Console.WriteLine("Welcome to the Farm Management Project");

        while (running)
        {
            ShowMenu();
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
                    FilterAnimals();
                    break;
                case 5:
                    AddFarm();
                    break;
                case 6:
                    AddAnimal();
                    break;
                default:
                    Console.WriteLine("Please enter a number between 0 and 6.");
                    break;
            }
        }
    }
    
    private void ShowAllFarms()
    {
        Console.WriteLine("All Farms");
        Console.WriteLine("=========");
        foreach (Farm farm in _mgr.GetAllFarms())
        {
            Console.WriteLine(farm.GetInfo());
        }

        Console.WriteLine();
    }

    private void FilterFarms()
    {
        Console.Write("Enter location: ");
        string location = Console.ReadLine();

        IEnumerable<Farm> filtered = _mgr.GetFarmsByLocation(location);

        if (!filtered.Any())
        {
            Console.WriteLine($"No farms found in {location}");
        }
        else
        {
            foreach (Farm farm in filtered)
            {
                Console.WriteLine(farm.GetInfo());
            }
        }

        Console.WriteLine();
    }

    private void ShowAllAnimals()
    {
        Console.WriteLine("All Animals");
        Console.WriteLine("===========");

        foreach (Animal animal in _mgr.GetAllAnimals())
        {
            Console.WriteLine(animal.GetInfo());
        }

        Console.WriteLine();
    }

    private void FilterAnimals()
    {
        Console.Write("Type: ");
        AnimalType[] types = ShowAnimalTypes();

        string typeInput = Console.ReadLine();

        AnimalType? typeResult = null;

        if (!string.IsNullOrEmpty(typeInput))
        {
            if (Enum.TryParse(typeInput, out AnimalType type) && Enum.IsDefined(typeof(AnimalType), type))
            {
                typeResult = type;
            }
            else
            {
                Console.WriteLine("Please enter a number between 1 and " + types.Length);
                Console.WriteLine();
                return;
            }
        }

        Console.Write("Enter minimum livespan or leave blank:");
        string lifespanInput = Console.ReadLine();

        int? minimumLifespan = null;

        if (!string.IsNullOrWhiteSpace(lifespanInput))
        {
            if (int.TryParse(lifespanInput, out int parsedLivespan))
            {
                minimumLifespan = parsedLivespan;
            }
            else
            {
                Console.WriteLine("Please enter a valid number for livespan.");
                Console.WriteLine();
                return;
            }
        }

        IEnumerable<Animal> filtered = _mgr.GetAnimalsByTypeAndLifespan(typeResult, minimumLifespan);

        if (!filtered.Any())
        {
            Console.WriteLine("No animals found with the given criteria.");
        }
        else
        {
            foreach (Animal animal in filtered)
            {
                Console.WriteLine(animal.GetInfo());
            }
        }

        Console.WriteLine();
    }

    private void AddFarm()
    {
        Console.WriteLine("Add Farm");
        Console.WriteLine("========");

        while (true)
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Location: ");
            string location = Console.ReadLine();
            
            Console.Write("Established year: ");
            string yearInput = Console.ReadLine();
            if (!int.TryParse(yearInput, out int year))
            {
                Console.WriteLine("Please enter a valid year.");
                continue;
            }

            Console.Write("Size in hectares (optional): ");
            string sizeInput = Console.ReadLine();
            double? sizeResult = null;
            if (!string.IsNullOrWhiteSpace(sizeInput))
            {
                if (double.TryParse(sizeInput, out double size))
                {
                    sizeResult = size;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number for size in hectares.");
                    continue;
                }
            }

            try
            {
                Farm newFarm = _mgr.AddFarm(name, location,year,sizeResult);
                Console.WriteLine("Farm added successfully.");
                break;
            }
            catch (ValidationException exception)
            {
                string [] errorMessages = exception.Message.Split("|");
                foreach (string errorMessage in errorMessages) 
                {
                    Console.WriteLine($"Error: {errorMessage}");
                    
                }
                Console.WriteLine("Please try again...");
            }
        }
        Console.WriteLine();
    }

    private void AddAnimal()
    {
        Console.WriteLine("Add animal");
        Console.WriteLine("========");


        while (true)
        {

            Console.Write("Species: ");
            string inputSpecies = Console.ReadLine();

            Console.Write("Lifespan (years): ");
            string inputLifespan = Console.ReadLine();
            if (!int.TryParse(inputLifespan, out int lifespan))
            {
                Console.WriteLine("Please enter a valid number for lifespan.");
                continue;
            }

            Console.Write("Average weight (kg): ");
            string inputAverageWeight = Console.ReadLine();

            if (!double.TryParse(inputAverageWeight, out double averageWeight))
            {
                Console.WriteLine("Please enter a valid number for average weight.");
                continue;
            }
            
            Console.Write("Type: ");
            AnimalType[] types = ShowAnimalTypes();

            string typeInput = Console.ReadLine();

            if (!Enum.TryParse(typeInput, out AnimalType type) || !Enum.IsDefined(typeof(AnimalType), type))
            {
                Console.WriteLine("Please enter a number between 1 and " + types.Length);
                continue;
            }
            
            AnimalType selectedType = type;
            
            try
            {
                Animal newAnimal = _mgr.AddAnimal(inputSpecies, lifespan, averageWeight, selectedType);
                Console.WriteLine("Animal added successfully.");
                break;
            }
            catch (ValidationException exception)
            {
                string[] errorMessages = exception.Message.Split("|");
                foreach (string errorMessage in errorMessages)
                {
                    Console.WriteLine($"Error: {errorMessage}");
                }
                Console.WriteLine("Please try again...");
            }
        }
    }
    
    private void ShowMenu()
    {
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("0) Quit");
        Console.WriteLine("1) Show all farms");
        Console.WriteLine("2) Show farms from location");
        Console.WriteLine("3) Show all animals");
        Console.WriteLine("4) Show all animals of type and/or minimum lifespan");
        Console.WriteLine("5) Add a Farm");
        Console.WriteLine("6) Add a Animal");

        Console.Write("Choice (0-6): ");
    }
    
    private AnimalType[] ShowAnimalTypes()
    {
        var types = Enum.GetValues<AnimalType>();
        for (int i = 0; i < types.Length; i++)
        {
            Console.Write($"{(int)types[i]}={types[i]}");
            if (i < types.Length - 1)
                Console.Write(", ");
        }
        Console.Write(": ");
        return types;
    }

}