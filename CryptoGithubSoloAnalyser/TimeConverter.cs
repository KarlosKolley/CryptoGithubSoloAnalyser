using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGithubSoloAnalyser
{
    public static class TimeConverter
    {
        public static string GetTimeFromEpoch(string epoch)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            double eptime = Double.Parse(epoch);
            DateTime dtDateTimeAdj = dtDateTime.AddSeconds(eptime);
            return GetStandardTime(dtDateTimeAdj);
        }

        public static string GetStandardTime(string currdt)
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

        private static string GetStandardTime(DateTime dt)
        {
            string year = dt.Year.ToString();
            string mnth = dt.Month.ToString();
            if (dt.Month < 10) mnth = "0" + mnth;
            string day = dt.Day.ToString();
            if (dt.Day < 10) day = "0" + day;
            return year + mnth + day;
        }
    }
}
