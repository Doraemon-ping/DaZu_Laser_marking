using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
//using NLog;
using System.Diagnostics;
//using Aqua.EnumerableExtensions;

namespace DaZu_Laser_marking
{
    public partial class Form6 : Form
    {
        bool _isHandlingTextChanged = false;

        int b_bt1 = 0;//button1状态
        int b_bk = 0;//标刻状态9
        Thread fresh;

        public Form6()
        {
            InitializeComponent();
            chushihau();
        }

        public void chushihau() {

            fresh = new Thread(flash);
            fresh.Start();
        }

        public void flash() {

            Console.WriteLine("fresh线程开始");

            while (true) {
                if (b_bt1 == 0) { button1.BackColor = Color.Red; }

                if (b_bt1 == 1 || b_bt1 == 2) { button1.BackColor = Color.Green; }
            }

            Thread.Sleep(5000);

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
                if (textBox1.Text == "\n" || textBox1.Text == "\r\n" || textBox1.Text == "\r")
                {
                    textBox1.Text = string.Empty;
                    //只按下空格，将输入清空
                    //
                }

                //修改值
                bool isinput = !string.IsNullOrEmpty(textBox1.Text);//不为空，用户输入
                                                                //;//用户是否完成输入
                isinput = isinput && !(textBox1.Text == "\n" || textBox1.Text == "\r\n" || textBox1.Text == "\r");

                bool inputExit = false;//默认未完成

                bool endN = false; //以回车结尾
                bool includeN = false; //包含回车

                if (textBox1.Text.EndsWith("\r\n") || textBox1.Text.EndsWith("\n") || textBox1.Text.EndsWith("\r"))
                {
                    Console.WriteLine("字符串以换行符结尾");
                    endN = true;//用户完成输入 //换行了，输入完成
                }

                if (textBox1.Text.Contains("\r\n") || textBox1.Text.Contains("\n") || textBox1.Text.Contains("\r"))
                {
                    includeN = true;
                    Console.WriteLine("字符串中存在换行符");
                }
                inputExit = (endN || includeN);

                if (isinput && inputExit)
                {
                   string code = textBox1.Text.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); //删除换行

                     code = code.ToUpper(); // 示例：将文本转换为大写
                                            // richTextBox1.SelectionStart = richTextBox1.Text.Length; // 保持光标位置
                    richTextBox1.Text = string.Empty;
                    richTextBox6.Text = string.Empty;
                    int lr = MyTool.isLorR(code);
                    b_bt1 = lr;
                    if (lr == 1) { richTextBox1.Text = code; }
                    if (lr == 2) { richTextBox6.Text = code; }
                    // ScanRead = richTextBox1.Text.IsNotNullOrEmpty();
                    textBox1.Text = string.Empty;
                }

                ///
            }
            finally
            {
                // 重置标志位，允许再次处理事件
                _isHandlingTextChanged = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
