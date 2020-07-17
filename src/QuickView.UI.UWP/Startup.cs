using System;
using System.Collections.Generic;
using System.Linq;
namespace QuickView.UI.UWP
{
    using System.IO;
    using System.Reflection;

    using Windows.Storage;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using QuickView.Commanding.Feeds.CreateNew;
    using QuickView.Data.GitHub;
    using QuickView.Data.GitHub.Providers;
    using QuickView.Data.LocalStorage;
    using QuickView.Data.LocalStorage.Repositories;
    using QuickView.Data.LocalStorage.Stores;
    using QuickView.Querying;
    using QuickView.Repositories;
    using QuickView.Services;
    using QuickView.Services.Feeds;
    using QuickView.UI.UWP.ViewModels;

    public static class Startup
    {
        private const string GitHubConfigSectionName = "GitHub";
        public static IServiceProvider ServiceProvider { get; set; }

        public static void Init()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            var configFile = ExtractResource("QuickView.UI.UWP.appSettings.json", localFolder.Path);

            var host = new HostBuilder()
                .ConfigureHostConfiguration(
                    config =>
                    {
                        config.AddCommandLine(new string[] {$"ContentRoot={localFolder.Path}"});

                        config.AddJsonFile(configFile);
                        config.Build();
                    })
                .ConfigureLogging(logger => 
                    logger.AddConsole(options => { options.DisableColors = true; }))
                .ConfigureServices((context, services) => { ConfigureServices(context, services); })
                .Build();

            ServiceProvider = host.Services;
        }

        static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {

            services.Configure<GitHubOptions>(options =>
            {
                options.User = context.Configuration.GetSection(GitHubConfigSectionName).GetValue<string>("Username");
                options.Token = context.Configuration.GetSection(GitHubConfigSectionName).GetValue<string>("Token");
                options.PageSize = context.Configuration.GetSection(GitHubConfigSectionName).GetValue<int>("PageSize");
            });
            services.Configure<LocalStorageOptions>(options => { new LocalStorageOptions(); });

            services.AddOptions();
            services.AddSingleton<IConfiguration>(context.Configuration);
            
            var factory = LoggerFactory.Create(
                builder =>
                {
                    builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                });
            
            services.AddSingleton(factory);
            services.AddLogging();

            services.AddHttpClient();

            //service
            services.AddTransient<IFeedService, FeedService>();

            //settings repository
            services.AddTransient<IFeedRepository, FeedRepository>();

            //querying providers
            services.AddTransient<IFeedProvider, Data.LocalStorage.Providers.FeedProvider>();

            //stores
            services.AddTransient<IFeedStore, WindowsStorageStore>();

            //command handlers
            services.AddTransient<ICreateNewFeedCommandHandler, CreateNewFeedCommandHandler>();

            //viewmodels
            services.AddTransient<MainViewModel>();

        }

        static string ExtractResource(string filename, string location)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var resourceFileStream = assembly.GetManifestResourceStream(filename))
            {
                if (resourceFileStream != null)
                {
                    var fullPath = Path.Combine(location, filename);
                    using (var stream = File.Create(fullPath))
                    {
                        resourceFileStream.CopyTo(stream);
                    }
                }
            }

            return Path.Combine(location, filename);
        }
    }
}
