namespace QuickView.Querying.Dto
{
    using System;

    public class Message
    {
        public string Subject { get; set; }
        public DateTime Timestamp { get; set; }
        public string Creator { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
    }
}
