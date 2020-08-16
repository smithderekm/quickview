namespace QuickView.UI.Windows.Feeds
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    using ArgSentry;

    using QuickView.Services.Feeds;
    using QuickView.UI.Windows.Feeds.Models;

    public class FeedListViewModel : BindableBase
    {
        private readonly IFeedService feedService;
        private ObservableCollection<Feed> feeds;

        public FeedListViewModel(IFeedService feedService)
        {
            Prevent.NullObject(feedService, nameof(feedService));

            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject()))
            {
                return;
            }

            this.feedService = feedService;

            AddFeedCommand = new RelayCommand(this.OnAddFeed);
            EditFeedCommand = new RelayCommand<Feed>(OnEditFeed);
            DeleteCommand = new RelayCommand(this.OnDeleteFeed, this.CanDeleteFeed);
        }

        public ObservableCollection<Feed> Feeds
        {
            get => this.feeds;
            set => SetProperty(ref this.feeds, value);
        }

        public async void LoadFeeds()
        {
            this.Feeds = new ObservableCollection<Feed>(
                (await this.feedService.GetFeedsAsync())
                .Select(f => new Feed
                {
                    Id = f.Id,
                    Name = f.Name,
                    Source = f.SourceName
                })
                .ToList());
        }

        public RelayCommand AddFeedCommand { get; private set; }
        public RelayCommand<Feed> EditFeedCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

        public event Action<Feed> AddFeedRequested = delegate { };
        public event Action<Feed> EditFeedRequested = delegate { };
        public event Action<Feed> DeleteFeedRequested = delegate { };
        
        void OnAddFeed()
        {
            this.AddFeedRequested(new Feed());
        }

        void OnEditFeed(Feed feed)
        {
            EditFeedRequested(feed);
        }

        void OnDeleteFeed()
        {

        }

        bool CanDeleteFeed()
        {
            return true;
        }
    }
}
