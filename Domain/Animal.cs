using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmManagement.BL.Domain;

public class Animal
{
    public int Id { get; set; }
    
    [Range(0.1, 2500)]
    public double AverageWeight { get; set; }
    
    [Required]
    public AnimalType Type { get; set; }
    
    [Range(1, 100)]
    public int Lifespan { get; set; }
    
    [Required]
    [StringLength(40,MinimumLength = 2)]
    public string Species { get; set; }
    [NotMapped]
    public ICollection<Farm> Farms { get; set; }

    public Animal()
    {
        
    }

    public Animal(string species, int lifespan, double averageWeight, AnimalType type)
    {
        Species = species;
        Lifespan = lifespan;
        AverageWeight = averageWeight;
        Type = type;
        Farms = new List<Farm>();
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