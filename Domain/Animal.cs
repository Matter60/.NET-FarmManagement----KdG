using System.ComponentModel.DataAnnotations;

namespace FarmManagement.BL.Domain;

public class Animal
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    
    [Range(0.1, 2500)]
    public double AverageWeight { get; set; }
    
    [Required]
    public AnimalType Type { get; set; }
    
    [Range(1, 100)]
    public int Lifespan { get; set; }
    
    [Required]
    [StringLength(40,MinimumLength = 2)]
    public string Species { get; set; }
    public ICollection<Farm> Farms { get; set; }

    public Animal(string species, int lifespan, double averageWeight, AnimalType type)
    {
        Id = _nextId++;
        Species = species;
        Lifespan = lifespan;
        AverageWeight = averageWeight;
        Type = type;
        Farms = new List<Farm>();
    }


    public override string ToString()
    {
        return $"{Type} (Species: {Species}), Lifespan: {Lifespan} yrs, Avg. weight: {AverageWeight} kg";
    }

    
    
    public void ConnectToFarm(Farm farm)
    {
        // Ensure this farm is not already linked to the animal
        if (!Farms.Contains(farm))
        {
            // Link the farm to the animal
            Farms.Add(farm);

            // Ensure the animal is also linked to the farm
            if (!farm.Animals.Contains(this))
            {
                farm.Animals.Add(this);
            }
        }
    }



}