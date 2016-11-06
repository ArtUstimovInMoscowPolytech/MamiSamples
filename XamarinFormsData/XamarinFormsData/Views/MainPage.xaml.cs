using System;
using Xamarin.Forms;
using XamarinFormsData.ViewModels;

namespace XamarinFormsData.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainViewModel(this);
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
