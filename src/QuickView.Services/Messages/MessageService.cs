namespace QuickView.Services.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ArgSentry;

    using QuickView.Querying;
    using QuickView.Querying.Dto;

    public class MessageService : IMessageService
    {
        private readonly List<IFeedMessagesProvider> messageProviders;

        public MessageService(IEnumerable<IFeedMessagesProvider> messageProviders)
        {
            Prevent.NullObject(messageProviders, nameof(messageProviders));
            this.messageProviders = messageProviders.ToList();
        }

        public async Task<IReadOnlyList<Message>> GetMessagesAsync(Feed feed)
        {
            Prevent.NullObject(feed, nameof(feed));

            return await this.GetMessagesInternalAsync(new List<Feed> { feed }).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<Message>> GetMessagesAsync(IList<Feed> feeds)
        {
            Prevent.NullObject(feeds, nameof(feeds));

            return await this.GetMessagesInternalAsync(feeds).ConfigureAwait(false);
        }

        private async Task<IReadOnlyList<Message>> GetMessagesInternalAsync(IList<Feed> feeds)
        {
            var results = new List<Message>();

            foreach (var feed in feeds)
            {
                var provider = this.messageProviders.FirstOrDefault(p => p.Source() == feed.SourceName);

                if (provider == null)
                {
                    continue;
                }

                //TODO make parallel
                results.AddRange(await provider.GetMessagesAsync(feed.Id, feed.Subjects.ToList(), feed.Identity));
            }

            return results.AsReadOnly();
        }
    }
}
