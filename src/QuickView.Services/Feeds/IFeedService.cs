namespace QuickView.Services.Feeds
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuickView.Querying.Dto;

    public interface IFeedService
    {
        Task<IEnumerable<Feed>> GetFeedsAsync();

        Task UpdateFeedAsync(IFeedRequest request);

        Task AddFeedAsync(IFeedRequest request);

        Task DeleteFeedAsync(IFeedRequest request);

    }
}
