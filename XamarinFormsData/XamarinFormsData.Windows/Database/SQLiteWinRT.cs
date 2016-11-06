using System.IO;
using Windows.Storage;
using SQLite;
using Xamarin.Forms;
using XamarinFormsData.Database;
using XamarinFormsData.Windows.Database;

[assembly: Dependency(typeof(SQLiteWinRT))]

namespace XamarinFormsData.Windows.Database
{
    public class SQLiteWinRT : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "DataSQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}
