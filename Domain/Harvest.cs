using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmManagement.BL.Domain;

public class Harvest
{
    public int Id { get; set; }
    [Required]
    public CropType CropType { get; set; }
    [Required]
    public DateOnly HarvestDate { get; set; }
    [Range(0.1, double.MaxValue)]

    public double Quantity { get; set; }
    
   
    [Required]
    public Farm Farm { get; set; }

    public Harvest()
    {
        
    }

    public Harvest(CropType cropType, DateOnly harvestDate, double quantity, Farm farm)
    {
        CropType = cropType;
        HarvestDate = harvestDate;
        Quantity = quantity;
        Farm = farm;
    }
    
}