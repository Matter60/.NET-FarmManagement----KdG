namespace FarmManagement;

public class Animal
{
    
    public DateOnly BirthDate { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    
    public AnimalType Type { get; set; }
    public ICollection<Farm> Farms { get; set; }

    public Animal( DateOnly birthDate, string name, double weight, AnimalType type)
    {
        BirthDate = birthDate;
        Name = name;
        Weight = weight;
        Type = type;
        Farms = new List<Farm>();
    }


    public override string ToString()
    {
        return $"Name: {Name} ({Type}), Born on {BirthDate.ToShortDateString()} (Weight {Weight} kg)";
    }
    
    
    public void AddFarm(Farm farm)
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