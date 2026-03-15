using TourGuideApp.Models;
using SensorLocation = Microsoft.Maui.Devices.Sensors.Location;
using LocationModel = TourGuideApp.Models.Location;
using Microsoft.Maui.ApplicationModel;
using Plugin.Maui.Audio;

namespace TourGuideApp.Pages
{
    public partial class LocationDetailPage : ContentPage
    {
        private LocationModel location;
        IAudioManager audioManager;
        IAudioPlayer? player;

        public LocationDetailPage(LocationModel loc)
        {
            InitializeComponent();
            audioManager = AudioManager.Current;
            location = loc;


            nameLabel.Text = location.Name;
            descriptionLabel.Text = location.Description;
            locationImage.Source = location.ImageUrl; 

            latitudeLabel.Text = "Vĩ độ: " + location.Latitude;
            longitudeLabel.Text = "Kinh độ: " + location.Longitude;

            CalculateDistance();
        }

        private async Task CalculateDistance()
        {
            try
            {
                var request = new GeolocationRequest(
                    GeolocationAccuracy.Medium,
                    TimeSpan.FromSeconds(10));

                var locationUser = await Geolocation.Default.GetLocationAsync(request);

                if (locationUser != null)
                {
                    double distance = SensorLocation.CalculateDistance(
                        locationUser.Latitude,
                        locationUser.Longitude,
                        location.Latitude,
                        location.Longitude,
                        DistanceUnits.Kilometers);

                    distanceLabel.Text = $"Bạn cách địa điểm: {distance:F2} km";
                }
            }
            catch
            {
                distanceLabel.Text = "Không lấy được GPS";
            }
        }

        private async void OnNavigateClicked(object sender, EventArgs e)
        {
            var uri = new Uri($"https://www.google.com/maps/search/?api=1&query={location.Latitude},{location.Longitude}");

            await Launcher.Default.OpenAsync(uri);
        }

        private async void OnPlayAudioClicked(object sender, EventArgs e)
        {
            try
            {
                if (player != null && player.IsPlaying)
                {
                    player.Pause();
                    playButton.Text = "Play";
                    return;
                }

                if (player == null)
                {
                    if (string.IsNullOrEmpty(location.AudioFile))
                    {
                        await DisplayAlert("Lỗi", "Không có file audio", "OK");
                        return;
                    }

                    var stream = await FileSystem.OpenAppPackageFileAsync(location.AudioFile);
                    player = audioManager.CreatePlayer(stream);
                }

                player.Play();
                playButton.Text = "Pause";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Audio Error", ex.Message, "OK");
            }
        }

        //Hàm dừng audio
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (player != null && player.IsPlaying)
            {
                player.Stop();
            }
        }

        private async void OnButtonPressed(object sender, EventArgs e)
        {
            await playButton.ScaleTo(1.1, 100);
        }

        private async void OnButtonReleased(object sender, EventArgs e)
        {
            await playButton.ScaleTo(1, 100);
        }

    }
}