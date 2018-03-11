using System;
using System.Collections.Generic;

namespace CryptoGithubSoloAnalyser
{
    public class TokenGitHubInfo
    {
        public string Symbol { get; set; }
        public string CoinID { get; set; }
        public string GitHubRoot { get; set; }
        public string GitHubLeadPrj { get; set; }
        public string ReposNumber { get; set; }
        public string Followers { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public List<Repo> RepoList { get; set; }
        public int ErrNum { get; set; }
        public string ErrMes { get; set; }
        public string Url { get; set; }

        public static void SetErr88(TokenGitHubInfo inf, string errmes, string url)
        {
            inf.ReposNumber = "0";
            inf.Followers = "0";
            inf.CreatedAt = "0";
            inf.UpdatedAt = "0";
            inf.Url = url;
            inf.ErrMes = "Jason Error: " + errmes;
            inf.ErrNum = 8888;
        }

        public static void SetErr100(TokenGitHubInfo inf, string errmes, string url)
        {
            inf.ReposNumber = "0";
            inf.Followers = "0";
            inf.CreatedAt = "0";
            inf.UpdatedAt = "0";
            inf.ErrMes = "Dictionary Error: " + errmes;
            inf.ErrNum = 100;
            inf.Url = url;
        }
    }
}
