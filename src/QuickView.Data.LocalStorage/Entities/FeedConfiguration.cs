namespace QuickView.Data.LocalStorage.Entities
{
    using System;
    using System.Collections.Generic;

    public class FeedConfiguration
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string SourceName { get; set; }

        public List<Subject> Subjects { get; set; }

        public string Token { get; set; }
    }
}
