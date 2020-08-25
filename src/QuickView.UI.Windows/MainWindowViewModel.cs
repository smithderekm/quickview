namespace QuickView.UI.Windows
{
    using Microsoft.Extensions.DependencyInjection;

    using QuickView.UI.Windows.Feeds;
    using QuickView.UI.Windows.Feeds.Models;
    using QuickView.UI.Windows.Home;
    using QuickView.UI.Windows.Options;

    public class MainWindowViewModel : BindableBase
    {
        private const string SummaryCommand = "summary";
        private const string MessageStreamCommand = "messageStream";
        private const string FeedsCommand = "feedList";
        private const string SettingsCommand = "settings";
        private const string AddEditFeedCommand = "addEditFeed";
        private const string AboutCommand = "about";

        private MessageStreamViewModel messageStreamViewModel;
        private SummaryViewModel summaryViewModel;
        private FeedListViewModel feedListViewModel;
        private AddEditFeedViewModel addEditFeedViewModel;
        private OptionsViewModel optionsViewModel;
        private AboutViewModel aboutViewModel;

        private BindableBase currentViewModel;

        public string SummaryCommandParameter { get; } = SummaryCommand;
        public string MessageStreamCommandParameter { get; } = MessageStreamCommand;

        public string FeedsCommandParameter { get; } = FeedsCommand;
        public string SettingsCommandParameter { get; } = SettingsCommand;
        public string AddEditFeedCommandParameter { get; }= AddEditFeedCommand;
        public string AboutCommandParameter { get; }= AboutCommand;

        public BindableBase CurrentViewModel
        {
            get => this.currentViewModel;
            set => this.SetProperty(ref this.currentViewModel, value);
        }

        public MainWindowViewModel()
        {
            NavCommand = new RelayCommand<string>(this.OnNavigate);

            this.messageStreamViewModel = App.ServiceProvider.GetRequiredService<MessageStreamViewModel>();
            this.summaryViewModel = App.ServiceProvider.GetRequiredService<SummaryViewModel>();
            this.feedListViewModel = App.ServiceProvider.GetRequiredService<FeedListViewModel>();
            this.addEditFeedViewModel = App.ServiceProvider.GetRequiredService<AddEditFeedViewModel>();
            this.aboutViewModel = App.ServiceProvider.GetRequiredService<AboutViewModel>();
            this.optionsViewModel = App.ServiceProvider.GetRequiredService<OptionsViewModel>();

            this.feedListViewModel.AddFeedRequested += NavigateToAddFeed;
            this.feedListViewModel.EditFeedRequested += NavigateToEditFeed;
            this.addEditFeedViewModel.Done += NavigateToFeedList;

        }
        public RelayCommand<string> NavCommand { get; private set; }
        
        private void OnNavigate(string destination)
        {
            switch (destination)
            {
                case FeedsCommand:
                    CurrentViewModel = this.feedListViewModel;
                    break;
                case AddEditFeedCommand:
                    CurrentViewModel = this.addEditFeedViewModel;
                    break;
                case SettingsCommand:
                    CurrentViewModel = this.optionsViewModel;
                    break;
                case AboutCommand:
                    CurrentViewModel = this.aboutViewModel;
                    break;
                case SummaryCommand:
                    CurrentViewModel = this.summaryViewModel;
                    break;
                case MessageStreamCommand:
                    CurrentViewModel = this.messageStreamViewModel;
                    break;
                default:
                    CurrentViewModel = this.summaryViewModel;
                    break;
            }
        }

        private void NavigateToAddFeed(Feed feed)
        {
            this.addEditFeedViewModel.EditMode = false;
            this.addEditFeedViewModel.SetFeed(feed);
            CurrentViewModel = this.addEditFeedViewModel;
        }

        private void NavigateToEditFeed(Feed feed)
        {
            this.addEditFeedViewModel.EditMode = true;
            this.addEditFeedViewModel.SetFeed(feed);
            CurrentViewModel = this.addEditFeedViewModel;
        }

        void NavigateToFeedList()
        {
            CurrentViewModel = this.feedListViewModel;
        }
    }
}
