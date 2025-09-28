namespace FarmManagement;

public class Farm
{
    private static int _nextId = 1;
    public int Id { get; private set; }
    
    public string Name { get; set; }
    public string Location { get; set; }
    public double? SizeInHectares { get; set; }
    public int EstablisedYear { get; set; }
    public ICollection<Harvest> Harvests { get; set; }
    public ICollection<Animal> Animals { get; set; }

    public Farm(string name, string location, int establisedYear,double? sizeInHectares = null)
    {
        Id = _nextId++;
        Name = name;
        Location = location;
        SizeInHectares = sizeInHectares;
        EstablisedYear = establisedYear;
        Harvests = new List<Harvest>();
        Animals = new List<Animal>();
    }

    public override string ToString()
    {
        return $"Farm: {Name} (ID: {Id}) located at {Location} with {(SizeInHectares.HasValue ? $"{SizeInHectares} hectares" : "unknown size of hectares")}. Founded in {EstablisedYear} with {Harvests.Count} harvests and {Animals.Count} animals.";
    }

}