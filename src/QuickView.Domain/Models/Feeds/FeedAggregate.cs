namespace QuickView.Domain.Models.Feeds
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ArgSentry;

    public class FeedAggregate
    {
        private FeedAggregate(string name, Provider provider)
        {
            this.FeedId = Guid.Empty;
            this.Name = name;
            this.Provider = provider;
            this.Subjects = new List<Subject>();
        }

        private FeedAggregate(Guid id, string name, Provider provider, IEnumerable<Subject> subjects)
        {
            this.FeedId = id;
            this.Name = name;
            this.Provider = provider;
            this.Subjects = subjects.ToList();
        }

        public Guid FeedId { get; private set; }

        public string Name { get; private set; }

        public Provider Provider { get; private set; }

        public List<Subject> Subjects { get; private set; }

        public static FeedAggregate CreateNew(string name, Provider provider)
        {
            Prevent.NullOrWhiteSpaceString(name, nameof(name));
            Prevent.NullObject(provider, nameof(provider));
            return new FeedAggregate(name, provider);
        }

        public static FeedAggregate Hydrate(Guid id, string name, Provider provider, IEnumerable<Subject> subjects)
        {
            Prevent.EmptyGuid(id, nameof(id));
            Prevent.NullOrWhiteSpaceString(name, nameof(name));
            Prevent.NullObject(provider, nameof(provider));

            return new FeedAggregate(id, name, provider, subjects);
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
