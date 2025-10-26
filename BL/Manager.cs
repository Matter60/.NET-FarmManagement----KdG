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