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

    using QuickView.Querying;
    using QuickView.Querying.Dto;

    using Feed = QuickView.Querying.Dto.Feed;

    public class FeedProvider : IFeedProvider
    {
        private readonly GitHubClient client;
        private readonly ILogger<FeedProvider> logger;
        private readonly GitHubOptions options;

        public FeedProvider(ILogger<FeedProvider> logger, IOptions<GitHubOptions> options)
        {
            Prevent.NullObject(logger, nameof(logger));
            Prevent.NullObject(options, nameof(options));

            this.logger = logger;
            this.options = options.Value;

            this.client = new GitHubClient(new ProductHeaderValue("QuickView"))
            {
                Credentials = new Credentials(this.options.Token)
            };
        }

        public Task<IEnumerable<Feed>> GetFeedsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Feed> GetFeedAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Subject>> GetSubjectsAsync(Guid feedId)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> GetSubjectAsync(string subject)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(string subject)
        {
            var events = await this.client.Activity.Events.GetAllForRepository(this.options.User, subject,
                new ApiOptions {PageSize = this.options.PageSize});
            
            this.logger.LogDebug($"{events.Count} items retrieved for {subject} repository");
            
            return events.Select(e =>
            {
                string messageSubject = string.Empty;
                string body = string.Empty;
                string url = string.Empty;

                var payload = e.Payload;

                if (payload is CommitCommentPayload commentPayload)
                {
                    messageSubject = $"Commit comment on {e.Repo.Name}";
                    body = commentPayload.Comment.Body;
                    url = commentPayload.Comment.Url;
                }
                else if (payload is PullRequestEventPayload eventPayload)
                {
                    messageSubject = $"New pull request on {e.Repo.Name}";
                    body = eventPayload.PullRequest.Body;
                    url = eventPayload.PullRequest.Url;
                }
                else if (payload is PullRequestCommentPayload requestCommentPayload)
                {
                    messageSubject = $"New comment pull request on {e.Repo.Name}";
                    body = requestCommentPayload.Comment.Body;
                    url = requestCommentPayload.Comment.Url;
                }
                else if (payload is PushEventPayload pushEventPayload)
                {
                    messageSubject = $"{pushEventPayload.Commits.Count} new commits pushed to {e.Repo.Name}";
                    body = this.FormatCommitList(pushEventPayload.Commits);
                    url = pushEventPayload.Repository.Url;
                }
                else if (payload is ReleaseEventPayload releaseEventPayload)
                {
                    messageSubject = $"New release issued on {e.Repo.Name}";
                    body = releaseEventPayload.Release.Name;
                    url = releaseEventPayload.Release.Url;
                }
                
                return new Message
                {
                    Subject = messageSubject,
                    Timestamp = new DateTime(e.CreatedAt.Ticks),
                    Creator = e.Actor.Login,
                    Body = body,
                    Url = url
                };
            }).ToList();
        }

        private string FormatCommitList(IReadOnlyList<Commit> commits)
        {
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
