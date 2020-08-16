namespace QuickView.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuickView.Querying.Dto;

    public interface IFeedSubjectProvider
    {
        Task<IReadOnlyList<Subject>> GetSubjectsAsync(Guid feedId);

        Task<Subject> GetSubjectAsync(string subject);

    }
}
