using System;
using System.Collections.Generic;
using System.Text;

namespace QuickView.Data.GitHub
{
    using Microsoft.Extensions.Options;

    public  class GitHubOptions
    {
        public string User { get; set; }

        public string Token { get; set; }

        public int PageSize { get; set; }
    }
}
