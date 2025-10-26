using FarmManagement.BL.Domain;

namespace FarmManagement.DAL.EF;

public class Repository : IRepository
{
    private readonly FarmManagementDbContext _ctx;

    public Repository(FarmManagementDbContext farmManagementDbContext)
    {
        _ctx = farmManagementDbContext;
    }
    
    public Farm ReadFarm(int id)
    {
        return _ctx.Farms.Find(id);
    }

    public IEnumerable<Farm> ReadAllFarms()
    {
        return _ctx.Farms.ToList();
    }

    public IEnumerable<Farm> ReadFarmsByLocation(string location)
    {
        return _ctx.Farms.Where(farm => farm.Location.ToLower().Contains(location.ToLower())).ToList();
    }

    public void CreateFarm(Farm farm)
    {
        _ctx.Farms.Add(farm);
        _ctx.SaveChanges();
    }

    public Animal ReadAnimal(int id)
    {
        return _ctx.Animals.Find(id);
    }

    public IEnumerable<Animal> ReadAllAnimals()
    {
        return _ctx.Animals.ToList();
    }

    public IEnumerable<Animal> ReadAnimalsByTypeAndLifespan(AnimalType? type, int? minimumLifespan)
    {
        IQueryable<Animal> animals = _ctx.Animals;

        if (type.HasValue)
        {
            animals = animals.Where(animal => animal.Type == type);
        }
        
        if (minimumLifespan.HasValue)
        {
            animals = animals.Where(animal => animal.Lifespan >= minimumLifespan);
        }

        return animals.ToList();
    }

    public void CreateAnimal(Animal animal)
    {
        _ctx.Animals.Add(animal);
        _ctx.SaveChanges();

    }
}