namespace QuickView.UI.Windows.Feeds
{
    using System;

    using QuickView.Services;
    using QuickView.Services.Feeds;
    using QuickView.UI.Windows.Feeds.Models;

    public class AddEditFeedViewModel : BindableBase
    {
        private IFeedService feedService;

        public AddEditFeedViewModel(IFeedService feedService)
        {
            this.feedService = feedService;
            this.CancelCommand = new RelayCommand(OnCancel);
            this.SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action Done = delegate { };

        private bool editMode;

        public bool EditMode
        {
            get => this.editMode;
            set => this.SetProperty(ref editMode, value);
        }

        private EditableFeed feed;

        public EditableFeed Feed
        {
            get => this.feed;
            set => this.SetProperty(ref this.feed, value);
        }


        private Feed editingFeed = null;

        public void SetFeed(Feed feed)
        {
            this.editingFeed = feed;
            if (this.Feed != null)
            {
                this.Feed.ErrorsChanged -= RaiseCanExecuteChanged;
            }

            this.Feed = new EditableFeed();
            this.Feed.ErrorsChanged += RaiseCanExecuteChanged;
            this.CopyFeed(feed, this.Feed);
        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            this.SaveCommand.RaiseCanExecuteChanged();
        }

        private void CopyFeed(Feed source, EditableFeed target)
        {
            target.Id = source.Id;
            
            if (this.EditMode)
            {
                target.Name = source.Name;
            }
        }

        private void OnCancel()
        {
            this.Done();
        }

        private async void OnSave()
        {
            this.UpdateFeed(Feed, this.editingFeed);
            
            if (this.EditMode)
                await this.feedService.UpdateFeedAsync(new UpdateFeedRequest
                {
                    Id = this.editingFeed.Id,
                    Name = this.editingFeed.Name,
                    Source = this.editingFeed.Source,
                    Subjects = this.editingFeed.Subjects
                });
            else
                await this.feedService.AddFeedAsync(new AddFeedRequest
                {
                    Name = this.editingFeed.Name,
                    Source = this.editingFeed.Source,
                    Subjects = this.editingFeed.Subjects
                });
            
            this.Done();
        }

        private void UpdateFeed(EditableFeed source, Feed target)
        {
            target.Name = source.Name;
        }

        bool CanSave()
        {
            return !this.Feed.HasErrors;
        }
    }
}
