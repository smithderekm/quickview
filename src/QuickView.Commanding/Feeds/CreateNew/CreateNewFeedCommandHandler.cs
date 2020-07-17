namespace QuickView.Commanding.Feeds.CreateNew
{
    using System.Threading;
    using System.Threading.Tasks;

    using ArgSentry;

    using QuickView.Domain.Models.Feeds;
    using QuickView.Repositories;

    public class CreateNewFeedCommandHandler : ICreateNewFeedCommandHandler
    {
        private readonly IFeedRepository repository;

        public CreateNewFeedCommandHandler(IFeedRepository repository)
        {
            Prevent.NullObject(repository, nameof(repository));
            this.repository = repository;
        }

        public async Task<CreateNewFeedResult> HandleAsync(CreateNewFeedCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var feed = FeedAggregate.CreateNew(command.Name, command.Provider);

            await this.repository.CreateAsync(feed, cancellationToken).ConfigureAwait(false);

            return new CreateNewFeedResult();
        }
    }
}
