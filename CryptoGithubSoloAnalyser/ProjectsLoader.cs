using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CryptoGithubSoloAnalyser
{
    public class ProjectsLoader
    {
        private string strCoinID = "";
        private string strSymbol = "";
        private string strRoot = "";
        private string DL = "|";
        private static int intErr = 0;
        private static string strErr = "";

        public int ErrNum { get { return intErr; } }
        public string ErrMes { get { return strErr; } }

        public ProjectsLoader() { }

        public int LoadGitHubInfo(TokenGitHubInfo inf, bool truncate)
        {
            StringBuilder sbRepo = new StringBuilder();
            StringBuilder sbContribCommits = new StringBuilder();
            Repo rpRepo = null;
            ContribCommit cbcCommits = null;
            //sb.Append(inf.CoinID + DL)

            //string RootInfo = inf.CoinID + DL + inf.Symbol + DL + inf.GitHubRoot + DL + inf.ReposNumber + DL + inf.Followers + DL +
            //                  inf.CreatedAt + DL + inf.UpdatedAt + DL + inf.ErrNum.ToString() + DL + inf.ErrMes;

            //File.WriteAllText("rootinfo.txt", RootInfo);
            if (inf.ErrNum > 0) { LoadInfoDB(inf, truncate); return inf.ErrNum; }

            for (int i = 0; i < inf.RepoList.Count; i++)
            {
                rpRepo = inf.RepoList[i];
                sbRepo.Append(inf.CoinID + DL + inf.Symbol + DL + rpRepo.RepoID + DL + rpRepo.RepoName + DL + rpRepo.Created + DL + rpRepo.Updated + DL +
                              rpRepo.Pushed + DL + rpRepo.Size + DL + rpRepo.Language + DL + rpRepo.Forks + DL + rpRepo.ErrNum.ToString() + DL + 
                              rpRepo.ErrMes + DL + rpRepo.Cnt.ToString() + DL + rpRepo.Url +  "\n");

                if (rpRepo.Contribs == null) continue;

                for (int k = 0; k < rpRepo.Contribs.Count; k++)
                {
                    cbcCommits = rpRepo.Contribs[k];
                    if (cbcCommits == null) continue;
                    sbContribCommits.Append(inf.CoinID + DL + inf.Symbol + DL + rpRepo.RepoID + DL + rpRepo.RepoName + DL + cbcCommits.LoginName + DL +
                                            cbcCommits.Additions + DL + cbcCommits.Deletions + DL + cbcCommits.Commits + DL + cbcCommits.CommTime + DL + 
                                            cbcCommits.ErrNum.ToString() + DL + cbcCommits.ErrMes + DL + cbcCommits.Cnt.ToString() + DL + cbcCommits.Url + "\n");
                }
            }

            File.WriteAllText("repoinfo.txt", sbRepo.ToString());
            File.WriteAllText("contribinfo.txt", sbContribCommits.ToString());

            if (LoadInfoDB(inf, truncate) > 0)
            {
                return intErr;
            }

            return intErr;
        }

        private int LoadInfoDB(TokenGitHubInfo inf, bool truncate)
        {
            DbOper dbo = new DbOper();
            if (truncate) dbo.TruncateTables();

            dbo.LoadRoot(inf.CoinID, inf.Symbol, inf.GitHubRoot, inf.GitHubLeadPrj, inf.ReposNumber, inf.Followers, inf.CreatedAt, 
                         inf.UpdatedAt, inf.ErrNum.ToString(), inf.ErrMes, inf.Url);
            if (dbo.ErrNum > 0)
            {
                intErr = dbo.ErrNum;
                strErr = dbo.ErrMes;
                dbo.CloseConn();
                return intErr;
            }

            if (inf.ErrNum > 0) return intErr;

            dbo.LoadHithubInfoTable("RepoInfo", "repoinfo.txt");
            if (dbo.ErrNum > 0)
            {
                intErr = dbo.ErrNum;
                strErr = dbo.ErrMes;
                dbo.CloseConn();
                return intErr;
            }

            dbo.LoadHithubInfoTable("ContribInfo", "contribinfo.txt");
            if (dbo.ErrNum > 0)
            {
                intErr = dbo.ErrNum;
                strErr = dbo.ErrMes;
                dbo.CloseConn();
                return intErr;
            }

            //dbo.CloseConn();
            return intErr;
        }
    }  
}
