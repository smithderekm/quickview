namespace QuickView.Domain.Models.Feeds
{
    using ArgSentry;

    public class Subject
    {
        private Subject(string name, string owner)
        {
            this.Name = name;
            this.Owner = owner;
        }

        public string Name { get; private set; }

        public string Owner { get; private set; }

        public static Subject CreateNew(string name, string owner)
        {
            Prevent.NullOrWhiteSpaceString(name, nameof(name));
            Prevent.NullOrWhiteSpaceString(owner, nameof(owner));
            return new Subject(name, owner);
        }

    }
}
