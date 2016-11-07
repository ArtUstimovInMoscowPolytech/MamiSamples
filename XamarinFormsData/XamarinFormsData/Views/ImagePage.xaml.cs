using Xamarin.Forms;
using XamarinFormsData.ViewModels;

namespace XamarinFormsData.Views
{
    public partial class ImagePage : ContentPage
    {
        public ImagePage()
        {
            InitializeComponent();
            BindingContext = new ImagePageViewModel(this);
        }
    }
}
