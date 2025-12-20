using FarmManagement.BL.Domain;
namespace FarmManagement.DAL;

public interface IRepository
{
    public Farm ReadFarm(int id);
    public IEnumerable<Farm> ReadAllFarms();
    public IEnumerable<Farm> ReadFarmsByLocation(string location);
    public void CreateFarm(Farm farm);
    
    public Animal ReadAnimal(int id);
    public IEnumerable<Animal> ReadAllAnimals();
    public IEnumerable<Animal> ReadAnimalsByTypeAndLifespan(AnimalType? type, int? minimumLifespan);
    public void CreateAnimal(Animal animal);

    public IEnumerable<Farm> ReadAllFarmsWithHarvests();

    public IEnumerable<Animal> ReadAllAnimalsWithFarms();
    
    public void CreateFarmAnimal(FarmAnimal farmAnimal);
    public void DeleteFarmAnimal(int farmId, int animalId);
    
    public IEnumerable<Animal> ReadAnimalsOfFarm(int farmId);

    public FarmAnimal ReadFarmAnimal(int farmId, int animalId);
    
    public Farm ReadFarmWithAnimals(int farmId);
    
    public IEnumerable<Harvest> ReadAllHarvests();
    
    public void CreateHarvest(Harvest harvest);
}