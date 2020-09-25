namespace QuickView.Data.LocalStorage.Stores
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using ArgSentry;

    using QuickView.Data.LocalStorage.Entities;
    using QuickView.Domain;

    public class WindowsStorageStore : IFeedStore
    {
        private const string fileName = "Quickview.Settings.json";

        public async Task<IEnumerable<FeedConfiguration>> GetAllAsync()
        {
            var settingsManager = new SettingsManager<List<FeedConfiguration>>(fileName);

            var result = settingsManager.LoadSettings();

            return await Task.FromResult(result);
        }

        public async Task<FeedConfiguration> GetAsync(Guid id)
        {
            var settingsManager = new SettingsManager<List<FeedConfiguration>>(fileName);

            var result = settingsManager.LoadSettings();

            return await Task.FromResult(result.FirstOrDefault(r => r.Id.Equals(id)));
        }

        public Task CreateAsync(FeedConfiguration feedConfiguration)
        {
            Prevent.NullObject(feedConfiguration, nameof(feedConfiguration));

            var settingsManager = new SettingsManager<List<FeedConfiguration>>(fileName);

            var result = settingsManager.LoadSettings();

            result.Add(feedConfiguration);

            settingsManager.SaveSettings(result);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(FeedConfiguration feedConfiguration)
        {
            Prevent.NullObject(feedConfiguration, nameof(feedConfiguration));

            var settingsManager = new SettingsManager<List<FeedConfiguration>>(fileName);

            var result = settingsManager.LoadSettings();
            
            result.Remove(feedConfiguration);

            settingsManager.SaveSettings(result);

            return Task.CompletedTask;
        }
    }
}
