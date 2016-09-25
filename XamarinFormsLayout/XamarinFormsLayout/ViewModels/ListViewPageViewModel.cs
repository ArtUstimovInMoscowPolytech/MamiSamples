using System.Collections.ObjectModel;
using XamarinFormsLayout.Models;

namespace XamarinFormsLayout.ViewModels
{
    class ListViewPageViewModel
    {
        public ObservableCollection<MessageModel> Messages = new ObservableCollection<MessageModel>
        {
            new MessageModel { Image = "http://icons.iconarchive.com/icons/visualpharm/must-have/256/User-icon.png", Text = "Hello!", Username = "User 1"},
            new MessageModel { Image = "http://downloadicons.net/sites/default/files/user-icon-44709.png", Text = "Hi!", Username = "User 2"},
        };
    }
}
