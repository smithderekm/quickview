namespace QuickView.UI.Windows
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using QuickView.Commanding.Feeds.CreateNew;
    using QuickView.Data.GitHub;
    using QuickView.Data.LocalStorage;
    using QuickView.Data.LocalStorage.Repositories;
    using QuickView.Data.LocalStorage.Stores;
    using QuickView.Querying;
    using QuickView.Repositories;
    using QuickView.Services;
    using QuickView.Services.Feeds;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string GitHubConfigSectionName = "GitHub";
        private readonly IHost host;

        public App()
        {
            this.host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {

            services.Configure<GitHubOptions>(options =>
            {
                options.User = configuration.GetSection(GitHubConfigSectionName).GetValue<string>("Username");
                options.Token = configuration.GetSection(GitHubConfigSectionName).GetValue<string>("Token");
                options.PageSize = configuration.GetSection(GitHubConfigSectionName).GetValue<int>("PageSize");
            });
            services.Configure<LocalStorageOptions>(options => { new LocalStorageOptions(); });

            services.AddOptions();

            services.AddSingleton<IConfiguration>(configuration);

            var factory = LoggerFactory.Create(
                builder =>
                {
                    builder.AddConfiguration(configuration.GetSection("Logging"));
                });

            services.AddSingleton(factory);
            services.AddLogging();

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

            //windows
            services.AddSingleton<MainWindow>();
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
        //public static void Init()
        //{
        //    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        //    var configFile = ExtractResource("QuickView.UI.UWP.appSettings.json", localFolder.Path);

        //    var host = new HostBuilder()
        //        .ConfigureHostConfiguration(
        //            config =>
        //            {
        //                config.AddCommandLine(new string[] { $"ContentRoot={localFolder.Path}" });

        //                config.AddJsonFile(configFile);
        //                config.Build();
        //            })
        //        .ConfigureLogging(logger =>
        //            logger.AddConsole(options => { options.DisableColors = true; }))
        //        .ConfigureServices((context, services) => { ConfigureServices(context, services); })
        //        .Build();

        //    ServiceProvider = host.Services;
        //}

    }
}
