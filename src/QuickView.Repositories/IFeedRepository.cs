namespace QuickView.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using QuickView.Domain.Models.Feeds;

    public interface IFeedRepository
    {
        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FeedAggregate> GetByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The get all async.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IEnumerable<FeedAggregate>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The create async.
        /// </summary>
        /// <param name="feed">
        /// The feed.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateAsync(FeedAggregate feed, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The update async.
        /// </summary>
        /// <param name="feed">
        /// The feed.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FeedAggregate> UpdateAsync(FeedAggregate feed, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="feed">
        /// The feed.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task DeleteAsync(FeedAggregate feed, CancellationToken cancellationToken = default(CancellationToken));

    }
}
