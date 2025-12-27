using System.ComponentModel.DataAnnotations;
using FarmManagement.BL.Domain;
using FarmManagement.DAL;


namespace FarmManagement.BL;

public class Manager : IManager
{
    private readonly IRepository _repository;

    public Manager(IRepository repository)
    {
        _repository = repository;
    }

    public Farm GetFarm(int id)
    {
        return _repository.ReadFarm(id);
    }

    public IEnumerable<Farm> GetAllFarms()
    {
        return _repository.ReadAllFarms();
    }

    public IEnumerable<Farm> GetFarmsByLocation(string location)
    {
        return _repository.ReadFarmsByLocation(location);
    }

    public Farm AddFarm(string name, string location, int establishedYear, double? sizeInHectares)
    {
        Farm farmToCreate = new Farm(name, location, establishedYear, sizeInHectares);
        ValidateEntity(farmToCreate);
        _repository.CreateFarm(farmToCreate);
        return farmToCreate;
    }

    public Animal GetAnimal(int id)
    {
        return _repository.ReadAnimal(id);
    }

    public IEnumerable<Animal> GetAllAnimals()
    {
        return _repository.ReadAllAnimals();
    }

    public IEnumerable<Animal> GetAnimalsByTypeAndLifespan(AnimalType? type, int? minimumLifespan)
    {
        return _repository.ReadAnimalsByTypeAndLifespan(type, minimumLifespan);
    }

    public Animal AddAnimal(string species, int lifespan, double averageWeight, AnimalType type)
    {
        Animal animalToCreate = new Animal(species, lifespan, averageWeight, type);
        ValidateEntity(animalToCreate);
        _repository.CreateAnimal(animalToCreate);
        return animalToCreate;
    }

    public IEnumerable<Animal> GetAllAnimalsWithFarms()
    {
        return _repository.ReadAllAnimalsWithFarms();
    }

    public IEnumerable<Farm> GetAllFarmsWithHarvests()
    {
        return _repository.ReadAllFarmsWithHarvests();
    }

    public void RemoveFarmAnimal(int farmId, int animalId)
    {
       _repository.DeleteFarmAnimal(farmId, animalId);
    }

  

    public FarmAnimal AddFarmAnimal(int farmId, int animalId, int count)
    {
        var farm = _repository.ReadFarm(farmId);
        
        var animal = _repository.ReadAnimal(animalId);
        
        var farmAnimal = new FarmAnimal
        {
            Farm = farm,
            Animal = animal,
            Count = count
        };
        
        ValidateEntity(farmAnimal);
        
        _repository.CreateFarmAnimal(farmAnimal);
        return farmAnimal;
    }

    public IEnumerable<Animal> GetAnimalsOfFarm(int farmId)
    {
        return _repository.ReadAnimalsOfFarm(farmId);    
    }

    public FarmAnimal GetFarmAnimal(int farmId, int animalId)
    {
       return _repository.ReadFarmAnimal(farmId, animalId);
    }

    public Farm GetFarmWithAnimals(int farmId)
    {
        return _repository.ReadFarmWithAnimals(farmId);
    }

    public IEnumerable<Harvest> GetAllHarvests()
    {
        return _repository.ReadAllHarvests();
    }

    public Harvest AddHarvest(CropType cropType, DateOnly harvestDate, double quantity, Farm farm)
    { 
        var harvestToCreate = new Harvest(cropType, harvestDate, quantity, farm);
        ValidateEntity(harvestToCreate);
        _repository.CreateHarvest(harvestToCreate);
        return harvestToCreate;
    }

    public IEnumerable<Farm> GetFarmsOfAnimal(int animalId)
    {
        return _repository.ReadFarmsOfAnimal(animalId);
        
    }

    public IEnumerable<Farm> GetAvailableFarmsOfAnimal(int animalId)
    {
        return _repository.ReadAvailableFarmsOfAnimal(animalId);
    }

    public Harvest GetHarvest(int id)
    {
        return _repository.ReadHarvest(id);
    }

    private void ValidateEntity(object entity)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(
            entity,
            new ValidationContext(entity),
            errors,
            validateAllProperties: true
        );

        if (!isValid)
        {
            throw new ValidationException(String.Join('|', errors));
        }
    }
}