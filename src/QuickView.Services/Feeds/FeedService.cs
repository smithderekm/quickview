namespace QuickView.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using ArgSentry;

    using QuickView.Commanding.Feeds.CreateNew;
    using QuickView.Domain.Models.Feeds;
    using QuickView.Querying;
    using QuickView.Querying.Dto;
    using QuickView.Services.Feeds;

    using Subject = QuickView.Domain.Models.Feeds.Subject;

    public class FeedService : IFeedService
    {
        private readonly IFeedProvider feedProvider;
        private readonly ICreateNewFeedCommandHandler createNewFeedCommandHandler;

        public FeedService(IFeedProvider feedProvider, ICreateNewFeedCommandHandler createNewFeedCommandHandler)
        {
            Prevent.NullObject(feedProvider, nameof(feedProvider));
            Prevent.NullObject(createNewFeedCommandHandler, nameof(createNewFeedCommandHandler));

            this.feedProvider = feedProvider;
            this.createNewFeedCommandHandler = createNewFeedCommandHandler;
        }

        public async Task<IEnumerable<Feed>> GetFeedsAsync()
        {
            return await this.feedProvider.GetFeedsAsync();
        }

        public Task UpdateFeedAsync(IFeedRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddFeedAsync(IFeedRequest request)
        {
            Prevent.NullObject(request, nameof(request));

            if (request.GetType() != typeof(AddFeedRequest))
            {
                throw new ArgumentException("Invalid request type.  This method requires AddFeedRequest.");
            }

            var typedRequest = request as AddFeedRequest;

            var command = new CreateNewFeedCommand(
                typedRequest.Name, 
                new Source(typedRequest.Source), 
                typedRequest.Subjects.Select(
                    s => Domain.Models.Feeds.Subject.CreateNew(s.Key, s.Value))
                    .ToList());

            await this.createNewFeedCommandHandler.HandleAsync(command, new CancellationToken()).ConfigureAwait(false);
        }

        public Task DeleteFeedAsync(IFeedRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
