using FarmManagement.BL.Domain;
namespace FarmManagement.BL;

public interface IManager
{
    public Farm GetFarm(int id);
    public IEnumerable<Farm> GetAllFarms();
    public List<Farm> GetFarmsByLocation(string location);
    public Farm AddFarm(string name, string location, int establishedYear, double? sizeInHectares);
    
    public Animal GetAnimal(int id);
    public IEnumerable<Animal> GetAllAnimals();
    public List<Animal> GetAnimalsByTypeAndLifespan(int? type, int? minimumLifespan);
    public Animal AddAnimal(string species, int lifespan, double averageWeight, AnimalType type);
}