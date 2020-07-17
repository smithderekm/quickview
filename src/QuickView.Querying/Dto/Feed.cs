namespace QuickView.Querying.Dto
{
    using System;

    public class Feed
    {
        public Feed(Guid id, string name, string providerName)
        {
            this.Id = Id;
            this.Name = name;
            this.ProviderName = ProviderName;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ProviderName { get; set; }
    }
}
