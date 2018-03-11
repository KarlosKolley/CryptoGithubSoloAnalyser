using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using ADOW;
using System.IO;

namespace CryptoGithubSoloAnalyser
{
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }

        private static int intErr = 0;
        private static string strErr = "";

        private void cmdRunAnalysis_Click(object sender, EventArgs e)
        {
            if (txtCoinID.Text.Equals(string.Empty)) { MessageBox.Show("Please enter Coin ID!"); return; }
            if (txtSymbol.Text.Equals(string.Empty)) { MessageBox.Show("Please enter Symbol!"); return; }
            if (txtGtRoot.Text.Equals(string.Empty)) { MessageBox.Show("Please enter Github Root!"); return; }
            if (txtLeadPrj.Text.Equals(string.Empty)) { MessageBox.Show("Please enter Lead Project or '-'!"); return; }

            //** Secret Key
            string strGAuth = ConfigurationManager.AppSettings["gthauth"];

            //** get Token GitHub Info
            ProjectInfoBuilder infoBuild = new ProjectInfoBuilder(strGAuth);
            TokenGitHubInfo inf = infoBuild.GetProjectInfo(txtCoinID.Text, txtSymbol.Text, txtGtRoot.Text, txtLeadPrj.Text);

            if(inf.ErrNum > 0)
            {
                //** If there is an error - return, no point to search for repos and commits
                MessageBox.Show("Token Info Error - " + inf.ErrNum.ToString() + "::" + inf.ErrMes);
                return;
            }

            //** Get Repo List
            RepoListCompiler repListComp = new RepoListCompiler(strGAuth);
            repListComp.GetRepoList(inf);

            ContribInfoCompiler ctrbListComp = new ContribInfoCompiler(strGAuth);

            Repo rpRep = null;
            List<Repo> lstRepos = inf.RepoList;

            for (int i = 0; i < lstRepos.Count; i++)
            {
                rpRep = lstRepos[i];
                ctrbListComp.GetContribCommits(rpRep, inf.GitHubRoot);
                Thread.Sleep(1000);
            }

            ProjectsLoader ldPrj = new ProjectsLoader();

            if (ldPrj.LoadGitHubInfo(inf, chkIsSingle.Checked) > 0)
            {
                MessageBox.Show(ldPrj.ErrMes);
                return;
            }

            MessageBox.Show("Success!");

        }

        private void cmdTest11_Click(object sender, EventArgs e)
        {
            JasonHelper jsh = new JasonHelper();
            jsh.TestContribs11();
        }

        private void cmdGetRoots_Click(object sender, EventArgs e)
        {
            int truncate = 0;
            int smallset = 0;
            if(chkCleanRoots.Checked) truncate = 1;
            if (chkSmallSet.Checked) truncate = 1;

            RootFinder rtFind = new RootFinder(truncate, smallset);
            if(rtFind.GetRoots() > 0)
            {
                MessageBox.Show(rtFind.ErrMes);
                return;
            }

            rtFind.WriteRoots();

            if (rtFind.LoadRoots() > 0)
            {
                MessageBox.Show(rtFind.ErrMes);
                return;
            }

            MessageBox.Show("Success Loading Roots!");
        }

        private void cmdGetRootTest_Click(object sender, EventArgs e)
        {
            RootFinder rtFind = new RootFinder(0, 0);
            rtFind.GetCoinGithubRoot("", "", "");
        }

        private void cmdRunAll_Click(object sender, EventArgs e)
        {
            int smallset = 0;
            DbOper dbo = new DbOper();
            if (chkClean.Checked) dbo.TruncateTables();
            if (chkSmall.Checked) { smallset = 1; }

            DataTable dtCoins = dbo.GetCoinsForGitHub(smallset);

            //** Secret Key
            string strGAuth = ConfigurationManager.AppSettings["gthauth"];

            TokenGitHubInfo inf = null;
            ProjectInfoBuilder infoBuild = new ProjectInfoBuilder(strGAuth);
            RepoListCompiler repListComp = new RepoListCompiler(strGAuth);
            ContribInfoCompiler ctrbListComp = new ContribInfoCompiler(strGAuth);
            ProjectsLoader ldPrj = new ProjectsLoader();
            Repo rpRep = null;
            List<Repo> lstRepos = null; ;
            string lprj = "";

            foreach (DataRow dr in dtCoins.Rows)
            {
                lprj = dr[3].ToString().Replace("/", "");
                if (lprj.Length > 20) lprj = "-";
                if (lprj.Equals("")) lprj = "-";
                inf = infoBuild.GetProjectInfo(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), lprj);

                //** Get Repo List
                repListComp.GetRepoList(inf);

                lstRepos = inf.RepoList;

                for (int i = 0; i < lstRepos.Count; i++)
                {
                    rpRep = lstRepos[i];
                    ctrbListComp.GetContribCommits(rpRep, inf.GitHubRoot);
                    Thread.Sleep(3000);
                }

                ldPrj.LoadGitHubInfo(inf, false);

                //if (ldPrj.LoadGitHubInfo(inf, false) > 0)
                //{
                //    //MessageBox.Show(ldPrj.ErrMes);
                //    //continue;
                //}

                Thread.Sleep(2000);
            }

            MessageBox.Show("Success!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblDbConn.Text = "DB Connection - " + DBConn.ConnUsed.ToUpper();
        }

        private void cmdRepeatCommit_Click(object sender, EventArgs e)
        {
            DbOper dbOper = new DbOper();
            DataTable dtRepCommits = dbOper.GetCommitsForRepeat();
            string coinid = "";
            string symbol = "";
            string githubroot = "";
            string reponame = "";
            string repoid = "";

            //** Secret Key
            string strGAuth = ConfigurationManager.AppSettings["gthauth"];
            ContribInfoCompiler ctrbListComp = null;
            ContribCommit cbcCommits = null;
            StringBuilder sbContribCommits = null;
            string DL = "|";

            foreach(DataRow dr in dtRepCommits.Rows)
            {
                coinid = dr[0].ToString();
                symbol = dr[1].ToString();
                githubroot = dr[2].ToString();
                repoid = dr[3].ToString();
                reponame = dr[4].ToString();

                Repo rpRepo = new Repo();
                rpRepo.RepoID = repoid;
                rpRepo.RepoName = reponame;

                sbContribCommits = new StringBuilder();

                ctrbListComp = new ContribInfoCompiler(strGAuth);
                ctrbListComp.GetContribCommits(rpRepo, githubroot);

                for (int k = 0; k < rpRepo.Contribs.Count; k++)
                {
                    cbcCommits = rpRepo.Contribs[k];
                    if (cbcCommits == null) continue;
                    sbContribCommits.Append(coinid + DL + symbol + DL + rpRepo.RepoID + DL + rpRepo.RepoName + DL + cbcCommits.LoginName + DL +
                                            cbcCommits.Additions + DL + cbcCommits.Deletions + DL + cbcCommits.Commits + DL + cbcCommits.CommTime + DL +
                                            cbcCommits.ErrNum.ToString() + DL + cbcCommits.ErrMes + DL + cbcCommits.Cnt.ToString() + DL + cbcCommits.Url + "\n");
                }

                if(sbContribCommits.Length > 0)
                {
                    dbOper.DeleteRepeatCommits(coinid, symbol, reponame);
                    if (dbOper.ErrNum > 0)
                    {
                        intErr = dbOper.ErrNum;
                        strErr = dbOper.ErrMes;
                    }

                    File.WriteAllText("contribinfo.txt", sbContribCommits.ToString());

                    dbOper.LoadHithubInfoTable("ContribInfo", "contribinfo.txt");
                    if (dbOper.ErrNum > 0)
                    {
                        intErr = dbOper.ErrNum;
                        strErr = dbOper.ErrMes;
                    }
                }

                Thread.Sleep(3000);

            }

            MessageBox.Show("Success!");
        }

       
    }
}
