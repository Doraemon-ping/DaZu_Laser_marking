using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.Code
{
    public class GetNewCode
    {

        public GetNewCode() { }


        public static string GetNewCodeE3S6(string s, Model.Peifang PF)
        {
            string result = null;
            string res =s.ToUpper();

            SQLite.dataSql dataSql = new SQLite.dataSql();
            int number = dataSql.getNumner() + 1;

            if (res == "FKLH")
            {
                //左前
                string S1 = "[>";
                string TH = PF.TH1;
                string BB = "VAL";
                DateTime now = DateTime.Now;
                string year = now.Year.ToString();
                DateTime dt = new DateTime(now.Year, 1, 1);
                int days = (now.Date - dt.Date).Days + 1;
                string s3 = "D"+days.ToString() + year.Substring(2,2);
                string s4 = "V88896";
                string s5 = "S0001";
                
                string s6 = null;
                DateTime todayStart = DateTime.Today.AddHours(8); // 今天8点
                DateTime todayEnd = DateTime.Today.AddHours(20);  // 今天20点

                if (now >= todayStart && now < todayEnd) { s6 = "Y1"; }
                else { s6 = "Y2"; }

                string s7 = "N";

                string s8 = number.ToString("00000");
                
                result = S1+TH+BB+s3+s4+s5+s6+s7+s8;
            }
            else if (res == "FKRH") {
                //右前
                string S1 = "[>";

                string TH = PF.TH1;
                string BB = "VAL";
                DateTime now = DateTime.Now;
                string year = now.Year.ToString();
                DateTime dt = new DateTime(now.Year, 1, 1);
                int days = (now.Date - dt.Date).Days + 1;
                string s3 = "D" + days.ToString() + year.Substring(2, 2);
                string s4 = "V88896";
                string s5 = "S0001";

                string s6 = null;
                DateTime todayStart = DateTime.Today.AddHours(8); // 今天8点
                DateTime todayEnd = DateTime.Today.AddHours(20);  // 今天20点

                if (now >= todayEnd && now < todayEnd) { s6 = "Y1"; }
                else { s6 = "Y2"; }

                string s7 = "N";

                string s8 = number.ToString("00000");
                result = S1 + TH + BB + s3 + s4 + s5 + s6 + s7 + s8;



            }
            else if (res == "RKLH")
            {
                //左后
                string S1 = "[>";

                string TH = "P0367690";
                string BB = "AK";

            }
            else if (res == "RKRH")
            {
                //右后
                string S1 = "[>";

                string TH = "P0367691";
                string BB = "AK";

            }


            return result;
        }

        public static string GetNewCodeW04(string s, Model.Peifang PF)
        {
            string result = null;
            string res = s.ToUpper();

            SQLite.dataSql dataSql = new SQLite.dataSql();
            int number = dataSql.getNumner() + 1;

            if (res == "L")
            {

                DateTime now = DateTime.Now;
                string TH = PF.TH1;
                string s1 = "/";
                string s2 = "2CZ";
                string year = now.Year.ToString().Substring(2, 2);
                string month = now.Month.ToString().Trim();
                int m = int.Parse(month);
                if (m == 10) { month = "A"; }
                if (m == 11) { month = "B"; }
                if (m == 12) { month = "C"; }
                String day = now.Day.ToString("00");

                string s4 = year + month + day + number.ToString("00000");

                string s5 = "M05";
                return (TH + s1 + s2 + s1 + s4 + s1 + s5).Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");

            }

            if (res == "R")
            {

                DateTime now = DateTime.Now;
                string TH = PF.TH1;
                string s1 = "/";
                string s2 = "2CZ";
                string year = now.Year.ToString().Substring(2, 2);
                string month = now.Month.ToString().Trim();
                int m = int.Parse(month);
                if (m == 10) { month = "A"; }
                if (m == 11) { month = "B"; }
                if (m == 12) { month = "C"; }
                String day = now.Day.ToString("00");

                string s4 = year + month + day + number.ToString("00000");

                string s5 = "M05";
                return (TH + s1 + s2 + s1 + s4 + s1 + s5).Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");


            }



            return result;

        }

    }
}
