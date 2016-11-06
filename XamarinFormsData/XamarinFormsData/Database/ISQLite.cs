using SQLite;

namespace XamarinFormsData.Database
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
