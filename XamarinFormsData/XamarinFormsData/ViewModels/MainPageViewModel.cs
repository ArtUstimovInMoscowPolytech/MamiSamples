using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsData.Views;

namespace XamarinFormsData.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand OpenImagePageCommand { get; private set; }

        public ICommand OpenDatabasePageCommand { get; private set; }

        public MainPageViewModel(INavigation navigation)
        {
            OpenImagePageCommand = new Command(async () => await navigation.PushAsync(new ImagePage()));
            OpenDatabasePageCommand = new Command(async () => await navigation.PushAsync(new DatabasePage()));
        }
    }
}