namespace QuickView.Domain.Models.Feeds
{
    using ArgSentry;

    public class Subject
    {
        private Subject(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public Subject CreateNew(string name)
        {
            Prevent.NullOrWhiteSpaceString(name, nameof(name));
            return new Subject(name);
        }

    }
}
