using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.Model
{
    internal class FreshStatus
    {
        //用户界面根据操作决定的状态
        //每次扫码完成后new
        bool Lcode = false; //左件
        bool Rcode = false;

        bool LNewCode = false; //新码L
        bool RNewCode = false;//新码R

 

       public FreshStatus() { }

        public bool Lcode1 { get => Lcode; set => Lcode = value; }
        public bool Rcode1 { get => Rcode; set => Rcode = value; }
        public bool LNewCode1 { get => LNewCode; set => LNewCode = value; }
        public bool RNewCode1 { get => RNewCode; set => RNewCode = value; }
    }
}
