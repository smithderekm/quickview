namespace QuickView.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuickView.Querying.Dto;

    public interface IFeedProvider
    {
        Task<IReadOnlyList<Feed>> GetFeedsAsync();

        Task<Feed> GetFeedAsync(Guid id);
    }
}
