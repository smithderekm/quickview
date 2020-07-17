namespace QuickView.Data.LocalStorage.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using QuickView.Domain.Models.Feeds;
    using QuickView.Repositories;

    public class FeedRepository : IFeedRepository
    {
        public Task<FeedAggregate> GetByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FeedAggregate>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(FeedAggregate feed, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<FeedAggregate> UpdateAsync(FeedAggregate feed, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(FeedAggregate feed, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
