using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace CryptoGithubSoloAnalyser
{
    public class JSonReader
    {
        private int intErr = 0;
        private string strErr = "";
        public int ErrNum { get { return intErr; } }
        public string ErrMes { get { return strErr; } }
        //private WebClient currClient;
        private string GthAuth = "";
        private List<int> lstWeeks = null;

        public JSonReader(string auth) { GthAuth = auth; FillWeeks(); }

        public int GetProjectInfo(TokenGitHubInfo inf)
        {
            intErr = 0; strErr = "";
            //** Avoid certificate error
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //** Format URL string
            WebClient currClient = new WebClient();
            currClient.Headers.Add("user-agent", "karloskolley");
            string input = "https://api.github.com/users/" + inf.GitHubRoot + "?access_token=" + GthAuth;
            string jsonInput = "";

            try
            {
                jsonInput = currClient.DownloadString(input);
            }
            catch(Exception ex)
            {
                inf.ReposNumber = "0";
                inf.Followers = "0";
                inf.CreatedAt = "0";
                inf.UpdatedAt = "0";
                inf.Url = input;
                intErr = 8888;
                strErr = "No Data - " + ex.Message;
                inf.ErrMes = "Info Error: " + ex.Message;
                inf.ErrNum = intErr;
                currClient.Dispose();
                currClient = null;
                return intErr;
            }

            //** Add brakets for array representation
            jsonInput = "[" + jsonInput + "]";

            try
            {
                Dictionary<string, string>[] ccurData = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(jsonInput);
                inf.ReposNumber = ccurData[0]["public_repos"];
                inf.Followers = ccurData[0]["followers"];
                inf.CreatedAt = GetStandardTime(ccurData[0]["created_at"]);
                inf.UpdatedAt = GetStandardTime(ccurData[0]["updated_at"]);
                inf.Url = input;
            }
            catch (Exception ex)
            {
                intErr = 100;
                strErr = ex.Message;
                inf.ReposNumber = "0";
                inf.Followers = "0";
                inf.CreatedAt = "0";
                inf.UpdatedAt = "0";
                inf.Url = input;
                inf.ErrMes = "Info Error: " + ex.Message;
                inf.ErrNum = intErr;
                inf.Url = input;

                //** Project has error, stop analyser
                currClient.Dispose();
                currClient = null;
                return intErr;
            }

            currClient.Dispose();
            currClient = null;
            inf.ErrNum = 0;
            inf.ErrMes = "-";
            return intErr;
        }

        public int GetRepoList(TokenGitHubInfo inf)
        {
             intErr = 0; strErr = "";
            //** Avoid certificate error
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //** Format URL string
            string input = "https://api.github.com/users/" + inf.GitHubRoot + "/repos?access_token=" + GthAuth;
            // https://api.github.com/users/input-output-hk/repos?page=1&per_page=200
            WebClient currClient = new WebClient();
            currClient.Headers.Add("user-agent", "karloskolley");
            string jsonInput = "";

            try
            {
                jsonInput = currClient.DownloadString(input);
            }
            catch(Exception ex)
            {
                Repo rep = new Repo();
                rep.RepoID = "0";
                rep.RepoName = "No Repos Available";
                rep.Created = "0";
                rep.Updated = "0";
                rep.Pushed = "0";
                rep.Size = "0";
                rep.Language = "0";
                rep.Forks = "0";
                intErr = 333;
                strErr = ex.Message;
                inf.ErrMes = "No Repos 404 " + ex.Message;
                inf.ErrNum = intErr;
                inf.Url = input;
                List<Repo> lstRepo = new List<Repo>();
                lstRepo.Add(rep);
                inf.RepoList = lstRepo;
                currClient.Dispose();
                currClient = null;
                return intErr;
            }

            try
            {
                JArray RepoList = JArray.Parse(jsonInput);
                inf.RepoList = GetRepos(RepoList, inf.GitHubRoot,inf. GitHubLeadPrj, input);
            }
            catch (Exception ex)
            {
                Repo rep = new Repo();
                rep.RepoID = "0";
                rep.RepoName = "No Repos Available";
                rep.Created = "0";
                rep.Updated = "0";
                rep.Pushed = "0";
                rep.Size = "0";
                string lang = "0";
                rep.Language = "0";
                rep.Forks = "0";
                intErr = 444;
                strErr = ex.Message;
                inf.ErrMes = "Repo Array Error: " + ex.Message;
                inf.ErrNum = intErr;
                inf.Url = input;
                List<Repo> lstRepo = new List<Repo>();
                lstRepo.Add(rep);
                inf.RepoList = lstRepo;
                currClient.Dispose();
                currClient = null;
                return intErr;
            }

            currClient.Dispose();
            currClient = null;
            return intErr;
        }

        private List<Repo> GetRepos(JArray RepoList, string githubroot, string leadprj, string url)
        {
            Repo rep = null;
            int Cnt = 0;
            List<Repo> lstRepo = new List<Repo>();
            bool hasLeadPrjRun = false;
            bool hasLeadPrj = false;

            for (Cnt = 0; Cnt < RepoList.Count; Cnt++)
            {
                rep = new Repo();
                rep.Url = url;
                rep.Cnt = Cnt;
                hasLeadPrjRun = SetSingleRepo(RepoList, rep, Cnt, leadprj);
                if (hasLeadPrjRun) hasLeadPrj = true;
                lstRepo.Add(rep);
                
                if (rep.ErrNum > 0) continue;
                else
                {
                    Thread.Sleep(5000);
                    GetContribCommits(rep, githubroot);
                }
            }

            if (!hasLeadPrj)
            {
                if (!leadprj.Equals("-"))
                {
                    rep = new Repo();
                    rep.Url = url;
                    rep.Cnt = Cnt;
                    rep.RepoID = "0";
                    rep.RepoName = leadprj.Replace("/", "");
                    rep.Created = "20180101";
                    rep.Updated = "20180101";
                    rep.Pushed = "20180101";
                    rep.Size = "0";
                    rep.Language = "-";
                    rep.Forks = "0";
                    rep.ErrNum = 0;
                    rep.ErrMes = "-";
                    lstRepo.Add(rep);

                    Thread.Sleep(5000);
                    GetContribCommits(rep, githubroot);
                }
            }

            //Thread.Sleep(2000);
            return lstRepo;
        }

        private bool SetSingleRepo(JArray RepoList, Repo rep, int cnt, string leadprj)
        {
            bool hasleadprj = false;
            string rname = "";

            try
            {
                rep.RepoID = RepoList[cnt]["id"].ToString();
                rname = RepoList[cnt]["name"].ToString().Replace("/", "");
                if (rname.Equals(leadprj)) hasleadprj = true;
                rep.RepoName = RepoList[cnt]["name"].ToString();
                rep.Created = GetStandardTime(RepoList[cnt]["created_at"].ToString());
                rep.Updated = GetStandardTime(RepoList[cnt]["updated_at"].ToString());
                rep.Pushed = GetStandardTime(RepoList[cnt]["pushed_at"].ToString());
                rep.Size = RepoList[cnt]["size"].ToString();
                string lang = RepoList[cnt]["language"].ToString();
                if (lang.Equals("")) rep.Language = "-";
                else rep.Language = lang;
                rep.Forks = RepoList[cnt]["forks_count"].ToString();
            }
            catch (Exception ex)
            {
                rep.RepoID = "0";
                rep.RepoName = "0";
                rep.Created = "0";
                rep.Updated = "0";
                rep.Pushed = "0";
                rep.Size = "0";
                rep.Language = "0";
                rep.Forks = "0";
                rep.ErrNum = 300;
                rep.ErrMes = "Cannot Find Repo in Array - " + ex.Message;
                return false;
            }

            rep.ErrNum = 0;
            rep.ErrMes = "-";

            return hasleadprj;
        }

        private void GetContribCommits(Repo rep, string proot)
        {
            //** Avoid certificate error
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //** Format URL string
            string input = "https://api.github.com/repos/" + proot + "/" + rep.RepoName + "/stats/contributors" + "?access_token=" + GthAuth;
            WebClient currClient = new WebClient();
            currClient.Headers.Add("user-agent", "karloskolley");
            string jsonInput = "";

            ContribCommit contrib = null;
            List<ContribCommit> lstContrib = new List<ContribCommit>();

            try
            {
                jsonInput = currClient.DownloadString(input);
            }
            catch(Exception ex)
            {
                contrib = new ContribCommit();
                contrib.Cnt = 0;
                contrib.Url = input;
                contrib.ErrNum = 300;
                contrib.ErrMes = "404 No Commits Data 300 - " + ex.Message;
                lstContrib.Add(contrib);
                rep.Contribs = lstContrib;
                currClient.Dispose();
                currClient = null;
                return;
            }

            

            if (jsonInput.Equals(string.Empty))
            {
                contrib = new ContribCommit();
                contrib.Cnt = 0;
                contrib.Url = input;
                contrib.ErrNum = 400;
                contrib.ErrMes = "No Commits Data - 400";
                lstContrib.Add(contrib);
                rep.Contribs = lstContrib;
                currClient.Dispose();
                currClient = null;
                return;
            }

            if (jsonInput.Equals("{}"))
            {
                contrib = new ContribCommit();
                contrib.Cnt = 0;
                contrib.Url = input;
                contrib.ErrNum = 500;
                contrib.ErrMes = "No Commits Data  500";
                lstContrib.Add(contrib);
                rep.Contribs = lstContrib;
                currClient.Dispose();
                currClient = null;
                return;
            }

            JArray ContribList = null;
            int Cnt = 0;

            try
            {
                ContribList = JArray.Parse(jsonInput);
            }
            catch(Exception ex)
            {
                contrib = new ContribCommit();
                contrib.Cnt = 600;
                contrib.Url = input;
                contrib.ErrNum = 600;
                contrib.ErrMes = "Contrib Array Error 600 - " + ex.Message;
                lstContrib.Add(contrib);
                rep.Contribs = lstContrib;
                currClient.Dispose();
                currClient = null;
                return;
            }

            for(Cnt = 0; Cnt < ContribList.Count;Cnt++)
            {
                List<ContribCommit> lstContribLogin = GetCommits(ContribList, input, Cnt);
                if(lstContribLogin.Count == 0) continue;
                for (int i = 0; i < lstContribLogin.Count; i++)
                {
                    lstContrib.Add(lstContribLogin[i]);
                }
            }

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

                    int contribdate = Int32.Parse(GetTimeFromEpoch(comms[Wcnt]["w"].ToString()));
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
            catch(Exception ex)
            {
                if (contrib == null) { contrib = new ContribCommit(); }
                contrib.LoginName = strLoginName;
                contrib.ErrNum = 700;
                contrib.ErrMes = ex.Message;
                contrib.Url = url;
                contrib.Cnt = cnt;
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
            int contribdate = Int32.Parse(GetTimeFromEpoch(comms[0]["w"].ToString()));
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

        private string GetTimeFromEpoch(string epoch)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            double eptime = Double.Parse(epoch);
            DateTime dtDateTimeAdj = dtDateTime.AddSeconds(eptime);
            return GetStandardTime(dtDateTimeAdj);
        }

        public string GetStandardTime(string currdt)
        {
            string strDt = "";
            if (currdt.IndexOf("/") > 0)
            {
                strDt = currdt.Substring(0, currdt.IndexOf(" "));
                DateTime dt = DateTime.Parse(strDt);
                strDt = GetStandardTime(dt);
            }
            else
            {
                strDt = currdt.Substring(0, currdt.IndexOf("T")).Replace("-", "");
            }

            return strDt;
        }

        private string GetStandardTime(DateTime dt)
        {
            string year = dt.Year.ToString();
            string mnth = dt.Month.ToString();
            if (dt.Month < 10) mnth = "0" + mnth;
            string day = dt.Day.ToString();
            if (dt.Day < 10) day = "0" + day;
            return year + mnth + day;
        }

        private void FillWeeks()
        {
            lstWeeks = new List<int>();
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
            lstWeeks.Add(20180304);
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
        }
    }
}

/*









 */
// https://api.github.com/repos/vertcoin-project/lit/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5
// https://api.github.com/repos/PoC-Consortium/burstcoin-db-manager/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5
// https://api.github.com/repos/cygnusxi/CurecoinSource/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5
// https://api.github.com/repos/216k155/lux/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5

// https://api.github.com/users/216k155/followers

// https://developer.github.com/v3/#rate-limiting
