namespace QuickView.Services.Feeds
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuickView.Querying.Dto;

    public interface IFeedService
    {
        Task<IEnumerable<Feed>> GetFeedsAsync();
    }
}
