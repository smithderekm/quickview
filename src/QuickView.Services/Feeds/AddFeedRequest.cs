namespace QuickView.Services
{
    using QuickView.Services.Feeds;

    using System.Collections.Generic;

    public class AddFeedRequest : IFeedRequest
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public Dictionary<string, string> Subjects { get; set; }
    }
}