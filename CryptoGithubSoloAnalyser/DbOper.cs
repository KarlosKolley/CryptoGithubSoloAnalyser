using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOW;
using System.Data;

namespace CryptoGithubSoloAnalyser
{
    public class DbOper
    {
        private int intErr = 0;
        private string strErr = "";
        public int ErrNum { get { return intErr; } }
        public string ErrMes { get { return strErr; } }
        SqlClientBuilder ConnMain;

        public DbOper()
        {
            ConnMain = DBConn.GetConn();
            if(DBConn.ErrNum > 0)
            {
                intErr = DBConn.ErrNum;
                strErr = DBConn.ErrMes;
            }
        }

        public int TruncateTables() 
        { 
            ConnMain.SetData("reset_github_tables", false);
            if(ConnMain.ErrNum > 0)
            {
                intErr = ConnMain.ErrNum;
                strErr = ConnMain.ErrMes;
            }
            return intErr;
        }

        public DataTable GetCoinsForGitHub(int intSmallSet)
        {
            Hashtable htParms = new Hashtable();
            htParms.Add("@smallset", intSmallSet);
            DataTable dtCoinsForRoots = ConnMain.GetData("get_roots", htParms, false);
            //return ConnMain.GetDataSQL("select coinid, symbol, root, leadproj from devroot where err = 0", false);
            return dtCoinsForRoots;
        }

        public DataTable GetCommitsForRepeat()
        {
            DataTable dtCoinsForRoots = ConnMain.GetData("get_commits_for_repeat", false);
            //return ConnMain.GetDataSQL("select coinid, symbol, root, leadproj from devroot where err = 0", false);
            return dtCoinsForRoots;
        }

        public int DeleteRepeatCommits(string coinid, string symbol, string reponame)
        {
            Hashtable htParms = new Hashtable();
            htParms.Add("@coinid", coinid);
            htParms.Add("@symbol", symbol);
            htParms.Add("@reponame", reponame);
            ConnMain.SetData("delete_repeat_commits", htParms, false);
            return intErr;
        }

        public int LoadRoot(string CoinID, string Symbol, string GitHubRoot, string LeadPrj, string ReposNumber, string Followers, string CreatedAt,
                        string UpdatedAt, string ErrNum, string ErrMes, string url)
        {
            intErr = 0;  strErr = "";
            Hashtable htParms = new Hashtable();
            htParms.Add("@CoinID", CoinID);
            htParms.Add("@Symbol", Symbol);
            htParms.Add("@GitHubRoot", GitHubRoot);
            htParms.Add("@LeadPrj", LeadPrj);
            htParms.Add("@ReposNumber", ReposNumber);
            htParms.Add("@Followers", Followers);
            htParms.Add("@CreatedAt", CreatedAt);
            htParms.Add("@UpdatedAt", UpdatedAt);
            htParms.Add("@ErrNum", ErrNum);
            htParms.Add("@ErrMes", ErrMes);
            htParms.Add("@url", url);
            ConnMain.SetData("load_hithub_root", htParms, false);
            if (ConnMain.ErrNum > 0)
            {
                intErr = ConnMain.ErrNum;
                strErr = ConnMain.ErrMes;
            }
            return intErr;
        }

        public int LoadHithubInfoTable(string table, string filepath)
        {
            intErr = 0; strErr = "";
            Hashtable htParms = new Hashtable();
            htParms.Add("@table", table);
            htParms.Add("@filepath", AppDomain.CurrentDomain.BaseDirectory + filepath);
            htParms.Add("@fieldbreak", "|");
            htParms.Add("@rowbreak", "\n");
            htParms.Add("@truncate", 0);
            ConnMain.SetData("load_table", htParms, false);
            if (ConnMain.ErrNum > 0)
            {
                intErr = ConnMain.ErrNum;
                strErr = ConnMain.ErrMes;
            }
            return intErr;
        }

        public void CloseConn() { ConnMain.ConnClose(); }
    }
}
