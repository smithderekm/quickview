namespace QuickView.Services
{
    using QuickView.Services.Feeds;

    using System;
    using System.Collections.Generic;

    public class UpdateFeedRequest : IFeedRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public Dictionary<string, string> Subjects { get; set; }
    }
}