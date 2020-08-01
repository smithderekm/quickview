namespace QuickView.Domain.Models.Feeds
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ArgSentry;

    public class FeedAggregate
    {
        private FeedAggregate(string name, Source source)
        {
            this.FeedId = Guid.Empty;
            this.Name = name;
            this.Source = source;
            this.Subjects = new List<Subject>();
        }

        private FeedAggregate(Guid id, string name, Source source, IEnumerable<Subject> subjects)
        {
            this.FeedId = id;
            this.Name = name;
            this.Source = source;
            this.Subjects = subjects.ToList();
        }

        public Guid FeedId { get; private set; }

        public string Name { get; private set; }

        public Source Source { get; private set; }

        public List<Subject> Subjects { get; private set; }

        public static FeedAggregate CreateNew(string name, Source source)
        {
            Prevent.NullOrWhiteSpaceString(name, nameof(name));
            Prevent.NullObject(source, nameof(source));
            return new FeedAggregate(name, source);
        }

        public static FeedAggregate Hydrate(Guid id, string name, Source source, IEnumerable<Subject> subjects)
        {
            Prevent.EmptyGuid(id, nameof(id));
            Prevent.NullOrWhiteSpaceString(name, nameof(name));
            Prevent.NullObject(source, nameof(source));

            return new FeedAggregate(id, name, source, subjects);
        }

        public void AddSubject(Subject subject)
        {
            Prevent.NullObject(subject, nameof(subject));

            if (this.Subjects == null)
            {
                this.Subjects = new List<Subject> { subject };
                return;
            }

            if (this.Subjects.Contains(subject))
            {
                return;
            }

            this.Subjects.Add(subject);
        }
    }
}
