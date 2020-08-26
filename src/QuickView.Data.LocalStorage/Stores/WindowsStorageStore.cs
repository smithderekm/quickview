namespace QuickView.Data.LocalStorage.Stores
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using QuickView.Data.LocalStorage.Entities;
    using QuickView.Domain;

    public class WindowsStorageStore : IFeedStore
    {
        private const string FeedsContainer = "FeedsContainer";
        private const string FeedsKey = "Feeds";

        public async Task<IEnumerable<FeedConfiguration>> GetAllAsync()
        {

            var result = new List<FeedConfiguration>
            {
                new FeedConfiguration
                {
                    Id=Guid.NewGuid(),
                    Name = "Github - FMS",
                    SourceName = Sources.GitHub(),
                    Subjects = new List<Subject>
                    {
                    }
                }
            };

            return await Task.FromResult(result);

            //if (this.localSettings.Containers.ContainsKey(FeedsContainer))
            //{
            //    var container = this.localSettings.Containers[FeedsContainer];
            //    var feeds = (ApplicationDataCompositeValue) container.Values[FeedsKey];

            //    if (feeds == null)
            //    {
            //        return null;
            //    }

            //    var results = feeds.Select(f =>
            //    {

            //        var entity = f.Value as FeedConfiguration;
            //        entity.Id = new Guid(f.Key);

            //        return entity;
            //    }).ToList();

            //    return await Task.FromResult(results);
            //}

            //return null;
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

        private static Guid AppGuid
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();
                var attributes = (assembly.GetCustomAttributes(typeof(GuidAttribute), true));
                return new Guid((attributes[0] as GuidAttribute).Value);
            }
        }

        private static Guid AssemblyGuid
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var attributes = (assembly.GetCustomAttributes(typeof(GuidAttribute), true));
                return new Guid((attributes[0] as GuidAttribute).Value);
            }
        }

        private static string UserDataFolder
        {
            get
            {
                var appGuid = AppGuid;
                var folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var dir = $@"{folderBase}\{appGuid.ToString("B").ToUpper()}\";
                return CheckDirectoryAndCreateIfNotFound(dir);
            }
        }

        private static string UserRoamingDataFolder
        {
            get
            {
                var appGuid = AppGuid;
                var folderBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var dir = $@"{folderBase}\{appGuid.ToString("B").ToUpper()}\";
                return CheckDirectoryAndCreateIfNotFound(dir);
            }
        }

        private static string AllUsersDataFolder
        {
            get
            {
                var appGuid = AppGuid;
                var folderBase = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                var dir = $@"{folderBase}\{appGuid.ToString("B").ToUpper()}\";
                return CheckDirectoryAndCreateIfNotFound(dir);
            }
        }
        private static string CheckDirectoryAndCreateIfNotFound(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
