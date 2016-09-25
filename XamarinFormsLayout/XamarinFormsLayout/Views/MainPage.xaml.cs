using Xamarin.Forms;
using XamarinFormsLayout.ViewModels;

namespace XamarinFormsLayout.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel(this);
        }
    }
}
