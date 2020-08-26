namespace QuickView.UI.Windows.Home
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    using ArgSentry;

    using QuickView.Services.Feeds;
    using QuickView.Services.Messages;
    using QuickView.UI.Windows.Home.Models;

    public class MessageStreamViewModel : BindableBase
    {
        private readonly IMessageService messageService;
        private readonly IFeedService feedService;

        private ObservableCollection<Message> messages;

        public MessageStreamViewModel(IMessageService messageService, IFeedService feedService)
        {
            Prevent.NullObject(messageService, nameof(messageService));
            Prevent.NullObject(feedService, nameof(feedService));

            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject()))
            {
                return;
            }

            this.messageService = messageService;
            this.feedService = feedService;
        }

        public ObservableCollection<Message> Messages
        {
            get => this.messages;
            set => SetProperty(ref this.messages, value);
        }

        public async void LoadMessages()
        {
            var feeds = await this.feedService.GetFeedsAsync();

            this.Messages = new ObservableCollection<Message>(
                (await this.messageService.GetMessagesAsync(feeds.ToList()))
                .Select(m => new Message(m.SourceName, m.Subject, m.Url,m.Timestamp, m.Creator, m.Body))
                .ToList());
        }
    }
}
