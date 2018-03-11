using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOW;
using System.Configuration;

namespace CryptoGithubSoloAnalyser
{
    public static class DBConn
    {
        private static SqlClientBuilder sqMain = null;
        private static int intErr = 0;
        private static string strErr = "";
        public static int ErrNum { get { return intErr; } }
        public static string ErrMes { get { return strErr; } }
        public static string ConnUsed = "cryptoanlz";
        //public static string ConnUsed = "capcoin";

        public static SqlClientBuilder GetConn()
        {
            if (sqMain == null)
            {
                string conn = ConfigurationManager.AppSettings[ConnUsed];
                sqMain = new SqlClientBuilder(ConfigurationManager.AppSettings[conn], true);
                if (sqMain.ErrNum > 0) { intErr = sqMain.ErrNum; strErr = sqMain.ErrMes; }
            }

            return sqMain;
        }

    }
}
