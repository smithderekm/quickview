namespace QuickView.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ArgSentry;

    using QuickView.Querying;
    using QuickView.Querying.Dto;
    using QuickView.Services.Feeds;

    public class FeedService : IFeedService
    {
        private readonly IFeedProvider feedProvider;

        public FeedService(IFeedProvider feedProvider)
        {
            Prevent.NullObject(feedProvider, nameof(feedProvider));
            this.feedProvider = feedProvider;
        }
        public async Task<IEnumerable<Feed>> GetFeedsAsync()
        {
            return await this.feedProvider.GetFeedsAsync();
        }
    }
}
