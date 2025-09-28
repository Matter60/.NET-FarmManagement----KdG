namespace FarmManagement;

public class Harvest
{
    public CropType CropType { get; set; }
    public DateOnly HarvestDate { get; set; }
    public double Quantity { get; set; }
    public Farm Farm { get; set; }

    public Harvest(CropType cropType, DateOnly harvestDate, double quantity, Farm farm)
    {
        CropType = cropType;
        HarvestDate = harvestDate;
        Quantity = quantity;
        Farm = farm;
    }


    public override string ToString()
    {
        return $"{CropType} harvest on {HarvestDate.ToShortDateString()} ({Quantity} kg)";
    }

}