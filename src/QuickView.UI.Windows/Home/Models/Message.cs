namespace QuickView.UI.Windows.Home.Models
{
    using System;

    public class Message
    {
        public Message(string source, string subject, DateTime timestamp, string creator, string body)
        {
            this.Source = source;
            this.Subject = subject;
            this.TimeStamp = timestamp;
            this.Creator = creator;
            this.Body = body;
        }

        public string Source { get; private set; }

        public string Subject { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public string Creator { get; private set; }

        public string Body { get; private set; }
    }
}
