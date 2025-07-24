using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.Model
{
    public class MainFormStatus
    {
        int zhuzaomaLab = 0;
        int kehumaLab = 0;
        int equpmentLoginLab = 0;
        int QrcodeUpdate = 0;
        int Save = 0;
        int mesSave = 0;
        int mark = 0;
        // 1: L 
        // 2: R
        int lr = 0;

        bool isRecode = false;
 
        string res = null;




        public MainFormStatus()
        {
          
        }

        public void clean() {

            this.zhuzaomaLab = 0;
            this.kehumaLab = 0;
            this.equpmentLoginLab = 0;
            this.QrcodeUpdate = 0;
            this.Save = 0;
            this.mesSave = 0;
            this.mark = 0;
            this.lr = 0;
            this.res = null;
            this.isRecode = false;
        
        }

        public int ZhuzaomaLab { get => zhuzaomaLab; set => zhuzaomaLab = value; }
        public int KehumaLab { get => kehumaLab; set => kehumaLab = value; }
        public int EqupmentLoginLab { get => equpmentLoginLab; set => equpmentLoginLab = value; }
        public int QrcodeUpdate1 { get => QrcodeUpdate; set => QrcodeUpdate = value; }
        public int Save1 { get => Save; set => Save = value; }
        public int MesSave { get => mesSave; set => mesSave = value; }
        public int Mark { get => mark; set => mark = value; }
        public int Lr { get => lr; set => lr = value; }
        public string Res { get => res; set => res = value; }
        public bool IsRecode { get => isRecode; set => isRecode = value; }
    }
}
