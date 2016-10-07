using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsLayout.Views;

namespace XamarinFormsLayout.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly Page _page;

        public int ClickCount { get; private set; } = 0;

        public ICommand OpenListViewPageCommand { get; private set; }

        public ICommand PopupCommand { get; private set; }

        public ICommand PositioningCommand { get; private set; }

        public MainPageViewModel(Page page)
        {
            _page = page;

            OpenListViewPageCommand = new Command(async () =>
            {
                ClickCount++;
                OnPropertyChanged(nameof(this.ClickCount));
                await _page.Navigation.PushAsync(new Views.ListViewPage());
            });

            PopupCommand = new Command(async () => 
                await _page.DisplayAlert("Привет", String.Empty, "Закрыть"));

            PositioningCommand = new Command(async () => await _page.Navigation.PushAsync(new PositioningPage()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
