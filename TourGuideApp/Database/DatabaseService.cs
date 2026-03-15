using SQLite;
using Location = TourGuideApp.Models.Location;

namespace TourGuideApp.Database
{
    public class DatabaseService
    {
        private SQLiteConnection database;

        public DatabaseService(string dbPath)
        {
            database = new SQLiteConnection(dbPath);

            // tạo bảng nếu chưa có
            database.CreateTable<Location>();
        }

        // lấy toàn bộ địa điểm
        public List<Location> GetLocations()
        {
            return database.Table<Location>().ToList();
        }

        // thêm địa điểm mới
        public int SaveLocation(Location location)
        {
            return database.Insert(location);
        }

        // cập nhật địa điểm
        public int UpdateLocation(Location location)
        {
            return database.Update(location);
        }

        // xóa địa điểm
        public int DeleteLocation(Location location)
        {
            return database.Delete(location);
        }
    }
}