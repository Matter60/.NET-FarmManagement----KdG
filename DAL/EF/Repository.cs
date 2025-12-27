using FarmManagement.BL.Domain;
using Microsoft.EntityFrameworkCore;

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

    public IEnumerable<Farm> ReadAllFarmsWithHarvests()
    {
        return _ctx.Farms.Include(f => f.Harvests).ToList();
    }

    public IEnumerable<Animal> ReadAllAnimalsWithFarms()
    {
        return _ctx.Animals.Include(a => a.FarmAnimals).ThenInclude(fa => fa.Farm).ToList();
    }

    public void CreateFarmAnimal(FarmAnimal farmAnimal)
    {
        _ctx.FarmAnimals.Add(farmAnimal);
        _ctx.SaveChanges();
    }

    public void DeleteFarmAnimal(int farmId, int animalId)
    {
        var farmAnimal = _ctx.FarmAnimals
            .Find(farmId,animalId);

        if (farmAnimal != null)
        {
            _ctx.FarmAnimals.Remove(farmAnimal);
            _ctx.SaveChanges();
        }
       
    }

    public IEnumerable<Animal> ReadAnimalsOfFarm(int farmId)
    {
        return _ctx.Animals
            .Where(f => f.FarmAnimals.Any(fa => fa.Farm.Id == farmId))
            .ToList();
    }

    public FarmAnimal ReadFarmAnimal(int farmId, int animalId)
    {
        return _ctx.FarmAnimals.Find(farmId, animalId);
    }

    public Farm ReadFarmWithAnimals(int farmId)
    {
        return _ctx.Farms
            .Include(f => f.FarmAnimals)
            .ThenInclude(fa => fa.Animal)
            .SingleOrDefault(f => f.Id == farmId);
    }

    public IEnumerable<Harvest> ReadAllHarvests()
    {
        return _ctx.Harvests.ToList();
    }

    public void CreateHarvest(Harvest harvest)
    {
        _ctx.Harvests.Add(harvest);
        _ctx.SaveChanges();
    }

    public IEnumerable<Farm> ReadFarmsOfAnimal(int animalId)
    {
        return _ctx.Farms
            .Where(f => f.FarmAnimals.Any(fa => fa.Animal.Id == animalId))
            .ToList();
    }

    public IEnumerable<Farm> ReadAvailableFarmsOfAnimal(int animalId)
    {
        return _ctx.Farms
            .Where(f => f.FarmAnimals.All(fa => fa.Animal.Id != animalId))
            .ToList();
    }

    public Harvest ReadHarvest(int id)
    {
        return _ctx.Harvests.Find(id);
    }
}