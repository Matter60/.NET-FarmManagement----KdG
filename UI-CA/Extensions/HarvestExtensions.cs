using FarmManagement.BL.Domain;

namespace FarmManagement.UI.CA.Extensions;

public static class HarvestExtensions
{
    public static string GetInfo(this Harvest harvest)
    {
        return $"(ID: {harvest.Id}) {harvest.CropType} harvest on {harvest.HarvestDate.ToShortDateString()} ({harvest.Quantity} kg)";
    }
}