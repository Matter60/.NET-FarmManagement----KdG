using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmManagement.BL.Domain;
public class Farm : IValidatableObject
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Location { get; set; }
    
    [Range(0.1, double.MaxValue)]
    public double? SizeInHectares { get; set; }
    
    [RegularExpression(@"^\d{4}$", ErrorMessage = "exactly 4 digits")]
    public int EstablishedYear { get; set; }
    
    [NotMapped]
    public ICollection<Harvest> Harvests { get; set; }
    [NotMapped]
    public ICollection<Animal> Animals { get; set; }

    public Farm()
    {
        
    }

    public Farm(string name, string location, int establishedYear,double? sizeInHectares = null)
    {
        Name = name;
        Location = location;
        SizeInHectares = sizeInHectares;
        EstablishedYear = establishedYear;
        Harvests = new List<Harvest>();
        Animals = new List<Animal>();
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