using TourGuideApp.Database;
using TourGuideApp.Pages;

namespace TourGuideApp
{
    public partial class App : Application
    {
        public App(DatabaseService db)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LocationListPage(db));
        }
    }
}