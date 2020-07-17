namespace QuickView.UI.UWP.Views
{
    using Microsoft.Extensions.DependencyInjection;

    using QuickView.UI.UWP.ViewModels;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; private set; } 

        public MainPage()
        {
            InitializeComponent();
            ViewModel = Startup.ServiceProvider.GetService<MainViewModel>();
            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(MasterDetailsViewControl.ViewState);
        }
    }
}
