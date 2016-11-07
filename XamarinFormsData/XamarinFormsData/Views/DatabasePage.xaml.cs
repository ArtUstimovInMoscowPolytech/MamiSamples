using System;
using Xamarin.Forms;
using XamarinFormsData.ViewModels;

namespace XamarinFormsData.Views
{
    public partial class DatabasePage : ContentPage
    {
        private readonly DatabasePageViewModel _viewModel;

        public DatabasePage()
        {
            InitializeComponent();
            _viewModel = new DatabasePageViewModel(this);
            foreach (var dataSource in _viewModel.DataSources)
            {
                Picker.Items.Add(dataSource);
            }
            ItemListView.ItemsSource = _viewModel.ListItems;
            BindingContext = _viewModel;
        }

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            _viewModel.LoadCommand.Execute(null);
        }
    }
}
