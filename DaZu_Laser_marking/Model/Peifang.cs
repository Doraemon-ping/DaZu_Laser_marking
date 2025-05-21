using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.Model
{
    internal class Peifang
    {
       public string TH_L;
       public string TH_R;
       public int NUMBER;

        public Peifang(string tH_L, string tH_R, int nUMBER)
        {
            TH_L1 = tH_L;
            TH_R1 = tH_R;
            NUMBER1 = nUMBER;
        }

        public string TH_L1 { get => TH_L; set => TH_L = value; }
        public string TH_R1 { get => TH_R; set => TH_R = value; }
        public int NUMBER1 { get => NUMBER; set => NUMBER = value; }
    }
}
