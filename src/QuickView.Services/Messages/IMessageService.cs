namespace QuickView.Services.Messages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuickView.Querying.Dto;

    public interface IMessageService
    {
        Task<IReadOnlyList<Message>> GetMessagesAsync(Feed feed);

        Task<IReadOnlyList<Message>> GetMessagesAsync(IList<Feed> feeds);

    }
}
