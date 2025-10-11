using System.ComponentModel.DataAnnotations;

namespace FarmManagement.BL.Domain;
public class Farm : IValidatableObject
{
    private static int _nextId = 1;
    public int Id { get; private set; }
    
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Location { get; set; }
    
    [Range(0.1, double.MaxValue)]
    public double? SizeInHectares { get; set; }
    
    [RegularExpression(@"^\d{4}$")]
    public int EstablishedYear { get; set; }
    public ICollection<Harvest> Harvests { get; set; }
    public ICollection<Animal> Animals { get; set; }

    public Farm(string name, string location, int establisedYear,double? sizeInHectares = null)
    {
        Id = _nextId++;
        Name = name;
        Location = location;
        SizeInHectares = sizeInHectares;
        EstablishedYear = establisedYear;
        Harvests = new List<Harvest>();
        Animals = new List<Animal>();
    }

    public override string ToString()
    {
        return $"Farm: {Name} (ID: {Id}) located in {Location} with {(SizeInHectares.HasValue ? $"{SizeInHectares} hectares" : "unknown size of hectares")}. Founded in {EstablishedYear} with {Harvests.Count} harvests and {Animals.Count} animals.";
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        if (EstablishedYear > DateTime.Now.Year)
        {
            errors.Add(new ValidationResult("Established year cannot be in the future!", new [] {nameof(EstablishedYear)}));
        }
        return errors;
    }
}