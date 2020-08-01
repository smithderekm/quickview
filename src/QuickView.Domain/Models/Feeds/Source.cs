namespace QuickView.Domain.Models.Feeds
{
    using ArgSentry;

    public class Source
    {
        public Source(string name)
        {
            Prevent.NullOrWhiteSpaceString(name, nameof(name));
            this.Name = name;
        }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
