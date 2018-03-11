using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGithubSoloAnalyser
{
    public class Repo
    {
        public string RepoID { get; set; }
        public string RepoName { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string Pushed { get; set; }
        public string Size { get; set; }
        public string Language { get; set; }
        public string Forks { get; set; }
        public List<ContribCommit> Contribs { get; set; }
        public int ErrNum { get; set; }
        public string ErrMes { get; set; }
        public int Cnt { get; set; }
        public string Url { get; set; }

        public static void SetErr333(TokenGitHubInfo inf, string errmes, string url)
        {
            Repo rep = new Repo();
            rep.RepoID = "0";
            rep.RepoName = "N/A";
            rep.Created = "0";
            rep.Updated = "0";
            rep.Pushed = "0";
            rep.Size = "0";
            rep.Language = "0";
            rep.Forks = "0";
            inf.ErrMes = "No Data 404 - " + errmes;
            inf.ErrNum = 333;
            inf.Url = url;
            List<Repo> lstRepo = new List<Repo>();
            lstRepo.Add(rep);
            inf.RepoList = lstRepo;
        }

        public static void SetErr444(TokenGitHubInfo inf, string errmes, string url)
        {
            Repo rep = new Repo();
            rep.RepoID = "0";
            rep.RepoName = "N/A";
            rep.Created = "0";
            rep.Updated = "0";
            rep.Pushed = "0";
            rep.Size = "0";
            rep.Language = "0";
            rep.Forks = "0";
            inf.ErrMes = "Repo Array Error: " + errmes;
            inf.ErrNum = 444;
            inf.Url = url;
            List<Repo> lstRepo = new List<Repo>();
            lstRepo.Add(rep);
            inf.RepoList = lstRepo;
        }

        public static void SetErr555(Repo rep, string errmes)
        {
            rep.RepoID = "0";
            rep.RepoName = "0";
            rep.Created = "0";
            rep.Updated = "0";
            rep.Pushed = "0";
            rep.Size = "0";
            rep.Language = "0";
            rep.Forks = "0";
            rep.ErrNum = 555;
            rep.ErrMes = "Cannot Find Repo in Array - " + errmes;
        }

        public static void SetLeadProj(Repo rep)
        {
            rep.RepoID = "0";
            rep.Created = "0";
            rep.Updated = "0";
            rep.Pushed = "0";
            rep.Size = "0";
            rep.Language = "-";
            rep.Forks = "0";
            rep.ErrNum = 0;
            rep.ErrMes = "-";
        }
    }
}
