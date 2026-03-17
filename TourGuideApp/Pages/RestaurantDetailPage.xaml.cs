using Plugin.Maui.Audio;
using TourGuideApp.Database;

namespace TourGuideApp.Pages;

public partial class RestaurantDetailPage : ContentPage
{
    DatabaseService db;
    int restaurantId;

    IAudioManager audioManager = AudioManager.Current;
    IAudioPlayer? player;

    string? audioFile;

    public RestaurantDetailPage(int id)
    {
        InitializeComponent();

        restaurantId = id;

        db = new DatabaseService(Path.Combine(FileSystem.AppDataDirectory, "tourguide.db"));
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var restaurant = await db.GetRestaurant(restaurantId);
        var translation = await db.GetRestaurantTranslation(restaurantId);

        if (translation != null)
        {
            NameLabel.Text = translation.Name;
            DescriptionLabel.Text = translation.Description;
        }

        audioFile = restaurant?.AudioFile;
    }

    async void OnPlayAudioClicked(object sender, EventArgs e)
    {
        if (player != null)
        {
            player.Stop();
        }

        if (string.IsNullOrEmpty(audioFile))
            return;

        var stream = await FileSystem.OpenAppPackageFileAsync(audioFile);
        player = audioManager.CreatePlayer(stream);

        player.Play();
    }
}