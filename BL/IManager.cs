using FarmManagement.BL.Domain;
namespace FarmManagement.BL;

public interface IManager
{
    public Farm GetFarm(int id);
    public IEnumerable<Farm> GetAllFarms();
    public IEnumerable<Farm> GetFarmsByLocation(string location);
    public Farm AddFarm(string name, string location, int establishedYear, double? sizeInHectares);
    
    public Animal GetAnimal(int id);
    public IEnumerable<Animal> GetAllAnimals();
    public IEnumerable<Animal> GetAnimalsByTypeAndLifespan(AnimalType? type, int? minimumLifespan);
    public Animal AddAnimal(string species, int lifespan, double averageWeight, AnimalType type);
    
    public IEnumerable<Animal> GetAllAnimalsWithFarms();
    public IEnumerable<Farm> GetAllFarmsWithHarvests();

    public void RemoveFarmAnimal(int farmId, int animalId);

    public void AddFarmAnimal(int farmId, int animalId, int count = 1);
    
    public IEnumerable<Animal> GetAnimalsOfFarm(int farmId);
    
    public FarmAnimal GetFarmAnimal(int farmId, int animalId);

    public Farm GetFarmWithAnimals(int farmId);
    
    public IEnumerable<Harvest> GetAllHarvests();
    
    public Harvest AddHarvest(CropType cropType, DateOnly harvestDate, double quantity, Farm? farm);
}