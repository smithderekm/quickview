namespace QuickView.Querying.Dto
{
    public class TokenIdentity : IFeedIdentity
    {
        public TokenIdentity(string token)
        {
            this.Token = token;
        }

        public string Token { get; set; }
    }
}
