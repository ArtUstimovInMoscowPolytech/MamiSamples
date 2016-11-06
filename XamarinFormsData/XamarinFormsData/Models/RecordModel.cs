using System;
using SQLite;

namespace XamarinFormsData.Models
{
    public class RecordModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }
    }
}
