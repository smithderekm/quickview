using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using QuickView.UI.UWP.Core.Models;
using QuickView.UI.UWP.Core.Services;
using QuickView.UI.UWP.Helpers;
using QuickView.UI.UWP.Services;

namespace QuickView.UI.UWP.ViewModels
{
    public class ContentGridDetailViewModel : Observable
    {
        private SampleOrder _item;

        public SampleOrder Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        private ICommand _goBackCommand;

        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack));

        public ContentGridDetailViewModel()
        {
        }

        public async Task InitializeAsync(long orderID)
        {
            var data = await SampleDataService.GetContentGridDataAsync();
            Item = data.First(i => i.OrderID == orderID);
        }

        private void OnGoBack()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
