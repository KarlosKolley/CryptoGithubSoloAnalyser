using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace CryptoGithubSoloAnalyser
{
    public class RepoListCompiler
    {
        private int intErr = 0;
        private string strErr = "";
        public int ErrNum { get { return intErr; } }
        public string ErrMes { get { return strErr; } }
        private string GthAuth = "";

        public RepoListCompiler(string auth) { GthAuth = auth; }

        public int GetRepoList(TokenGitHubInfo inf)
        {
            intErr = 0; strErr = "";

            //** Format URL string
            string input = "https://api.github.com/users/" + inf.GitHubRoot + "/repos?page=1&per_page=200&access_token=" + GthAuth;
            // https://api.github.com/users/input-output-hk/repos?page=1&per_page=200
            WebClient currClient = new WebClient();
            currClient.Headers.Add("user-agent", "karloskolley");
            string jsonInput = "";

            try
            {
                jsonInput = currClient.DownloadString(input);
                Thread.Sleep(200);
            }
            catch (Exception ex)
            {
                Repo.SetErr333(inf, ex.Message, input);
                intErr = 333;
                strErr = "No Repos 404 " + ex.Message;
                currClient.Dispose();
                currClient = null;
                return intErr;
            }

            try
            {
                JArray RepoList = JArray.Parse(jsonInput);
                Thread.Sleep(200);
                inf.RepoList = GetRepos(RepoList, inf.GitHubRoot, input);
            }
            catch (Exception ex)
            {
                Repo.SetErr444(inf, ex.Message, input);
                intErr = 444;
                strErr = ex.Message;
                currClient.Dispose();
                currClient = null;
                return intErr;
            }

            currClient.Dispose();
            currClient = null;
            GC.Collect();
            Thread.Sleep(100);
            return intErr;
        }

        private List<Repo> GetRepos(JArray RepoList, string githubroot, string url)
        {
            Repo rep = null;
            int Cnt = 0;
            List<Repo> lstRepo = new List<Repo>();

            for (Cnt = 0; Cnt < RepoList.Count; Cnt++)
            {
                rep = new Repo();
                rep.Url = url;
                rep.Cnt = Cnt;
                SetSingleRepo(RepoList, rep, Cnt);
                lstRepo.Add(rep);
                Thread.Sleep(1000);
           }

           return lstRepo;
        }

        private void SetSingleRepo(JArray RepoList, Repo rep, int cnt)
        {
            try
            {
                rep.RepoID = RepoList[cnt]["id"].ToString();
                rep.RepoName = RepoList[cnt]["name"].ToString();
                rep.Created = TimeConverter.GetStandardTime(RepoList[cnt]["created_at"].ToString());
                rep.Updated = TimeConverter.GetStandardTime(RepoList[cnt]["updated_at"].ToString());
                rep.Pushed = TimeConverter.GetStandardTime(RepoList[cnt]["pushed_at"].ToString());
                rep.Size = RepoList[cnt]["size"].ToString();
                string lang = RepoList[cnt]["language"].ToString();
                if (lang.Equals("")) rep.Language = "-";
                else rep.Language = lang;
                rep.Forks = RepoList[cnt]["forks_count"].ToString();
            }
            catch (Exception ex)
            {
                Repo.SetErr555(rep, ex.Message);
                return;
            }

            rep.ErrNum = 0;
            rep.ErrMes = "-";
        }
    }
}
