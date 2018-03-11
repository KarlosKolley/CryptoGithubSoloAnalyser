using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net;
using ADOW;
using System.Threading;
using System.IO;

namespace CryptoGithubSoloAnalyser
{
    public class RootFinder
    {
        private int intErr = 0;
        private string strErr = "";
        public int ErrNum { get { return intErr; } }
        public string ErrMes { get { return strErr; } }
        private int intTruncate = 0;
        private int intSmallSet = 0;
        private string DL = "|";
        private List<CoinRoot> lstCoinRoots = new List<CoinRoot>();
        public List<CoinRoot> LstRoots { get { return lstCoinRoots; } }

        public RootFinder(int truncate, int smallset) { intTruncate = truncate; intSmallSet = smallset; }

        public int GetRoots()
        {
            SqlClientBuilder sbMain = DBConn.GetConn();

            Hashtable htParms = new Hashtable();
            htParms.Add("@smallset", intSmallSet);
            DataTable dtCoinsForRoots = sbMain.GetData("get_coinlist_for_roots", htParms, false);
            if (sbMain.ErrNum > 0)
            {
                intErr = sbMain.ErrNum;
                strErr = sbMain.ErrMes;
                return intErr;
            }

            CoinRoot cRoot = null;

            foreach(DataRow dr in dtCoinsForRoots.Rows)
            {
                cRoot = GetCoinGithubRoot(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                lstCoinRoots.Add(cRoot);
                Thread.Sleep(200);
            }

            return intErr;
        }

        public CoinRoot GetCoinGithubRoot(string coinid, string symbol, string mcap)
        {
            intErr = 0; strErr = "";
            string strRoot = "";
            string strSingleProj = "";

            CoinRoot cRoot = new CoinRoot();
            cRoot.CoinID = coinid;
            cRoot.Symbol = symbol;
            cRoot.MarketCap = mcap;

            WebClient currClient = new WebClient();

            try
            {
                string urlToRead = "https://coinmarketcap.com/currencies/" + coinid + "/";
                //string urlToRead = "https://coinmarketcap.com/currencies/luxcoin/";
                //string urlToRead = "https://coinmarketcap.com/currencies/asch/";
                string rootInput = currClient.DownloadString(urlToRead);

                int scodelocation = rootInput.IndexOf("Source Code");
                if (scodelocation < 0)
                {
                    cRoot.GHRoot = "N/A";
                    cRoot.LeadPrj = "N/A";
                    cRoot.Err = 100;
                    cRoot.Mes = "No Source Code avaiable";
                    currClient.Dispose();
                    currClient = null;
                    return cRoot;
                }

                rootInput = rootInput.Substring(rootInput.IndexOf("Source Code"), rootInput.LastIndexOf("Source Code") - rootInput.IndexOf("Source Code"));
                rootInput = rootInput.Substring(rootInput.IndexOf("github"));
                strRoot = rootInput.Substring(rootInput.IndexOf("/") + 1);
                if (strRoot.IndexOf("/") > 0)
                {
                    strSingleProj = strRoot.Substring(strRoot.IndexOf("/") + 1, strRoot.LastIndexOf(" ") - strRoot.IndexOf("/") - 2);
                    if (strSingleProj.Equals("")) strSingleProj = "-";
                    strRoot = strRoot.Substring(0, strRoot.IndexOf("/"));
                    cRoot.GHRoot = strRoot;
                    cRoot.LeadPrj = strSingleProj;
                }
                else
                {
                    strRoot = strRoot.Substring(0, strRoot.IndexOf(" ") - 1);
                    cRoot.GHRoot = strRoot;
                    cRoot.LeadPrj = "-";
                }

                cRoot.Err = 0;
                cRoot.Mes = "-";

            }
            catch (Exception ex)
            {
                cRoot.GHRoot = "N/A";
                cRoot.LeadPrj = "N/A";
                cRoot.Err = 200;
                cRoot.Mes = "Error while parsing HTML - " + ex.Message;
                currClient.Dispose();
                currClient = null;
                return cRoot;
            }

            currClient.Dispose();
            currClient = null;
            return cRoot;
        }

        public void WriteRoots()
        {

            StringBuilder sbRoots = new StringBuilder();
            CoinRoot cRoot = null;

            for (int i = 0; i < lstCoinRoots.Count; i++)
            {
                cRoot = lstCoinRoots[i];
                if (i == lstCoinRoots.Count - 1) sbRoots.Append(cRoot.CoinID + DL + cRoot.Symbol + DL + cRoot.GHRoot + DL + 
                   cRoot.LeadPrj + DL + cRoot.MarketCap + DL + cRoot.Err.ToString() + DL + cRoot.Mes);
                else sbRoots.Append(cRoot.CoinID + DL + cRoot.Symbol + DL + cRoot.GHRoot + DL +
                    cRoot.LeadPrj + DL + cRoot.MarketCap + DL + cRoot.Err.ToString() + DL + cRoot.Mes + "\n");
            }

            File.WriteAllText("rootslist.txt", sbRoots.ToString());
        }

        public int LoadRoots()
        {
            intErr = 0; strErr = "";

            SqlClientBuilder sbMain = DBConn.GetConn();

            Hashtable htPartms = new Hashtable();
            Hashtable htParms = new Hashtable();
            htParms.Add("@table", "devroot");
            htParms.Add("@filepath", AppDomain.CurrentDomain.BaseDirectory + "rootslist.txt");
            htParms.Add("@fieldbreak", DL);
            htParms.Add("@rowbreak", "\n");
            htParms.Add("@truncate", intTruncate);
            sbMain.SetData("load_table", htParms, true);
            if (sbMain.ErrNum > 0)
            {
                intErr = sbMain.ErrNum;
                strErr = sbMain.ErrMes;
                sbMain.ConnClose();
            }

            sbMain.ConnClose();
            return intErr;
        }
    }
}
