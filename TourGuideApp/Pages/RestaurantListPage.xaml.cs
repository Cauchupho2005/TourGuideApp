using TourGuideApp.Database;
using TourGuideApp.Models;
namespace TourGuideApp.Pages;

public partial class RestaurantListPage : ContentPage
{
    DatabaseService db;

    public RestaurantListPage()
    {
        InitializeComponent();

        db = new DatabaseService(Path.Combine(FileSystem.AppDataDirectory, "tourguide.db"));
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var restaurants = await db.GetRestaurants("vi");
        RestaurantList.ItemsSource = restaurants;
    }

    async void OnRestaurantClicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int id = (int)btn.CommandParameter;

        await Navigation.PushAsync(new RestaurantDetailPage(id));
    }
}