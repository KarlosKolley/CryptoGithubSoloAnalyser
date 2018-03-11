using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CryptoGithubSoloAnalyser
{
    public class ProjectInfoBuilder
    {
        private int intErr = 0;
        private string strErr = "";
        public int ErrNum { get { return intErr; } }
        public string ErrMes { get { return strErr; } }

        private string GthAuth = "";

        public ProjectInfoBuilder(string auth) { GthAuth = auth; }

        public TokenGitHubInfo GetProjectInfo(string coinid, string symbol, string githubroot, string leadproj)
        {
            intErr = 0; strErr = "";
            //** Avoid certificate error
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //** Format URL string
            WebClient currClient = new WebClient();
            currClient.Headers.Add("user-agent", "karloskolley");
            string input = "https://api.github.com/users/" + githubroot + "?access_token=" + GthAuth;
            string jsonInput = "";

            TokenGitHubInfo inf = new TokenGitHubInfo();
            inf.CoinID = coinid;
            inf.Symbol = symbol;
            inf.GitHubRoot = githubroot;
            inf.GitHubLeadPrj = leadproj;

            try
            {
                jsonInput = currClient.DownloadString(input);
            }
            catch (Exception ex)
            {
                TokenGitHubInfo.SetErr88(inf, ex.Message, input);
                intErr = 8888;
                strErr = "Jason Error - " + ex.Message;
                currClient.Dispose();
                currClient = null;
                return inf;
            }

            //** Add brakets for array representation
            jsonInput = "[" + jsonInput + "]";

            try
            {
                Dictionary<string, string>[] ccurData = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(jsonInput);
                inf.ReposNumber = ccurData[0]["public_repos"];
                inf.Followers = ccurData[0]["followers"];
                inf.CreatedAt = TimeConverter.GetStandardTime(ccurData[0]["created_at"]);
                inf.UpdatedAt = TimeConverter.GetStandardTime(ccurData[0]["updated_at"]);
                inf.Url = input;
            }
            catch (Exception ex)
            {
                TokenGitHubInfo.SetErr100(inf, ex.Message, input);
                intErr = 100;
                strErr = ex.Message;
                currClient.Dispose();
                currClient = null;
                return inf;
            }

            currClient.Dispose();
            currClient = null;
            inf.ErrNum = 0;
            inf.ErrMes = "-";
            return inf;
        }
    }
}
