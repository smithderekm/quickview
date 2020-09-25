namespace QuickView.Querying.Dto
{
    using System;
    using System.Collections.Generic;

    public class Feed
    {
        public Feed(Guid id, string name, string sourceName, IEnumerable<Subject> subjects, IFeedIdentity identity)
        {
            this.Id = Id;
            this.Name = name;
            this.SourceName = sourceName;
            this.Subjects = subjects;
            this.Identity = Identity;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SourceName { get; set; }

        public string SourceIconUri { get; set; }

        public IEnumerable<Subject> Subjects { get; set; }

        public IFeedIdentity Identity { get; set; }
    }
}
