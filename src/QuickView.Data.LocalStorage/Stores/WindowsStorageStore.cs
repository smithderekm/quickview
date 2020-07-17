namespace QuickView.Data.LocalStorage.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Windows.Storage;

    using QuickView.Data.LocalStorage.Entities;
    using QuickView.Querying.Dto;

    public class WindowsStorageStore : IFeedStore
    {
        private const string FeedsContainer = "FeedsContainer";
        private const string FeedsKey = "Feeds";

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        public async Task<IEnumerable<FeedConfiguration>> GetAllAsync()
        {
            if (this.localSettings.Containers.ContainsKey(FeedsContainer))
            {
                var container = this.localSettings.Containers[FeedsContainer];
                var feeds = (ApplicationDataCompositeValue) container.Values[FeedsKey];

                if (feeds == null)
                {
                    return null;
                }

                var results = feeds.Select(f =>
                {

                    var entity = f.Value as FeedConfiguration;
                    entity.Id = new Guid(f.Key);

                    return entity;
                }).ToList();

                return await Task.FromResult(results);
            }

            return null;
        }

        public Task<FeedConfiguration> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(FeedConfiguration aggregate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(FeedConfiguration aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
