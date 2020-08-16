namespace QuickView.Data.LocalStorage.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ArgSentry;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using QuickView.Data.LocalStorage.Stores;
    using QuickView.Querying;
    using QuickView.Querying.Dto;

    public class FeedProvider : IFeedProvider
    {
        private readonly ILogger<FeedProvider> logger;
        private readonly LocalStorageOptions options;
        private readonly IFeedStore store;

        public FeedProvider(ILogger<FeedProvider> logger, IOptions<LocalStorageOptions> options, IFeedStore store)
        {
            Prevent.NullObject(logger, nameof(logger));
            Prevent.NullObject(options, nameof(options));
            Prevent.NullObject(store, nameof(store));

            this.logger = logger;
            this.options = options.Value;
            this.store = store;

        }
        public async Task<IReadOnlyList<Feed>> GetFeedsAsync()
        {
            var feeds = await this.store.GetAllAsync();
            return feeds == null
                ? new List<Feed>()
                : feeds.Select(f => new Feed(
                    f.Id,
                    f.Name,
                    f.SourceName,
                    f.Subjects.Select(s => new Subject(s)).ToList()))
                    .ToList();
        }

        public Task<Feed> GetFeedAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
