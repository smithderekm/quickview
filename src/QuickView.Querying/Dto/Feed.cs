namespace QuickView.Querying.Dto
{
    using System;
    using System.Collections.Generic;

    public class Feed
    {
        public Feed(Guid id, string name, string sourceName)
        {
            this.Id = Id;
            this.Name = name;
            this.SourceName = SourceName;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SourceName { get; set; }

        public string SourceIconUri { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
