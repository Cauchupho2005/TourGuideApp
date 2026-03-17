namespace TourGuideApp.Models;

public class RestaurantImages
{
    public int Id { get; set; }

    public int RestaurantId { get; set; }

    public string? ImageUrl { get; set; }
}