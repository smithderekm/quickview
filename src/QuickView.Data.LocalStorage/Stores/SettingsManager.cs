namespace QuickView.Data.LocalStorage.Stores
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text.Json;

    public class SettingsManager<T> where T : class
    {
        private readonly string filePath;

        public SettingsManager(string fileName)
        {
            this.filePath = GetLocalFilePath(fileName);
        }

        public T LoadSettings() =>
            File.Exists(this.filePath) ?
                JsonSerializer.Deserialize<T>(File.ReadAllText(this.filePath)) :
                null;

        public void SaveSettings(T settings)
        {
            string json = JsonSerializer.Serialize(settings);
            File.WriteAllText(this.filePath, json);
        }

        private static string ProductName
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();
                var attributes = (assembly?.GetCustomAttributes(typeof(AssemblyProductAttribute), true));
                return (attributes[0] as AssemblyProductAttribute).Product;
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
                var product = ProductName;
                var folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var dir = $@"{folderBase}\{product}\";
                return CheckDirectoryAndCreateIfNotFound(dir);
            }
        }

        private static string UserRoamingDataFolder
        {
            get
            {
                var product = ProductName;
                var folderBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var dir = $@"{folderBase}\{product}\";
                return CheckDirectoryAndCreateIfNotFound(dir);
            }
        }

        private static string AllUsersDataFolder
        {
            get
            {
                var product = ProductName;
                var folderBase = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                var dir = $@"{folderBase}\{product}\";
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

        private string GetLocalFilePath(string fileName)
        {
            string appData = UserRoamingDataFolder;
            return Path.Combine(appData, fileName);
        }
    }
}