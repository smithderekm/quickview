namespace QuickView.Commanding.Feeds.CreateNew
{
    using QuickView.Domain.Models.Feeds;

    public class CreateNewFeedCommand : ICommand
    {
        public string Name { get; set; }
        public Provider Provider { get; set; }
    }
}
