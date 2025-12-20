using System.ComponentModel.DataAnnotations;
using FarmManagement.BL.Domain;

namespace FarmManagement.UI.Web.Models.Dto;

public class NewHarvestDto 
{
    public CropType CropType { get; set; }
    [Required]
    public DateOnly HarvestDate { get; set; }
    [Range(0.1, double.MaxValue)]

    public double Quantity { get; set; }
    
    public Farm Farm { get; set; }
}