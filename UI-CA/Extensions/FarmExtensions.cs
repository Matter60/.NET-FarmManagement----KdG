using FarmManagement.BL.Domain;

namespace FarmManagement.UI.CA.Extensions;

public static class FarmExtensions
{
    public static string GetInfo(this Farm farm)
    {
        var info = $"Farm {farm.Name}, active since {farm.EstablishedYear}, located in {farm.Location} with {(farm.SizeInHectares.HasValue ? $"{farm.SizeInHectares} hectares" : "an unknown size of hectares")}.";
        if (farm.Harvests != null && farm.Harvests.Any())
        {
            info += "\nHarvests";
            foreach (Harvest harvest in farm.Harvests)
            {
                info += "\n- " + harvest.GetInfo();
            }
        }
        else
        {
            info += "\nNo harvests recorded.";
        }

        return info;
    }

}