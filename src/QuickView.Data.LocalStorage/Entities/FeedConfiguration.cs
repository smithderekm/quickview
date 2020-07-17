namespace QuickView.Data.LocalStorage.Entities
{
    using System;

    public class FeedConfiguration
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string ProviderName { get; set; }

        public string[] Subjects { get; set; }
    }
}
