using TourGuideApp.Database;
using Location = TourGuideApp.Models.Location;
namespace TourGuideApp.Pages;

public partial class LocationListPage : ContentPage
{
    private DatabaseService database;

    public LocationListPage(DatabaseService db)
    {
        InitializeComponent();
        database = db;

        LoadLocations();
    }

    private void LoadLocations()
    {
        var locations = database.GetLocations();
        locationList.ItemsSource = locations;
    }

    private async void OnLocationSelected(object? sender, SelectionChangedEventArgs e)
    {
        var location = e.CurrentSelection.FirstOrDefault() as Location;

        if (location == null)
            return;

        await Navigation.PushAsync(new LocationDetailPage(location));
    }
}