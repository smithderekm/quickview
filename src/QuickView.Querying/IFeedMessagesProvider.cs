namespace QuickView.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuickView.Domain;
    using QuickView.Querying.Dto;

    public interface IFeedMessagesProvider
    {
        string Source();
        Task<IReadOnlyList<Message>> GetMessagesAsync(Guid feedId, string subject, IFeedIdentity identity);
        Task<IReadOnlyList<Message>> GetMessagesAsync(Guid feedId, IList<Subject> subjects, IFeedIdentity identity);

    }
}
