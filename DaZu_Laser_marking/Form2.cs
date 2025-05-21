using DaZu_Laser_marking.Model;
using DaZu_Laser_marking.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DaZu_Laser_marking
{
    public partial class Form2 : Form
    {
        bool _isHandlingTextChanged = false;


        public Form2()
        {
            InitializeComponent();
            MySqlite sqlite = new MySqlite();
            sqlite.load();

            chushihua();
           

        }

        //刷新UI
        public void chushihua() {

            PeiFangSql peiFangSql = new PeiFangSql();
            List<object> LP = peiFangSql.getById(1);
            List<object> RP = peiFangSql.getById(1);

            //左件配方实体
            Peifang LMOD = new Peifang(LP[1].ToString(), LP[2].ToString(), int.Parse(LP[3].ToString()));

            //右件配方实体
            Peifang RMOD = new Peifang(RP[1].ToString(), RP[2].ToString(), int.Parse(RP[3].ToString()));

            // System.Console.WriteLine("test:"+LMOD.TH_R);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Text = "1";
        }

       

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            // 检查是否已经在处理 TextChanged 事件
            if (_isHandlingTextChanged)
                return;

            try
            {
                Console.WriteLine("事件触发！");
                // 设置标志位，表示正在处理事件
                _isHandlingTextChanged = true;

                // 在这里执行你的逻辑，例如修改 TextBox 的内容
                // 注意：此时更改 TextBox.Text 不会再次触发 TextChanged 事件
                //
                if (textBox4.Text == "\n" || textBox4.Text == "\r\n" || textBox4.Text == "\r")
                {
                    textBox4.Text = string.Empty;
                    //只按下空格，将输入清空
                    //
                }

                //修改值
                bool isinput = !string.IsNullOrEmpty(textBox4.Text);//不为空，用户输入
                                                                    //;//用户是否完成输入
                isinput = isinput && !(textBox4.Text == "\n" || textBox4.Text == "\r\n" || textBox4.Text == "\r");

                bool inputExit = false;//默认未完成

                bool endN = false; //以回车结尾
                bool includeN = false; //包含回车

                if (textBox4.Text.EndsWith("\r\n") || textBox4.Text.EndsWith("\n") || textBox4.Text.EndsWith("\r"))
                {
                    Console.WriteLine("字符串以换行符结尾");
                    endN = true;//用户完成输入 //换行了，输入完成
                }

                if (textBox4.Text.Contains("\r\n") || textBox4.Text.Contains("\n") || textBox4.Text.Contains("\r"))
                {
                    includeN = true;
                    Console.WriteLine("字符串中存在换行符");
                }
                inputExit = (endN || includeN);

                if (isinput && inputExit)
                {
                    richTextBox1.Text = textBox4.Text.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); //删除换行

                    string code = richTextBox1.Text.ToUpper(); // 示例：将文本转换为大写
                    richTextBox1.SelectionStart = richTextBox1.Text.Length; // 保持光标位置
                    //ScanRead = richTextBox1.Text.IsNotNullOrEmpty();
                    int lr = MyTool.isLorR(code);

                    if (lr == 1) { richTextBox1.Text = code;  }

                    if (lr == 2) { richTextBox1.Text = code; }


                    textBox4.Text = string.Empty;
                }

                ///
            }
            finally
            {
                // 重置标志位，允许再次处理事件
                _isHandlingTextChanged = false;
            }

        }
    }
}
