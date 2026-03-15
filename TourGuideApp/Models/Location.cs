using SQLite;
using System.Linq;
namespace TourGuideApp.Models
{
    public class Location
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string? Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? Description { get; set; }

        public string? AudioFile { get; set; }

        public string? ImageUrl { get; set; }
    }
}