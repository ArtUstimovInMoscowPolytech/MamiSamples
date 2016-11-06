using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SQLite;
using Xamarin.Forms;
using XamarinFormsData.Database;
using XamarinFormsData.Models;

namespace XamarinFormsData.ViewModels
{
    public class MainViewModel
    {
        private readonly List<RecordModel> _memory = new List<RecordModel>();

        public ObservableCollection<RecordModel> ListItems = new ObservableCollection<RecordModel>();

        public string TextForRecord { get; set; }

        public string[] DataSources { get; set; } = Enum.GetNames(typeof(DataSource));

        public string DataSource { get; set; }

        public ICommand RecordCommand { get; private set; }

        public ICommand LoadCommand { get; private set; }

        public ICommand ClearCommand { get; private set; }

        public int SelectedIndex { get; set; }

        public MainViewModel(Page page)
        {
            using (var connection = GetConnection())
            {
                connection.CreateTable<RecordModel>();
            }

            RecordCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(TextForRecord))
                {
                    await page.DisplayAlert("Ошибка", "Поле ввода не должно быть пустым", "Ок");
                    return;
                }

                var record = new RecordModel
                {
                    DateTime = DateTime.Now,
                    Text = TextForRecord,
                };

                ListItems.Add(record);

                switch ((DataSource)SelectedIndex)
                {
                    case Models.DataSource.Memory:
                        RecordToMemory(record);
                        break;
                    case Models.DataSource.SQLite:
                        RecordToDatabase(record);
                        break;
                }
            });

            ClearCommand = new Command(() =>
            {
                ListItems.Clear();
                switch ((DataSource)SelectedIndex)
                {
                    case Models.DataSource.Memory:
                        ClearMemory();
                        break;
                    case Models.DataSource.SQLite:
                        ClearDatabase();
                        break;
                }
            });

            LoadCommand = new Command(() =>
            {
                ListItems.Clear();
                switch ((DataSource)SelectedIndex)
                {
                    case Models.DataSource.Memory:
                        LoadFromMemory();
                        break;
                    case Models.DataSource.SQLite:
                        LoadFromDatabase();
                        break;
                }
            });
        }

        private void LoadFromDatabase()
        {
            using (var connection = GetConnection())
            {
                var records = connection.Query<RecordModel>("SELECT * FROM RecordModel");
                foreach (var record in records)
                {
                    ListItems.Add(record);
                }
            }
        }

        private void LoadFromMemory()
        {
            foreach (var record in _memory)
            {
                ListItems.Add(record);
            }
        }

        private void ClearDatabase()
        {
            using (var connection = GetConnection())
            {
                connection.DeleteAll<RecordModel>();
            }
        }

        private void ClearMemory()
        {
            _memory.Clear();
        }

        private void RecordToMemory(RecordModel record)
        {
            _memory.Add(record);
        }

        private void RecordToDatabase(RecordModel record)
        {
            using (var connection = GetConnection())
            {
                connection.Insert(record);
            }
        }

        private SQLiteConnection GetConnection()
        {
            return DependencyService.Get<ISQLite>().GetConnection();
        }
    }
}