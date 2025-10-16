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
    public IEnumerable<Animal> ReadAnimalsByTypeAndLifespan(int? type, int? minimumLifespan);
    public void CreateAnimal(Animal animal);
}