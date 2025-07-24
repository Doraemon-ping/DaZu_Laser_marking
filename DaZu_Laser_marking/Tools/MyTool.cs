using DaZu_Laser_marking.Model;
using DaZu_Laser_marking.SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaZu_Laser_marking
{
    internal class MyTool
    {
       

        //切换窗口
        public static void showForm(Form form3, Panel p)
        {
            p.Controls.Clear();
            // form3.MdiParent = this;//指定当前窗体为顶级Mdi窗体
            form3.TopLevel = false;
            form3.Dock = DockStyle.Fill;//填充
            form3.Parent = p;//指定子窗体的父容器为
            form3.FormBorderStyle = FormBorderStyle.None;//隐藏子窗体边框，当然也可以在子窗体的窗体加载事件中实现
            form3.Show();

        }

        //获取文件路径
        public static string get_filePath()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string file = null;
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                file = dialog.FileName;
            }
            return file;
        }

        public static int isLorR(string code,Peifang L , Peifang R)
        {

            int number = L.NUMBER1;
            string LH = L.TH1;
            string RH = R.TH1;

            string BZL1 = L.STRs1;
            string BZL2 = L.ENDs1;

            string BZR1 = R.STRs1;
            string BZR2 = R.ENDs1;


            int codeLenght = code.Length;
            string code1;
            string code2;

            Program.Logger.Info("收到二维码:"+code);
            Program.Logger.Info("左图号:" + BZL1);
            Program.Logger.Info("右图号:" + BZL2);





            if (codeLenght != number )
            {
                return 0;
            }
            else
            {
                //从0开始截取位数，长度
                code1 = code.Substring(L.STR1-1, BZL1.Length);
                code2 = code.Substring(L.END1 - 1, BZL2.Length);
            }//位数不对，直接返回

            bool L1 = BZL1.Equals(code1);
            bool L2 = BZL2.Equals(code2);

            bool R1 = BZR1.Equals(code1);
            bool R2 = BZR2.Equals(code2);


            if (L1&&L2) { return 1; }

            if (R1 && R2) { return 2; }



            return 0;

        }


        public static string createKHM_L(Peifang L) {

            return null;


        }

        public static string createKHM_R(Peifang R)
        {
            return null;



        }

        public static bool isNotCm(string code ){
            //不重码
            dataSql dataSql = new dataSql();
            int num = dataSql.is_Only(code);

            if (num == 0) { return true; }
            else { 
            return false;
                    }

        }

        public static void OpenFolder(string folderPath)
        {
            try
            {
                Process.Start("explorer.exe", folderPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


    }


}
