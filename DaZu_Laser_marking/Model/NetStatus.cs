using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.Model
{
    internal class NetStatus
    {
        int ipL = 0;
        int ipR = 0;
        int ipMes = 0;

        public NetStatus()
        {
            
        }

        public int IpL { get => ipL; set => ipL = value; }
        public int IpR { get => ipR; set => ipR = value; }
        public int IpMes { get => ipMes; set => ipMes = value; }
    }
}
