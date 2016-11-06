using System.IO;
using Windows.Storage;
using SQLite;
using Xamarin.Forms;
using XamarinFormsData.Database;
using XamarinFormsData.UWP.Database;

[assembly: Dependency(typeof(SQLiteUWP))]

namespace XamarinFormsData.UWP.Database
{
    public class SQLiteUWP : ISQLite
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
