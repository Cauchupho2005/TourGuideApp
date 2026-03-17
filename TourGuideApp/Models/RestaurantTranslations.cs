namespace TourGuideApp.Models;

public class RestaurantTranslations
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string? LanguageCode { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}