namespace FarmManagement;

public class Animal
{
    
    public DateTime BirthDate { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    
    public AnimalType Type { get; set; }
    public ICollection<Farm> Farms { get; set; }

    public Animal( DateTime birthDate, string name, double weight, AnimalType type)
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

}