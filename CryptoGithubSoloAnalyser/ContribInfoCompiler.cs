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
    public class ContribInfoCompiler
    {
        private int intErr = 0;
        private string strErr = "";
        public int ErrNum { get { return intErr; } }
        public string ErrMes { get { return strErr; } }

        private string GthAuth = "";
        private List<int> lstWeeks = null;

        public ContribInfoCompiler(string auth) { GthAuth = auth; FillWeeks();}

        public void GetContribCommits(Repo rep, string proot)
        {
            //** Avoid certificate error
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //** Format URL string
            string input = "https://api.github.com/repos/" + proot + "/" + rep.RepoName + "/stats/contributors" + "?access_token=" + GthAuth;
            WebClient currClient = new WebClient();
            List<ContribCommit> lstContrib = new List<ContribCommit>();
            currClient.Headers.Add("user-agent", "karloskolley");
            string jsonInput = "";

            Uri urUrl = new Uri(input);
            Thread.Sleep(100);

            try
            {
                jsonInput = currClient.DownloadString(urUrl);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                ContribCommit.SetErr300(rep, lstContrib, ex.Message, input);
                currClient.Dispose();
                currClient = null;
                GC.Collect();
                Thread.Sleep(1000);
                return;
            }

            if (jsonInput.Equals(string.Empty))
            {
                ContribCommit.SetErrNoData400(rep, lstContrib, input);
                currClient.Dispose();
                currClient = null;
                GC.Collect();
                Thread.Sleep(1000);
                return;
            }

            Thread.Sleep(100);

            if (jsonInput.Equals("{}"))
            {
                ContribCommit.SetErrNoData500(rep, lstContrib, input);
                currClient.Dispose();
                currClient = null;
                GC.Collect();
                Thread.Sleep(1000);
                return;
            }

            JArray ContribList = null;
            int Cnt = 0;

            try
            {
                ContribList = JArray.Parse(jsonInput);
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                ContribCommit.SetErrArray600(rep, lstContrib, ex.Message, input);
                currClient.Dispose();
                currClient = null;
                GC.Collect();
                Thread.Sleep(1000);
                return;
            }

            List<ContribCommit> lstContribLogin = null;
            for (Cnt = 0; Cnt < ContribList.Count; Cnt++)
            {
                lstContribLogin = GetCommits(ContribList, input, Cnt);
                if (lstContribLogin.Count == 0) continue;
                for (int i = 0; i < lstContribLogin.Count; i++)
                {
                    lstContrib.Add(lstContribLogin[i]);
                }
            }

            currClient.Dispose();
            currClient = null;
            GC.Collect();
            GC.Collect();
            Thread.Sleep(100);
            rep.Contribs = lstContrib;
        }

        private List<ContribCommit> GetCommits(JArray ContribList, string url, int cnt)
        {
            ContribCommit contrib = null;
            List<ContribCommit> lstContribLogin = new List<ContribCommit>();
            int ActiveWeeks = 0;
            string strLoginName = "";
            JToken[] comms = null;

            try
            {
                strLoginName = ContribList[cnt]["author"]["login"].ToString();
                comms = ContribList[cnt]["weeks"].ToArray();

                int Wcnt = 0;
                for (Wcnt = comms.Length - 1; Wcnt >= 0; Wcnt--)
                {
                    ActiveWeeks++;
                    if (ActiveWeeks > 48) break;

                    int contribdate = Int32.Parse(TimeConverter.GetTimeFromEpoch(comms[Wcnt]["w"].ToString()));
                    contrib = new ContribCommit();
                    contrib.LoginName = strLoginName;
                    contrib.CommTime = contribdate.ToString();
                    contrib.Additions = comms[Wcnt]["a"].ToString();
                    contrib.Deletions = comms[Wcnt]["d"].ToString();
                    contrib.Commits = comms[Wcnt]["c"].ToString();
                    contrib.ErrNum = 0;
                    contrib.ErrMes = "-";
                    contrib.Url = url;
                    contrib.Cnt = Wcnt;
                    lstContribLogin.Add(contrib);
                }

            }
            catch (Exception ex)
            {
                ContribCommit.SetErrWeek700(contrib, ex.Message, url, cnt);
                lstContribLogin.Add(contrib);
                return lstContribLogin;
            }

            if (lstContribLogin.Count < 48)
            {
                ComplimentEmptyCommits(comms, lstContribLogin, url);
            }

            return lstContribLogin;
        }

        private void ComplimentEmptyCommits(JToken[] comms, List<ContribCommit> lstContribLogin, string url)
        {
            int contribdate = Int32.Parse(TimeConverter.GetTimeFromEpoch(comms[0]["w"].ToString()));
            int startdt = 0;
            int startind = 0;
            ContribCommit contrib = null;

            for (int i = 0; i < lstWeeks.Count; i++)
            {
                startdt = lstWeeks[i];
                if (startdt == contribdate) { startind = i; break; }
            }

            for (int k = startind - 1; k >= 0; k--)
            {
                contrib = new ContribCommit();
                contrib.LoginName = "00nologin00";
                contrib.CommTime = lstWeeks[k].ToString();
                contrib.Additions = "0";
                contrib.Deletions = "0";
                contrib.Commits = "0";
                contrib.ErrNum = 0;
                contrib.ErrMes = "-";
                contrib.Cnt = k;
                contrib.Url = url;
                lstContribLogin.Add(contrib);
            }

            string str = "checksum";
        }
        private void FillWeeks()
        {
            lstWeeks = new List<int>();
            lstWeeks.Add(20170305);
            lstWeeks.Add(20170312);
            lstWeeks.Add(20170319);
            lstWeeks.Add(20170326);
            lstWeeks.Add(20170402);
            lstWeeks.Add(20170409);
            lstWeeks.Add(20170416);
            lstWeeks.Add(20170423);
            lstWeeks.Add(20170430);
            lstWeeks.Add(20170507);
            lstWeeks.Add(20170514);
            lstWeeks.Add(20170521);
            lstWeeks.Add(20170528);
            lstWeeks.Add(20170604);
            lstWeeks.Add(20170611);
            lstWeeks.Add(20170618);
            lstWeeks.Add(20170625);
            lstWeeks.Add(20170702);
            lstWeeks.Add(20170709);
            lstWeeks.Add(20170716);
            lstWeeks.Add(20170723);
            lstWeeks.Add(20170730);
            lstWeeks.Add(20170806);
            lstWeeks.Add(20170813);
            lstWeeks.Add(20170820);
            lstWeeks.Add(20170827);
            lstWeeks.Add(20170903);
            lstWeeks.Add(20170910);
            lstWeeks.Add(20170917);
            lstWeeks.Add(20170924);
            lstWeeks.Add(20171001);
            lstWeeks.Add(20171008);
            lstWeeks.Add(20171015);
            lstWeeks.Add(20171022);
            lstWeeks.Add(20171029);
            lstWeeks.Add(20171105);
            lstWeeks.Add(20171112);  // 4 - 4
            lstWeeks.Add(20171119);
            lstWeeks.Add(20171126);
            lstWeeks.Add(20171203);
            lstWeeks.Add(20171210);  // 4 - 3
            lstWeeks.Add(20171217);
            lstWeeks.Add(20171224);
            lstWeeks.Add(20171231);
            lstWeeks.Add(20180107);  // 4 - 2
            lstWeeks.Add(20180114);
            lstWeeks.Add(20180121);
            lstWeeks.Add(20180128);
            lstWeeks.Add(20180204); // 4 - 1
            lstWeeks.Add(20180211);
            lstWeeks.Add(20180218);
            lstWeeks.Add(20180225);  // curr
            lstWeeks.Add(20180304);  //curr
            lstWeeks.Add(20180311);
            lstWeeks.Add(20180318);
            lstWeeks.Add(20180325);
            lstWeeks.Add(20180401);
            lstWeeks.Add(20180408);
            lstWeeks.Add(20180415);
            lstWeeks.Add(20180422);
            lstWeeks.Add(20180429);
            lstWeeks.Add(20180506);
            lstWeeks.Add(20180513);
            lstWeeks.Add(20180520);
            lstWeeks.Add(20180527);
            lstWeeks.Add(20180603);
            lstWeeks.Add(20180610);
            lstWeeks.Add(20180617);
            lstWeeks.Add(20180624);
        }
    }
}
