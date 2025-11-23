using System.ComponentModel.DataAnnotations;

namespace FarmManagement.BL.Domain;

public class FarmAnimal
{
    
    [Required]
    public Farm Farm { get; set; }
    [Required]
    public Animal Animal { get; set; }
    
    public int Count { get; set; }
    
}