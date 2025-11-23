using FarmManagement.BL.Domain;

namespace FarmManagement.UI.CA.Extensions;

public static class FarmExtensions
{
    public static string GetInfo(this Farm farm)
    {
        return $"Farm: {farm.Name} (ID: {farm.Id}) located in {farm.Location} with {(farm.SizeInHectares.HasValue ? $"{farm.SizeInHectares} hectares" : "unknown size of hectares")}. Founded in {farm.EstablishedYear} with {(farm.Harvests == null || !farm.Harvests.Any() ? "UNKNOWN" : farm.Harvests.Count.ToString())} harvests and {(farm.FarmAnimals == null || !farm.FarmAnimals.Any() ? "UNKNOWN" : farm.FarmAnimals.Count.ToString())} animals.";
    }
}