using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGithubSoloAnalyser
{
    public class ContribCommit
    {
        public string CommTime { get; set; }
        public string LoginName { get; set; }
        public string Commits { get; set; }
        public string Additions { get; set; }
        public string Deletions { get; set; }
        public int ErrNum { get; set; }
        public string ErrMes { get; set; }
        public int Cnt { get; set; }
        public string Url { get; set; }

        public static void SetErr300(Repo rep, List<ContribCommit> lstContrib, string errmes, string input)
        {
            ContribCommit contrib = new ContribCommit();
            contrib.Additions = "0";
            contrib.Deletions = "0";
            contrib.Commits = "0";
            contrib.CommTime = "0";
            contrib.LoginName = "0";
            contrib.Cnt = 0;
            contrib.Url = input;
            contrib.ErrNum = 300;
            contrib.ErrMes = "404 No Commits Data 300 - " + errmes;
            lstContrib.Add(contrib);
            rep.Contribs = lstContrib;
        }

        public static void SetErrNoData400(Repo rep, List<ContribCommit> lstContrib, string input)
        {
            ContribCommit contrib = new ContribCommit();
            contrib = new ContribCommit();
            contrib.Additions = "0";
            contrib.Deletions = "0";
            contrib.Commits = "0";
            contrib.CommTime = "0";
            contrib.LoginName = "0";
            contrib.Cnt = 0;
            contrib.Url = input;
            contrib.ErrNum = 400;
            contrib.ErrMes = "No Commits Data Empty - 400";
            lstContrib.Add(contrib);
            rep.Contribs = lstContrib;
        }

        public static void SetErrNoData500(Repo rep, List<ContribCommit> lstContrib, string input)
        {
            ContribCommit contrib = new ContribCommit();
            contrib.Additions = "0";
            contrib.Deletions = "0";
            contrib.Commits = "0";
            contrib.CommTime = "0";
            contrib.LoginName = "0";
            contrib.Cnt = 0;
            contrib.Url = input;
            contrib.ErrNum = 500;
            contrib.ErrMes = "No Commits Data {} - 500";
            lstContrib.Add(contrib);
            rep.Contribs = lstContrib;
        }

        public static void SetErrArray600(Repo rep, List<ContribCommit> lstContrib, string errmes, string input)
        {
            ContribCommit contrib = new ContribCommit();
            contrib.Additions = "0";
            contrib.Deletions = "0";
            contrib.Commits = "0";
            contrib.CommTime = "0";
            contrib.LoginName = "0";
            contrib.Cnt = 0;
            contrib.Url = input;
            contrib.ErrNum = 600;
            contrib.ErrMes = "Contrib Array Error 600 - " + errmes;
            lstContrib.Add(contrib);
            rep.Contribs = lstContrib;
        }

        public static void SetErrWeek700(ContribCommit contrib, string errmes, string url, int cnt)
        {
            if (contrib == null) { contrib = new ContribCommit(); }
            contrib.Additions = "0";
            contrib.Deletions = "0";
            contrib.Commits = "0";
            contrib.CommTime = "0";
            contrib.LoginName = "0";
            contrib.Cnt = cnt;
            contrib.Url = url;
            contrib.ErrNum = 700;
            contrib.ErrMes = "Error Reading Weeks - " + errmes;
        }
    }
}
