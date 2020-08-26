namespace QuickView.UI.Windows.Home
{
    using System.Reflection;

    public class AboutViewModel : BindableBase
    {
        public string ProductVersion
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var version = assembly.GetName().Version;
                return version.ToString();
            }
        }
    }
}
