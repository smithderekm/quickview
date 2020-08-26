namespace QuickView.Data.LocalStorage.Stores
{
    using System;

    using QuickView.Data.LocalStorage.Entities;

    public interface IFeedStore : IStore<FeedConfiguration,Guid>
    {
    }
}
