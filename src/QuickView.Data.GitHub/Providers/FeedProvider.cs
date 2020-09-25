namespace QuickView.Data.GitHub.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ArgSentry;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Octokit;

    using QuickView.Domain;
    using QuickView.Querying;
    using QuickView.Querying.Dto;

    using Feed = QuickView.Querying.Dto.Feed;

    public class FeedProvider : IFeedMessagesProvider, IFeedSubjectProvider
    {
        private GitHubClient client;
        private readonly ILogger<FeedProvider> logger;
        private readonly GitHubOptions options;

        public FeedProvider(ILogger<FeedProvider> logger, IOptions<GitHubOptions> options)
        {
            Prevent.NullObject(logger, nameof(logger));
            Prevent.NullObject(options, nameof(options));

            this.logger = logger;
            this.options = options.Value;
        }

        public Task<IReadOnlyList<Subject>> GetSubjectsAsync(Guid feedId)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> GetSubjectAsync(string subject)
        {
            throw new NotImplementedException();
        }

        public string Source()
        {
            return Sources.GitHub();
        }

        public async Task<IReadOnlyList<Message>> GetMessagesAsync(Guid feedId, string subject, IFeedIdentity identity)
        {
            Prevent.NullOrWhiteSpaceString(subject, nameof(subject));
            Prevent.NullObject(identity, nameof(identity));
            var token = ((TokenIdentity) identity).Token;

            return await this.GetMessagesInternalAsync(new List<Subject> { new Subject(subject, "owner") }, token);
        }

        public async Task<IReadOnlyList<Message>> GetMessagesAsync(Guid feedId, IList<Subject> subjects, IFeedIdentity identity)
        {
            Prevent.NullObject(subjects, nameof(subjects));
            Prevent.NullObject(identity, nameof(identity));

            var token = ((TokenIdentity)identity).Token;

            return await this.GetMessagesInternalAsync(subjects.ToList(), token);
        }

        private void Connect(string token)
        {
            Prevent.NullOrWhiteSpaceString(token, nameof(token));

            if (this.client == null)
            {
                this.client = new GitHubClient(new ProductHeaderValue("QuickView"))
                {
                    Credentials = new Credentials(token)
                };
            }
        }
        private async Task<IReadOnlyList<Message>> GetMessagesInternalAsync(IList<Subject> subjects, string token)
        {
            var results = new List<Message>();

            this.Connect(token);

            foreach (var subject in subjects)
            {
                var events = await this.client.Activity.Events.GetAllForRepository(
                    subject.Owner,
                    subject.Name,
                    new ApiOptions
                    {
                        StartPage = 1,
                        PageSize = this.options.PageSize,
                        PageCount = 1
                    });

                this.logger.LogDebug($"{events.Count} items retrieved for {subject} repository");

                results.AddRange(
                    events.Where(e => e.Type != "CreateEvent").Select(e =>
                    {
                        var messageSubject = string.Empty;
                        var body = string.Empty;
                        var url = string.Empty;

                        var payload = e.Payload;

                        switch (payload)
                        {
                            case CommitCommentPayload commentPayload:
                                messageSubject = $"Commit comment on {e.Repo.Name}";
                                body = commentPayload.Comment?.Body;
                                url = commentPayload.Comment?.Url;
                                break;
                            case PullRequestEventPayload eventPayload:
                                messageSubject = $"New pull request on {e.Repo.Name}";
                                body = eventPayload.PullRequest?.Body;
                                url = eventPayload.PullRequest?.Url;
                                break;
                            case PullRequestCommentPayload requestCommentPayload:
                                messageSubject = $"New comment pull request on {e.Repo.Name}";
                                body = requestCommentPayload.Comment?.Body;
                                url = requestCommentPayload.Comment?.Url;
                                break;
                            case PushEventPayload pushEventPayload:
                                messageSubject = $"{pushEventPayload.Commits?.Count} new commits pushed to {e.Repo.Name}";
                                body = this.FormatCommitList(pushEventPayload.Commits);
                                url = pushEventPayload.Commits?.FirstOrDefault()?.Url;
                                break;
                            case ReleaseEventPayload releaseEventPayload:
                                messageSubject = $"New release issued on {e.Repo.Name}";
                                body = releaseEventPayload.Release?.Name;
                                url = releaseEventPayload.Release?.Url;
                                break;
                        }

                        return new Message
                        {
                            SourceName = Sources.GitHub(),
                            Subject = messageSubject,
                            Timestamp = new DateTime(e.CreatedAt.Ticks),
                            Creator = e.Actor.Login,
                            Body = body,
                            Url = url
                        };
                    }).ToList());
            }
            return results.OrderByDescending(r => r.Timestamp).ToList().AsReadOnly();
        }

        private string FormatCommitList(IReadOnlyList<Commit> commits)
        {
            if (commits == null)
            {
                return string.Empty;

            }
            var result = new StringBuilder();
            foreach (var commit in commits)
            {
                result.AppendLine(commit.Message);
                result.AppendLine($"{commit.Author.Name} - {commit.Ref}");
                result.AppendLine(string.Empty);
            }

            return result.ToString();
        }
    }
}
