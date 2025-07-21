using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.Model
{
    internal class MainFormStatus
    {
        int zhuzaomaLab = 0;
        int kehumaLab = 0;
        int equpmentLoginLab = 0;
        int QrcodeUpdate = 0;
        int Save = 0;
        int mesSave = 0;
        int mark = 0;


        public MainFormStatus()
        {
          
        }

        public int ZhuzaomaLab { get => zhuzaomaLab; set => zhuzaomaLab = value; }
        public int KehumaLab { get => kehumaLab; set => kehumaLab = value; }
        public int EqupmentLoginLab { get => equpmentLoginLab; set => equpmentLoginLab = value; }
        public int QrcodeUpdate1 { get => QrcodeUpdate; set => QrcodeUpdate = value; }
        public int Save1 { get => Save; set => Save = value; }
        public int MesSave { get => mesSave; set => mesSave = value; }
        public int Mark { get => mark; set => mark = value; }
    }
}
