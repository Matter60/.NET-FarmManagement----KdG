using FarmManagement.BL.Domain;

namespace FarmManagement.UI.CA.Extensions;

public static class AnimalExtensions
{
    public static string GetInfo(this Animal animal)
    {
        return $"{animal.Type} (ID: {animal.Id}) (Species: {animal.Species}), Lifespan: {animal.Lifespan} yrs, Avg. weight: {animal.AverageWeight} kg";
    }
}