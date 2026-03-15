using Microsoft.Extensions.Logging;
using TourGuideApp.Database;
using Plugin.Maui.Audio;

namespace TourGuideApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Database
        builder.Services.AddSingleton<DatabaseService>(s =>
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "tourguide.db");

            if (!File.Exists(dbPath))
            {
                using var stream = FileSystem.OpenAppPackageFileAsync("tourguide.db").Result;
                using var fileStream = File.Create(dbPath);
                stream.CopyTo(fileStream);
            }

            return new DatabaseService(dbPath);
        });

        //Audio
        builder.Services.AddSingleton<IAudioManager>(AudioManager.Current);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}