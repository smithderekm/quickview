namespace QuickView.Commanding.Feeds.CreateNew
{
    using System.Collections;
    using System.Collections.Generic;

    using ArgSentry;

    using QuickView.Domain.Models.Feeds;

    public class CreateNewFeedCommand : ICommand
    {
        public CreateNewFeedCommand(string name, Source source, IEnumerable<Subject> subjects)
        {
            Prevent.NullOrWhiteSpaceString(name, nameof(name));
            Prevent.NullObject(source, nameof(source));

            this.Name = name;
            this.Source = source;
            this.Subjects = subjects;
        }

        public string Name { get; set; }

        public Source Source { get; set; }

        public IEnumerable<Subject> Subjects { get; set; }
    }
}
