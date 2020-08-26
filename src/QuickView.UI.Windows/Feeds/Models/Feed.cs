namespace QuickView.UI.Windows.Feeds.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Feed
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Source { get; set; }

        public Dictionary<string, string> Subjects { get; set; }
    }
}
