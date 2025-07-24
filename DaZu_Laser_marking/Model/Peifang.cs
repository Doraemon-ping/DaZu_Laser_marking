using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.Model
{
    public class Peifang
    {
        private string name;
        private string TH;
        private int NUMBER;
        private int STR;
        private string STRs;
        private int END;
        private string ENDs;

        public Peifang(string name, string tH, int nUMBER, int sTR, string sTRs, int eND, string eNDs)
        {
            this.Name = name;
            TH1 = tH;
            NUMBER1 = nUMBER;
            STR1 = sTR;
            STRs1 = sTRs;
            END1 = eND;
            ENDs1 = eNDs;
        }

        public string Name { get => name; set => name = value; }
        public string TH1 { get => TH; set => TH = value; }
        public int NUMBER1 { get => NUMBER; set => NUMBER = value; }
        public int STR1 { get => STR; set => STR = value; }
        public string STRs1 { get => STRs; set => STRs = value; }
        public int END1 { get => END; set => END = value; }
        public string ENDs1 { get => ENDs; set => ENDs = value; }
    }
}
