namespace QuickView.UI.Windows.Home.Models
{
    using System;

    using MahApps.Metro.IconPacks;
    
    public class Message
    {
        public Message(string source, string subject, string url, DateTime timestamp, string creator, string body)
        {
            this.Source = source;
            this.Subject = subject;
            this.Url = url;
            this.TimeStamp = timestamp;
            this.Creator = creator;
            this.Body = body;
        }

        public string Source { get; private set; }

        public string Subject { get; private set; }

        public string Url { get; private set; }
        
        public DateTime TimeStamp { get; private set; }

        public string Creator { get; private set; }

        public string Body { get; private set; }

        public PackIconFontAwesomeKind SourceIcon
        {
            get
            {
                return this.Source switch
                {
                    "GitHub" => PackIconFontAwesomeKind.GithubBrands,
                    "Jira" => PackIconFontAwesomeKind.JiraBrands,
                    _ => PackIconFontAwesomeKind.CubesSolid,
                };
            }
        }
    }
}
