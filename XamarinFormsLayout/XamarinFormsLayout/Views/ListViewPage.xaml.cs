using Xamarin.Forms;
using XamarinFormsLayout.Models;
using XamarinFormsLayout.ViewModels;

namespace XamarinFormsLayout.Views
{
    public partial class ListViewPage : ContentPage
    {
        public ListViewPage()
        {
            InitializeComponent();
            var viewModel = new ListViewPageViewModel();
            ListView.ItemsSource = viewModel.Messages;
            this.BindingContext = viewModel;
        }

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var msg = (MessageModel)e.Item;
            await DisplayAlert(msg.Username, msg.Text, "Закрыть");
        }
    }
}
