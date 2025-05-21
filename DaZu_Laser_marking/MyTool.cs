using DaZu_Laser_marking.Model;
using System;
using System.Collections.Generic;
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

        public static int isLorR(string code)
        {

            int number = 24;

            string LH = "LH";
            string RH = "RH";
            string BZ = "FK";

            int codeLenght = code.Length;
            string code1;
            string code2;

            System.Console.WriteLine(codeLenght);


            if (codeLenght != number)
            {
                return 0;
            }
            else
            {
                code1 = code.Substring(3, 2);
                code2 = code.Substring(5, 2);
            }//位数不对，直接返回

            code1 = code.Substring(3, 2);
            code2 = code.Substring(5, 2);

            bool isBz = BZ.Equals(code1);
            bool isBz1 = LH.Equals(code2);
            bool isBz2 = RH.Equals(code2);


            if (isBz&&isBz1) { return 1; }

            if (isBz && isBz2) { return 2; }



            return 0;

        }


        public static string createKHM(int number) {





            return null;
        
 
        
        }

    }

  
}
