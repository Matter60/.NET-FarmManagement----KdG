using FarmManagement.BL.Domain;
namespace FarmManagement.DAL;

public class InMemoryRepository : IRepository
{
    internal static List<Farm> Farms { get; }= new();
    internal static List<Animal> Animals { get; } = new();

    public Farm ReadFarm(int id)
    {
        foreach (var farm in Farms)
        {
            if (farm.Id == id)
            {
                return farm;
            }
        }

        return null;
    }

    public IEnumerable<Farm> ReadAllFarms()
    {
        return Farms;
    }


    public IEnumerable<Farm> ReadFarmsByLocation(string location)
    {
        List<Farm> filtered = new List<Farm>();
        foreach (Farm farm in Farms)
        {
            if (farm.Location.IndexOf(location, StringComparison.OrdinalIgnoreCase) >= 0) // niet case sensitive
            {
                filtered.Add(farm);
            }
        }
        return filtered;
    }

    public void CreateFarm(Farm farm)
    {
        farm.Id = Farms.Count + 1;
        Farms.Add(farm);
       
    }

    public Animal ReadAnimal(int id)
    {
        foreach (Animal animal in Animals)
        {
            if (animal.Id == id)
            {
                return animal;
            }
        }

        return null;
    }

    public IEnumerable<Animal> ReadAllAnimals()
    {
        return Animals;
    }

    public IEnumerable<Animal> ReadAnimalsByTypeAndLifespan(int? type, int? minimumLifespan)
    {
        List<Animal> filtered = new List<Animal>();
        foreach (Animal animal in Animals)
        {
            bool matchesType = !type.HasValue || (int)animal.Type == type.Value;
            bool matchesLifespan = !minimumLifespan.HasValue || animal.Lifespan >= minimumLifespan.Value;

            if (matchesType && matchesLifespan)
            {
                filtered.Add(animal);
            }
        } 
        return filtered;
    }

    public void CreateAnimal(Animal animal)
    {
        animal.Id = Animals.Count + 1;
        Animals.Add(animal);
    }
}