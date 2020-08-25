namespace QuickView.Querying.Dto
{
    using ArgSentry;

    public class Subject
    {
        public Subject(string name, string owner)
        {
            Prevent.NullOrWhiteSpaceString(name, nameof(name));
            Prevent.NullOrWhiteSpaceString(owner, nameof(owner));
            this.Name = name;
            this.Owner = owner;
        }

        public string Name { get; private set; }

        public string Owner { get; private set; }
    }
}
