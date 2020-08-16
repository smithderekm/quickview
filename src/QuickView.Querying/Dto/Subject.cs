namespace QuickView.Querying.Dto
{
    using ArgSentry;

    public class Subject
    {
        public Subject(string name)
        {
            Prevent.NullOrWhiteSpaceString(name, nameof(name));
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
