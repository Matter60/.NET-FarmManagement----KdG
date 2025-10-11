using FarmManagement.BL.Domain;
namespace FarmManagement.DAL;

public interface IRepository
{
    public Farm ReadFarm(int id);
    public IEnumerable<Farm> ReadAllFarms();
    public List<Farm> ReadFarmsByLocation(string location);
    public void CreateFarm(Farm farm);
    
    public Animal ReadAnimal(int id);
    public IEnumerable<Animal> ReadAllAnimals();
    public List<Animal> ReadAnimalsByTypeAndLifespan(int? type, int? minimumLifespan);
    public void CreateAnimal(Animal animal);
}