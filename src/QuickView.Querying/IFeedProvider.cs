namespace QuickView.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuickView.Querying.Dto;

    public interface IFeedProvider
    {
        Task<IEnumerable<Feed>> GetFeedsAsync();

        Task<Feed> GetFeedAsync(Guid id);

        Task<IEnumerable<Subject>> GetSubjectsAsync(Guid feedId);

        Task<Subject> GetSubjectAsync(string subject);

        Task<IEnumerable<Message>> GetMessagesAsync(string subject);

    }
}
