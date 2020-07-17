using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.UI.Controls;

using QuickView.UI.UWP.Core.Models;
using QuickView.UI.UWP.Core.Services;
using QuickView.UI.UWP.Helpers;

namespace QuickView.UI.UWP.ViewModels
{
    using ArgSentry;

    using QuickView.Querying.Dto;
    using QuickView.Services;
    using QuickView.Services.Feeds;

    public class MainViewModel : Observable
    {
        private IFeedService feedService;

        private Feed _selected;

        public Feed Selected
        {
            get => _selected;
            set => Set(ref _selected, value);
        }

        public ObservableCollection<Feed> Feeds { get; private set; } = new ObservableCollection<Feed>();

        public MainViewModel(IFeedService feedService)
        {
            Prevent.NullObject(feedService, nameof(feedService));
            this.feedService = feedService;
        }

        public async Task LoadDataAsync(MasterDetailsViewState viewState)
        {
            Feeds.Clear();

            var data = await this.feedService.GetFeedsAsync();

            foreach (var item in data)
            {
                Feeds.Add(item);
            }

            if (viewState == MasterDetailsViewState.Both && Feeds.Any())
            {
                Selected = Feeds.First();
            }
        }
    }
}
