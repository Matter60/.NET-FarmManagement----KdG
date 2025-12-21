using System.ComponentModel.DataAnnotations;
using FarmManagement.BL.Domain;

namespace FarmManagement.UI.Web.Models.Dto;

public class NewFarmAnimalDto
{
    [Required]
    public int FarmId { get; set; }

    [Required]
    public int AnimalId { get; set; } 

    public int Count { get; set; }
}