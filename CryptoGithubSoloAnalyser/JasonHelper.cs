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
    public class JasonHelper
    {
        private List<int> lstWeeks = null;
        public void TestContribs11()
        {
            //** Avoid certificate error
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            FillWeeks();

            //** Format URL string
            //string input = "https://api.github.com/repos/PoC-Consortium/burstcoin-db-manager/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5";
            //string input = "https://api.github.com/repos/216k155/lux/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5";
            // - has error 500 !! string input = "https://api.github.com/repos/aurarad/auroracoin-old/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5";
            //string input = "https://api.github.com/repos/aurarad/bitcoinj-scrypt/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5";
            //string input = "https://api.github.com/repos/ArkEcosystem/ark-kotlin/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5";
            //string input = "https://api.github.com/repos/xenonflux/1337/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5";
            string input = "https://api.github.com/repos/bounty0x/TelegramBots/stats/contributors?access_token=70cc8216843131458601b3f85a9cb7420079f9a5";
            WebClient currClient = new WebClient();
            currClient.Headers.Add("user-agent", "karloskolley");
            string jsonInput = currClient.DownloadString(input);

            ContribCommit contrib = null;
            List<ContribCommit> lstContrib = new List<ContribCommit>();
            Repo rep = new Repo();

            if (jsonInput.Equals(string.Empty))
            {
                contrib = new ContribCommit();
                contrib.ErrNum = 400;
                contrib.ErrMes = "No Commits Data - 400";
                lstContrib.Add(contrib);
                rep.Contribs = lstContrib;
                return;
            }

            if (jsonInput.Equals("{}"))
            {
                contrib = new ContribCommit();
                contrib.ErrNum = 500;
                contrib.ErrMes = "No Commits Data - 500";
                lstContrib.Add(contrib);
                rep.Contribs = lstContrib;
                return;
            }

            JArray ContribList = null;
            int Cnt = 0;

            try
            {
                ContribList = JArray.Parse(jsonInput);
            }
            catch (Exception ex)
            {
                contrib = new ContribCommit();
                contrib.ErrNum = 600;
                contrib.ErrMes = ex.Message;
                lstContrib.Add(contrib);
                rep.Contribs = lstContrib;
                return;
            }

            for (Cnt = 0; Cnt < ContribList.Count; Cnt++)
            {
                List<ContribCommit> lstContribLogin = GetCommits(ContribList, Cnt);
                if (lstContribLogin.Count == 0) continue;
                lstContrib.AddRange(lstContribLogin);
            }

            rep.Contribs = lstContrib;
        }

        private List<ContribCommit> GetCommits(JArray ContribList, int cnt)
        {
            ContribCommit contrib = null;
            List<ContribCommit> lstContribLogin = new List<ContribCommit>();
            int ActiveWeeks = 0;
            string strLoginName = "";
            try
            {
                strLoginName = ContribList[cnt]["author"]["login"].ToString();
                JToken[] comms = ContribList[cnt]["weeks"].ToArray();

                if(comms.Length < 24)
                {
                    ComplimentEmptyCommits(comms, lstContribLogin, strLoginName);
                }

                int Wcnt = 0;
                for (Wcnt = comms.Length - 1; Wcnt >= 0; Wcnt--)
                {
                    ActiveWeeks++;
                    if (ActiveWeeks > 24) break;

                    int contribdate = Int32.Parse(GetTimeFromEpoch(comms[Wcnt]["w"].ToString()));
                    contrib = new ContribCommit();
                    contrib.LoginName = strLoginName;
                    contrib.CommTime = contribdate.ToString();
                    contrib.Additions = comms[Wcnt]["a"].ToString();
                    contrib.Deletions = comms[Wcnt]["d"].ToString();
                    contrib.Commits = comms[Wcnt]["c"].ToString();
                    contrib.ErrNum = 0;
                    contrib.ErrMes = "-";
                    lstContribLogin.Add(contrib);
                }

            }
            catch (Exception ex)
            {
                if (contrib == null) { contrib = new ContribCommit(); }
                contrib.LoginName = strLoginName;
                contrib.ErrNum = 700;
                contrib.ErrMes = ex.Message;
                lstContribLogin.Add(contrib);
            }
            return lstContribLogin;
        }

        private void ComplimentEmptyCommits(JToken[] comms, List<ContribCommit> lstContribLogin, string strLoginName)
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

            int steps = 24 - comms.Length;

            for (int k = startind - 1; k >= 0; k--)
            {
                contrib = new ContribCommit();
                contrib.LoginName = strLoginName;
                contrib.CommTime = lstWeeks[k].ToString();
                contrib.Additions = "0";
                contrib.Deletions = "0"; ;
                contrib.Commits = "0"; ;
                contrib.ErrNum = 0;
                contrib.ErrMes = "-";
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
            lstWeeks.Add(20170917);
            lstWeeks.Add(20170924);
            lstWeeks.Add(20171001);
            lstWeeks.Add(20171008);
            lstWeeks.Add(20171015);
            lstWeeks.Add(20171022);
            lstWeeks.Add(20171029);
            lstWeeks.Add(20171105);
            lstWeeks.Add(20171112);
            lstWeeks.Add(20171119);
            lstWeeks.Add(20171126);
            lstWeeks.Add(20171203);
            lstWeeks.Add(20171210);
            lstWeeks.Add(20171217);
            lstWeeks.Add(20171224);
            lstWeeks.Add(20171231);
            lstWeeks.Add(20180107);
            lstWeeks.Add(20180114);
            lstWeeks.Add(20180121);
            lstWeeks.Add(20180128);
            lstWeeks.Add(20180204);
            lstWeeks.Add(20180211);
            lstWeeks.Add(20180218);
            lstWeeks.Add(20180225);
            lstWeeks.Add(20180304);
            lstWeeks.Add(20180311);
            lstWeeks.Add(20180318);
            lstWeeks.Add(20180325);
            lstWeeks.Add(20180401);
            lstWeeks.Add(20180408);
            lstWeeks.Add(20180415);
            lstWeeks.Add(20180422);
            lstWeeks.Add(2018049);
        }
    }
}
